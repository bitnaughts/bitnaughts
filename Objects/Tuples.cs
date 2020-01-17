using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

#region Point-related Classes
/* Holds relevant parameters for visualizing and calculating UI and World spatiotemporal (TM JV) values */
#region TupleF Class
public class TupleF {
    public float x { get; set; } = 0f;
    public float y { get; set; } = 0f;
    public float z { get; set; } = 0f;

    public TupleF (dynamic json) {
        this.x = json.x;
        this.y = json.y;
        this.z = json.z;
    }
    public TupleF (float x, float y) {
        this.x = x;
        this.y = y;
        this.z = 0;
    }
    public TupleF (float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    // public static implicit operator TupleF (string serial) {
    //     string[] data = serial.Split (',');
    //     return new TupleF (
    //         json.x,
    //         json.y,
    //         json.z
    //     );
    // }
    public JObject ToJObject () {
        dynamic json = new JObject ();
        json.x = x;
        json.y = y;
        json.z = z;
        return json;
    }
    public override string ToString () {
        return ToJObject ().ToString ();
    }
}
#endregion
#region ColorF Class
public class ColorF : TupleF {
    public static readonly ColorF white;

    static ColorF () {
        white = new ColorF (0, 0, 0);
    }
    public ColorF (dynamic json) : base ((object) json) { }
    public ColorF (int r, int g, int b) : base (r, g, b) { }
}
#endregion
#region PointF Class
public class PointF : TupleF {
    public const float Rad2Deg = 57.2958f;

    public static readonly PointF zero;

    static PointF () {
        zero = new PointF (0, 0, 0);
    }
    public PointF (dynamic json) : base ((object) json) { }
    public PointF (float x, float y) : base (x, y) { }
    public PointF (float x, float y, float z) : base (x, y, z) { }

    public static PointF operator + (PointF left, PointF right) {
        return new PointF (
            left.x + right.x,
            left.y + right.y,
            left.z + right.z
        );
    }
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
    public static void Clamp (ref PointF input, SizeF upper_constraint) => Clamp (ref input, (TupleF) PointF.zero, (TupleF) upper_constraint);
    public static void Clamp (ref PointF input, TupleF lower_constraint, TupleF upper_constraint) {
        if (input.x < lower_constraint.x) input.x = lower_constraint.x;
        if (input.x > upper_constraint.x) input.x = upper_constraint.x;
        if (input.y < lower_constraint.y) input.y = lower_constraint.y;
        if (input.y > upper_constraint.y) input.y = upper_constraint.y;
    }
    public static implicit operator Vector3 (PointF point) {
        return new Vector3 (point.x, point.y, point.z);
    }
    public static implicit operator PointF (Vector3 point) {
        return new PointF (point.x, point.y, point.z);
    }
}
#endregion
#region SizeF Class
public class SizeF : TupleF {
    public SizeF (dynamic json) : base ((object) json) { }
    public SizeF (float x, float y) : base (x, y) { }
    public SizeF (float x, float y, float z) : base (x, y, z) { }
    public static SizeF operator + (SizeF left, float right) {
        return new SizeF (
            left.x + right,
            left.y + right,
            left.z + right
        );
    }
    public static SizeF operator - (SizeF left, float right) {
        return new SizeF (
            left.x - right,
            left.y - right,
            left.z - right
        );
    }
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