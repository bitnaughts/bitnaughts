using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour {

	public GameObject console;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void execute (string function_name, string function_parameters) {
		switch (function_name) {
			case "WriteLine":
				print ("working!");
				break;
		}
	}
}