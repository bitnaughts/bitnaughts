using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObject {

    public string name;

    public List<ComponentObject> components;

    // public List<Point> attach_points;
    public List<Point> mount_points;
    public List<Point> occupied_points;

    public ShipObject (string name) {
        this.name = name;
        this.components = new List<ComponentObject> ();
        this.occupied_points = new List<Point> ();
        this.components.Add (new ComponentObject ("bridge", new Point (0, 0)));
        updatePoints ();
    }

    public bool isPlaceable (ComponentObject component) {
        return isPlaceable (component.id, component.position);
    }
    public bool isPlaceable (string type, Point position) {
        return isPlaceable (ComponentConstants.getComponentID (type), position);
    }
    public bool isPlaceable (short id, Point position) {
        return findOccupiedPoints (id, position).Count > 0; //If attaching at at least one point, cleared to place
    }
    public List<Point> findOccupiedPoints (short id, Point position) {
        Point[] component_mount_points = ComponentConstants.getComponentMountPoints (id);
        List<Point> potentially_occupied_points = new List<Point> ();
        for (int i = 0; i < occupied_points.Count; i++) {
            for (int j = 0; j < component_mount_points.Length; j++) {
                if (component_mount_points[j] + position == occupied_points[i]) {
                    return potentially_occupied_points; //Placing over existing connection not allowed, Count == 0, false
                }
            }
        }
        for (int i = 0; i < mount_points.Count; i++) {
            for (int j = 0; j < component_mount_points.Length; j++) {
                if (component_mount_points[j] + position == mount_points[i]) {
                    potentially_occupied_points.Add (component_mount_points[j] + position); //Count > 0, true
                }
            }
        }
        return potentially_occupied_points;
    }

    public void place (ComponentObject component, Point position) {
        // place(ComponentConstants.getComponentID (type), position, ship);\\
        component.position = position;
        components.Add (component);
        // findOccupiedPoints (id, position);
        updatePoints ();
    }

    public void remove (ComponentObject component) {
        components.Remove (component);

        updatePoints ();
        if (ComponentConstants.isStructural (component.id)) {
            for (int i = 0; i < components.Count; i++) {
        //     if (!isPlaceable (components[i].id, components[i].position)) {
        //         components.Remove (components[i]);
        //         i--;
        //         updatePoints ();
            }
        }
    }

    public void updatePoints () {
        /* Reset Lists */
        mount_points = new List<Point> ();
        occupied_points = new List<Point> ();

        for (int i = 0; i < components.Count; i++) {

            Point[] component_mount_points = ComponentConstants.getComponentMountPoints (components[i].id);
           
            for (int j = 0; j < component_mount_points.Length; j++) {
            
                Point potential_mount_point = components[i].position + component_mount_points[j];                
                /* Structural Components provide mounting points, unless two structural components overlap */
                if (ComponentConstants.isStructural (components[i].id)) {
                    bool is_unique = true;
                    for (int k = 0; k < mount_points.Count; k++) {
                        if (mount_points[k] == potential_mount_point) {
                            //occupied_points.Add (potential_mount_point);
                            is_unique = false;
                            break;
                        }
                    }
                    if (is_unique) {
                        mount_points.Add (potential_mount_point);
                    }
                /* Non-structural components only block placement of other components */
                } else {
                    occupied_points.Add (potential_mount_point);
                }
            }
        }
    }

    public override string ToString () {
        updatePoints ();
        string output = "Ship(" + name + "):\n";
        for (int i = 0; i < components.Count; i++) {
            output += "-> Component(" + components[i].id + ", " + components[i].position + "): " + isPlaceable (components[i]) + "\n";
        }
        return output;
    }
}