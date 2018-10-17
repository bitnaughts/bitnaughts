using UnityEngine;
using System.Collections;

public class MapPlayer : MonoBehaviour {
	public GameObject galaxy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector2 (0, (Input.GetAxis ("Vertical")) / 300f /* * speed */));
		this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
		//galaxy.transform.Translate (new Vector2(this.transform.position.x,this.transform.position.y));
		//this.transform.position = new Vector3 (0, 0, -2f);
	}
}
