using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEditor : MonoBehaviour {

	public string type;

	ComponentObject component;

	List<GameObject> markers = new List<GameObject> ();
	public GameObject markerPrefab;
	ShipObject ship; //reference to ship component is attached to. Null if none.

	bool clickedOverComponent = false;

	// Use this for initialization
	void Start () {
		component = new ComponentObject (type);
		//change this via UI (if double-click on a owned ship, set "ship" to that ship...)
		//ship = GameObject.Find ("Ship").GetComponent<ShipEditor> ().ship;

	}

	// Update is called once per frame
	void Update () {
		Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonUp (0)) {
			if (clickedOverComponent == true) {
				//for all ShipManager.getSelectedShip()s
				if (ShipManager.getSelectedShip ().isPlaceable (type, new Point ((short) this.transform.position.x, (short) this.transform.position.y))) {
					ShipManager.getSelectedShip ().place (component, new Point ((short) this.transform.position.x, (short) this.transform.position.y));
					ship = ShipManager.getSelectedShip ();
				}
			}
			clickedOverComponent = false;
		}
		if (clickedOverComponent && Input.GetMouseButton (0)) {
			Vector2 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			this.transform.position = position;
			if (ShipManager.getSelectedShip ().isPlaceable (type, new Point ((short) position.x, (short) position.y))) {
				this.transform.position = new Vector2 ((int) position.x, (int) position.y);
				this.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
			} else this.GetComponent<SpriteRenderer> ().color = new Color (255, 0, 0);
		}

		if (ship != null) {
			this.GetComponent<SpriteRenderer> ().color = new Color (0, 255, 0);
		}
	}
	void OnMouseEnter () {
		short id = ComponentConstants.getComponentID (type);
		Point[] component_mount_points = ComponentConstants.getComponentMountPoints (id);

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
			if (ship != null) {
				ship.remove (component);
				ship = null;
			}

		}

	}

	void OnMouseExit () {
		for (int i = 0; i < markers.Count; i++) {
			Destroy (markers[i]);
		}
	}

}