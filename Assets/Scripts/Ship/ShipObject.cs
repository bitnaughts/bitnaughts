using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObject {

    public string name;

    public List<ComponentObject> components;

    // public List<IntVector> attach_points;
    public List<IntVector> mount_points;

    public ShipObject (string name) {
        this.name = name;
        this.components = new List<ComponentObject> ();
        this.components.Add (new ComponentObject ("bridge", new IntVector (0, 0)));
        updatePoints (this);
    }

    public static bool isPlaceable (string type, IntVector position, ShipObject ship) {
        return isPlaceable (ComponentConstants.getComponentID (type), position, ship);
    }
    public static bool isPlaceable (short id, IntVector position, ShipObject ship) {
        IntVector[] component_mount_points = ComponentConstants.getComponentMountPoints(id);

        for (int i = 0; i < ship.mount_points.Count; i++) {
            for (int j = 0; j < component_mount_points.Length; j++) {
                if (component_mount_points[j] + position == ship.mount_points[i]) {
                    //attach point [j] to mount point [i]
                    return true;
                }
            }
        }
        return false;

        //bridges, struts, and girders give "mountable positions" for other objects

        //silos, depots, caches, bays, engines mount on positions from given "attachable positions", removing the "mountable position(s)" from use 

        //mountable position over top a mountable positions removes both positions from list, allows placement

        //attachable position over top a mountable position removes mountable position from list, allows placement
    }

    public static void updatePoints (ShipObject ship) {
        ship.mount_points = new List<IntVector> ();
        for (int i = 0; i < ship.components.Count; i++) {
            IntVector[] component_mount_points = ComponentConstants.getComponentMountPoints(ship.components[i].id);
            for (int j = 0; j < component_mount_points.Length; j++) {
                ship.mount_points.Add (ship.components[i].position + component_mount_points[j]);
            }
        }
    }

}