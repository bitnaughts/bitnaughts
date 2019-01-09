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
    private Stack<int> scope_tracker;
    private string[] script;
    private int pointer; //tracks what line is being processed

    int left_int, right_int;
    float left_float, right_float;
    bool left_bool, right_bool;
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
                case "":
                    break;
                case Operators.BREAK:
                case Operators.CLOSING_BRACKET:
                    if (scope_tracker.Count > 0) {
                        pointer = scope_tracker.Pop ();
                    }
                    break;
                case Operators.CONTINUE:
                    if (scope_tracker.Count > 0) {
                        pointer = scope_tracker.Peek ();
                    }
                    break;
                case Operators.IF:
                case Operators.WHILE:
                    /* e.g. "while (i < 10) {" */
                    condition = "";
                    for (int i = 1; i < line_parts.Length - 1; i++) condition += line_parts[i] + " ";
                    /* e.g. "(i < 10)" */
                    evaluateCondition (condition.Substring (1, condition.Length - 3), line_parts[0]);
                    break;
                case Operators.FOR:
                    /* e.g. "for (int i = 0; i < 10; i++) {" */
                    parameter = "";
                    for (int i = 1; i < line_parts.Length - 1; i++) parameter += line_parts[i] + " ";
                    /* e.g. "(int i = 0; i < 10; i++)" */
                    parameter = parameter.Substring (1, parameter.Length - 3);
                    /* e.g. "int i = 0; i < 10; i++" */
                    string[] parameters = parameter.Split (';');
                    variable_initialization = parameters[0];
                    condition = parameters[1].Substring (1);
                    variable_modifier = parameters[2].Substring (1);
                    /* e.g. ["int i = 0", "i < 10", "i++"] */
                    if (isVariable (variable_initialization.Split (' ') [1])) {
                        setVariable (variable_modifier); /* Is not the first time for loop has run */
                    } else {
                        declareVariable (variable_initialization); /* Run first part of for loop for first iteration */
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
                    } else if (false) evaluateFunction ("test", "test"); {
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
        variable_value = "";
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
        int index = indexOfVariable (parts[0]);
        if (index != -1) {
            variable_value = "";
            for (int i = 2; i < parts.Length; i++) {
                variable_value += scrubSymbols (parts[i]) + " ";
            }
            if (parts[1] != Operators.EQUALS) {
                switch (parts[1]) {
                    case Operators.ADDITION:
                        variable_value += Operators.ADD + " " + parts[0];
                        break;
                    case Operators.SUBTRACTION:
                        variable_value += Operators.SUBTRACT + " " + parts[0];
                        break;
                }
            }
            setVariable (index, variable_value);
            return true;
        } else return false;
    }
    private void setVariable (int index, string value) {
        if (value != "") {
            value = cast (parse (value), variables[index].type);
            variables[index].value = value;
        }
    }
    private void setVariable (string type, string name, string value) {
        /* VARIABLE DOES NOT EXIST, INITIALIZE IT */
        if (value != "") {
            /* e.g. "int i = 122;" */
            value = cast (parse (value), type);
            variables.Add (new VariableObject (type, name, value));
        } else {
            /* e.g. "int i;" */
            variables.Add (new VariableObject (type, name, ""));
        }
    }

    private string cast (string input, string cast_type) {
        if (getVariableType (input) == cast_type) {
            switch (cast_type) {
                case Variables.BOOLEAN:
                    return left_bool.ToString ();
                case Variables.INTEGER:
                    return left_int.ToString ();
                case Variables.FLOAT:
                    return left_float.ToString ();
                case Variables.STRING:
                    return input;
            }
        }
        return "";
    }
    private string parse (string input) {

        List<string> parts = input.Split (' ').ToList<string> ();

        /* EVALUATE PARATHESIS AND FUNCTIONS RECURSIVELY */
        /* e.g. "12 + function(2) * 4" ==> "12 + 4 * 4" */
        for (int part = 0; part < parts.Count; part++) {
            if (parts[part].Contains ("(")) {

                string parts_to_be_condensed = parts[part];
                while (parts[part].Contains (")") == false) {
                    part++;
                    parts_to_be_condensed += " " + parts[part];
                }
                if (parts[part].IndexOf ("(") == 0) {
                    parts[part] = parse (parts_to_be_condensed);
                } else {
                    //to support user-made functions, or functions that require interpreted lines of code to be executed first, will require logic here to allow for putting this parse in a stack to be popped on the "return" of said function
                    string function = parts[part].Substring (0, parts[part].IndexOf ("("));
                    parts[part] = evaluateFunction (function, parse (parts_to_be_condensed));
                }
            }
            //todo: remove ")" (once scanned) and ";"s (useless)
        }

        /* PEMDAS REST OF OPERATIONS */
        /* e.g. ["12", "+", 4, "*", "4"] ==> ["12", "+", "16"] ==> ["28"]*/
        if (parts.Count > 1) {
            for (int operation_set = 0; operation_set < Operators.PEMDAS.Length; operation_set++) {
                for (int part = 1; part < parts.Count - 1; part++) {
                    for (int operation = 0; operation < Operators.PEMDAS[operation_set].Length; operation++) {
                        if (parts[part] == Operators.PEMDAS[operation_set][operation]) {
                            parts[part - 1] = evaluateOperation (parts[part - 1], parts[part], parts[part + 1]);
                            parts.RemoveRange (part, 2);
                            part--;
                        }
                    }
                }
                // debugger += current_operation + ": ";
                // for (int part = 0; part < parts.Count - 1; part++) {
                // debugger += ">" + parts[part] + "<";
                // }
                // debugger += "\n";
            }
        }
        //RETURN FULLY SIMPLIFIED VALUE
        // debugger += "END: " + parts[0] + "\n";
        return parts[0];
    }
    private void evaluateCondition (string input, string type) {
        input = cast (parse (input), Variables.BOOLEAN);
        if (bool.Parse (input) == true) {
            /* e.g. "true", execute within brackets */
            if (type == "while" || type == "for") {
                scope_tracker.Push (pointer);
            }
        } else { skipScope (); }

    }

    private string evaluateFunction (string function, string parameter) {
        if (function.IndexOf ("Mathf") == 0) {
            return evaluateMathf (function, parameter);
        }
        if (function.IndexOf("this") == 0) {
            return evaluateControlFunctions();
        }

        //...
        return "";
    }
    private string evaluateControlFunctions (string function, string parameter) {
        switch (function) {
            case "this.rotate":
                return Mathf.Abs (float.Parse (parameter)) + "";
                //...
        }
        return "";
    }
    private string evaluateMathf (string function, string parameter) {
        switch (function) {
            case "Mathf.Abs":
                return Mathf.Abs (float.Parse (parameter)) + "";
                //...
        }
        return "";
    }
    private string evaluateOperation (string left, string arithmetic_operator, string right) {
        /* e.g. ["12", "*", "4"] ==> ["48"] */
        string left_type = getVariableType (left, true), right_type = getVariableType (right, false);
        if (left_type == right_type) {
            switch (left_type) {
                case Variables.BOOLEAN:
                    return evaulateBooleans (left_bool, arithmetic_operator, right_bool);
                case Variables.INTEGER:
                    return evaluateIntegers (left_int, arithmetic_operator, right_int);
                case Variables.FLOAT:
                    return evaluateFloats (left_float, arithmetic_operator, right_float);
                case Variables.STRING:
                    return evaulateString (left, arithmetic_operator, right);
            }
        }
        return "";
    }
    private string evaulateBooleans (bool left, string arithmetic_operator, bool right) {
        switch (arithmetic_operator) {
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            case Operators.AND:
                return (left && right).ToString ();
            case Operators.OR:
                return (left || right).ToString ();
            default:
                return "";
        }
    }
    private string evaluateIntegers (int left, string arithmetic_operator, int right) {
        switch (arithmetic_operator) {
            case Operators.MODULUS:
                return (left % right).ToString ();
            case Operators.TIMES:
                return (left * right).ToString ();
            case Operators.DIVIDE:
                return (left / right).ToString ();
            case Operators.ADD:
                return (left + right).ToString ();
            case Operators.SUBTRACT:
                return (left - right).ToString ();
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            case Operators.GREATER_THAN:
                return (left > right).ToString ();
            case Operators.GREATER_THAN_EQUAL:
                return (left >= right).ToString ();
            case Operators.LESS_THAN:
                return (left < right).ToString ();
            case Operators.LESS_THAN_EQUAL:
                return (left <= right).ToString ();
            default:
                return "";
        }
    }
    private string evaluateFloats (float left, string arithmetic_operator, float right) {
        switch (arithmetic_operator) {
            case Operators.MODULUS:
                return (left % right).ToString ();
            case Operators.TIMES:
                return (left * right).ToString ();
            case Operators.DIVIDE:
                return (left / right).ToString ();
            case Operators.ADD:
                return (left + right).ToString ();
            case Operators.SUBTRACT:
                return (left - right).ToString ();
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            case Operators.GREATER_THAN:
                return (left > right).ToString ();
            case Operators.GREATER_THAN_EQUAL:
                return (left >= right).ToString ();
            case Operators.LESS_THAN:
                return (left < right).ToString ();
            case Operators.LESS_THAN_EQUAL:
                return (left <= right).ToString ();
            default:
                return "";
        }
    }
    private void skipScope () {
        int bracket_counter = 1;
        while (bracket_counter > 0) {
            pointer++;
            if (script[pointer].Contains ("{")) {
                bracket_counter++;
            }
            if (script[pointer].Contains ("}")) {
                bracket_counter--;
            }
        }
    }
    private string evaulateString (string left, string arithmetic_operator, string right) {
        switch (arithmetic_operator) {
            case Operators.ADD:
                return left + right;
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            default:
                return "";
        }
    }

    private string getVariableType (string input) {
        return getVariableType (input, true);
    }
    private string getVariableType (string input, bool left) {

        int index = indexOfVariable (input);
        if (index != -1) {
            input = variables[index].value;
        }
        if (left) {
            if (bool.TryParse (input, out left_bool)) return Variables.BOOLEAN;
            if (int.TryParse (input, out left_int)) return Variables.INTEGER;
            if (float.TryParse (input, out left_float)) return Variables.FLOAT;

            //...
        } else {
            if (bool.TryParse (input, out right_bool)) return Variables.BOOLEAN;
            if (int.TryParse (input, out right_int)) return Variables.INTEGER;
            if (float.TryParse (input, out right_float)) return Variables.FLOAT;
            //...
        }
        return Variables.STRING;
    }

    private string scrubSymbols (string input) {
        string output = input;
        if (input.Contains (";")) {
            output = input.Remove (input.IndexOf (";"), 1);
        }
        return output;
    }

    private string[] splitIncrement (string input) {
        string variable_name, variable_operation;
        input = input.Split (';') [0]; //remove ; if there

        variable_name = input.Substring (0, input.Length - 2);
        variable_operation = input.Substring (input.Length - 2);

        switch (variable_operation) {
            case Operators.INCREMENT:
                return new string[] { variable_name, "=", variable_name, "+", "1" };
            case Operators.DECREMENT:
                return new string[] { variable_name, "=", variable_name, "-", "1" };

        }
        return new string[] {};
    }

    public bool isVariable (string name) {
        return indexOfVariable (name) != -1;
    }
    private int indexOfVariable (string name) {
        for (int i = 0; i < variables.Count; i++) {
            if (variables[i].name == name) {
                return i;
            }
        }
        return -1;
    }

    public override string ToString () {
        string output = debugger + "\n\n";
        debugger = "";
        for (int i = 0; i < variables.Count; i++) {
            output += "-> Variable(" + variables[i].ToString () + ")\n";
        }
        return output;
    }
}