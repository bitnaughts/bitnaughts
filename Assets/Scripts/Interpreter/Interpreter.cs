using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Interpreter {

    string debugger = "";
    private GameObject obj;

    private List<VariableObject> variables;
    private Stack<int> scope_tracker; //if statements could push their scope to their reciprocal "}"?
    private string[] script;
    private int pointer; //tracks what line is being processed

    string variable_type, variable_name, variable_value;
    string parameter;
    string variable_initialization;
    string condition;
    string variable_modifier;

    public Interpreter (string[] script, GameObject obj) {
        if (script == null) script = new string[] { };
        this.script = script;
        this.obj = obj;

        pointer = 0;
        variables = new List<VariableObject> ();
        scope_tracker = new Stack<int> ();
    }

    /* Parsing each line of text into code (a.k.a. where the magic happens) */
    public bool interpretLine () {
        if (script.Length > 0 && pointer >= 0 && pointer < script.Length) {
            debugger += "LINE: " + script[pointer] + "\n";
            string line = script[pointer];
            string[] line_parts = line.Split (' ');
            int pointer_saved = pointer;

            switch (line_parts[0]) {
                case Operators.EMPTY:
                    break;
                case Operators.BREAK:
                case Operators.CLOSING_BRACKET:
                case Operators.CONTINUE:
                    /* Find new pointer location when hitting a "end of scope" operation */
                    pointer = closeScope (line_parts[0] == Operators.CONTINUE);
                    break;
                case Operators.IF:
                case Operators.WHILE:
                    /* e.g. "while (i < 10) {" */
                    condition = Operators.EMPTY;
                    for (int i = 1; i < line_parts.Length - 1; i++) condition += line_parts[i] + " ";
                    /* e.g. "(i < 10)" */
                    evaluateCondition (condition.Substring (1, condition.Length - 3), line_parts[0]);
                    break;
                case Operators.FOR:
                    /* e.g. "for (int i = 0; i < 10; i++) {" */
                    parameter = Operators.EMPTY;
                    for (int i = 1; i < line_parts.Length - 1; i++) parameter += line_parts[i] + " ";
                    /* e.g. "(int i = 0; i < 10; i++)" */
                    parameter = parameter.Substring (1, parameter.Length - 3);
                    /* e.g. "int i = 0; i < 10; i++" */
                    string[] parameters = parameter.Split (Operators.END_LINE_CHAR);
                    variable_initialization = parameters[0];
                    condition = parameters[1].Substring (1);
                    variable_modifier = parameters[2].Substring (1);
                    /* e.g. ["int i = 0", "i < 10", "i++"] */
                    if (isVariable (variable_initialization.Split (' ') [1])) {
                        setVariable (variable_modifier); /* Is not the first time for loop has run, e.g. "i" exists*/
                    } else {
                        declareVariable (variable_initialization); /* Run first part of for loop for first iteration, e.g. "i" needs to be initialized*/
                    }
                    evaluateCondition (condition, line_parts[0]);
                    break;
                case Variables.BOOLEAN:
                case Variables.INTEGER:
                case Variables.FLOAT:
                case Variables.STRING:
                    declareVariable (line_parts);
                    break;
                default:
                    if (setVariable (line_parts)) {
                        /* Check if line is referring to a variable */
                        /* e.g. "i = 10;" */
                    } else if (false) /* Evaluator.evaluateFunction ("test", "test");*/ {
                        /* Check if line is preferring to a function */
                        /* e.g. "this.flyTowards(enemy);" */
                    }
                    break;
            }
            if (pointer == pointer_saved) pointer++; //step line
            if (pointer >= script.Length) return true; //script done
            return false;
        } else return true;
    }
    private void declareVariable (string line) {
        declareVariable (line.Split (' '));
    }
    private void declareVariable (string[] parts) {
        /* e.g. ["int", "i", "=", "123;"] */
        /*      [ 0   ,  1,   2,   3 ...] */
        variable_type = parts[0];
        variable_name = parts[1];
        variable_value = Operators.EMPTY;
        for (int i = 3; i < parts.Length; i++) {
            variable_value += scrubSymbols (parts[i]) + " ";
        }
        setVariable (variable_type, variable_name, variable_value);
    }
    private bool setVariable (string line) {
        return setVariable (line.Split (' '));
    }
    private bool setVariable (string[] parts) {
        if (parts.Length == 1) {
            /* e.g. "i++;" */
            parts = splitIncrement (parts[0]);
        }
        string variable_name = parts[0];
        string variable_operator = parts[1];
        int index;
        if (isVariable (variable_name, out index)) {
            variable_value = Evaluator.simplifyCondensedOperators (variable_name, variable_operator);
            for (int i = 2; i < parts.Length; i++) {
                variable_value += scrubSymbols (parts[i]);
                if (i != parts.Length - 1) variable_value += " ";
                else if (variable_operator != Operators.EQUALS) variable_value += Operators.CLOSING_PARENTHESIS;
            }
            setVariable (index, variable_value);
            return true;
        } else return false;
    }
    private void setVariable (int index, string value) {
        if (value != Operators.EMPTY) {
            variables[index].value = cast (parse (value), variables[index].type);
        }
    }
    private void setVariable (string type, string name, string value) {
        /* VARIABLE DOES NOT EXIST, INITIALIZE IT, e.g. "int i = 122;" */
        variables.Add (new VariableObject (type, name, cast (parse (value), type), pointer));
    }

    private string cast (string input, string cast_type) {
        if (input != Operators.EMPTY && Evaluator.getType (getValue (input)) == cast_type) {
            switch (cast_type) {
                case Variables.BOOLEAN:
                    return bool.Parse (input).ToString ();
                case Variables.INTEGER:
                    return int.Parse (input).ToString ();
                case Variables.FLOAT:
                    return float.Parse (input).ToString ();
                case Variables.STRING:
                    return input;
            }
        }
        //add cases to convert floats to ints, etc? 
        //but, preferrably implement casting (int.Parse...)
        return Operators.EMPTY;
    }
    private string parse (string input) {
        if (input != Operators.EMPTY) {
            List<string> parts = input.Split (' ').ToList<string> ();
            /* EVALUATE PARATHESIS AND FUNCTIONS RECURSIVELY, e.g. "12 + function(2) * 4" ==> "12 + 4 * 4" */
            for (int part = 0; part < parts.Count; part++) {
                if (parts[part].Contains (Operators.OPENING_PARENTHESIS)) {

                    string parts_to_be_condensed = parts[part];
                    while (parts[part].Contains (Operators.CLOSING_PARENTHESIS) == false) {
                        part++;
                        parts_to_be_condensed += " " + parts[part];
                    }
                    if (parts[part].IndexOf (Operators.OPENING_PARENTHESIS) == 0) {
                        parts[part] = parse (parts_to_be_condensed);
                    } else {
                        //to support user-made functions, or functions that require interpreted lines of code to be executed first, will require logic here to allow for putting this parse in a stack to be popped on the "return" of said function
                        string function = parts[part].Substring (0, parts[part].IndexOf (Operators.OPENING_PARENTHESIS));
                        /* e.g. Mathf.Abs(-10) == 10 */
                        parts[part] = Evaluator.simplifyFunction (function, parse (parts_to_be_condensed));
                    }
                }
            }

            /* PEMDAS REST OF OPERATIONS, e.g. ["12", "+", 4, "*", "4"] ==> ["12", "+", "16"] ==> ["28"]*/
            if (parts.Count > 1) {
                for (int operation_set = 0; operation_set < Operators.PEMDAS.Length; operation_set++) {
                    for (int part = 1; part < parts.Count - 1; part++) {
                        for (int operation = 0; operation < Operators.PEMDAS[operation_set].Length; operation++) {
                            string operation_type = parts[part];
                            if (operation_type == Operators.PEMDAS[operation_set][operation]) {
                                string left = getValue (parts[part - 1]), right = getValue (parts[part + 1]);
                                parts[part - 1] = Evaluator.simplify (left, operation_type, right);
                                parts.RemoveRange (part, 2);
                                part--;
                            }
                        }
                    }
                }
            }
            /* RETURN FULLY SIMPLIFIED VALUE, e.g. "28" */
            return parts[0];
        }
        return Operators.EMPTY;
    }
    private void evaluateCondition (string input, string type) {
        debugger += cast (parse (input), Variables.BOOLEAN) + " should be parseable!";
        input = cast (parse (input), Variables.BOOLEAN);

        if (bool.Parse (input) == true) {
            /* e.g. "true", execute within brackets */
            if (type == Operators.WHILE || type == Operators.FOR) {
                scope_tracker.Push (pointer);
            } else if (type == Operators.IF) {
                scope_tracker.Push (getPointerTo(pointer, Operators.CLOSING_BRACKET));
            }
        } else { skipScope (); }

    }

    private string scrubSymbols (string input) {
        string output = input;
        if (input.Contains (Operators.END_LINE)) output = input.Remove (input.IndexOf (Operators.END_LINE), 1);
        return output;
    }

    private string[] splitIncrement (string input) {
        string variable_name, variable_operation;
        input = input.Split (Operators.END_LINE_CHAR) [0]; //remove ; if there

        variable_name = input.Substring (0, input.Length - 2);
        variable_operation = input.Substring (input.Length - 2);

        switch (variable_operation) {
            case Operators.INCREMENT:
                return new string[] { variable_name, Operators.EQUALS, variable_name, Operators.ADD, "1" };
            case Operators.DECREMENT:
                return new string[] { variable_name, Operators.EQUALS, variable_name, Operators.SUBTRACT, "1" };

        }
        return new string[] { };
    }
    public bool isVariable (string name) {
        return getIndexOfVariable (name) != -1;
    }
    public bool isVariable (string name, out int index) {
        index = getIndexOfVariable (name);
        return index != -1;
    }
    private int getIndexOfVariable (string name) {
        for (int i = 0; i < variables.Count; i++)
            if (variables[i].name == name) return i;
        return -1;
    }
    private int closeScope (bool isContinuing) {
        if (scope_tracker.Count > 0) {
            if (isContinuing) return scope_tracker.Peek ();
            else {
                /* REMOVE VARIABLES DEFINED IN PREVIOUS SCOPE, e.g. "if (i < 10) { int j = 10; }" */
                variables = GarbageCollector.removeBetween (getPointerTo(pointer, Operators.OPENING_BRACKET), pointer, variables);

                //need logic for if statements being different
                // if an if closes, it needs to collect any possible garbage, but not go back to start of if statement
                return scope_tracker.Pop ();
            }
        }
        return -1;
    }

    private int getPointerTo (int starting_pointer, string target) {
        int bracket_counter = 1;
        switch (target) {
            case Operators.CLOSING_BRACKET:
                while (bracket_counter > 0) {
                    starting_pointer++;
                    if (script[starting_pointer].Contains (Operators.OPENING_BRACKET)) bracket_counter++;
                    else if (script[starting_pointer].Contains (Operators.CLOSING_BRACKET)) bracket_counter--;
                }
                return starting_pointer;
            case Operators.OPENING_BRACKET:
                while (bracket_counter > 0) {
                    starting_pointer--;
                    if (script[starting_pointer].Contains (Operators.OPENING_BRACKET)) bracket_counter--;
                    else if (script[starting_pointer].Contains (Operators.CLOSING_BRACKET)) bracket_counter++;
                }
                return starting_pointer;
        }
        return starting_pointer;
    }
    private void skipScope () {
        pointer = getPointerTo(pointer, Operators.CLOSING_BRACKET);
    }
    private string getValue (string input) {
        int index;
        if (isVariable (input, out index)) return variables[index].value;
        return input;
    }

    public override string ToString () {
        string output = debugger + "\n\n";
        debugger = Operators.EMPTY;
        for (int i = 0; i < variables.Count; i++) {
            output += "-> Variable(" + variables[i].ToString () + ")\n";
        }
        return output;
    }
}