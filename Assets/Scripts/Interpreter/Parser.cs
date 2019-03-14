using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

public class Parser {

     public static string parse (string input) {
        if (input != Operators.EMPTY) {
            List<string> parts = input.Split (' ').ToList<string> ();
            /* EVALUATE PARENTHESIS AND FUNCTIONS RECURSIVELY, e.g. "12 + function(2) * 4" ==> "12 + 4 * 4" */

            //this section is still untested and requires thorough testing
            //for edge cases with parenthesis, e.g. "Mathf.Abs((2 + 1) * 2)"
            //
            //I think this logic isn't the best suited for parenthesis
            //Fix:
            //find deepest set of parenthesis, combine spaces to parameters[], parse each parameter in parameters (for ","s in functions)
            //in the end, "2 * (2 + 2)" = "2 * 4"
            //or with functions, "2 * Mathf.Abs(2 + 2)" = "2 * Mathf.Abs(4)"...
            //If function preceeds ()s, keep parenthesis, else return parse(parameter)

            for (int part = 0; part < parts.Count; part++) {
                if (parts[part].Contains (Operators.OPENING_PARENTHESIS)) {
                    string parts_to_be_condensed = parts[part];
                    while (parts[part].Contains (Operators.CLOSING_PARENTHESIS) == false) {
                        part++;
                        parts_to_be_condensed += " " + parts[part];
                    }
                    if (parts[part].IndexOf (Operators.OPENING_PARENTHESIS) == 0) {
                        parts[part] = parse (parts_to_be_condensed);
                    } else {
                        //to support user-made functions, or functions that require interpreted lines of code to be executed first, will require logic here to allow for putting this parse in a stack to be popped on the "return" of said function
                        string function = parts[part].Substring (0, parts[part].IndexOf (Operators.OPENING_PARENTHESIS));
                        /* e.g. Mathf.Abs(-10) == 10 */
                        // parts[part] = Evaluator.simplifyFunction (function, parse (parts_to_be_condensed));
                    }
                }
            }

            /* PEMDAS REST OF OPERATIONS, e.g. ["12", "+", 4, "*", "4"] ==> ["12", "+", "16"] ==> ["28"] */
            if (parts.Count > 1) {
                for (int operation_set = 0; operation_set < Operators.PEMDAS.Length; operation_set++) {
                    for (int part = 1; part < parts.Count - 1; part++) {
                        for (int operation = 0; operation < Operators.PEMDAS[operation_set].Length; operation++) {
                            string operation_type = parts[part];
                            if (operation_type == Operators.PEMDAS[operation_set][operation]) {
                                string left = getValue (parts[part - 1]), right = getValue (parts[part + 1]);
                                parts[part - 1] = Evaluator.simplify (left, operation_type, right);
                                parts.RemoveRange (part, 2);
                                part--;
                            }
                        }
                    }
                }
            }
            /* RETURN FULLY SIMPLIFIED VALUE, e.g. "28" */
            return parts[0];
        }
        return Operators.EMPTY;
    }
      public static string scrubSymbols (string input) {
        string output = input;
        if (input.Contains (Operators.END_LINE)) output = input.Remove (input.IndexOf (Operators.END_LINE), 1);
        return output;
    }

      public static string[] splitIncrement (string input) {
        string variable_name, variable_operation;
        input = input.Split (Operators.END_LINE_CHAR) [0]; //remove ; if there

        variable_name = input.Substring (0, input.Length - 2);
        variable_operation = input.Substring (input.Length - 2);

        switch (variable_operation) {
            case Operators.INCREMENT:
                return new string[] { variable_name, Operators.EQUALS, variable_name, Operators.ADD, "1" };
            case Operators.DECREMENT:
                return new string[] { variable_name, Operators.EQUALS, variable_name, Operators.SUBTRACT, "1" };

        }
        return new string[] { };
    }
}