﻿public static class Operators {
    public const string EMPTY = "",
        DOT = ".",
        EQUALS = "=",
        INCREMENT = "++",
        DECREMENT = "--",
        ADDITION = "+=",
        SUBTRACTION = "-=",
        MULTIPLICATION = "*=",
        DIVISION = "/=",
        MODULO = "%=",
        EQUAL_TO = "==",
        NOT_EQUAL = "!=",
        GREATER_THAN = ">",
        GREATER_THAN_EQUAL = ">=",
        LESS_THAN = "<",
        LESS_THAN_EQUAL = "<=",
        AND = "&&",
        OR = "||",
        MODULUS = "%",
        TIMES = "*",
        DIVIDE = "/",
        ADD = "+",
        SUBTRACT = "-",
        OPENING_BRACKET = "{",
        CLOSING_BRACKET = "}",
        OPENING_PARENTHESIS = "(",
        CLOSING_PARENTHESIS = ")",
        END_LINE = ";";

        //public const string[] ALL_OPERATORS = { EQUALS, INCREMENT, DECREMENT, ADDITION, SUBTRACTION, MULTIPLICATION, DIVISION, MODULO, EQUAL_TO, NOT_EQUAL, GREATER_THAN, GREATER_THAN_EQUAL, LESS_THAN, LESS_THAN_EQUAL, AND, OR, MODULUS, TIMES, DIVIDE, ADD, SUBTRACT };

    public const char END_LINE_CHAR = ';';

    public static readonly string[][] PEMDAS = {
        new string[] { "%" },
        new string[] { "*", "/" },
        new string[] { "+", "-" },
        new string[] { "==", "!=", ">", ">=", "<", "<=" },
        new string[] { "&&" },
        new string[] { "||" }
    };

}