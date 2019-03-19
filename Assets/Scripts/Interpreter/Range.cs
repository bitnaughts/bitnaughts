/* Wrapper for scope starts/ends */
public class Range {
    public int start;
    public int end;

    public Range (int start, int end) {
        this.start = start;
        this.end = end;
    }

    public static Range getScopeRange (string[] script, int line) {
        int bracket_count = 1;
        while (bracket_count > 0) {
            line++;
            if (script[line].Contains (Operators.OPENING_BRACKET)) {
                bracket_count++;
            } else if (script[line] == Operators.CLOSING_BRACKET) {
                bracket_count--;
            }
        }
        return line + 1;
    }
    public override string ToString () {
        return "Range(" + start + " -> " + end + ")\n";
    }
}