public class FunctionObject {

    public string return_type;
    public string name;
    public Range scope_bounds;
    VariableObject[] parameters;

    public FunctionObject (string return_type, string name, Range scope_bounds, VariableObject[] parameters) {
        this.return_type = return_type;
        this.name = name;
        this.scope_bounds = scope_bounds;
        this.parameters = parameters;
    }

    public override string ToString () {
        string output = return_type + " " + name;
        if (parameters != null) {
            for (int i = 0; i < parameters.Length; i++) {
                output += "-> -> Parameter(" + parameters[i].ToString () + ")\n";
            }
        }
        return output;
    }

}