using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Camera))]
public class CameraController : MonoBehaviour {

	Camera camera;
	Transform camera_transform;
	Vector3 movement;

	// bool isPerspective = true;

	void Start () {
		// camera_transform = this.transform.GetChild (0);
		camera = this.GetComponent<Camera> ();
		// setCameraProperties ();
	}
	void Update () {
		/* Get Movement Vector */
		// if (ShipManager.isShipSelected ()) movement = new Vector3 (-this.transform.position.x + (ShipManager.getSelectedShipPosition ().x - CameraProperties.OFFSET_X), Input.GetAxis ("Mouse ScrollWheel"), -this.transform.position.z + (ShipManager.getSelectedShipPosition ().z - CameraProperties.OFFSET_Z));

		// float x = 0;
		// if (Input.GetKey("a")) x = -1;
		// if (Input.GetKey("d")) x = 1;
		
		// float y = 0;
		// if (Input.GetKey("s")) y = -1;
		// if (Input.GetKey("w")) y = 1;

		// /* Move Camera via Input */
		// this.transform.Translate (
		// 	new Vector2 (
		// 		x * 10 * Time.deltaTime,
		// 		y * 10 * Time.deltaTime
		// 	)
		// );
		/* Handle Zooming based on Camera Mode */
		camera.orthographicSize += Input.GetAxis ("Mouse ScrollWheel") * -100 * Time.deltaTime;

		this.transform.position = new Vector3(GameObject.Find("Fighter").transform.position.x, GameObject.Find("Fighter").transform.position.y, -10);


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
	// void switchCameraMode () {
	// 	// isPerspective = !isPerspective;
	// 	setCameraProperties ();
	// }
	// void setCameraProperties () {
	// 	if (isPerspective) {
	// 		// TODO: reset properties
	// 		camera_transform.rotation = Quaternion.Euler(new Vector3(90 - CameraProperties.ANGLE_X, CameraProperties.ANGLE_Y,0));

	// 	} else {
	// 		// TODO: reset properties
	// 	}
	// }
}