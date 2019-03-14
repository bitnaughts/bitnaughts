using System.Collections.Generic;

public class ScopeHandler {

    private int pointer;
    private Stack<ScopeNode> scope;

    public bool isVariableInScope (string name) {
        return scope.Peek ().variable_handler.isVariable (name);
    }
    public void setVariableInScope (string line) {
        scope.Peek ().variable_handler.setVariable (line);
    }
    public void declareVariableInScope (string line) {
        scope.Peek ().variable_handler.declareVariable (line);
    }
    public string parseInScope (string line) {
        return scope.Peek ().variable_handler.parse (line);
    }
    public List<VariableObject> getVariablesInScope () {
        return scope.Peek ().variable_handler.variables;
    }

    public void step() {
        pointer++;
    }

    public void push (int end_line) {
       push(pointer, end_line);
    }
    public void push (int start_line, int end_line) {
        
        //Add all existing variables to new scope
        ScopeNode node = new ScopeNode (start_line, end_line, getVariablesInScope ());
        scope.Push (node);
    }
    public void pop () {
        pointer = scope.Peek ().getEndLine ();
        scope.Pop ();
    }
    public void back () { //continue keyword
        pointer = scope.Peek ().getStartLine();
    }
    // public void skipScope () {
    //     pointer = scope.Peek ().getEndLine (); //getPointerTo (pointer, Operators.CLOSING_BRACKET); //will get "End line" value here
    //     //   variables = GarbageCollector.removeBetween (getPointerTo (pointer, Operators.OPENING_BRACKET), pointer, variables);
    // }

    public int getPointer () {
        return pointer;
    }
    // public int getPointerTo (int starting_pointer, string target) {
    //     int bracket_counter = 1;
    //     switch (target) {
    //         case Operators.CLOSING_BRACKET:
    //             while (bracket_counter > 0) {
    //                 starting_pointer++;
    //                 if (script[starting_pointer].Contains (Operators.OPENING_BRACKET)) bracket_counter++;
    //                 else if (script[starting_pointer].Contains (Operators.CLOSING_BRACKET)) bracket_counter--;
    //             }
    //             return starting_pointer;
    //         case Operators.OPENING_BRACKET:
    //             while (bracket_counter > 0) {
    //                 starting_pointer--;
    //                 if (script[starting_pointer].Contains (Operators.OPENING_BRACKET)) bracket_counter--;
    //                 else if (script[starting_pointer].Contains (Operators.CLOSING_BRACKET)) bracket_counter++;
    //             }
    //             return starting_pointer;
    //     }
    //     return starting_pointer;
    // }
}