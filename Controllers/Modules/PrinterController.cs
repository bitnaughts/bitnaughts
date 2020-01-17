using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public class Printer : Module {

    public PointF cursor;

    public Printer (dynamic json) : base ((object) json) { }
    public Printer (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.cursor = new PointF (0, 0);
        this.prefab_path = "Prefabs/Modules/Hardpoint";
    }
    public Printer (PointF center, SizeF size, int rotation, string prefab_path) : base (center, size, rotation) {
        this.cursor = new PointF (0, 0);
        this.prefab_path = prefab_path;
    }

    public bool CanMoveToPoint (PointF point) {
        return true;
    }
    public void MoveTo (PointF point) {
        cursor = point;
    }
}

public class PrinterController : ModuleController<Printer> {

    TupleF axes_offsets;
    float counter;
    public override void Visualize () {
        // switch (obj.prefab_path) {
        //     case "Prefabs/Modules/Hardpoint":
        counter += .03f;
        //         break;
        //     case "Prefabs/Modules/Cache":
        //         counter -= .03f;
        //         break;
        // }

        obj.cursor.x = Mathf.Sin (counter) * 3 / 3;
        obj.cursor.y = Mathf.Cos (counter) * 3 / 3;

        PointF.Clamp (ref obj.cursor, obj.size);

        parts["cursor"].transform.localPosition = obj.cursor;
        parts["y_axis"].transform.localPosition = new PointF (0, obj.cursor.y);
        parts["x_axis"].transform.localPosition = new PointF (obj.cursor.x, 0);
    }
    public override void Initialize (Printer obj) {
        this.obj = obj;
        switch (obj.prefab_path) {
            case "Prefabs/Modules/Hardpoint":
                axes_offsets = new TupleF (.52f, .60f);
                break;
            case "Prefabs/Modules/Cache":
                axes_offsets = new TupleF (.72f, .72f);
                break;
        }
        parts = new Dictionary<string, GameObject> { { "cursor", null },
            { "x_axis", null },
            { "y_axis", null }
        };

        parts["cursor"] = Referencer.prefab_controller.Add (
            "Prefabs/Modules/PrinterHead",
            obj.cursor,
            new SizeF (2, 2),
            0,
            this.transform
        );

        parts["y_axis"] = Referencer.prefab_controller.Add (
            "Prefabs/Modules/Axis",
            new PointF (0, 0),
            new SizeF (1, obj.size.x),
            90,
            this.transform
        );
        parts["y_axis"].transform.GetChild (0).localPosition = new PointF (0, obj.size.x / 2 - axes_offsets.y);
        parts["y_axis"].transform.GetChild (1).localPosition = new PointF (0, -obj.size.x / 2 + axes_offsets.y);

        parts["x_axis"] = Referencer.prefab_controller.Add (
            "Prefabs/Modules/Axis",
            new PointF (0, 0),
            new SizeF (1, obj.size.y),
            0,
            this.transform
        );
        parts["x_axis"].transform.GetChild (0).localPosition = new PointF (0, obj.size.y / 2 - axes_offsets.x);
        parts["x_axis"].transform.GetChild (1).localPosition = new PointF (0, -obj.size.y / 2 + axes_offsets.x);

        parts["overlay"] = Referencer.prefab_controller.Add (
            "Prefabs/Modules/Overlay",
            new PointF (0, 0),
            obj.size - 2,
            0,
            this.transform
        );

    }
}

// parts["x_axis_r"].transform.localPosition = new PointF (
//     obj.cursor.x,
//     (obj.cursor.y - 1.75f - (obj.size.y / 2)) / 2
// );
// parts["x_axis_r"].GetComponent<SpriteRenderer> ().size = new Vector3 (1, obj.size.y / 2 + obj.cursor.y);

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