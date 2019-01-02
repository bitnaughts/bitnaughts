using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (ShipManager.getSelectedShip () != null) {

			this.transform.position = new Vector3 (ShipManager.getSelectedShip ().ship.transform.position.x, ShipManager.getSelectedShip ().ship.transform.position.y, -20);
			this.GetComponent<Camera> ().orthographicSize -= Input.GetAxis ("Mouse ScrollWheel");
		}
	}
}