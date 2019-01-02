﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObject {

    public string name;

    public List<ComponentObject> components;

    public GameObject ship;

    // public List<Point> attach_points;
    public List<Point> mount_points;
    public List<Point> occupied_points;

    public ShipObject (string name, GameObject ship) {
        this.name = name;
        this.ship = ship;
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
        for (int i = 0; i < components.Count; i++) {
            if (components[i].disconnectComponent (component)) {
                remove (components[i]);
            }
        }
        component.connected_components = new List<ComponentObject> ();
        components.Remove (component);
        updatePoints ();

        //for structural
        // set all components to not-connected
        // recursicely iterate across componets, if connected, set to connected
        // end
        // for all not-connected, remove component
        // update points

        //for non structural, remove if not over any mount points

        // if (ComponentConstants.isStructural (component.id)) {
        //     for (int i = 0; i < components.Count; i++) {
        //         if (ComponentConstants.isStructural (components[i].id)) {
        //             if (!isPlaceable (components[i].id, components[i].position)) {
        //                 remove (components[i]);
        //                 i--;
        //             }
        //         }
        //     }

        // for (int i = 0; i < components.Count; i++) {
        //     if (!ComponentConstants.isStructural (components[i].id)) {
        //         if (!isPlaceable (components[i].id, components[i].position)) {
        //             remove (components[i]);
        //             i--;
        //         }
        //     }
        // }
        //}
    }

    public bool isConnected (ComponentObject component) {

        return false;
    }

    public void updatePoints () {
        /* Reset Lists */
        mount_points = new List<Point> ();
        occupied_points = new List<Point> ();

        for (int i = 0; i < components.Count; i++) {

            components[i].connected_components = new List<ComponentObject> ();
            Point[] component_mount_points = ComponentConstants.getComponentMountPoints (components[i].id);

            for (int j = 0; j < component_mount_points.Length; j++) {

                Point potential_mount_point = components[i].position + component_mount_points[j];
                /* Structural Components provide mounting points, unless two structural components overlap */
                //if (ComponentConstants.isStructural (components[i].id)) {
                bool is_unique = true;
                for (int k = 0; k < mount_points.Count; k++) {
                    if (mount_points[k] == potential_mount_point) {
                        /* Two structural components are connected! */
                        ComponentObject responsible_component = findResponsibleComponent (mount_points[k]);
                        components[i].addConnectedComponent (responsible_component);

                        occupied_points.Add (potential_mount_point);
                        is_unique = false;
                        break;
                    }
                }
                if (ComponentConstants.isStructural (components[i].id) && is_unique) {
                    mount_points.Add (potential_mount_point);
                }
                /* Non-structural components only block placement of other components */
                else occupied_points.Add (potential_mount_point);
            }
        }

        for (int i = 0; i < components.Count; i++) {
            if (components[i].id != 0 && components[i].connected_components.Count == 0) {
                if (ComponentConstants.isStructural (components[i].id)) remove (components[i]);
            }
        }
    }

    public ComponentObject findResponsibleComponent (Point point) {
        for (int i = 0; i < components.Count; i++) {
            Point[] component_mount_points = ComponentConstants.getComponentMountPoints (components[i].id);
            for (int j = 0; j < component_mount_points.Length; j++) {
                if (point == components[i].position + component_mount_points[j]) {
                    return components[i];
                }
            }
        }
        return null;
    }

    public override string ToString () {
        //updatePoints ();
        string output = "Ship(" + name + "):\n";
        for (int i = 0; i < components.Count; i++) {
            output += "-> Component(" + components[i].id + ", " + components[i].position + "): \n";
            for (int j = 0; j < components[i].connected_components.Count; j++) {
                output += "-> -> ConnectedTo(" + components[i].connected_components[j].position + "): \n";
            }
        }
        return output;
    }
}