public class FunctionObject {

    public string return_type;
    public string name;
    public int line_defined;
    VariableObject[] parameters;

    public FunctionObject (string return_type, string name, int line_defined, VariableObject[] parameters) {
        this.return_type = return_type;
        this.name = name;
        this.line_defined = line_defined;
        this.parameters = parameters;
    }

    public override string ToString () {
        string output = return_type + " " + name + " " + line_defined;
        if (parameters != null) {
            for (int i = 0; i < parameters.Length; i++) {
                output += "-> -> Parameter(" + parameters[i].ToString () + ")\n";
            }
        }
        return output;
    }

}