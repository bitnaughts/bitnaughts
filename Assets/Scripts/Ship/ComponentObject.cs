using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVector {
    public short x;
    public short y;
    public IntVector (short x, short y) {
        this.x = x;
        this.y = y;
    }
    public static IntVector operator + (IntVector left, IntVector right) {
        return new IntVector ((short) (left.x + right.x), (short) (left.y + right.y));
    }
    public static IntVector operator - (IntVector left, IntVector right) {
        return new IntVector ((short) (left.x - right.x), (short) (left.y - right.y));
    }
    public static bool operator == (IntVector left, IntVector right) {
        return left.x == right.x && left.y == right.y;
    }
    public static bool operator != (IntVector left, IntVector right) {
        return !(left.x == right.x && left.y == right.y);
    }
    public override string ToString () {
        return "IntVector(" + x + ", " + y + ")";
    }
}

public static class ComponentConstants {
    public const string BRIDGE_NAME = "bridge"; /* Command module of a ship */
    public static short BRIDGE_ID = 0;

    public const string STRUT_NAME = "strut"; /* Simple spanning structure */
    public static short STRUT_ID = 1;

    public const string GIRDER_NAME = "girder"; /* Large spanning structure */
    public static short GIRDER_ID = 2;

    public const string SILO_NAME = "silo"; /* Tubular component, great for lauching things */
    public static short SILO_ID = 3;

    public const string DEPOT_NAME = "depot"; /* Storage box */
    public static short DEPOT_ID = 4;

    public const string CACHE_NAME = "cache"; /* Large storage box, great for mounting things on */
    public static short CACHE_ID = 5;

    public const string BAY_NAME = "bay"; /* Extra large box, great for misc */
    public static short BAY_ID = 6;

    public const string ENGINE_NAME = "engine"; /* Main thrust-providing system (RCS being considered)*/
    public static short ENGINE_ID = 7;

    public static IntVector[][] MOUNT_POINTS = { /* Where the Object supports construction off of itself */
        /* BRIDGE */
        new IntVector[] { new IntVector (-1, -2), new IntVector (0, 2), new IntVector (0, -2), new IntVector (1, -2) },
        /* STRUT */
        new IntVector[] { new IntVector (0, -2), new IntVector (0, 2), new IntVector (0, 1), new IntVector (0, 0), new IntVector (0, -1) },
        /* GIRDER */
        new IntVector[] { new IntVector (0, -2), new IntVector (0, 2), new IntVector (-1, 2), new IntVector (-1, 1), new IntVector (-1, 0), new IntVector (-1, -1), new IntVector (-1, -2), new IntVector (0, 1), new IntVector (0, 0), new IntVector (0, -1), new IntVector (1, 2), new IntVector (1, 1), new IntVector (1, 0), new IntVector (1, -1), new IntVector (1, -2) },
        /* SILO */
        new IntVector[] { new IntVector (0, 0) },
        /* DEPOT */
        new IntVector[] { new IntVector (-1, 0), new IntVector (0, 0), new IntVector (1, 0) },
        /* CACHE */
        new IntVector[] { new IntVector (-1, -1), new IntVector (-1, 0), new IntVector (-1, 1), new IntVector (0, -1), new IntVector (0, 1), new IntVector (1, -1), new IntVector (1, 0), new IntVector (1, 1), new IntVector (0, 0) },
        /* BAY */
        new IntVector[] { new IntVector (-1, 2), new IntVector (-1, 1), new IntVector (-1, 0), new IntVector (-1, -1), new IntVector (-1, -2), new IntVector (0, -2), new IntVector (0, 2), new IntVector (0, 1), new IntVector (0, 0), new IntVector (0, -1), new IntVector (1, 2), new IntVector (1, 1), new IntVector (1, 0), new IntVector (1, -1), new IntVector (1, -2) },
        /* ENGINE */
        new IntVector[] { new IntVector (-1, 0), new IntVector (0, 0), new IntVector (1, 0), new IntVector (-1, 1), new IntVector (0, 1), new IntVector (1, 1) }
    };

    public static short getComponentID (string type) {
        switch (type) {
            case ComponentConstants.BRIDGE_NAME:
                return ComponentConstants.BRIDGE_ID;
            case ComponentConstants.STRUT_NAME:
                return ComponentConstants.STRUT_ID;
            case ComponentConstants.GIRDER_NAME:
                return ComponentConstants.GIRDER_ID;
            case ComponentConstants.SILO_NAME:
                return ComponentConstants.SILO_ID;
            case ComponentConstants.DEPOT_NAME:
                return ComponentConstants.DEPOT_ID;
            case ComponentConstants.CACHE_NAME:
                return ComponentConstants.CACHE_ID;
            case ComponentConstants.BAY_NAME:
                return ComponentConstants.BAY_ID;
            case ComponentConstants.ENGINE_NAME:
                return ComponentConstants.ENGINE_ID;
            default:
                return -1;
        }
    }

    public static IntVector[] getComponentMountPoints (string type) {
        return getComponentMountPoints (getComponentID (type));
    }
    public static IntVector[] getComponentMountPoints (short type) {
        return MOUNT_POINTS[type];
    }

    public static bool isStructural (string type) {
        return isStructural (getComponentID (type));
    }
    public static bool isStructural (short type) {
        return type <= 2; //bridge, strut, girder are structural, all other components are placed on top of these
    }
}

public class ComponentObject {
    public string type;
    public short id;

    public IntVector position;

    public ComponentObject (string type) {
        this.type = type;
        this.position = new IntVector (-10, -10);
        
        id = ComponentConstants.getComponentID (type);
    }
    public ComponentObject (string type, IntVector position) {
        this.type = type;
        this.position = position;

        id = ComponentConstants.getComponentID (type);
    }

}