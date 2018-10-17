using UnityEngine;
using System.Collections;

public class showBetweenObjects : MonoBehaviour {
	public GameObject obj1;
	public GameObject obj2;

	public Vector3 position;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		position.x = obj1.transform.position.x;
		position.y = obj1.transform.position.y;

		position.x += 8f;
		position.z = -100f;
		this.transform.position = position;
	}
}
