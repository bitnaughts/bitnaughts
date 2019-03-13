public class ScopeNode {
    private int start_line;
    private int end_line;
    private List<VariableObject> variables;

    /* 
    1   :   if (true) {
    2   :       int var = 10;
    3   :       if (false) { 
    4   :           print(var);
    5   :       }
    6   :   }
    
    OUTER
    start line == 1
    end line == 6

    INNER
    start line == 3
    end line == 5

    when closing node, if condition if false, go to end_line + 1; if condition is true, go to start_line + 1
    */

    public ScopeNode(int start_line, int end_line) {
        this.start_line = start_line;
        this.end_line = end_line;
        variables = new List<VariableObject>();
    }
    public ScopeNode(int start_line, int end_line, List<VariableObject> variables) {
        this.start_line = start_line;
        this.end_line = end_line;
        this.variables = variables;
    }

    public void addVariableToScope(VariableObject variable) {
        variables.Add(variable);
    }

  //  public void init (int start_line, int end_line) {
  //  }
}