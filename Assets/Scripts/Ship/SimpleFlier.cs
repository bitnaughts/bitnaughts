using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlier : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		ShipObject ship = this.GetComponent<ShipEditor>().ship;

		this.transform.Translate (new Vector2 (0, Input.GetAxis ("Vertical") * .05f * ship.thrust / Mathf.Sqrt(ship.weight)));
		this.transform.Rotate (new Vector3 (0, -Input.GetAxis ("Horizontal"), 0));
	}
}