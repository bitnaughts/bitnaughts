using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Camera camera;
	Transform camera_transform;
	Vector3 movement;

	bool isPerspective = true;

	void Start () {
		camera_transform = this.transform.GetChild (0);
		camera = camera_transform.GetComponent<Camera> ();
		setCameraProperties ();
	}
	void Update () {
		/* Get Movement Vector */
		// if (ShipManager.isShipSelected ()) movement = new Vector3 (-this.transform.position.x + (ShipManager.getSelectedShipPosition ().x - CameraProperties.OFFSET_X), Input.GetAxis ("Mouse ScrollWheel"), -this.transform.position.z + (ShipManager.getSelectedShipPosition ().z - CameraProperties.OFFSET_Z));
		// else movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Mouse ScrollWheel"), Input.GetAxis ("Vertical"));

		// /* Move Camera via Input */
		// if (movement.x != 0 || movement.z != 0) {
		// 	this.transform.Translate (new Vector3 (
		// 		movement.x * CameraProperties.SENSITIVITY * Time.deltaTime,
		// 		0,
		// 		movement.z * CameraProperties.SENSITIVITY * Time.deltaTime
		// 	));
		// }
		// /* Handle Zooming based on Camera Mode */
		// if (movement.y != 0) {

		// 	if (isPerspective) {
		// 		this.transform.Translate (new Vector3 (0, movement.y * CameraProperties.SENSITIVITY * Time.deltaTime * this.transform.position.y, 0));
		// 		if (this.transform.position.y < CameraProperties.MIN_Y || this.transform.position.y > CameraProperties.MAX_Y) {
		// 			this.transform.position = new Vector3 (this.transform.position.x, Mathf.Clamp (this.transform.position.y, CameraProperties.MIN_Y, CameraProperties.MAX_Y), this.transform.position.z);
		// 		}
		// 	} else {
		// 		// camera.orthographicZoom += ...
		// 		// clamp ...
		// 	}

		// }
	}
	void switchCameraMode () {
		isPerspective = !isPerspective;
		setCameraProperties ();
	}
	void setCameraProperties () {
		if (isPerspective) {
			// TODO: reset properties
			camera_transform.rotation = Quaternion.Euler(new Vector3(90 - CameraProperties.ANGLE_X, CameraProperties.ANGLE_Y,0));

		} else {
			// TODO: reset properties
		}
	}
}