using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEditor : MonoBehaviour {

	public string type;

	List<GameObject> markers = new List<GameObject> ();
	public GameObject markerPrefab;

	bool clickedOverComponent = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {

		} else if (Input.GetMouseButtonUp (0)) {
			clickedOverComponent = false;
		}
		if (clickedOverComponent && Input.GetMouseButton (0)) {
			Vector2 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			this.transform.position = new Vector2 ((int) position.x, (int) position.y);
		}
	}
	void OnMouseEnter () {
		short id = ComponentConstants.getComponentID (type);
		IntVector[] component_mount_points = ComponentConstants.getComponentMountPoints (id);

		for (int i = 0; i < component_mount_points.Length; i++) {
			GameObject marker_instance = Instantiate (markerPrefab, new Vector2 (0, 0), Quaternion.identity) as GameObject;
			marker_instance.transform.parent = this.transform;
			marker_instance.transform.localPosition = new Vector3 (component_mount_points[i].x, component_mount_points[i].y, -5);
			markers.Add (marker_instance);
		}
	}

	void OnMouseOver () {
		if (Input.GetMouseButtonDown (0)) {
			clickedOverComponent = true;
		}

	}

	void OnMouseExit () {
		for (int i = 0; i < markers.Count; i++) {
			Destroy (markers[i]);
		}
	}

}