using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Evaluator {

    private static int left_int, right_int;
    private static float left_float, right_float;
    private static bool left_bool, right_bool;

    public static string simplifyFunction (string function, string parameter) {
        if (function.IndexOf ("Mathf") == 0) {
            return simplifyMathf (function, parameter);
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
    public static string simplify (string left, string arithmetic_operator, string right) {
        /* e.g. ["12", "*", "4"] ==> ["48"] */
        string left_type = getVariableType (left, true), right_type = getVariableType (right, false);
        if (left_type == right_type) {
            switch (left_type) {
                case Variables.BOOLEAN:
                    return simplifyBooleans (left_bool, arithmetic_operator, right_bool);
                case Variables.INTEGER:
                    return simplifyIntegers (left_int, arithmetic_operator, right_int);
                case Variables.FLOAT:
                    return simplifyFloats (left_float, arithmetic_operator, right_float);
                case Variables.STRING:
                    return simplifyString (left, arithmetic_operator, right);
            }
        }
        return "";
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
    public static string getVariableType (string input) {
        return getVariableType (input, true);
    }
    public static string getVariableType (string input, bool left) {

        int index = indexOfVariable (input);
        if (index != -1) {
            input = variables[index].value;
        }
        if (left) {
            if (bool.TryParse (input, out left_bool)) return Variables.BOOLEAN;
            if (int.TryParse (input, out left_int)) return Variables.INTEGER;
            if (float.TryParse (input, out left_float)) return Variables.FLOAT;

            //...
        } else {
            if (bool.TryParse (input, out right_bool)) return Variables.BOOLEAN;
            if (int.TryParse (input, out right_int)) return Variables.INTEGER;
            if (float.TryParse (input, out right_float)) return Variables.FLOAT;
            //...
        }
        return Variables.STRING;
    }

}