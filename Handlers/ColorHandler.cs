using System;

public class ColorF {
    public int r, g, b;
    public ColorF(int r, int g, int b) {
        this.r = r;
        this.g = g;
        this.b = b;
    }
    public override string ToString() {
        return r + " " + g + " " + b;
    }
}