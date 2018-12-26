using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVector
{
    public short x;
    public short y;
    public IntVector(short x, short y)
    {
        this.x = x;
        this.y = y;
    }
}

public static class ComponentConstants
{
    static string BRIDGE_NAME = "bridge";   /* Command module of a ship */  
    static short BRIDGE_ID = 0; 

    static string STRUT_NAME = "strut";     /* Simple spanning structure */
    static short STRUT_ID = 1;

    static string GIRDER_NAME = "girder";   /* Large spanning structure */
    static short GIRDER_ID = 2;

    static string SILO_NAME = "silo";       /* Tubular component, great for lauching things */
    static short SILO_ID = 3;

    static string DEPOT_NAME = "depot";     /* Storage box */ 
    static short DEPOT_ID = 4;

    static string CACHE_NAME = "cache";     /* Large storage box, great for mounting things on */
    static short CACHE_ID = 5;

    static string BAY_NAME = "bay";         /* Extra large box, great for misc */
    static short BAY_ID = 6;

    static string ENGINE_NAME = "engine";   /* Main thrust-providing system (RCS being considered)*/
    static short ENGINE_ID = 7;

    static IntVector[] SIZES = {
        new IntVector(2, 2)

    };

    static IntVector[][] ATTACH_POINTS = {
        /* BRIDGE */ new IntVector[] { },
        /* STRUT */ new IntVector[] { new IntVector(0,-2), new IntVector(0, 2) },
        /* GIRDER */ new IntVector[] { new IntVector(0,-2), new IntVector(0, 2) },
        /* SILO */ new IntVector[] { new IntVector(0,0) },
        /* DEPOT */ new IntVector[] { new IntVector(-1,0), new IntVector(0, 0), new IntVector(1, 0) }
    };
    static IntVector[][] MOUNT_POINTS = {
        new IntVector[] { new IntVector(-1, -2), new IntVector(0, 2), new IntVector(0, -2), new IntVector(1, -2) },
        new IntVector[] { new IntVector(0, 1), new IntVector(0, 0), new IntVector(0, -1) },
        new IntVector[] { new IntVector(-1,2), new IntVector(-1, 1), new IntVector(-1, 0), new IntVector(-1, -1), new IntVector(-1, -2), new IntVector(0, 1), new IntVector(0, 0), new IntVector(0, -1), new IntVector(1, 2), new IntVector(1, 1), new IntVector(1, 0), new IntVector(1, -1), new IntVector(1, -2) },
        new IntVector[] { },
        new IntVector[] { },
    };
}

public class ComponentObject
{
    string type; 
    short id;



    public ComponentObject(string type)
    {
        this.type = type;
    }

}
