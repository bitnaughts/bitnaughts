using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime;
using System;
using UnityEngine.UI;
//using UnityEngine.

public class mapDrag : MonoBehaviour
{

    Vector2[] allLocations = new Vector2[10000];
    public float limit = 0f;
    int counter = 0;
    bool generating = true;

    public float size = 10f;
    float tempSize = 0;
    Vector2 systemLocation = new Vector2(0, 0);
    public float declineRate = .5f;
    public float width = 100f;
    public bool after = true;

    public GameObject testText;

    int starCount = 0;

    public GameObject camera;
    public GameObject title;

    Vector2 dragLocation;
    Vector2 newLocation;
    Vector2 mouseLocation;


    //	public static string defaultSaveAddress = Application.dataPath + "galaxy.dat";
    //	public static string defaultItemsAddress = "Z:/AufstandDefaults/Items.txt";

    string[] stars = new string[2000];

    String savedGalaxy;
    int amountSavedPlanets = 0;
    string[] savedPlanets = File.ReadAllLines(Application.persistentDataPath + "galaxyData.txt");

    int clickDuration = 0;
    public bool fromSave;

    int planetSelected;

    //GameObject[] test = new GameObject[10000];
    LineRenderer testLine;
    int testCount = 0;

    int lineCount;


    public GameObject solarInformation;
    public GameObject dataInformation;
    public GameObject politicInformation;


    GameObject codeBase;
    GameObject test;


    Text outputText;

    //SETS UP VARIABLES
    void Start()
    {
        ;
    }
}
	/*	outputText = GameObject.Find("Output").GetComponent<Text>();
		codeBase = GameObject.Find("CodeBase");
		
		title.GetComponent<TitleDisplay>().updateMessage("         Scanning ");
		
		File.WriteAllText (Application.persistentDataPath + "fleetData.txt", "");

		string[] items = File.ReadAllLines(Application.persistentDataPath +"fleetData.txt");
		string output = "";
		for (int i = 0; i < items.Length; i++)
			output += items [i];
		print (output);

		tempSize = size;
		for (int i = 0; i < allLocations.Length; i++) allLocations [i] = new Vector2 (0, 0);
		if (fromSave) amountSavedPlanets = File.ReadAllLines (Application.persistentDataPath + "galaxyData.txt").Length;
	}



	//RUNS GENERATION METHOD AND CHECKS MOUSE 
	void Update () 
	{
        if (generating) ;// generate (fromSave);
		else if (after) 
		{
			after = false;
			print (counter);
		}
		else
		{
			title.GetComponent<TitleDisplay>().updateMessage("GalaxyView");
			//testText.GetComponent<Text>().text = camera.GetComponent<cameraScrool>().overUI +"";
			outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n")+1);
			if (Input.GetMouseButtonDown (0))
			{
				//Application.LoadLevel("Overview");
				dragLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				newLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				print (Input.mousePosition);
				mouseLocation = Input.mousePosition;
				//if on a ship
				solarInformation.GetComponent<solarReport>().over = false;
				dataInformation.GetComponent<dataReport>().over = false;
				politicInformation.GetComponent<politicsReport>().over = false;
				camera.GetComponent<cameraScrool>().overUI = false;
				//if (politicInformation
				if (politicInformation.GetComponent<politicsReport>().opened)
				{
					//IF OVER FORM
					if (clickOn(-189f,189f,-121f,121f,mouseLocation,politicInformation.GetComponent<RectTransform>().transform.position))
					{politicInformation.GetComponent<politicsReport>().over = true;camera.GetComponent<cameraScrool>().overUI = true;}
					//IF DATA X
					if (clickOn(-14f,14f,-13f,13f,mouseLocation,new Vector2(politicInformation.GetComponent<RectTransform>().transform.position.x+163f,politicInformation.GetComponent<RectTransform>().transform.position.y+100f))) politicInformation.GetComponent<politicsReport>().open(false, "");
				}	
				if (dataInformation.GetComponent<dataReport>().opened)
				{
					//IF OVER FORM
					if (clickOn(-163f,163f,-121f,121f,mouseLocation,dataInformation.GetComponent<RectTransform>().transform.position))
					{dataInformation.GetComponent<dataReport>().over = true;camera.GetComponent<cameraScrool>().overUI = true;}
					//IF DATA X
					if (clickOn(-14f,14f,-13f,13f,mouseLocation,new Vector2(dataInformation.GetComponent<RectTransform>().transform.position.x+137.35f,dataInformation.GetComponent<RectTransform>().transform.position.y+100f))) dataInformation.GetComponent<dataReport>().open(false, "");
					//IF BUILD
					if (clickOn(-54f,54f,-23f,23f,mouseLocation,new Vector2(dataInformation.GetComponent<RectTransform>().transform.position.x+8f,dataInformation.GetComponent<RectTransform>().transform.position.y-105f)))
					{	buildShip(dataInformation.GetComponent<dataReport>().build ()); dataInformation.GetComponent<dataReport>().open(false, ""); }
				}			
				//SOLAR REPORT FORM
				if (solarInformation.GetComponent<solarReport>().opened)
				{
					//IF OVER FORM
					if (clickOn(-215f,215f,-223f,223f,mouseLocation,solarInformation.GetComponent<RectTransform>().transform.position))
					{solarInformation.GetComponent<solarReport>().over = true;camera.GetComponent<cameraScrool>().overUI = true;}
					if (clickOn(-128f,128f,-170f,170f,mouseLocation,new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x+284,solarInformation.GetComponent<RectTransform>().transform.position.y-6)))
					{solarInformation.GetComponent<solarReport>().over = true;camera.GetComponent<cameraScrool>().overUI = true;}
					//IF OVER PLANET 
					for (int i = 0; i < solarInformation.GetComponent<solarReport>().numberPlanets; i++) if (clickOn(-30f,30f,-30f,30f,mouseLocation,solarInformation.GetComponent<solarReport>().planets[i].transform.position)) solarInformation.GetComponent<solarReport>().planetClick(true,i);
					//IF SIDEBAR BUTTONS
					for (int i = 0; i < 3; i++) if (clickOn(-30f,30f,-30f,30f,mouseLocation,new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x+362,solarInformation.GetComponent<RectTransform>().transform.position.y+56 - (76*i)))) solarInformation.GetComponent<solarReport>().planetButtonClick(i);
					//IF SIDEBAR ARROW UP
					if (clickOn(-11f,11f,-12f,12f,mouseLocation,new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x+402,solarInformation.GetComponent<RectTransform>().transform.position.y+54.25f))) solarInformation.GetComponent<solarReport>().sideBar.GetComponent<informationDisplay>().arrow(true);
					//IF SIDEBAR ARROW DOWN
					if (clickOn(-11f,11f,-12f,12f,mouseLocation,new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x+402,solarInformation.GetComponent<RectTransform>().transform.position.y-97.75f))) solarInformation.GetComponent<solarReport>().sideBar.GetComponent<informationDisplay>().arrow(false);
					//IF SIDEBAR X
					if (clickOn(-19f,19f,-17f,17f,mouseLocation,new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x+371,solarInformation.GetComponent<RectTransform>().transform.position.y+108.9f))) solarInformation.GetComponent<solarReport>().planetClick(false, -1);//..Equals),"");
					//IF REPORT X
					if (clickOn(-14f,14f,-13f,13f,mouseLocation,new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x+189,solarInformation.GetComponent<RectTransform>().transform.position.y+202))) solarInformation.GetComponent<solarReport>().open(false, "");
				}
			}
			if (Input.GetKeyDown(KeyCode.P)) politicInformation.GetComponent<politicsReport>().open(true, "");


			if (Input.GetMouseButton (0)) 
			{
			
				newLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Vector2 newMouseLocation = Input.mousePosition;
				if (politicInformation.GetComponent<politicsReport>().over)
				{

					politicInformation.transform.Translate(new Vector2(-(mouseLocation.x - newMouseLocation.x), -(mouseLocation.y - newMouseLocation.y)));
				}
				else if (dataInformation.GetComponent<dataReport>().over)
				{
					dataInformation.transform.Translate(new Vector2(-(mouseLocation.x - newMouseLocation.x), -(mouseLocation.y - newMouseLocation.y)));
				}
				else if (solarInformation.GetComponent<solarReport>().over)
				{
					solarInformation.transform.Translate(new Vector2(-(mouseLocation.x - newMouseLocation.x), -(mouseLocation.y - newMouseLocation.y)));
				} 
				//else if (Input.touchCount <= 1) this.transform.Translate(new Vector2(-(dragLocation.x - newLocation.x), -(dragLocation.y - newLocation.y)));//position = 

				dragLocation = newLocation;
				mouseLocation = newMouseLocation;
			}
			if (Input.GetMouseButtonUp (0)) 
			{

				camera.GetComponent<cameraScrool>().overUI = false;
				dragLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				newLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				if (clickDuration < 10) 
				{
					//testText.GetComponent<Text>().text = "up";
					Vector2 mapLocation = new Vector2 (newLocation.x - this.transform.position.x, newLocation.y - this.transform.position.y);
					if (!solarInformation.GetComponent<solarReport>().over) findPlanet(mapLocation.x, mapLocation.y);
				}
				clickDuration = 0;
			}
		}
	}

	//CHECKS IF CLICK IS WITHIN GIVEN BOUNDS
	public bool clickOn(float xMin, float xMax, float yMin, float yMax, Vector2 mousePosition, Vector2 otherPosition)
	{
		if (mousePosition.x + xMin < otherPosition.x && mousePosition.x + xMax > otherPosition.x) if (mousePosition.y + yMin < otherPosition.y && mousePosition.y + yMax > otherPosition.y) return true;
		return false;
	}

	//CONSISTENT TIMER FOR DETERMINING BETWEEN CLICK AND DRAG
	void FixedUpdate()
	{
		if (Input.GetMouseButton (0)) clickDuration++;
	}

	//RUNS THROUGH ALL POSITIONS TO SEE WHERE USER CLICKED, RUNS NECESSARY METHODS PER ITEM
	public void findPlanet(float x, float y)
	{
		//Ships Check
		//XFloat YFloat NumberShips Ship1Type Ship2Type ... ShipXType
		for (int i = 0; i < File.ReadAllLines(Application.persistentDataPath +"fleetData.txt").Length; i++) 
		{



		}







		//look for being on a ship or gui or stars
		for (int i = 0; i < counter; i++) 
		{
			if (x - .3 < allLocations[i].x && x + .3 > allLocations[i].x)
			{
				if (y - .3 < allLocations[i].y && y + .3 > allLocations[i].y)
				{
					//resets
					for (int g = 0; g < 5; g++) solarInformation.GetComponent<solarReport>().planetClick(false,-1);
					//displays correct data (or else previous openings will show up
					solarInformation.GetComponent<solarReport>().open(false, "");
					solarInformation.GetComponent<solarReport>().open(true, File.ReadAllLines (Application.persistentDataPath + "galaxyData.txt")[i]);///savedPlanets[i]);
					planetSelected = i;
					break;
				}
			}
		}
	}


	public void buildShip(string type)
	{
		print (type);
		float x = allLocations [planetSelected].x;
		float y = allLocations [planetSelected].y;

		//for all ships, check if x and y are the same, then put it that fleet, else new fleet
		string[] fleets = File.ReadAllLines (Application.persistentDataPath + "fleetData.txt");
	//	string fleetsCompacted = "";
	//	for (int i = 0; i < fleets.Length; i++) fleetsCompacted += fleets[i];
	//	fleets = fleetsCompacted.Split ('\n');

		bool done = false;

		for (int i = 0; i < fleets.Length; i++) 
		{
			//float result;
			//if (float.TryParse(fleets[i].Split(' ')[0], out result))
			//{
			if (x + .0001f > float.Parse(fleets[i].Split(' ')[0]) && x - .0001f < float.Parse(fleets[i].Split(' ')[0]) && y + .0001f > float.Parse(fleets[i].Split(' ')[1]) && y - .0001f < float.Parse(fleets[i].Split(' ')[1]))
				{
					//fleets[i] = x + " " + y + " " + (int.Parse(fleets[i].Split(' ')[2]) + 1);
					//for (int j = 3; j < fleets[i].Split(' ').Length; j++) fleets[i] += " " + fleets[i].Split(' ') [j];
					fleets[i] += " " + type;

					string fleet = "";

					for (int j = 0; j < fleets.Length; j++)
					{
						fleet += fleets[j] + "\n";
					}

					File.WriteAllText(Application.persistentDataPath +"fleetData.txt", fleet);

					print ("ADDED");

					done = true;
				}			
			//}
		}
		if (!done)
		{
			string newFleet = x + " " + y + " " + type + "\n";
			string fleet = "";
			for (int i = 0; i < fleets.Length; i++) fleet += fleets[i] + "\n";
			fleet += newFleet;

			print ("NEW");

			File.WriteAllText(Application.persistentDataPath +"fleetData.txt", fleet);
		}
		//print (File.ReadAllLines(Application.persistentDataPath +"fleetData.txt")[0]);	

		string[] swag = File.ReadAllLines (Application.persistentDataPath + "fleetData.txt");
		for (int i = 0; i < swag.Length; i++)
			print (swag [i]);




	}

	//MAKES/LOADS FROM FILE THE GALAXY, ITS SOLAR SYSTEMS AND PLANETS
	public void generate(bool fromSave)
	{
		if (fromSave) 
		{
			if (counter < amountSavedPlanets) // make variable
			{
				string starInfo = savedPlanets[counter];
				string[] parts = starInfo.Split (' ');
				GameObject temp = Instantiate (Resources.Load ("star" + parts[2].Substring(0,1)), new Vector2(float.Parse(parts[0]),float.Parse(parts[1])), this.transform.rotation) as GameObject;			
				temp.transform.parent = this.transform;
				allLocations [counter++] = new Vector2(float.Parse(parts[0]),float.Parse(parts[1]));
			}
			else generating = false;
		}
		else
		{
			if (declineRate < size) 
			{
				tempSize -= declineRate;
				if (tempSize < 0) 
				{
					tempSize = size;
					systemLocation = new Vector2 (UnityEngine.Random.value * width - (width/2), UnityEngine.Random.value * width - (width/2));
					declineRate *= 3 / 2f;
				}
				Vector2 potentialLocation = new Vector2 (UnityEngine.Random.value * tempSize - (tempSize / 2) + systemLocation.x, UnityEngine.Random.value * tempSize - (tempSize / 2) + systemLocation.y);
				bool available = true;
				for (int i = 0; i < allLocations.Length; i++) 
				{
					if (Mathf.Abs (potentialLocation.x - allLocations [i].x) + Mathf.Abs (potentialLocation.y - allLocations [i].y) < limit) 
					{
						available = false;
						outputText.text += "space_taken;\n";
						outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n")+1);
						break;
					}
				}
				if (available) 
				{
					int swag = (int)(UnityEngine.Random.value * 4);
					GameObject temp = Instantiate (Resources.Load ("star" + swag), potentialLocation, this.transform.rotation) as GameObject;					
					temp.transform.parent = this.transform;
					int numberPlanets = (int) ((UnityEngine.Random.value * (2+swag))+ (swag));//(UnityEngine.Random.value * ((swag+1)*2));
					if (numberPlanets == 7) if (UnityEngine.Random.value > .5f) numberPlanets++;
					stars[counter] = potentialLocation.x + " " + potentialLocation.y + " " + swag + "{";
					for (int i = 0; i < numberPlanets; i++)
					{
						int planetSize = (int) (UnityEngine.Random.value * 10); //0-9
						int agricultureLevel = 0;
						if (planetSize > 2) agricultureLevel = (int) (UnityEngine.Random.value * (planetSize / 2));
						int industryLevel = 0;
						industryLevel = (int) (UnityEngine.Random.value * (planetSize - agricultureLevel));
						int commericalLevel = 0;
						if (planetSize > 6) commericalLevel = (int) (UnityEngine.Random.value * (planetSize - 6));
						int technologyLevel = 0;
						if (planetSize >= 8) technologyLevel = (int) (UnityEngine.Random.value * 2);
					    int militaryLevel = 0;
					    if (technologyLevel == 1) militaryLevel = (int) (UnityEngine.Random.value * 2);


						//planetType, technology level (0 == uninhabitated), 
						stars[counter] += planetSize + "{" + agricultureLevel + "," + industryLevel + "," + commericalLevel + "," + technologyLevel + "," + militaryLevel + "}";
						
					}
					outputText.text += stars[counter] + "\n";
					stars[counter] += "}\n";
					allLocations [counter++] = potentialLocation;
					outputText.text += potentialLocation + "\n";
					if (counter > 50)
					{
						outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n")+1);
						outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n")+1);
					}
				}
			} 
			else 
			{
				animateLines(lineCount);
				lineCount++;
				
				outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n")+1);
				outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n")+1);

				//Proof of concept draw hyperlink lines  Subspace corridors
			
				if (lineCount == counter)
				{
					generating = false;
				
				//compress string array into string and write to file
				string output = "";
				for (int i = 0; i < counter; i++) output += stars[i]; //+ "&");
				File.WriteAllText(Application.persistentDataPath +"galaxyData.txt", output);
				amountSavedPlanets = File.ReadAllLines (Application.persistentDataPath +"galaxyData.txt").Length;
				string[] savedPlanets =  File.ReadAllLines(Application.persistentDataPath +"galaxyData.txt");	
				
				
				
				
				}
				
				
				
				
				

				
				
				
				
				
				
			}
		}
	}
	
	void animateLines(int i)
	{	
		int closestAmount = 4;
		
					float closestStarDistance = 10;
					int[] closestStarID = new int[closestAmount];
					for (int m = 0; m < closestAmount; m++) closestStarID[m] = -1;
					
					for (int k = 0; k < closestAmount; k++)
					{
						
						for (int j = 0; j < allLocations.Length; j++)
						{
							bool chosen = false;
							for (int n = 0; n < closestAmount; n++)
								if (j == closestStarID[n]) chosen = true;
							if (j == i) chosen = true;
							
							if (!chosen)
							{
								float distance = Mathf.Sqrt(Mathf.Pow(allLocations[i].x-allLocations[j].x,2) + Mathf.Pow(allLocations[i].y-allLocations[j].y,2));  
								if (distance < closestStarDistance)
								{
									closestStarDistance = distance;
									closestStarID[k] = j;
									
								}	
							}

						}
						if (closestStarID[k] != -1)
						{
								
							codeBase.GetComponent<LineRendererHolder>().setLine(allLocations[i],allLocations[closestStarID[k]]);
							closestStarDistance = 10;
						}
						//outputText.text += allLocations[closestStarID[k]] + "\n";
					}
					outputText.text += allLocations[i] + "\n";
					for (int m = 0; m < closestAmount; m++) outputText.text += "{"+ closestStarID[m]+"} ";
		
			outputText.text +="\n";
	}
	
	
}
*/