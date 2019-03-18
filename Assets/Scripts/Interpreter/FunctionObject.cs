public class FunctionObject {

    public string return_type;
    public string name;
    public int line_defined;
    VariableObject[] parameters;

    public FunctionObject (string[] line_parts, int line) {
        this.return_type = line_parts[0];
        this.name = line_parts[1].Split ('(') [0];
        this.line_defined = line;
        string parameter =  line_parts[1].Split ('(') [1];
        for (int i = 2; i < line_parts.Length - 1; i++) parameter += line_parts[i] + " ";
        parameter = parameter.Substring (1, parameter.Length - 2);

        //parse into variables   
    }

}