using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour {

	static ShipObject selected_ship;

	// Use this for initialization
	void Start () {
		//print (GameObject.Find ("Ship").GetComponent<ShipEditor> ().ship);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (selected_ship == null) selected_ship = GameObject.Find ("Ship").GetComponent<ShipEditor> ().ship;

		GameObject.Find("Output").GetComponent<Text>().text = selected_ship.ToString();
	
	}

	public static ShipObject getSelectedShip() {
		return selected_ship;
	}
}
