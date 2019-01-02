using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : MonoBehaviour {

	static ShipObject selected_ship;
	public static GameObject holder;

	public GameObject[] ship_prefabs;

	int counter = 0;
	int prefab_counter = 0;

	// Use this for initialization
	void Start () {
		//print (GameObject.Find ("Ship").GetComponent<ShipEditor> ().ship);
		holder = this.transform.gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if (selected_ship == null) selected_ship = GameObject.Find ("Ship").GetComponent<ShipEditor> ().ship;

		GameObject.Find("Output").GetComponent<Text>().text = selected_ship.ToString();
		if (counter++ < 200) {
	Instantiate (ship_prefabs[(prefab_counter++ % 7) + 1], new Vector2 ((Random.value * 100) - 50, (Random.value * 100) - 50), Quaternion.identity);
		}
		else if (counter++ % 50 == 0) {
			Instantiate (ship_prefabs[(prefab_counter++ % 7) + 1], new Vector2 ((Random.value * 100) - 50, (Random.value * 100) - 50), Quaternion.identity);
		}

	}

	public static ShipObject getSelectedShip() {
		return selected_ship;
	}
}
