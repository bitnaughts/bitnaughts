using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObject {

    public string name;

    public List<ComponentObject> components;

    public List<IntVector> attach_points;
    public List<IntVector> mount_points;

    public ShipObject (string name) {
        this.name = name;
        this.components = new List<ComponentObject> ();
        this.components.Add(new ComponentObject("bridge", new IntVector(0,0)));
        updatePoints(this);
    }

    public static bool isPlaceable (string type, IntVector position, ShipObject ship) {
        return isPlaceable (ComponentConstants.getComponentID (type), position, ship);
    }
    public static bool isPlaceable (short id, IntVector position, ShipObject ship) {
        IntVector[] component_attach_points = ComponentConstants.ATTACH_POINTS[id];
        IntVector[] component_mount_points = ComponentConstants.MOUNT_POINTS[id];

        for (int i = 0; i < ship.mount_points.Count; i++) {
            for (int j = 0; j < component_attach_points.Length; j++) {
                if (component_attach_points[j] + position == ship.mount_points[i]) {
                    //attach point [j] to mount point [i]
                    return true;
                }
            }
        }
        return false;
        //check if attach_point overlaps with existing mount_point(s)
        //check if 
        //IntVector shifted_points = component_attach_points[1] + position; 
    }

    public static void updatePoints(ShipObject ship) {
        ship.mount_points = new List<IntVector>();
        for (int i = 0; i < ship.components.Count; i++) {
            for (int j = 0; j < ComponentConstants.MOUNT_POINTS[ship.components[i].id].Length; j++) {
               ship.mount_points.Add(ship.components[i].position + ComponentConstants.MOUNT_POINTS[ship.components[i].id][j]);
            }
        }
    }

}