using System.Collections.Generic;

public class VariableHandler {
    string variable_type, variable_name, variable_value, variable_modifier, variable_initialization, parameter, condition, debugger;

    //this should probably become a dictionary...
    public List<VariableObject> variables;

    public VariableHandler () {
        variables = new List<VariableObject> ();
    }
    public VariableHandler (List<VariableObject> variables) {
        this.variables = variables;
    }
    public void add (VariableObject variable) {
        variables.Add (variable);
    }

    public bool isVariable (string name) {
        return getIndexOfVariable (name) != -1;
    }
    public bool isVariable (string name, out int index) {
        index = getIndexOfVariable (name);
        return index != -1;
    }
    public int getIndexOfVariable (string name) {
        for (int i = 0; i < variables.Count; i++)
            if (variables[i].name == name) return i;
        return -1;
    }

    public string getValue (string input) {
        int index;
        if (isVariable (input, out index)) return variables[index].value;
        return input;
    }
    public void declareVariable (string line) {
        declareVariable (line.Split (' '));
    }
    public void declareVariable (string[] parts) {
        /* e.g. ["int", "i", "=", "123;"] */
        variable_type = parts[0];
        variable_name = parts[1];
        variable_value = Operators.EMPTY;
        for (int i = 3; i < parts.Length; i++) {
            variable_value += Evaluator.scrubSymbols (parts[i]) + " ";
        }
        setVariable (variable_type, variable_name, variable_value);
    }
    public void setVariable (string line) {
        setVariable (line.Split (' '));
    }
    public void setVariable (string[] parts) {
        if (parts.Length == 1) {
            /* e.g. "i++;" */
            parts = Parser.splitIncrement (parts[0]);
        }
        string variable_name = parts[0];
        string variable_operator = parts[1];
        int index;
        if (isVariable (variable_name, out index)) {
            variable_value = Evaluator.simplifyCondensedOperators (variable_name, variable_operator);
            for (int i = 2; i < parts.Length; i++) {
                variable_value += Evaluator.scrubSymbols (parts[i]);
                if (i != parts.Length - 1) variable_value += " ";
                else if (variable_operator != Operators.EQUALS) variable_value += Operators.CLOSING_PARENTHESIS;
            }
            setVariable (index, variable_value);
        }
    }
    public void setVariable (int index, string value) {
        if (value != Operators.EMPTY) {
            variables[index].value = cast (parse (value), variables[index].type);
        }
    }
    public void setVariable (string type, string name, string value) {
        /* VARIABLE DOES NOT EXIST, INITIALIZE IT, e.g. "int i = 122;" */
        variables.Add (new VariableObject (type, name, cast (parse (value), type), 1 /* pointer */ ));
    }
    public string parse (string input) {
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
    public string cast (string input, string cast_type) {
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
}