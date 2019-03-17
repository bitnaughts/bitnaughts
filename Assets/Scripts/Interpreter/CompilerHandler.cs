using System.Collections.Generic;
using UnityEngine;

public class CompilerHandler {
    /*
        Needs to determine::
            - Where the main function is
            - What Listeners to add (list)
            - What Function Objects to add (list)
            - What Variable Objects to add (list) to base scope...
     */

    public int main_function_line;

    public List<String> handlers;
    public ScopeNode base_scope;
    // public List<VariableObject> variables;

    public CompilerHandler (string[] lines) {
        compile (lines);
    }

    public void compile (string[] lines) {
        int scope_depth = 0;

        for (int i = 0; i < lines.Length; i++) {
            string line = lines[i];

            /* Manage current depth of scope (relevant for public variables, function declarations) */
            if (line.Contains (Operators.OPENING_BRACKET)) scope_depth++;
            if (line.Contains (Operators.CLOSING_BRACKET)) scope_depth--;

            string[] line_parts = line.Split (' ');

            if (scope_depth == 0) {
                /* Outside Class declaration, for imports */
                switch (line_parts[0]) {
                    case Keywords.LIBRARY_IMPORT:
                        handlers.Add (line_parts[1]);
                        break;
                }
            }

            if (scope_depth == 1) {
                /* Inside Class declaration, but not inside any functions */
                switch (line_parts[0]) {
                    case Variables.BOOLEAN:
                    case Variables.INTEGER:
                    case Variables.FLOAT:
                    case Variables.STRING:
                        //Was going to say "if you read a function header, set new scope... but you never read a function header without a call first"
                        if (line_parts[1].Contains(OPENING_PARENTHESIS)) {
                            
                        }
                        else {
                            base_scope.variable_handler.declareVariable (line);
                        }
                        break;
                    case Keywords.STATIC:
                        /* the Main function is the only static function allowed */
                        main_function_line = i;
                        break;
                }
            }
        }
    }
}