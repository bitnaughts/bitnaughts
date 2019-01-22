﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class VariableObject {

    public string type;
    public string name;
    /* For Primitive Data Types, value == value, otherwise value holds ToString() */
    public string value;

    VariableObject[] fields;

    public VariableObject (string type) {
        init (type, "", "");
    }
    public VariableObject (string type, string name) {
        init (type, name, "");
    }
    public VariableObject (string type, string name, string value) {
        init (type, name, value);
    }
    public void init (string type, string name, string value) {
        this.type = type;
        this.name = name;
        this.value = value;

        switch (type) {
            case "Vector2":
                fields = new VariableObject[] {
                    new VariableObject ("float", "x", value.Split (',') [0]),
                    new VariableObject ("float", "y", value.Split (',') [1])
                };
                break;
            default:
                fields = null;
                break;
        }
    }
    public override string ToString () {
        string output = type + " " + name + " = " + value;
        if (fields != null) {

            for (int i = 0; i < fields.Length; i++) {
                output += "-> -> Field(" + fields[i].ToString () + ")\n";
            }
        }
        return output;
    }

}
public class Interpreter {

    string debugger = "";

    private GameObject obj;
    private string[] script;

    private List<VariableObject> variables;
    private List<string> listeners;
    private Stack<int> scope_tracker;

    private int pointer; //tracks what line is being processed

    string variable_type, variable_name, variable_value, variable_modifier, variable_initialization;
    string parameter, condition;

    public Interpreter (string[] script, GameObject obj) {
        if (script == null) script = new string[] { };
        this.script = script;
        this.obj = obj;

        pointer = 0;
        variables = new List<VariableObject> ();
        scope_tracker = new Stack<int> ();
        listeners = new List<string> ();
    }

    /* Parsing each line of text into code (a.k.a. where the magic happens) */
    public void interpretLine () {
        if (script.Length > 0) {
            string line = script[pointer];
            string[] line_parts = line.Split (' ');

            switch (line_parts[0]) {
                case "{": //this shouldn't be possible if linting to "if () {" lines
                case "}":
                    //scope control:
                    //update visible variables?
                    pointer = pointer + 1; //find new pointer position, might be going back to loop, skip else, etc
                    interpretLine (); //does not cost anything in interpreter, recalls
                    break;
                case Keywords.LIBRARY_IMPORT:
                    listeners.Add (scrubSymbols (line_parts[1]));
                    switch (scrubSymbols (line_parts[1])) {
                        case Classes.CONSOLE:
                            Referencer.consoleManager.execute (Console.OPEN, "", obj);
                            break;
                        case Classes.PLOTTER:

                            break;
                    }
                    /* e.g. "(int i = 0; i < 10; i++)" */

                    break;
                case Keywords.BREAK:
                case Operators.CLOSING_BRACKET:
                case Keywords.CONTINUE:
                    /* Find new pointer location when hitting a "end of scope" operation */
                    pointer = closeScope (line_parts[0] == Keywords.CONTINUE);
                    break;
                case Keywords.IF:
                case Keywords.WHILE:
                    /* e.g. "while (i < 10) {" */
                    condition = Operators.EMPTY;
                    for (int i = 1; i < line_parts.Length - 1; i++) condition += line_parts[i] + " ";
                    /* e.g. "(i < 10)" */
                    evaluateCondition (condition.Substring (1, condition.Length - 3), line_parts[0]);
                    break;
                case Keywords.FOR:
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
                    setVariable (variable_type, variable_name, variable_value);
                    break;
                default:
                    if (isVariable (line_parts[0])) {
                        /* CHECK IF LINE REFERS TO A VARIABLE, e.g. "i = 10;" */
                        setVariable (line_parts);
                    } else {
                        if (line_parts[0].Contains (".")) {
                            /* e.g. "Console.WriteLine("test")" */
                            string class_name = line_parts[0].Split ('.') [0];
                            string function_name = line_parts[0].Split ('.') [1].Split ('(') [0];
                            string function_parameters = line.Substring (line.IndexOf ("(") + 1);
                            function_parameters = function_parameters.Substring (0, function_parameters.Length - 2);
                            function_parameters = parse (function_parameters);

                            /* e.g. "Console", "WriteLine" */
                            switch (class_name) {
                                case Classes.CONSOLE:
                                    Referencer.consoleManager.execute (function_name, function_parameters, obj);
                                    break;
                                case "Application":
                                    break;
                                case "Mathf":
                                    break;
                            }
                        }

                        /* CHECK IF LINE REFERS TO A FUNCTION, e.g. "Console.WriteLine("Test");" */
                        /*

                            switch className

                         */
                        //how to handle the many various "action" functions people could call, e.g. Console.log, this.rotate(), etc.

                    }
                    break;
            }
            if (pointer == pointer_saved) pointer++; //step line
            if (pointer >= script.Length) return true; //script done]

            /* UPDATING LISTENERS */
            for (int listener = 0; listener < listeners.Count; listener++) {
                switch (listeners[listener]) {
                    case Classes.CONSOLE:
                        Referencer.consoleManager.execute (Console.UPDATE, pointer + "", obj);
                        break;
                    case Classes.PLOTTER:

                        break;
                }
            }

            return false;
        } else return true;
    }
    private void declareVariable (string line) {
        declareVariable (line.Split (' '));
    }
    private void declareVariable (string[] parts) {
        /* e.g. ["int", "i", "=", "123;"] */
        variable_type = parts[0];
        variable_name = parts[1];
        variable_value = Operators.EMPTY;
        for (int i = 3; i < parts.Length; i++) {
            variable_value += scrubSymbols (parts[i]) + " ";
        }
    }

    private void setVariable (string type, string name, string value) {
        int index = indexOfVariable (name);
        if (index != -1) {
            if (value != "") {
                value = cast (parse (value), type);
                variables[index].value = value;
            }
        } else {
            if (value != "") {
                /* e.g. "int i = 122;" */
                value = cast (parse (value), type);
                variables.Add (new VariableObject (type, name, value));
            } else {
                /* e.g. "int i;" */
                variables.Add (new VariableObject (type, name, ""));
            }
        }
    }
    private string cast (string input, string cast_type) {
        if (getVariableType (input) == cast_type) {
            switch (cast_type) {
                case Variables.BOOLEAN:
                    return left_bool + "";
                case Variables.INTEGER:
                    return left_int + "";
                case Variables.FLOAT:
                    return left_float + "";
                case Variables.STRING:
                    return input;
            }
        }
        return "";
    }
    private string parse (string input) {
        if (input != Operators.EMPTY) {
            List<string> parts = input.Split (' ').ToList<string> ();
            /* EVALUATE PARENTHESIS AND FUNCTIONS RECURSIVELY, e.g. "12 + function(2) * 4" ==> "12 + 4 * 4" */

            //this section is still untested and requires thorough testing
            //for edge cases with parenthesis, e.g. "Mathf.Abs((2 + 1) * 2)"
            //
            //I think this logic isn't the best suited for parenthesis
            //Fix:
            //find deepest set of parenthesis, combine spaces to parameters[], parse each parameter in parameters (for ","s in functions)
            //in the end, "2 * (2 + 2)" = "2 * 4"
            //or with functions, "2 * Mathf.Abs(2 + 2)" = "2 * Mathf.Abs(4)"...
            //If function preceeds ()s, keep parenthesis, else return parse(parameter)

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
                        // parts[part] = Evaluator.simplifyFunction (function, parse (parts_to_be_condensed));
                    }
                }
            }
            //todo: remove ")" (once scanned) and ";"s (useless)
        }

        /* PEMDAS REST OF OPERATIONS */
        /* e.g. ["12", "+", 4, "*", "4"] ==> ["12", "+", "16"] ==> ["28"]*/
        if (parts.Count > 1) {
            for (int operation = 0; operation < Operators.PEMDAS.Length; operation++) {
                string current_operation = Operators.PEMDAS[operation];
                for (int part = 1; part < parts.Count - 1; part++) {
                    if (parts[part] == current_operation) {
                        parts[part - 1] = evaluateOperation (parts[part - 1], parts[part], parts[part + 1]);
                        parts.RemoveRange (part, 2);
                        part -= 2;
                    }
                }
            }
        }
        //RETURN FULLY SIMPLIFIED VALUE
        return parts[0];
    }
    private void evaluateCondition (string input, string type) {
        input = cast (parse (input), Variables.BOOLEAN);

        if (bool.Parse (input) == true) {
            /* e.g. "true", execute within brackets */
            if (type == Keywords.WHILE || type == Keywords.FOR) {
                scope_tracker.Push (pointer);
            } else if (type == Keywords.IF) {
                scope_tracker.Push (getPointerTo (pointer, Operators.CLOSING_BRACKET));
            }
        } else { skipScope (); }

    private string evaluateFunction (string function, string parameter) {
        if (function.IndexOf ("Mathf") == 0) {
            return evaluateMathf (function, parameter);
        }
        //...

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
                    return evaulateBooleans (left_bool, arithmetic_operator, right_bool) + "";
                case Variables.INTEGER:
                    return evaluateIntegers (left_int, arithmetic_operator, right_int) + "";
                case Variables.FLOAT:
                    return evaluateFloats (left_float, arithmetic_operator, right_float) + "";
                case Variables.STRING:
                    return evaulateString (left, arithmetic_operator, right) + "";
            }
        }
        return "";
    }
    private bool evaulateBooleans (bool left, string arithmetic_operator, bool right) {
        switch (arithmetic_operator) {
            case Operators.EQUAL_TO:
                return left == right;
            case Operators.AND:
                return left && right;
            case Operators.OR:
                return left || right;
            default:
                return false;
        }
    }
    private int evaluateIntegers (int left, string arithmetic_operator, int right) {
        switch (arithmetic_operator) {
            case Operators.MODULUS:
                return left % right;
            case Operators.TIMES:
                return left * right;
            case Operators.DIVIDE:
                return left / right;
            case Operators.ADD:
                return left + right;
            case Operators.SUBTRACT:
                return left - right;
            default:
                return 0;
        }
    }
    private float evaluateFloats (float left, string arithmetic_operator, float right) {
        switch (arithmetic_operator) {
            case Operators.MODULUS:
                return left % right;
            case Operators.TIMES:
                return left * right;
            case Operators.DIVIDE:
                return left / right;
            case Operators.ADD:
                return left + right;
            case Operators.SUBTRACT:
                return left - right;
            default:
                return 0;
        }
    }
    private string evaulateString (string left, string arithmetic_operator, string right) {
        switch (arithmetic_operator) {
            case Operators.ADD:
                return left + right;
            default:
                return "";
        }
    }

    private string getVariableType (string input) {
        return getVariableType (input, true);
    }
    private string getVariableType (string input, bool left) {
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

    private int indexOfVariable (string name) {
        for (int i = 0; i < variables.Count; i++) {
            if (variables[i].name == name) {
                return i;
            }
        }
        return -1;
    }

    public override string ToString () {
        string output = "";
        for (int i = 0; i < variables.Count; i++) {
            output += "-> Variable(" + variables[i].ToString () + ")\n";
        }
        return output;
    }
}

/* if (line.IndexOf("
                while (") == 0)
 {
     string condition = (" (" + Parser_splitStringBetween(line, " (", ")
                        ") + ")
                    ");
     //  print(condition);
     if (Compiler_booleanEvaluation(condition))
     {
         tasks.Push(new TaskObject(current_line, Compiler_findMatchingBracket(current_line), condition, "
                    "));
     }
     else
     {
         current_line = Compiler_findNextInstanceOf(current_line, "
                }
                ");
     }
 }
 if (line.IndexOf("
                for (") == 0)
 {
     Compiler_addVariable(Parser_splitStringBetween(line, " (", ";
                        ") + ";
                        ");
     string condition = (" (" + Parser_splitStringBetween(line, ";
                            ", ";
                            ") + ")
                        ");
     if (Compiler_booleanEvaluation(condition))
     {
         tasks.Push(new TaskObject(current_line, Compiler_findMatchingBracket(current_line), condition, (Parser_splitStringBetween(line, Parser_splitStringBetween(line, ";
                        ", ";
                        "), ")
                    ") + ";
                    ").Substring(2)));
     }
     else
     {
         current_line = Compiler_findNextInstanceOf(current_line, "
                }
                ");
     }
 }
 if (tasks.Count != 0)
 {
     if (current_line == tasks.Peek().ending_line)
     {
         current_line = Compiler_doEndOfTask(tasks.Peek());
         if (current_line == tasks.Peek().ending_line) tasks.Pop();
     }
 }

 if (line.IndexOf("
                if (") == 0)
 {
     next_else = false; //Used to know if an "
                    else " is linked to an if/will be executed           
     if (!Compiler_booleanEvaluation(line))
     {
         //Jump past If Statement
         current_line = Compiler_findMatchingBracket(current_line);//Compiler_findNextInstanceOf(current_line, "
                }
                ");
         //Enter next else if available
         next_else = true;
     }
 }
 if (line.IndexOf("
                else ") == 0)
 {
     if (next_else == false)
     {
         current_line = Compiler_findMatchingBracket(current_line);
     }

 }

 //Variable statements
 for (int types_of_PDTs = 0; types_of_PDTs < list_of_PDTs.Length; types_of_PDTs++)
 {
     //DECLARING A NEW VARIABLE OF TYPE (list_of_PDTs[types_of_PDTs])
     if (line.IndexOf(list_of_PDTs[types_of_PDTs] + "
                ") == 0)
     {
         Compiler_addVariable(line);
     }
 }
 //Looks for a simple statement that is modifying variables
 Compiler_modifyVariable(line);

 if (line.IndexOf("
                System.out.print ") == 0)
 {
     string output = Parser_splitStringBetween(line, " (", ")
                ");
     print(output);
     output = Compiler_evaluateExpression(output);
     print(output);
     if (line.IndexOf("
                System.out.println ") == 0)
     {
         Display_pushConsoleText(output + "\
                n ");
     }
     else Display_pushConsoleText(output);
 }
 if (line.IndexOf("
                Iterate (") == 0)
 {
     //Iterate(Phone.Vibrate);
     if (line.Contains("
                        Vibrate "))
     {
         if (LevelManager.vibrating) Vibration.Vibrate(10);
     }
     else if (line.Contains("
                        Position "))
     {

         string type = Parser_splitStringBetween(line, " (", ", ");
         type = type.Substring(1, type.Length - 2);
         string pos = Parser_splitStringBetween(line, ", new Position (", ")
                            ");

         string x = Compiler_evaluateExpression(pos.Remove(pos.IndexOf(", ")));
         string y = Compiler_evaluateExpression(pos.Substring(pos.IndexOf(", ") + 1));

         Vector2 position = new Vector2(float.Parse(x), float.Parse(y));
         position /= 6;
         GameObject obj = Instantiate(Resources.Load(type), position, this.transform.rotation) as GameObject;
         obj.transform.SetParent(this.transform);

         //obj.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
     }
     else
     {
         string output = Parser_splitStringBetween(line, " (", ")
                            ");
         output = output.Substring(1, output.Length - 2);
         Instantiate(Resources.Load(output), this.transform.position, this.transform.rotation);
     }
 }
 if (line.IndexOf("
                            ClearIterations ();
                            ") == 0)
 {
     for (int i = 0; i < this.transform.childCount; i++)
     {
         Destroy(this.transform.GetChild(i).gameObject);
     }
 }
 if (line.IndexOf("
                            IterateArduino (new Color (") == 0)
 {//lines.Add("
                                    IterateArduino (new Color (1, 1, 1));
                                    ");

     string message = Parser_splitStringBetween(line, " (new Color (", ")
                                        ");//1,1,1
     string[] values = message.Split (',');
     for (int i = 0; i < values.Length; i++)
     {
         int val = int.Parse (Compiler_evaluateExpression (values[i]));
         if (val > 255) val = 255;
         if (val < 0) val = 0;
         bluetooth_module.message = byte.Parse (val.ToString ());
         bluetooth_module.send ();
     }
 }*/