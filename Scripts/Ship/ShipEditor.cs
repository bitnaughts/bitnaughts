using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEditor : MonoBehaviour {

    List<GameObject> markers = new List<GameObject>();

    public GameObject markerPrefab;

    public ShipObject ship;

	// Use this for initialization
	void Start () { 
        ship = new ShipObject("tester", this.transform.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
             for (int i = 0; i < ship.mount_points.Count; i++) {
                 GameObject marker_instance = Instantiate (markerPrefab, new Vector2(0,0),  Quaternion.identity) as GameObject;
                 marker_instance.transform.parent = this.transform;
                 marker_instance.transform.localPosition = new Vector2(ship.mount_points[i].x,ship.mount_points[i].y);
                 markers.Add(marker_instance);
             }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
              for (int i = 0; i < markers.Count; i++) {
                  Destroy(markers[i]);
              }

        }
	}
}
