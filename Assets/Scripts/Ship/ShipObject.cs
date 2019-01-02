using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObject {

    public string name;

    public List<ComponentObject> components;

    public GameObject ship;

    public int thrust;
    public int weight;

    // public List<Point> attach_points;
    public List<Point> mount_points;
    public List<Point> occupied_points;

    public ShipObject (string name, GameObject ship) {
        this.name = name;
        this.ship = ship;
        this.components = new List<ComponentObject> ();
        this.occupied_points = new List<Point> ();
        this.components.Add (new ComponentObject ("bridge", new Point (0, 0)));
        update ();
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
        component.position = position;
        components.Add (component);
        update ();
    }

    public void remove (ComponentObject component) {
        for (int i = 0; i < components.Count; i++) {
            if (components[i].disconnectComponent (component)) {
                remove (components[i]);
            }
        }
        component.connected_components = new List<ComponentObject> ();
        components.Remove (component);
        update ();
    }

    public bool isConnected (ComponentObject component) {

        return false;
    }

    public void update() {
        updatePoints();
        updateStats();
    }

    public void updateStats() {
        weight = 0;
        thrust = 0;
        for (int i = 0; i < components.Count; i++) {
            weight += ComponentConstants.getWeight(components[i].id);
            if (components[i].type == "engine") thrust += 20;
        }
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
                remove (components[i]);
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