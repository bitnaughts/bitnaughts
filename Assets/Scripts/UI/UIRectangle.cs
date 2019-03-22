using UnityEngine;

public class UIRectangle {

    public float x_left, x_right, y_bottom, y_top;
    public UIRectangle (UIRectangle rectangle) {
        this.x_left = rectangle.x_left;
        this.x_right = rectangle.x_right;
        this.y_bottom = rectangle.y_bottom;
        this.y_top = rectangle.y_top;
    }
    public UIRectangle (float x_left, float x_right, float y_bottom, float y_top) {
        this.x_left = x_left;
        this.x_right = x_right;
        this.y_bottom = y_bottom;
        this.y_top = y_top;
    }

    public Vector2 getMidpoint () {
        return new Vector2 ((x_right - x_left) / 2f + x_left, (y_top - y_bottom) / 2f + y_bottom);
    }
    public Vector2 getSize () {
        return new Vector2 (x_right - x_left, y_top - y_bottom);
    }

    public static UIRectangle partition (ref UIRectangle position_in, string snap_direction, float size, float total_width, float total_height) {
        UIRectangle position_out = new UIRectangle (position_in);

        float x_total = position_in.x_right - position_in.x_left;
        float y_total = position_in.y_top - position_in.y_bottom;
        float new_line = 0;

        switch (snap_direction) {
            case UIStyle.SNAP_TOP:
                new_line = position_in.y_top - (size / total_height) * y_total;
                position_in.y_top = new_line;
                position_out.y_bottom = new_line;
                return position_out;
            case UIStyle.SNAP_BOTTOM:
                new_line = position_in.y_bottom + (size / total_height) * y_total;
                position_in.y_bottom = new_line;
                position_out.y_top = new_line;
                return position_out;
            case UIStyle.SNAP_LEFT:
                new_line = position_in.x_left + (size / total_width) * x_total;
                position_in.x_left = new_line;
                position_out.x_right = new_line;
                return position_out;
            case UIStyle.SNAP_RIGHT:
                new_line = position_in.x_right - (size / total_width) * x_total;
                position_in.x_right = new_line;
                position_out.x_left = new_line;
                return position_out;
            default:
                return null;
        }
    }
    public override string ToString () {
        string output = "RECT( x:" + x_left + " -> " + x_right + ", y: " + y_bottom + " -> " + y_top + "): Midpoint(" + getMidpoint () + "), Size(" + getSize () + ")";
        return output;
    }
}