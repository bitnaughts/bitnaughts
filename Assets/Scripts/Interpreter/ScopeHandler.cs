using System.Collections.Generic;

public class ScopeHandler {

    private int pointer;
    private Stack<ScopeNode> scope;

    public int closeScope (bool isContinuing) {
        if (scope.Count > 0) {
            if (isContinuing) return scope_tracker.Peek ();
            else {
                /* REMOVE VARIABLES DEFINED IN PREVIOUS SCOPE, e.g. "if (i < 10) { int j = 10; }" */
                //variables = GarbageCollector.removeBetween (getPointerTo (pointer, Operators.OPENING_BRACKET), pointer, variables);

                //need logic for if statements being different
                // if an if closes, it needs to collect any possible garbage, but not go back to start of if statement
                return scope_tracker.Pop ();
            }
        }
        return -1;
    }
    public void skipScope () {
        pointer = getPointerTo (pointer, Operators.CLOSING_BRACKET);
        variables = GarbageCollector.removeBetween (getPointerTo (pointer, Operators.OPENING_BRACKET), pointer, variables);
    }

    private void evaluateCondition (string input, string type) {
        input = cast (parse (input), Variables.BOOLEAN);

        if (bool.Parse (input) == true) {
            /* e.g. "true", execute within brackets */
            if (type == Keywords.WHILE || type == Keywords.FOR) {
              //  scope_tracker.Push (pointer);
            } else if (type == Keywords.IF) {
               // scope_tracker.Push (getPointerTo (pointer, Operators.CLOSING_BRACKET));
            }
        } else { skipScope (); }

    }

    public int getPointerTo (int starting_pointer, string target) {
        int bracket_counter = 1;
        switch (target) {
            case Operators.CLOSING_BRACKET:
                while (bracket_counter > 0) {
                    starting_pointer++;
                    if (script[starting_pointer].Contains (Operators.OPENING_BRACKET)) bracket_counter++;
                    else if (script[starting_pointer].Contains (Operators.CLOSING_BRACKET)) bracket_counter--;
                }
                return starting_pointer;
            case Operators.OPENING_BRACKET:
                while (bracket_counter > 0) {
                    starting_pointer--;
                    if (script[starting_pointer].Contains (Operators.OPENING_BRACKET)) bracket_counter--;
                    else if (script[starting_pointer].Contains (Operators.CLOSING_BRACKET)) bracket_counter++;
                }
                return starting_pointer;
        }
        return starting_pointer;
    }

}