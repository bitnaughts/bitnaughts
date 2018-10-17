using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime;
using System;
using UnityEngine.UI;

public class FormationConfigurer : MonoBehaviour {

	public GameObject[] shipLocations = new GameObject[50];
	public Slider fleetSize;

	//How to set up which formation you want your fleet to fly in.
	/*
	 * 	Pick from a one of a preset list 
	 * 		- Denfensive
	 * 		- Spear
	 * 		- Line
	 * 		- Wall
	 * 
	 * 
	 */


	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < 50; i++)
		{
			shipLocations[i] = Instantiate (Resources.Load ("shipRepresentative"), this.transform.position, this.transform.rotation) as GameObject;
			shipLocations[i].transform.parent = this.transform;
			shipLocations[i].transform.localPosition = new Vector2(0,0);
		}



		//float spreader = 2f;
		//SPIRAL STUFF
		//for (int i = 0; i < 50; i++)
		//{
			//spreader -= .05f;
			//print (spreader);
			//shipLocations[i].transform.localPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(i*25f*spreader))  *(10f*i), Mathf.Sin (Mathf.Deg2Rad*(i*25f*spreader))  *(10f*i));
		//}
	}
	public void updateFleet()
	{
		int amount = (int)fleetSize.value;

		//SINGLE CIRCLE

		float value;
		float distance;

		if (false) {
			//LARGE CIRCLE
			value = 360f/amount;
			distance = 100f;
			for (int i = 0; i < amount; i++) { 
				shipLocations [i + 1].SetActive (true);

				shipLocations [i + 1].transform.localPosition = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (((i + 1 - 1) * value))) * distance, Mathf.Sin (Mathf.Deg2Rad * (((i + 1 - 1) * value))) * distance);
			}
		}
		if (false) {
			//HALF-CIRCLE
			value = 180f/amount;
			distance = 150f;
			for (int i = 0; i < amount; i++) { 
				shipLocations [i + 1].SetActive (true);
				
				shipLocations [i + 1].transform.localPosition = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * ((((i* value)-90)))) * distance, Mathf.Sin (Mathf.Deg2Rad * ((((i* value) +90)))) * distance);
			}
		}
		if (true) {
			//CONCENTRIC CIRCLES
			distance = 50f;
			if (amount < 8) value = 360f/(amount);
			else value = 360f/8f;
			for (int i = 0; i < 8 &&  i < amount; i++) { 
				shipLocations [i + 1].SetActive (true);
				
				shipLocations [i + 1].transform.localPosition = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (((i + 1 - 1) * value))) * distance, Mathf.Sin (Mathf.Deg2Rad * (((i + 1 - 1) * value))) * distance);
			}
			distance = 100f;
			value = 360f/(amount-8);
			for (int i = 8; i < amount; i++) { 
				shipLocations [i + 1].SetActive (true);
				
				shipLocations [i + 1].transform.localPosition = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (((i + 1 - 1) * value))) * distance, Mathf.Sin (Mathf.Deg2Rad * (((i + 1 - 1) * value))) * distance);
			}

		}
		if (false) {
			//Collumn
			distance = 8f;
			if (amount < 8) value = 360f/(amount);
			else value = 360f/8f;
			for (int i = 0; i < amount; i++) { 
				shipLocations [i + 1].SetActive (true);
				if (i % 2 == 0)
				{
					shipLocations [i + 1].transform.localPosition = new Vector2 (i*distance,0);
				}
				else 
				{
					shipLocations [i + 1].transform.localPosition = new Vector2 ((i-1)*-1*distance,0);
				}
			}
		}
		if (false) {
			//Row
			distance = 6f;
			if (amount < 8) value = 360f/(amount);
			else value = 360f/8f;
			for (int i = 0; i < amount; i++) { 
				shipLocations [i + 1].SetActive (true);
				if (i % 2 == 0)
				{
					shipLocations [i + 1].transform.localPosition = new Vector2 (0,i*distance);
				}
				else 
				{
					shipLocations [i + 1].transform.localPosition = new Vector2 (0,(i-1)*-1*distance);
				}
			}
		}


		for (int i = amount; i < 25; i++) {
			shipLocations [i + 1].SetActive (false);
		}


	}
	// Update is called once per frame
	void Update () 
	{
		


	}
}
