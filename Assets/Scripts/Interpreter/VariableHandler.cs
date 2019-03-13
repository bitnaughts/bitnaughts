using System.Collections.Generic;

public class VariableHandler {

    public List<VariableObject> variables;

    public VariableHandler() {
        variables = new List<VariableObject>();
    }
    public VariableHandler(List<VariableObject> variables) {
        this.variables = variables;
    }
    public void add(VariableObject variable) {
        variables.Add(variable);
    }

    public static bool isVariable (string name) {
        return getIndexOfVariable (name) != -1;
    }
    public static bool isVariable (string name, out int index) {
        index = getIndexOfVariable (name);
        return index != -1;
    }
    public static int getIndexOfVariable (string name) {
        for (int i = 0; i < variables.Count; i++)
            if (variables[i].name == name) return i;
        return -1;
    }

    public static string getValue (string input) {
        int index;
        if (isVariable (input, out index)) return variables[index].value;
        return input;
    }
    public static void declareVariable (string line) {
        declareVariable (line.Split (' '));
    }
    public static void declareVariable (string[] parts) {
        /* e.g. ["int", "i", "=", "123;"] */
        variable_type = parts[0];
        variable_name = parts[1];
        variable_value = Operators.EMPTY;
        for (int i = 3; i < parts.Length; i++) {
            variable_value += scrubSymbols (parts[i]) + " ";
        }
        setVariable (variable_type, variable_name, variable_value);
    }
    public static void setVariable (string line) {
        setVariable (line.Split (' '));
    }
    public static void setVariable (string[] parts) {
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
        }
    }
    public static void setVariable (int index, string value) {
        if (value != Operators.EMPTY) {
            variables[index].value = cast (parse (value), variables[index].type);
        }
    }
    public static void setVariable (string type, string name, string value) {
        /* VARIABLE DOES NOT EXIST, INITIALIZE IT, e.g. "int i = 122;" */
        variables.Add (new VariableObject (type, name, cast (parse (value), type), pointer));
    }

    public static string cast (string input, string cast_type) {
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