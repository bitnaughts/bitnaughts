using System.Collections.Generic;

public class FunctionHandler {

    public List<FunctionObject> functions;

    public FunctionHandler () {
        this.functions = new List<FunctionObject> ();
    }
    public FunctionHandler (List<FunctionObject> functions) {
        this.functions = functions;
    }

    public void declareFunction (string line, int line_defined) {
        declareFunction (line.Split (' '), line_defined);
    }
    public void declareFunction (string[] line_parts, int line_defined) {
        /* e.g. { "void", "funct(int", "i)", "{" }  */
        string return_type = line_parts[0];
        string name = line_parts[1].Split ('(') [0];

        string parameter = line_parts[1].Split ('(') [1];
        for (int i = 2; i < line_parts.Length - 1; i++) parameter += line_parts[i] + " ";
        parameter = parameter.Substring (1, parameter.Length - 1);

        string[] parameters_string = parameter.Split (',');

        //might need a check here for if parameters is null...

        VariableObject[] parameters = null;

        if (parameter.Length > 0) {
            parameters = new VariableObject[parameters_string.Length];

            for (int i = 0; i < parameters_string.Length; i++) {
                string type = parameters_string[i].Split (' ') [0];
                string variable_name = parameters_string[i].Split (' ') [1];
                parameters[i] = new VariableObject (type, name, "");
            }
        }

        FunctionObject function = new FunctionObject (return_type, name, line_defined, parameters);

        functions.Add (function);
    }

    public override string ToString () {
        string output = "";
        for (int i = 0; i < functions.Count; i++) {
            output += functions[i].ToString () + "\n";
        }
        return output;
    }

}