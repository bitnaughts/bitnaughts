using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

    public Ship ship;
    List<GameObject> markers = new List<GameObject> ();
    public GameObject markerPrefab;

    void Start () {
        ship = new Ship (1);
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

    public Vector2 addVectors (Vector2 left, Vector2 right) {
        return new Vector2 (left.x + right.x, left.y + right.y);
    }
}