using System;
using System.Collections;
using System.Drawing;
using UnityEngine;


public static class ConversionHandler 
{
    
    public static Vector2 ToVector2(PointF point)
    {
        return new Vector2(point.X, point.Y);
    }

}