using System;

public static class PointHandler {
    public const float Rad2Deg = 57.2958f;
    public static float GetDistance (PointF start, PointF end) {
        return (float) Math.Sqrt (
            Math.Pow ((end.x - start.x), 2) +
            Math.Pow ((end.y - start.y), 2)
        );
    }
    public static PointF GetMidpoint (PointF start, PointF end) {
        return new PointF (
            (start.x + end.x) / 2,
            (start.y + end.y) / 2
        );
    }
    public static float GetAngle (PointF start, PointF end) {
        return Rad2Deg * (float) Math.Atan (
            (start.y - end.y) / (start.x - end.x)
        );
    }
}
#region Point-related Classes
/* Holds relevant parameters for visualizing and calculating UI and World spatiotemporal (TM JV) values */
#region TupleF Class
public class TupleF {
    public float x { get; set; } = 0f;
    public float y { get; set; } = 0f;
    public float z { get; set; } = 0f;
    public TupleF (float x, float y) { this.x = x; this.y = y; this.z = 0; }
    public TupleF (float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    public override string ToString () {
        return x + " " + y + " " + z;
    }
}
#endregion
#region PointF Class
public class PointF : TupleF {
    public PointF (float x, float y) : base (x, y) { }
    public PointF (float x, float y, float z) : base (x, y, z) { }
    public static PointF operator + (PointF left, PointF right) {
        return new PointF (
            left.x + right.x,
            left.y + right.y,
            left.z + right.z
        );
    }
}
#endregion
#region SizeF Class
public class SizeF : TupleF {
    public SizeF (float x, float y) : base (x, y) { }
    public SizeF (float x, float y, float z) : base (x, y, z) { }
}
#endregion
#region OrbitF Class
public class OrbitF {
    public float radius { get; set; } = 0f;
    public float theta { get; set; } = 0f;
    public OrbitF (float radius, float theta) { this.radius = radius; this.theta = theta; }
    public override string ToString () {
        return radius + " " + theta;
    }
}
#endregion
#endregion