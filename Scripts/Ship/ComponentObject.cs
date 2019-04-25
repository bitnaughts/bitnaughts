using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point {
    public short x;
    public short y;

    public Point (short x, short y) {
        this.x = x;
        this.y = y;
    }
    public static Point operator + (Point left, Point right) {
        return new Point ((short) (left.x + right.x), (short) (left.y + right.y));
    }

    public static Point operator - (Point left, Point right) {
        return new Point ((short) (left.x - right.x), (short) (left.y - right.y));
    }
    public static bool operator == (Point left, Point right) {
        return left.x == right.x && left.y == right.y;
    }
    public static bool operator != (Point left, Point right) {
        return !(left.x == right.x && left.y == right.y);
    }
    public override string ToString () {
        return "Point(" + x + ", " + y + ")";
    }
}

public static class ComponentConstants {
    /* Command module of a ship */
    public const string BRIDGE_NAME = "bridge";
    public static short BRIDGE_ID = 0;
    /* Simple spanning structure */
    public const string STRUT_NAME = "strut";
    public static short STRUT_ID = 1;
    /* Large spanning structure */
    public const string GIRDER_NAME = "girder";
    public static short GIRDER_ID = 2;
    /* Tubular component, great for lauching things */
    public const string SILO_NAME = "silo";
    public static short SILO_ID = 3;
    /* Storage box */
    public const string DEPOT_NAME = "depot";
    public static short DEPOT_ID = 4;
    /* Large storage box, great for mounting things on */
    public const string CACHE_NAME = "cache";
    public static short CACHE_ID = 5;
    /* Extra large box, great for misc */
    public const string BAY_NAME = "bay";
    public static short BAY_ID = 6;
    /* Main thrust-providing system (RCS being considered)*/
    public const string ENGINE_NAME = "engine";
    public static short ENGINE_ID = 7;
    /* Where the Object supports construction off of itself */
    public static Point[][] MOUNT_POINTS = {
        /* BRIDGE */
        new Point[] { new Point (-1, -2), new Point (0, 2), new Point (0, -2), new Point (1, -2) },
        /* STRUT */
        new Point[] { new Point (0, -2), new Point (0, 2), new Point (0, 1), new Point (0, 0), new Point (0, -1) },
        /* GIRDER */
        new Point[] { new Point (0, -2), new Point (0, 2), new Point (-1, 2), new Point (-1, 1), new Point (-1, 0), new Point (-1, -1), new Point (-1, -2), new Point (0, 1), new Point (0, 0), new Point (0, -1), new Point (1, 2), new Point (1, 1), new Point (1, 0), new Point (1, -1), new Point (1, -2) },
        /* SILO */
        new Point[] { new Point (0, 0) },
        /* DEPOT */
        new Point[] { new Point (-1, 0), new Point (0, 0), new Point (1, 0) },
        /* CACHE */
        new Point[] { new Point (-1, -1), new Point (-1, 0), new Point (-1, 1), new Point (0, -1), new Point (0, 1), new Point (1, -1), new Point (1, 0), new Point (1, 1), new Point (0, 0) },
        /* BAY */
        new Point[] { new Point (-1, 2), new Point (-1, 1), new Point (-1, 0), new Point (-1, -1), new Point (-1, -2), new Point (0, -2), new Point (0, 2), new Point (0, 1), new Point (0, 0), new Point (0, -1), new Point (1, 2), new Point (1, 1), new Point (1, 0), new Point (1, -1), new Point (1, -2) },
        /* ENGINE */
        new Point[] { new Point (-1, 0), new Point (0, 0), new Point (1, 0), new Point (-1, 1), new Point (0, 1), new Point (1, 1) }
    };

     public static short[] WEIGHT = {
        /* BRIDGE */
        5,
        /* STRUT */
        1,
        /* GIRDER */
        3,
        /* SILO */
        1,
        /* DEPOT */
        2,
        /* CACHE */
        5,
        /* BAY */
        7,
        /* ENGINE */
        5
    };

    /* String to ID */
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
    /* Get Mount Points for Component */
    public static Point[] getComponentMountPoints (string type) {
        return getComponentMountPoints (getComponentID (type));
    }
    public static Point[] getComponentMountPoints (short type) {
        return MOUNT_POINTS[type];
    }

    public static short getWeight (string type) {
        return getWeight (getComponentID (type));
    }
    public static short getWeight (short type) {
        return WEIGHT[type];
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

    public ComponentEditor obj;

    public Point position;

    //If connected_at.Count == 0, disconnected from a ship

    public List<ComponentObject> connected_components;

    public ComponentObject () { }
    public ComponentObject (string type, ComponentEditor obj) {
        this.type = type;
        this.id = ComponentConstants.getComponentID (type);
        this.obj = obj;
        connected_components = new List<ComponentObject> ();
    }
    public ComponentObject (string type, Point position) {
        this.type = type;
        this.position = position;

        this.id = ComponentConstants.getComponentID (type);
        connected_components = new List<ComponentObject> ();

        // connected_components = new List<ComponentObject>();
        // for (int i = 0; i < connectors.Length; i++) {
        //     connected_components.Add(connectors[i]);
        // }
    }

    public void addConnectedComponent (ComponentObject component) {
        if (connected_components.Contains (component) == false) {
            connected_components.Add (component);
        }
    }

    public bool disconnectComponent (ComponentObject component) {
        if (connected_components.Contains (component)) {
            connected_components.Remove (component);
            return connected_components.Count == 0; //if no other connected components, disconnect is TRUE
        }
        return false;
    }
    public bool isConnected () {
        return connected_components.Count > 0;
    }
}

/* Could child class structural components to clean logic, but isn't too messy at the moment */
// public class StructuralObject : ComponentObject
// {
//     List<ComponentObject> connected_components;

//     public StructuralObject() { }
//     public StructuralObject(string type, Point position)
//     {
//         this.type = type;
//         this.position = position;

//         id = ComponentConstants.getComponentID(type);
//     }
// }