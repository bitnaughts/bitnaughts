using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEditor : MonoBehaviour {

	public string type;

	List<GameObject> markers = new List<GameObject> ();
	public GameObject markerPrefab;

	ShipObject ship; //reference to ship component is attached to. Null if none.

	bool clickedOverComponent = false;

	// Use this for initialization
	void Start () {
		//ship = null;
	}

	// Update is called once per frame
	void Update () {
		Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {

		} else if (Input.GetMouseButtonUp (0)) {
			if (ship == null && clickedOverComponent == true) {
				//for all ships
				ship = GameObject.Find("Ship").ship;
				if (ship.isPlaceable (type, new IntVector ((short) this.transform.position.x, (short) this.transform.position.y))) {
					ship.place (type, new IntVector ((short) this.transform.position.x, (short) this.transform.position.y));
				}
			}
			clickedOverComponent = false;
		}
		if (clickedOverComponent && Input.GetMouseButton (0)) {
			Vector2 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			this.transform.position = new Vector2 ((int) position.x, (int) position.y);
			if (ship.isPlaceable (type, new IntVector ((short) position.x, (short) position.y))) {
				this.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
			} else this.GetComponent<SpriteRenderer> ().color = new Color (255, 0, 0);
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