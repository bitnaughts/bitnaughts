
using System.Collections.Generic;

public class ScopeNode {
    private int start_line;
    private int end_line;

    private VariableHandler variables;

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
        variables = new VariableHandler();
    }
    public ScopeNode(int start_line, int end_line, List<VariableObject> variables) {
        this.start_line = start_line;
        this.end_line = end_line;
        this.variables = new VariableHandler(variables);
    }

    public void addVariableToScope(VariableObject variable) {
        variables.add(variable);
    }
}