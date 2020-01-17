using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ship {

    public int id;
    public int player_id;

    public PointF center;
    public int rotation;

    public GameObject obj;

    public List<Module> modules;

    public string debug;

    /*
        Cap types: (all caps can add gimbals for rotational control)
            Cap (nothing)
            Engines (sudden 4x boost and incremental up to 8x boost, controllable activation programmatically, wrapper libraries to help but not needed)
            Docks (mates to other docks of equal size, controllable detactment programmatically, wrapper libraries to help but not needed)
            "Mainframe" bridge module - Computers (redundant back-ups of ship, ship always alive if it has > 1 computer, only one computer runs at a time?)
            Guns (super customizable from gimbal to barrel size and number of barrel)
            Sensors (non-damage abilities, seeing through fog, populating content on computers for informed calculations)
     
        Structures:
            Printer (A 3D-printer for ship parts, able to make and flash ships)
            Gravitational Unit (to be named... a rotator module to spin rings to produced desired gravitational levels, useful for populated ships)
            Bay
            Depot ()
            Cache ()

        Rotating sections would have to be constructed seperately and mated together
      */

    public Ship (dynamic json) {

        modules = new List<Module> ();
        debug = "";

        foreach (var module_json in json.modules) {
            debug += module_json.type.ToString () + "\n";
            switch (module_json.type.ToString ()) {
                case "Structure":
                    modules.Add (new Structure (module_json));
                    break;
                case "Mainframe":
                    modules.Add (new Mainframe (module_json));
                    break;
                case "Sensor":
                    modules.Add (new Sensor (module_json));
                    break;
                case "Cannon":
                    modules.Add (new Cannon (module_json));
                    break;
                case "Gimbal":
                    modules.Add (new Gimbal (module_json));
                    break;
                case "Printer":
                    modules.Add (new Printer (module_json));
                    break;
            }
        }
    }

    public Ship (List<Module> modules) {
        this.id = Galaxy.ShipID++;
        this.modules = modules;

        this.center = new PointF (1, 2);
        this.rotation = 0;
    }

    public override string ToString () {
        dynamic json = new JObject ();
        json.id = id;
        json.player_id = player_id;
        json.debug = debug;
        json.modules = new JArray (modules.Select (x => x.ToJObject ()));

        return json.ToString ();
        // return JSONHandler.ToJSON (
        //     ToDictionary ()
        // );
    }
}

public class ShipController : Controller<Ship> {

    PrefabController prefab_controller;

    public override void Initialize (Ship ship) {
        this.obj = ship;
        Initialize (this.obj.modules, this.transform);
    }
    public void Initialize (List<Module> modules, Transform parent) {
        foreach (var module in modules) {
            module.obj = Referencer.prefab_controller.Add (module, parent);
            switch (module) {
                case Sensor s:
                    module.obj.AddComponent<SensorController> ().Initialize (s);
                    break;
                case Cannon c:
                    module.obj.AddComponent<CannonController> ().Initialize (c);
                    break;
                case Printer p:
                    module.obj.AddComponent<PrinterController> ().Initialize (p);
                    break;
                case Gimbal g:
                    module.obj.AddComponent<GimbalController> ().Initialize (g);
                    Initialize (g.modules, module.obj.transform.GetChild (0));
                    break;
                default:
                    break;
            }
        }
    }

    public override void Visualize () {

    }

    bool Fetch () {
        // if (Controller<Ship>.fetch_obj != null && Controller<Ship>.fetch_obj.IsCompleted) {
        //     Controller<Ship>.obj = new Ship (fetch_obj.Result);
        //     Visualize ();
        //     return true;
        // }
        return false;
    }

    // List<GameObject> markers = new List<GameObject> ();
    // public GameObject markerPrefab;

    void Start () {

        // Note to future self: gimbals and printers are only allowed on first level of ship (in terms of gimbals)
        // all other modules allowed on any layer (0 or 1 currently)

        Visualize ();
    }

    void Update () {

        // // Camera.main.ScreenToWorldPoint (Input.mousePosition);
        // if (Input.GetMouseButtonUp (0)) {
        // 	if (clickedOverComponent == true) {
        // 		this.transform.parent = ShipManager.getSelectedShip ().ship.transform;
        // 		//for all ShipManager.getSelectedShip()s
        // 		if (ShipManager.getSelectedShip ().isPlaceable (type, new Point ((short) this.transform.localPosition.x, (short) this.transform.localPosition.y))) {
        // 			ShipManager.getSelectedShip ().place (component, new Point ((short) this.transform.localPosition.x, (short) this.transform.localPosition.y));
        // 			ship = ShipManager.getSelectedShip ();
        // 		//	gameObject.AddComponent<BoxCollider2D>();
        // 		} else {
        // 			// gameObject.AddComponent<SphereCollider>();
        // 			this.transform.parent = ShipManager.holder.transform;
        // 	//		gameObject.AddComponent<BoxCollider2D>();
        // 		}
        // 	}
        // 	clickedOverComponent = false;
        // }
        // if (clickedOverComponent && Input.GetMouseButton (0)) {
        // 	Destroy(this.GetComponent<Rigidbody2D>());
        // 	//Destroy(this.GetComponent<BoxCollider2D>());
        // 	this.transform.parent = ShipManager.getSelectedShip ().ship.transform;
        // 	this.transform.rotation = ShipManager.getSelectedShip ().ship.transform.rotation;

        // 	Vector2 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        // 	this.transform.position = position;

        // 	position = this.transform.localPosition; //addVectors (position, ShipManager.getSelectedShip ().ship.transform.position);

        // 	if (ShipManager.getSelectedShip ().isPlaceable (type, new Point ((short) position.x, (short) position.y))) {
        // 		this.transform.localPosition = new Vector2 ((int) position.x, (int) position.y);
        // 	}
        // } else {
        // 	if (!component.isConnected ()) {
        // 		this.transform.Translate (drift);
        // 	}
        // }

        // if (type == "bridge" || component.isConnected ()) {
        // 	//this.GetComponent<SpriteRenderer> ().color = new Color (0, 255, 0);
        // 	if (ship != null) {
        // 		this.transform.parent = ship.ship.transform;
        // 		this.transform.rotation = ship.ship.transform.rotation;
        // 		this.transform.localPosition = new Vector2 (component.position.x, component.position.y);

        // 		//this.GetComponent<Rigidbody2D> ().simulated = false;
        // 	}
        // 	if (type == "silo") {
        // 		if (Input.GetKey(KeyCode.E)) {
        // 			Instantiate(Resources.Load("missile") as GameObject, this.transform.position,  this.transform.rotation);
        // 		}
        // 	}
        // 	if (type == "engine") {
        // 		this.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        //         ParticleSystem.EmissionModule emitter = this.transform.GetChild(0).GetComponent<ParticleSystem>().emission;
        // 		emitter.rateOverTime = Mathf.Abs(Input.GetAxis("Vertical") * 300f); 
        // 		//this.transform.GetChild(0).GetComponent<ParticleSystem>().emission = emitter;
        // 	}

        // } else {
        // 	if (type == "engine") {
        // 		// this.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        // 	}
        // 	//this.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
        // 	this.transform.parent = ShipManager.holder.transform;
        // 	if (this.GetComponent<Rigidbody2D>() == null) 
        // 	{
        // 		gameObject.AddComponent<Rigidbody2D>();
        // 	}

        // }
    }

    void OnMouseEnter () {
        // short id = ComponentConstants.getComponentID (type);
        // Point[] component_mount_points = ComponentConstants.getComponentMountPoints (id);

        // for (int i = 0; i < component_mount_points.Length; i++) {
        // 	GameObject marker_instance = Instantiate (markerPrefab, new Vector2 (0, 0), Quaternion.identity) as GameObject;
        // 	marker_instance.transform.parent = this.transform;
        // 	marker_instance.transform.localPosition = new Vector3 (component_mount_points[i].x, component_mount_points[i].y, -5);
        // 	markers.Add (marker_instance);
        // }
    }

    void OnMouseOver () {
        // if (Input.GetMouseButtonDown (0)) {
        // 	clickedOverComponent = true;
        // 	if (ship != null) {
        // 		ship.remove (component);
        // 		ship = null;
        // 	}
        // }

    }

    void OnMouseExit () {
        // for (int i = 0; i < markers.Count; i++) {
        // 	Destroy (markers[i]);
        // }
    }

}