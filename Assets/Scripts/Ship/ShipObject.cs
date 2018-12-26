using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObject {

    string name;

    IntVector[] attach_points;
    IntVector[] mount_points;

    public ShipObject (string name) {
        this.name = name;
    }

    public static bool isPlaceable (string type, IntVector position, ShipObject ship) {
        return isPlaceable(ComponentConstants.getComponentID (type), position, ship);
    }
    public static bool isPlaceable (short id, IntVector position, ShipObject ship) {
        IntVector[] component_attach_points = ComponentConstants.ATTACH_POINTS[id];
        IntVector[] component_mount_points = ComponentConstants.MOUNT_POINTS[id];
        
        

        //IntVector shifted_points = component_attach_points[1] + position; 
    }

}