using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEditor : MonoBehaviour {

	public string type;

	List<GameObject> markers = new List<GameObject> ();
	List<GameObject> markers2 = new List<GameObject> ();
	public GameObject markerPrefab;
	public GameObject marker2Prefab;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {
			short id = ComponentConstants.getComponentID (type);
			for (int i = 0; i < ComponentConstants.MOUNT_POINTS[id].Length; i++) {
				GameObject marker_instance = Instantiate (markerPrefab, new Vector2 (0, 0), Quaternion.identity) as GameObject;
				marker_instance.transform.parent = this.transform;
				marker_instance.transform.localPosition = new Vector3 (ComponentConstants.MOUNT_POINTS[id][i].x, ComponentConstants.MOUNT_POINTS[id][i].y, -5);
				markers.Add (marker_instance);
			}
			for (int i = 0; i < ComponentConstants.ATTACH_POINTS[id].Length; i++) {
				GameObject marker_instance = Instantiate (marker2Prefab, new Vector2 (0, 0), Quaternion.identity) as GameObject;
				marker_instance.transform.parent = this.transform;
				marker_instance.transform.localPosition = new Vector3 (ComponentConstants.ATTACH_POINTS[id][i].x, ComponentConstants.ATTACH_POINTS[id][i].y, -5);
				markers2.Add (marker_instance);
			}
		} else if (Input.GetMouseButtonUp (0)) {
			for (int i = 0; i < markers.Count; i++) {
				Destroy (markers[i]);
			}
			for (int i = 0; i < markers2.Count; i++) {
				Destroy (markers[i]);
			}
		}
	}
}