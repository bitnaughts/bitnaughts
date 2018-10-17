using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour {
	
	public static Vector2 mouseLocation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		mouseLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	
	
	
	}
	
	public Vector2 getMousePosition()
	{
		return mouseLocation;
	}
	
	
}
