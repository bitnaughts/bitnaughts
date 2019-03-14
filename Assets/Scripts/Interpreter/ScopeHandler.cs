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
        return scope.Peek ().variable_handler.parse(line);
    }

    public List<VariableObject> getVariablesInScope () {
        return scope.Peek ().variable_handler.variables;
    }

        // List <VariableObject> collection = new List<VariableObject>();

        // ScopeNode[] full_scope = scope.ToArray(); 
        // for (int i = 0; i < full_scope; i++) {
        //     foreach (VariableObject variable in full_scope[i].variables)
        //     {
        //         /* Bad O(N^2) Solution, but ensures if there are multiple variables of the same name
        //            (e.g. recursive calls), the highest in scope is picked and others are ignored.
        //          */
        //         bool isUnique = true;
        //         foreach(VariableObject collection_variable in collection) {
        //             if (collection_variable.name == variable.name) {
        //                 isUnique = false;
        //                 break;
        //             }
        //         }
        //         if (isUnique) {
        //           collection.Add(variable);
        //         }
        //     }
        // }
        // return collection;       

    public void pop (bool isContinuing) {
        if (scope.Count > 0) {
            //   if (isContinuing) return scope.Peek ();
            // else {
            /* REMOVE VARIABLES DEFINED IN PREVIOUS SCOPE, e.g. "if (i < 10) { int j = 10; }" */
            //variables = GarbageCollector.removeBetween (getPointerTo (pointer, Operators.OPENING_BRACKET), pointer, variables);

            //need logic for if statements being different
            // if an if closes, it needs to collect any possible garbage, but not go back to start of if statement
            //     return scope.Pop ();
            //}
        }
        //return -1;
    }
    public void skipScope () {
        pointer = scope.Peek ().getEndLine (); //getPointerTo (pointer, Operators.CLOSING_BRACKET); //will get "End line" value here
        //   variables = GarbageCollector.removeBetween (getPointerTo (pointer, Operators.OPENING_BRACKET), pointer, variables);
    }

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
