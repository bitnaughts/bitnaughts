using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class ConversionHandler {

    public static Vector2 ToVector2 (PointF point) {
        return new Vector2 (point.x, point.y);
    }
    public static Vector2 ToVector2 (OrbitF orbit) {
        return new Vector2 (
            orbit.radius * Mathf.Cos (orbit.theta),
            orbit.radius * Mathf.Sin (orbit.theta)
        );
    }
    public static PointF ToPointF (OrbitF orbit) {
        return new PointF (
            orbit.radius * Mathf.Cos (orbit.theta),
            orbit.radius * Mathf.Sin (orbit.theta)
        );
    }
    public static Vector3 ToVector3 (PointF point) {
        return new Vector3 (point.x, point.y, point.z);
    }
    public static Vector3 ToVector3 (SizeF size) {
        return new Vector3 (size.x, size.y, size.z);
    }
    public static Quaternion ToQuaternion (float z_angle) {
        return Quaternion.Euler (new Vector3 (0, 0, z_angle));
    }
    public static Quaternion ToQuaternionY (float y_angle) {
        return Quaternion.Euler (new Vector3 (0, y_angle, 0));
    }
    public static void Each<T> (this IEnumerable<T> ie, Action<T, int> action) {
        var i = 0;
        foreach (var e in ie) action (e, i++);
    }
}