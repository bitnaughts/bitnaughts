public static class Operators {
    public const string EQUALS = "=",
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
        SUBTRACT = "-";

    public static readonly string[][] PEMDAS = {
        new string[] { "%" },
        new string[] { "*", "/" },
        new string[] { "+", "-" },
        new string[] { "==", "!=", ">", ">=", "<", "<=" },
        new string[] { "&&", "||" }
    };
    
}