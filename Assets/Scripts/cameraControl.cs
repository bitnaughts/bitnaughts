using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour {

	//public GameObject fleet;

	public Transform follow_target;
    //public ProjectileManager projectile_manager;

	public bool style_snap;
	
	public bool style_smooth;
	public float style_smoothness;



	// Update is called once per frame
	void Update () {
        //if (fleet == null) fleet = GameObject.Find ("Holder0");
        //if (Input.GetMouseButton(2))
        if (Input.mouseScrollDelta.y != 0 || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ProjectileManager.ScaleProjectilesToZooming(this.GetComponent<Camera>().orthographicSize);
            this.GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y;
            this.GetComponent<Camera>().orthographicSize -= -Input.GetAxis("Mouse ScrollWheel");

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2)) 
			this.GetComponent<Camera>().orthographicSize+=1;

		if (style_snap) this.transform.position = new Vector3(follow_target.position.x,follow_target.position.y,this.transform.position.z);	
		if (style_smooth) this.transform.position = new Vector3(this.transform.position.x - (this.transform.position.x - follow_target.position.x) / style_smoothness, this.transform.position.y - ( this.transform.position.y - follow_target.position.y) / style_smoothness,this.transform.position.z);

	}
}
