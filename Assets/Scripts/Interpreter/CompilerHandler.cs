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

    public List<string> handlers;
    public List<FunctionObject> functions;
    public ScopeNode base_scope;
    // public List<VariableObject> variables;

    public CompilerHandler (string[] lines) {
        handlers = new List<string>();
        functions = new List<FunctionObject>();
        base_scope = new ScopeNode(-1,1000, false);

        compile (lines);
    }

    public void compile (string[] lines) {
        int scope_depth = 0;

        for (int i = 0; i < lines.Length; i++) {
            string line = lines[i];
            string[] line_parts;

            /* Manage current depth of scope (relevant for public variables, function declarations) */
            if (line.Contains (Operators.OPENING_BRACKET)) scope_depth++;
            if (line.Contains (Operators.CLOSING_BRACKET)) scope_depth--;

            switch (scope_depth) {
                case 0:
                    /* Outside Class declaration, for imports */
                    line_parts = line.Split (' ');
                    switch (line_parts[0]) {
                        case Keywords.LIBRARY_IMPORT:
                            handlers.Add (line_parts[1]);
                            break;
                    }
                    break;
                case 1:
                    /* Inside Class declaration, but not inside any functions */
                    line_parts = line.Split (' ');
                    switch (line_parts[0]) {
                        case Variables.VOID:
                        case Variables.BOOLEAN:
                        case Variables.INTEGER:
                        case Variables.FLOAT:
                        case Variables.STRING:
                            if (line_parts[1].Contains (Operators.OPENING_PARENTHESIS)) {
                                /* Function declaration, e.g. "void sum (int x, int y) {" */
                                functions.Add(new FunctionObject(line_parts, i));
                            } else {
                                 /* Variable declaration, e.g. "int y;" */
                                base_scope.variable_handler.declareVariable (line);
                            }
                            break;
                        case Keywords.STATIC:
                            /* the Main function is the only static function allowed */
                            main_function_line = i;
                            break;
                    }
                    break;
            }

        }
    }
}