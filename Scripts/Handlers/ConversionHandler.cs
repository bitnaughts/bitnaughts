using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class ConversionHandler {

    public static Vector2 ToVector2 (PointF point) {
        return new Vector2 (point.x, point.y);
    }
    public static Quaternion ToQuaternion (float z_angle) {
        return Quaternion.Euler (
            new Vector3 (
                0,
                0,
                z_angle
            )
        );
    }
    public static void Each<T> (this IEnumerable<T> ie, Action<T, int> action) {
        var i = 0;
        foreach (var e in ie) action (e, i++);
    }
}