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
        updatePoints ();
    }

    public bool isPlaceable (string type, IntVector position) {
        return isPlaceable (ComponentConstants.getComponentID (type), position);
    }
    public bool isPlaceable (short id, IntVector position) {
        IntVector[] component_mount_points = ComponentConstants.getComponentMountPoints (id);

        for (int i = 0; i < mount_points.Count; i++) {
            for (int j = 0; j < component_mount_points.Length; j++) {
                if (component_mount_points[j] + position == mount_points[i]) {
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
    public void place (string type, IntVector position) {
        // place(ComponentConstants.getComponentID (type), position, ship);\\
        components.Add (new ComponentObject (type, position));
        updatePoints ();
    }

    // public static void place(short id, IntVector position, ShipObject ship) {
    //     ship.components.Add(new ComponentObject(id, position)); 
    //     updatePoints(ship);
    // }

    public void updatePoints () {
        mount_points = new List<IntVector> ();
        for (int i = 0; i < components.Count; i++) {
            IntVector[] component_mount_points = ComponentConstants.getComponentMountPoints (components[i].id);
            if (ComponentConstants.isStructural (components[i].id)) {
                for (int j = 0; j < component_mount_points.Length; j++) {
                    IntVector potential_mount_point = components[i].position + component_mount_points[j];
                    bool isUnique = true;
                    for (int k = 0; k < mount_points.Count; k++) {
                        if (mount_points[k] == potential_mount_point) {
                            mount_points.RemoveAt(k--);
                            isUnique = false;
                        }
                    }
                    if (isUnique) {
                        mount_points.Add (potential_mount_point);
                    }
                }
            } else {
                for (int j = 0; j < component_mount_points.Length; j++) {
                    IntVector potential_mount_point = components[i].position + component_mount_points[j];
                    //remove all mount points being occupied
                    for (int k = 0; k < mount_points.Count; k++) {
                        if (mount_points[k] == potential_mount_point) {
                            mount_points.RemoveAt(k--);
                        }
                    }
                }
            }

        }
    }

}