using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interpreter {

    string debugger = "";

    private GameObject obj;
    private string[] script;


    //To be replaced by scope tree....
    // private List<VariableObject> variables;
    // private Stack<int> scope_tracker;
    
    
    private List<string> listeners;
    // private Tree<ScopeNode> scope_tree;

    
    // variables list needs to be a tree of lists, each scope stepped into adds leaf, each scope popped out of removes leaf
    // scope tracker should be derived from this tree?

    // each variable may need an "id" plus a unique hash for what scope it is in?


    //If step into a new scope, make a new leaf off of current leaf in scope_tree
    //if declaring variable in scope, put it in scope_node's variable list
    //if accessing a variable, check from current node up to see what the value is (preferring closer scope)
    //this.variable_name == only check top of tree

    // void example_function(int x) {
    //
    //}

    // void example_function() {
    //  int x = parameter_in[0];
    //  
    //  return null;
    //}

    //I lied, trees are unnecessary in their memory storage of scopes that will be ignored/deleted, use a stack...

    private Stack<ScopeNode> scope;


    private int pointer; //tracks what line is being processed

    string variable_type, variable_name, variable_value, variable_modifier, variable_initialization;
    string parameter, condition;

    public Interpreter (string[] script, GameObject obj) {
        if (script == null) script = new string[] { };
        this.script = script;
        this.obj = obj;

        pointer = 0;

        scope = new Stack<ScopeNode>();
        // variables = new List<VariableObject> ();
        // scope_tracker = new Stack<int> ();
        listeners = new List<string> ();
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
                case Keywords.LIBRARY_IMPORT:
                    listeners.Add (scrubSymbols (line_parts[1]));
                    switch (scrubSymbols (line_parts[1])) {
                        case Classes.CONSOLE:
                            Referencer.consoleManager.execute (Console.OPEN, "", obj);
                            break;
                        case Classes.PLOTTER:

                            break;
                    }
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
                    evaluateCondition (condition, line_parts[0]);
                    break;
                case Variables.BOOLEAN:
                case Variables.INTEGER:
                case Variables.FLOAT:
                case Variables.STRING:
                    declareVariable (line_parts);
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
            if (pointer >= script.Length) return true; //script done

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

            /* PEMDAS REST OF OPERATIONS, e.g. ["12", "+", 4, "*", "4"] ==> ["12", "+", "16"] ==> ["28"] */
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

    //... to be moved...
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
    public int getPointer () {
        return pointer;
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