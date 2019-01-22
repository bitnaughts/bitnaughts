using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Camera camera;
	Vector3 movement;

	bool isPerspective = false;

	void Start () {
		Camera camera = this.GetComponent<Camera> ();
		setCameraProperties ();
	}
	void Update () {
		updateCamera ();
	}
	void updateCamera () {
		/* Get Movement Vector */
		if (ShipManager.isShipSelected ()) movement = new Vector3 (-this.transform.position.x + ShipManager.getSelectedShipPosition ().x, Input.GetAxis ("Mouse ScrollWheel"), -this.transform.position.z + ShipManager.getSelectedShipPosition ().z);
		else movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Mouse ScrollWheel"), Input.GetAxis ("Vertical"));

		/* Move Camera via Input */
		if (movement.x != 0 || movement.z != 0) {
			this.transform.Translate (new Vector3 (
				movement.x * CameraProperties.SENSITIVITY * Time.deltaTime,
				0,
				movement.z * CameraProperties.SENSITIVITY * Time.deltaTime
			));
		}
		/* Handle Zooming based on Camera Mode */
		if (movement.y != 0) {

			if (isPerspective) {
				this.transform.Translate (new Vector3 (0, movement.y * CameraProperties.SENSITIVITY * Time.deltaTime * this.transform.position.y, 0));
				if (this.transform.position.y < CameraProperties.MIN_Y || this.transform.position.y > CameraProperties.MAX_Y) {
					this.transform.position = new Vector3 (this.transform.position.x, Mathf.Clamp (this.transform.position.y, CameraProperties.MIN_Y, CameraProperties.MAX_Y), this.transform.position.z);
				}
			} else {
				// camera.orthographicZoom += ...
				// clamp ...
			}

		}
	}
	void switchCameraMode () {
		isPerspective = !isPerspective;
		setCameraProperties ();
	}
	void setCameraProperties () {
		if (isPerspective) {
			// TODO: reset properties
		} else {
			// TODO: reset properties
		}
	}
}