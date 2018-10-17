using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void open (string level)
	{
		if (level == "close")
			Application.Quit ();
		else
			Application.LoadLevel (level);

	}
}
