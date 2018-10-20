using UnityEngine;
using System.Collections;

public class control : MonoBehaviour {
	public bool friendly = true;

	public bool demo = false;
	// Use this for initialization
	void Start () {
		if (name == "enemy")
			friendly = false;
	}
}
