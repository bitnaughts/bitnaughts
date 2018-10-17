using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class solarReport : MonoBehaviour {

    public GalaxyManager galaxyManager;

	public GameObject title;
	public GameObject galaxyTab;
	public GameObject shipyardTab;
	public GameObject scrapyardTab;
	public GameObject canteenTab;
	public GameObject capitolTab;

	public bool opened = false;
	public bool over = false;

	public GameObject[] planets = new GameObject[8];

	public GameObject star;

	public GameObject planetHolder;

	public GameObject sideBar;
	public GameObject sideBarItems;

	public StarObject starInfo;

	public int numberPlanets;

	public int buttonTier;

	int selectedPlanet = 0;

	public GameObject dataInformation;

    public GameObject planetSelector;


    // Use this for initialization
    void Start () 
	{
        galaxyManager = GameObject.Find("CodeBase").GetComponent<GalaxyManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.GetComponent<Image> ().enabled = opened;
        planetSelector.transform.position = planets[selectedPlanet].transform.position;
        planetSelector.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }


	public void planetButtonClick(int button)
	{

        int clickIndex = button + sideBar.GetComponent<informationDisplay> ().index + buttonTier;
		//for (int i = 0; i < 9; i ++)	print (thingClicked);


        //All possible Solar Report click buttons
        switch(clickIndex)
        {
            case 0: buttonTier = 10; break; //Options for landing on surface, star bases, ect
            case 10: buttonTier = 40; break; //Options for accessing surface items
            case 42: galaxyManager.GUI_interfacePanel.GetComponent<GUIManager_InterfacePanel>().loadShipyard(galaxyManager.fleets[0].ships[0]);  break; //open shipyard


        }
        sideBar.GetComponent<informationDisplay>().index = 1;
        sideBar.GetComponent<informationDisplay>().arrow(true);


        /*if (thingClicked == 3) 
		{	/*technology*/
			//TODO upgrade techonology level

		//}
		//if (thingClicked == 0) //4
		//{	
			/*Enter Orbit*/
			//title.GetComponent<TitleDisplay>().updateMessage("SHIPYARD");
			//galaxyTab.SetActive(false);
			//shipyardTab.SetActive(true);
		//	buttonTier += 10;

		//}
		//if (thingClicked == 1) {
			/*Send Comms
		//	title.GetComponent<TitleDisplay>().updateMessage("SCRAPYARD");
		//	galaxyTab.SetActive(false);
		//	shipyardTab.SetActive(true);
			buttonTier += 20;
			sideBar.GetComponent<informationDisplay> ().index = 1;
			sideBar.GetComponent<informationDisplay> ().arrow(true);


		}
		if (thingClicked == 2) {
			title.GetComponent<TitleDisplay>().updateMessage("CANTEEN");
			galaxyTab.SetActive(false);
			canteenTab.SetActive(true);
		}
		if (thingClicked == 3) {
			title.GetComponent<TitleDisplay>().updateMessage("CAPITOL");
			galaxyTab.SetActive(false);
			capitolTab.SetActive(true);
		}
		if (thingClicked >= 10 && thingClicked < 20) 
		{
			if (thingClicked == 10) 
			{
                buttonTier = 40;
                sideBar.GetComponent<informationDisplay>().index = 1;
                sideBar.GetComponent<informationDisplay>().arrow(true);
            }	
			if (thingClicked == 11) 
			{ 
				buttonTier += 10; //20s
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
			}
			if (thingClicked == 12) 
			{ 
				buttonTier += 20; //30s
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
			}
			if (thingClicked == 13) 
			{ 
				buttonTier += 30; //40s
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
			}
		}
		if (thingClicked >= 20) 
		{
			if (thingClicked == 20) 
			{

            }	
			if (thingClicked == 21) 
			{ 
				buttonTier += 30; //50s
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
			}
			if (thingClicked == 22) 
			{ 
				buttonTier += 20; //30s
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
			}
			if (thingClicked == 23) 
			{ 
				buttonTier += 30; //40s
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
			}
		}*/

	}	
	public void planetClick (bool opening, int planet)//,// string starInfo)
	{
		sideBarItems.SetActive (opening);
        
		if (opening) 
		{
			buttonTier = 0;
			sideBar.GetComponent<RectTransform> ().localPosition = new Vector2 (281, -6);
			sideBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (250, 340);
			sideBar.GetComponent<Image> ().sprite = (Resources.Load ("sprites/planetMenuOpen") as GameObject).GetComponent<SpriteRenderer> ().sprite;
			selectedPlanet = planet;
			sideBar.GetComponent<informationDisplay>().display(starInfo, planet);
			sideBar.GetComponent<informationDisplay> ().arrow(true);
            planetSelector.SetActive(opening);
           
        }
		else
		{
			if (buttonTier == 0)
			{
                planetSelector.SetActive(opening);
                sideBar.GetComponent<RectTransform> ().localPosition = new Vector2 (203, -6);
				sideBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (94, 340);
				sideBar.GetComponent<Image> ().sprite = (Resources.Load ("sprites/planetMenuClosed") as GameObject).GetComponent<SpriteRenderer> ().sprite;
			}
			else if (buttonTier == 10) 
			{
				sideBarItems.SetActive (true);
				buttonTier = 0;
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
				//sideBar.GetComponent<informationDisplay>().display(starInfo, selectedPlanet);

				
			}
			else if (buttonTier > 10)
			{
				dataInformation.GetComponent<dataReport>().open(false, "");
				sideBarItems.SetActive (true);
				buttonTier = 10;
				sideBar.GetComponent<informationDisplay> ().index = 1;
				sideBar.GetComponent<informationDisplay> ().arrow(true);
				//sideBar.GetComponent<informationDisplay>().display(starInfo, selectedPlanet);
			
			}
		
		}



	}

	public void open(bool opening, StarObject starInfo)
	{
  		opened = opening;
		if (opened) {
	        this.starInfo = starInfo;
			/*GUI*/
			sideBar.SetActive(true);
			sideBar.GetComponent<RectTransform>().localPosition = new Vector2 (203,-6);
			sideBar.GetComponent<RectTransform>().sizeDelta = new Vector2(94,340);
			/*STAR*/
			star.GetComponent<Image>().enabled = true;
			star.GetComponent<Image>().sprite = (Resources.Load("star" + starInfo.planets.Count / 3) as GameObject).GetComponent<SpriteRenderer>().sprite;
			/*PLANETS*/
			//string[] parts = starInfo.Split (' ');
			//s//tring planetInfo = parts [2].Substring (2, parts [2].Length - 3);
			//string[] swag = planetInfo.Split ('}');
			for (int i = 0; i < starInfo.planets.Count; i++) 
			{
				//positioning
				planets [i].GetComponent<Image> ().enabled = true;
				float value = 360f /(starInfo.planets.Count);
				float distance = 100f;
				planets [i].GetComponent<RectTransform>().localPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad*(((i+1-1)*value))) * distance,Mathf.Sin (Mathf.Deg2Rad*(((i+1-1)*value)))* distance);
				//image
				planets [i].GetComponent<Image>().sprite = (Resources.Load("sprites/planet" + starInfo.planets[i].habitability) as GameObject).GetComponent<SpriteRenderer>().sprite;
			}
			numberPlanets = starInfo.planets.Count;
			planetHolder.GetComponent<planetRotation>().randomizeRotation();
		}
		else 
		{
            planetSelector.SetActive(opening);
            for (int i = 0; i < planets.Length; i++)
			{
				star.GetComponent<Image>().enabled = false;
				planets[i].GetComponent<Image>().enabled = false;
				sideBar.SetActive(false);
				//add code to make sure all image components are disabled when form is closed
			}
		}
	}
}
