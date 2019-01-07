using System;
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

    private List<VariableObject> variables;
    private Stack<int> scope_tracker;
    private string[] script;

    private int pointer; //tracks what line is being processed

    int left_int, right_int;
    float left_float, right_float;
    bool left_bool, right_bool;
    string variable_type, variable_name, variable_value;

    // Stack<short> backlog; 

    public Interpreter (string[] script, GameObject obj) {
        if (script == null) script = new string[] { };
        this.script = script;
        this.obj = obj;

        pointer = 0;
        variables = new List<VariableObject> ();
        scope_tracker = new Stack<int> ();
    }

    /* Parsing each line of text into code (a.k.a. where the magic happens) */
    public void interpretLine () {
        if (script.Length > 0) {
            debugger += "LINE: " + script[pointer] + "\n\n";
            string line = script[pointer];
            string[] line_parts = line.Split (' ');
            int pointer_saved = pointer;

            switch (line_parts[0]) {
                case Operators.CLOSING_BRACKET:
                    if (scope_tracker.Count > 0) {
                        pointer = scope_tracker.Pop ();
                    }
                    //scope control:
                    //update visible variables?
                    //find new pointer position, might be going back to loop, skip else, etc
                    //does not cost anything in interpreter, recalls
                    break;
                case "if":
                case "while":
                    /* e.g. "while (i < 10) {" */
                    string condition = "";
                    for (int i = 1; i < line_parts.Length - 1; i++) {
                        condition += line_parts[i];
                    }
                    /* e.g. "(i < 10)" */
                    condition = condition.Substring (1, condition.Length - 2);
                    condition = cast (parse (condition), Variables.BOOLEAN);
                    if (bool.Parse (condition) == true) {
                        if (line_parts[0] == "while") scope_tracker.Push (pointer);
                    }
                    else {
                        int bracket_counter = 1;
                        while (bracket_counter > 0) {
                            pointer++;
                            if (script[pointer].Contains("{")) {
                                bracket_counter++;
                            }
                            if (script[pointer].Contains("}")) {
                                bracket_counter--;
                            }
                        }
                    }

                    debugger += condition;
                    break;
                case "for":
                    /* e.g. "for (int i = 0; i < 10; i++) {" */
                    break;
                case "break":
                case "continue":
                    //scope-flow control?
                    //break => go to current loop's } 
                    //continue => go to current loop's { [where ++ and conditionals are also checked]
                    break;
                case Variables.BOOLEAN:
                case Variables.INTEGER:
                case Variables.FLOAT:
                case Variables.STRING:
                    /* e.g. ["int", "i", "=", "123;"] */
                    /*      [ 0   ,  1,   2,   3 ...] */
                    variable_type = line_parts[0];
                    variable_name = line_parts[1];
                    variable_value = "";

                    for (int i = 3; i < line_parts.Length; i++) {
                        variable_value += scrubSymbols (line_parts[i]) + " ";
                    }

                    setVariable (variable_type, variable_name, variable_value);
                    break;
                default:
                    int index = indexOfVariable (line_parts[0]);
                    debugger += index + ", " + line_parts[0];
                    if (index != -1) {
                        /* e.g. "i = 10;" */
                        variable_value = "";
                        for (int i = 2; i < line_parts.Length; i++) {
                            variable_value += scrubSymbols (line_parts[i]) + " ";
                        }
                        setVariable (index, variable_value);
                    }
                    break;
            }
            if (pointer == pointer_saved) {
                pointer++;
                if (pointer == script.Length) {
                    pointer = 0;
                    variables = new List<VariableObject> ();
                }
            }
        }
    }
    private void setVariable (int index, string value) {
        if (value != "") {
            value = cast (parse (value), variables[index].type);
            variables[index].value = value;
        }
    }
    private void setVariable (string type, string name, string value) {
        /* Variable does not exist, initialize it */
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
            for (int operation_set = 0; operation_set < 5; operation_set++) {
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
                for (int part = 0; part < parts.Count - 1; part++) {
                    debugger += ">" + parts[part] + "<";
                }
                debugger += "\n";
            }
        }
        //RETURN FULLY SIMPLIFIED VALUE
        debugger += "END: " + parts[0] + "\n";
        return parts[0];
    }

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
