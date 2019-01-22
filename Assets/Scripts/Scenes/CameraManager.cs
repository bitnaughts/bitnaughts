using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Camera camera;
	Vector2 movement;

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
		if (Referencer.shipManager.isShipSelected ()) movement = new Vector2 (-this.transform.position.x + Referencer.shipManager.getSelectedShipPosition ().x, -this.transform.position.y + Referencer.shipManager.getSelectedShipPosition ().y)
		else movement = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		/* Move Camera via Input */
		if (movement.x != 0 || movement.y != 0) {
			this.transform.Translate (new Vector3 (
				movement.x * CameraProperties.SENSITIVITY * Time.deltaTime,
				movement.y * CameraProperties.SENSITIVITY * Time.deltaTime,
				0
			));
		}
		/* Handle Zooming based on Camera Mode */
		if (isPerspective) ? updatePerspectiveCameraZoom () : updateOrthographicCameraZoom ();
	}
	void updatePerspectiveCameraZoom () {
		/* Zooming Camera */
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			this.transform.Translate (new Vector3 (0, 0 Input.GetAxis ("Mouse ScrollWheel") * CameraProperties.SENSITIVITY * Time.deltaTime * this.transform.position.y));
			if (this.transform.position.y < CameraProperties.MIN_Y || this.transform.position.y > CameraProperties.MAX_Y) {
				this.transform.position = new Vector3 (this.transform.position.x, Mathf.Clamp (this.transform.position.y, CameraProperties.MIN_Y, CameraProperties.MAX_Y), this.transform.position.z);
			}
		}
	}
	void updateOrthographicCameraZoom () {
		// TODO: implement if desired
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