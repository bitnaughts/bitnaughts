using System.Collections.Generic;

public class ScopeNode {
    private int start_line;
    private int end_line;
    private int return_to_line;

    private bool looping;

    public VariableHandler variable_handler;

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

    public ScopeNode (int start_line, int end_line, bool looping) {
        this.start_line = start_line;
        this.end_line = end_line;
        this.return_to_line = end_line;
        this.variable_handler = new VariableHandler ();
        this.looping = looping;

    }
    public ScopeNode (int start_line, int end_line, List<VariableObject> variables, bool looping) {
        this.start_line = start_line;
        this.end_line = end_line;
        this.return_to_line = end_line;
        this.variable_handler = new VariableHandler (variables);
        this.looping = looping;
    }

    public void setRange(int start_line, int end_line) {
        this.start_line = start_line;
        this.end_line = end_line;
    }

    public int getStartLine () {
        return start_line;
    }
    public int getEndLine () {
        return end_line;
    }
    public int getReturnToLine() {
        return return_to_line;
    }
    public bool isLooping () {
        return looping;
    }

    public void addVariableToScope (VariableObject variable) {
        variable_handler.add (variable);
    }
    public override string ToString () {
        string output = "Range(" + start_line + "->" + end_line + ")\t" + variable_handler.ToString ();
        return output;
    }
}