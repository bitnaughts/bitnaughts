﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlier : MonoBehaviour {

	// ShipObject ship; // = this.GetComponent<ShipEditor>().ship;

	// Use this for initialization
	void Start () {
		// ship = this.GetComponent<ShipEditor> ().ship;
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)); //Input.GetAxis ("Vertical") * .05f * ship.thrust / Mathf.Sqrt (ship.weight)));
		// this.transform.Rotate (new Vector3 (0, -Input.GetAxis ("Horizontal"), 0));
	}
}