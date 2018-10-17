using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime;
//using System;
using UnityEngine.UI;

public class fleet : MonoBehaviour {
	GameObject[] positions = new GameObject[64];
	public string shipTags = "friend";
	GameObject targeting;

	GameObject fleetHolder;

	public string fleetShape = "CC";
	public int fleetSize = 5;
	public int ships;

	public int team;

//	public bool demo = false;
	// Use this for initialization
	void Start () {

		//print (team);

		//fleetHolder = GameObject.Find (shipTags);
		ships = this.transform.childCount;

		GameObject pointHolder = Instantiate( Resources.Load ("template"),new Vector2(0,0), this.transform.rotation) as GameObject;
		pointHolder.transform.parent = this.transform;
		pointHolder.transform.localPosition = new Vector2 (0,0);
		pointHolder.name = "PointHolder";
		pointHolder.tag = "Untagged";

		for (int i = 0; i < 64; i++)
		{

			positions[i] = Instantiate( Resources.Load ("star1"),new Vector2(0,0), this.transform.rotation) as GameObject;
			positions[i].transform.parent = this.transform.GetChild(this.transform.childCount-1);		
			//positions[i].position = Formations.getFormationType("LC",i,ships,5);			
		}
		updateFleetShape ();
		//string[] data = File.ReadAllLines (Application.persistentDataPath + "fleetData.txt");
		//positions = new Transform[100];
		//for (int i = 0; i < 61; i++) positions[i] = GameObject.Find (shipTags+"Point"+i).transform;
	//	print (positions[3].position.x);
	}

	void updateFleetShape()
	{
		for (int i = 0; i < ships; i++)		
		{
			positions[i].SetActive(true);
			positions[i].transform.localPosition = Formations.getFormationType(fleetShape,i,ships,fleetSize);		
			//positions[i].position = Formations.getFormationType("LC",i,ships,5);	
		}
		for (int i = ships; i < 64; i++) 
		{
			positions[i].SetActive(false);
		}

	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (ships != this.transform.childCount) 
		{
			//fleetHolder = GameObject.Find (shipTags);
			for (int i = 0; i < this.transform.childCount-1; i++)
			{
				this.transform.GetChild(i).gameObject.GetComponent<Ship>().position = i;
			}

			updateFleetShape();

		}
		ships = this.transform.childCount;
		

		if (team == 0) {
			//this.transform.Translate (new Vector2 (0, (Input.GetAxis ("Vertical")) / 10f));
			//this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));	
			this.transform.GetChild(this.transform.childCount-1).Translate (new Vector2 (0, (Input.GetAxis ("Vertical")) / 10f));
			this.transform.GetChild(this.transform.childCount-1).Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));	
			//for (int i = 0; i < this.transform.childCount; i++)
			//{
			//	this.transform.GetChild(i).Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));	
			//}


		} else {
			this.transform.Translate(new Vector2(0f,.075f));
			float turnSpeed = 1.5f;
			Vector3 targetPoint;

			//.transform.position;
			if (targeting == null) 
			{
				
				targetPoint = new Vector2(0,0);
				targeting = GameObject.Find ("Holder0");		/////ANYTHING THAT IS ENEMY
				//targeting = GameObject.Find ("enemyFleet");
					

			}
			else targetPoint = targeting.transform.GetChild(targeting.transform.childCount-1).position;
			Vector3 turretPoint = this.transform.position;
			//Finding angle through tan^-1
			float angle = Mathf.Rad2Deg*Mathf.Atan((targetPoint.y-turretPoint.y)/(targetPoint.x-turretPoint.x) );
			//Configuring produced angle to 0-360
			if (targetPoint.y < turretPoint.y)
			{
				if(angle < 0) angle += 180f;
				angle += 180f;			 
			}
			else if(angle < 0) angle += 180f;		
			//finding current 0-360 angle
			float current = this.transform.rotation.eulerAngles.z + 90f;
			float reverseDirectionMultiplier = 1f;
			if (Mathf.Abs (current-angle) > 180f && Mathf.Abs (current-angle) < 360f) reverseDirectionMultiplier = -1f;
			//Random turn speed
			float random = Random.value;
			//random = ;
			//Rotating in desired direction
			if (current > angle) this.transform.Rotate(0,0,-random*turnSpeed * reverseDirectionMultiplier);
			else transform.Rotate(0,0,random*turnSpeed * reverseDirectionMultiplier);
		}



			
			//GameObject[] ships = GameObject.FindGameObjectsWithTag(shipTags);

		}
		public Vector2 getLocation(int position)
		{
			//return Formations.getFormationType(fleetShape,position,ships,5);
			return positions[position].transform.position;
		}	
		
}






















/*			
			//print (ships.Length);
			
			for (int i = 0; i < ships.Length; i++)
			{
			//	print (ships.Length);
			//print (ships[i].tag);	
			ships[i].GetComponent<ship>().position = 1;
			}
			
			int location = 0;
			string target = "big";
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < ships.Length; j++) if (ships[j].layer == LayerMask.NameToLayer(target)) ships[j].GetComponent<ship>().position = location++;
				if (i == 0) target = "medium";
				if (i == 1) target = "small";
			}
			
			int amount = ships.Length;
			float value;
			if (amount < 6) value = 360f/(amount-1);
			else value = 360f/6f;
			if (value > 10000) value = 360/24f;
			float distance = 4f;
			for (int i = 0; i < 6 || i < amount; i++)
			{ 
			positions[i+1].localPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(((i+1-1)*value))) * distance,Mathf.Sin (Mathf.Deg2Rad*(((i+1-1)*value)))* distance);
			}
			distance = 8f;
			if (amount < 18) value = 360f/(amount-7);
			else value = 360f/12f;
			if (value > 10000) value = 360/24f;
			for (int i = 6; i < 18 || i < amount; i++)
			{
			positions[i+1].localPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(((i+1-1)*value))) * distance,Mathf.Sin (Mathf.Deg2Rad*(((i+1-1)*value)))* distance);
			}
			distance = 11f;
			if (amount < 36) value = 360f/(amount-7-12);
			else value = 360f/18f;
			if (value > 10000) value = 360/18f;
			for (int i = 18; i < 36 || i < amount; i++)
			{
			positions[i+1].localPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(((i+1-1)*value))) * distance,Mathf.Sin (Mathf.Deg2Rad*(((i+1-1)*value)))* distance);
			}
			distance = 13f;
			if (amount < 60) value = 360f/(amount-7-12-18);
			else value = 360f/24f;
			if (value > 10000) value = 360/24f;
			for (int i = 36; i < 60 || i < amount; i++)
			{
			positions[i+1].localPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(((i+1-1)*value))) * distance,Mathf.Sin (Mathf.Deg2Rad*(((i+1-1)*value)))* distance);
			}
*/
