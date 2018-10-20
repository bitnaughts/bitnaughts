using UnityEngine;
using System.Collections;

public class superMissile : MonoBehaviour {

	public float delay = 4f;
	private float count = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		count += .1f;
		if (count >= delay) {

			Instantiate(Resources.Load("missile"),this.transform.position,this.transform.rotation);



			Destroy(this.gameObject,3f);
			Destroy(this,3f);
		}
	}
}
