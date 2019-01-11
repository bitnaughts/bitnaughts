using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Evaluator {

    private static int int_result;
    private static float float_result;
    private static bool bool_result;

    public static string simplify (string left, string arithmetic_operator, string right) {
        /* e.g. ["12", "*", "4"] ==> ["48"] */
        string left_type = getType (left), right_type = getType (right);
        if (left_type == right_type) {
            switch (left_type) {
                case Variables.BOOLEAN:
                    return simplifyBooleans (bool.Parse (left), arithmetic_operator, bool.Parse (right));
                case Variables.INTEGER:
                    return simplifyIntegers (int.Parse (left), arithmetic_operator, int.Parse (right));
                case Variables.FLOAT:
                    return simplifyFloats (float.Parse (left), arithmetic_operator, float.Parse (right));
                case Variables.STRING:
                    return simplifyString (left, arithmetic_operator, right);
            }
        }
        if ((left_type == Variables.FLOAT && right_type == Variables.INTEGER) || (right_type == Variables.FLOAT && left_type == Variables.INTEGER)) {
            /* AUTO TYPE CASTING INTEGERS IN FLOAT CALCULATIONS, e.g. "12 / 1.0" == "12.0", not "12" */
            return simplifyFloats (float.Parse (left), arithmetic_operator, float.Parse (right));
        }
        /* CASTS NOT HANDLED, e.g. "true + 1.0", TREAT AS STRINGS */
        return simplifyString (left, arithmetic_operator, right);
    }
    public static string simplifyBooleans (bool left, string arithmetic_operator, bool right) {
        switch (arithmetic_operator) {
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            case Operators.AND:
                return (left && right).ToString ();
            case Operators.OR:
                return (left || right).ToString ();
            default:
                return "";
        }
    }
    public static string simplifyIntegers (int left, string arithmetic_operator, int right) {
        switch (arithmetic_operator) {
            case Operators.MODULUS:
                return (left % right).ToString ();
            case Operators.TIMES:
                return (left * right).ToString ();
            case Operators.DIVIDE:
                return (left / right).ToString ();
            case Operators.ADD:
                return (left + right).ToString ();
            case Operators.SUBTRACT:
                return (left - right).ToString ();
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            case Operators.GREATER_THAN:
                return (left > right).ToString ();
            case Operators.GREATER_THAN_EQUAL:
                return (left >= right).ToString ();
            case Operators.LESS_THAN:
                return (left < right).ToString ();
            case Operators.LESS_THAN_EQUAL:
                return (left <= right).ToString ();
            default:
                return "";
        }
    }
    public static string simplifyFloats (float left, string arithmetic_operator, float right) {
        switch (arithmetic_operator) {
            case Operators.MODULUS:
                return (left % right).ToString ();
            case Operators.TIMES:
                return (left * right).ToString ();
            case Operators.DIVIDE:
                return (left / right).ToString ();
            case Operators.ADD:
                return (left + right).ToString ();
            case Operators.SUBTRACT:
                return (left - right).ToString ();
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            case Operators.GREATER_THAN:
                return (left > right).ToString ();
            case Operators.GREATER_THAN_EQUAL:
                return (left >= right).ToString ();
            case Operators.LESS_THAN:
                return (left < right).ToString ();
            case Operators.LESS_THAN_EQUAL:
                return (left <= right).ToString ();
            default:
                return "";
        }
    }

    public static string simplifyString (string left, string arithmetic_operator, string right) {
        switch (arithmetic_operator) {
            case Operators.ADD:
                return left + right;
            case Operators.EQUAL_TO:
                return (left == right).ToString ();
            default:
                return "";
        }

    }
    public static string getType (string input) {
        if (bool.TryParse (input, out bool_result)) return Variables.BOOLEAN;
        if (int.TryParse (input, out int_result)) return Variables.INTEGER;
        if (float.TryParse (input, out float_result)) return Variables.FLOAT;
        return Variables.STRING;
    }

    public static string simplifyFunction (string function, string parameter) {
        if (function.IndexOf ("Mathf") == 0) {
            return simplifyMathf (function, parameter);
        }
        if (function.IndexOf ("Console") == 0) {
            return simplifyControlFunctions (function, parameter);
        }
        if (function.IndexOf ("this") == 0) {
            return simplifyControlFunctions (function, parameter);
        }
        //...
        return "";
    }
    
    public static string simplifyControlFunctions (string function, string parameter) {
        switch (function) {
            case "this.rotate":
                return Mathf.Abs (float.Parse (parameter)) + "";
                //...
        }
        return "";
    }
    public static string simplifyMathf (string function, string parameter) {
        switch (function) {
            case "Mathf.Abs":
                return Mathf.Abs (float.Parse (parameter)) + "";
                //...
        }
        return "";
    }
    public static string simplifyCondensedOperators (string variable_name, string arithmetic_operator) {
        switch (arithmetic_operator) {
            case Operators.EQUALS:
                return Operators.EMPTY;
            case Operators.ADDITION:
                return variable_name + " " + Operators.ADD + " " + Operators.OPENING_PARENTHESIS;
            case Operators.SUBTRACTION:
                return variable_name + " " + Operators.SUBTRACT + " " + Operators.OPENING_PARENTHESIS;
            case Operators.MULTIPLICATION:
                return variable_name + " " + Operators.TIMES + " " + Operators.OPENING_PARENTHESIS;
            case Operators.DIVISION:
                return variable_name + " " + Operators.DIVIDE + " " + Operators.OPENING_PARENTHESIS;
            case Operators.MODULO:
                return variable_name + " " + Operators.MODULUS + " " + Operators.OPENING_PARENTHESIS;
            default:
                return Operators.EMPTY;
        }

    }
}