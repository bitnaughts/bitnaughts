using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class informationDisplay : MonoBehaviour {

	public GameObject[] texts;
	public string[] strings;
	public GameObject[] images;
	public GameObject arrowUp;
	public GameObject arrowDown;

	public int numberEntries;

	public GameObject formSolar;
	public GameObject title;

    StarObject starInfo;
    int planetSelected;

	public int index = 0;
	// Use this for initialization
	void Start () {
		strings = new string[70];
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void display(StarObject starInfo, int planet)
	{
        this.starInfo = starInfo;
        planetSelected = planet;
        //string[] parts = starData.Split (' ');
        //string planetInfo = parts [2].Substring (2, parts [2].Length - 3);
        //string[] swag = planetInfo.Split ('}');
        //string data = swag [planet].Substring (2, swag [planet].Length - 3);
        //string[] dataArray = data.Split (',');
        //print (data);

        /*
            Enter Orbital trajectory
                Dock at space station (Random number of different stations : randomly generated names) 
                    Military station
                        SHIPYARD
                            DRYDOCKS
                            DEPOT
                        CANTINA

                    Economics station
                        MARKET
                        CANTINA

                    Civilian station
                        CANTINA

                Dock on surface
                    SHIPYARD
                    SCRAPYARD
                    MARKET
                    CANTINA
                    CAPITOL	

            Send Comms.
                PING						Get very basic data on planet, standings, and favorability
                Scan						Get detailed information about economy, pricings, planet politics, ect
                    SCAN MILITARY
                    SCAN ECONOMY
                    SCAN POPULATION		
                    SCAN GOVERNMENT
                Negotiate		
                    BRIBE					Give selected amount of money at once or a little over time to increase favorability (effectivity varies on government type)	
                    DEMAND PAYMENT			Demand payments at once or over time (lowers favorability, risk being attacked if there is a military present on the planet)
                    (if friendly)
                        REQUEST HELP
                        DEMAND HELP	
                    (if enemy)
                        COERCE HELP			Might fight back, might give resources (even military support if you overpower)
                        ASSIMILATE			Might fight back against you, requires troop transports

            Send Ships

            /*
            //**////
        //
        //	MAKE OBJECTS OF GALAXY, ALL SOLAR SYSTEMS, ALL PLANETS, ALL FEATURES/STATIONS ON PLANETS
        //	KEEP TABS ON PLANET'S ECONOMICS, POLITICS, ECT-- Graphs over time are cool
        //
        //**////
        /*





    */

        //START
        strings [0] =
			"ENTER ORBIT\t\n" + //Orbital Trajectory
			"Buy and sell ships and ship parts. Damaged products not tendered";
		strings [1] =
			"SEND COMMS\t\n" +
			"Trade scrap materials and damaged ships and ship parts";
		strings [2] =
			"SEND SHIPS\t\n" +
			"Hire employees, find missions, and hear the latest gossip and trade information";
		//ENTER ORBIT
		strings [10] =
			"DOCK ON SURFACE\t\n" + //Orbital Trajectory
			"Buy and sell ships and ship parts. Damaged products not tendered";
		for (int i = 0; i < /*planet.stations.length*/ 5; i++)
		{
			strings [11+i] =
				"STARBASE ALPHA\t\n" +
				"blablabla space station info/stat/data/stuff/tech level";
		}	
		//SEND COMMS
		strings [20] =
			"PING\t\n" + //Orbital Trajectory
			"Force a response from the planet, gives simple specs of the planet and its inhabitants";
		strings [21] =
			"SCAN\t\n" +
			"Preform an intrustive scan of the planet, outputting more detailed information";
		strings [22] =
			"NEGOTIATE\t\n" +
			"Open a two-way channel with a representative of the planet to discuss various subjects";
		//SEND SHIPS
		strings [30] =
			"SHIPS";
		//DOCK ON SURFACE


		for (int i = 0; i < starInfo.planets[planet].buildings.Count; i++)
		{
            BuildingObject obj = starInfo.planets[planet].buildings[i];

            strings[40 + i] = obj.name + "\t\n" + obj.description; /*planet.surfaceStructures[i].ToString()*/;
		}
		//SCAN
		strings[50] = 
			"SCAN MILITARY\t\n" +
			"Scan the planet's miltary capabilities and technologies";
		strings[51] = 
			"SCAN ECONOMY\t\n" +
			"Scan the planet's economic productivity and exchanges";
		strings[52] = 
			"SCAN GOVERNMENT\t\n" +
			"Scan the planet's baises, perceptions and ideologies";
		//NEGOTIATE
		strings[50] = 
			"BRIBE\t\n" +
			"Scan the planet's miltary capabilities and technologies";
		strings[51] = 
			"DEMAND\t\n" +
			"Scan the planet's economic productivity and exchanges";
		strings[52] = 
			"HELP\t\n" +
			"Scan the planet's baises, perceptions and ideologies";
		strings[53] =
			"DEMAND HELP\t\n" +
			"";
        //--request assimilation
        //demand, coerce


        //"Production\t\t" + ": " +(Mathf.Pow(float.Parse(dataArray [2]),3f)) + "\n\n" +
        //"Upgrade" + "\n" +
        //"Cost\t\t" + ": " + (Mathf.Pow(float.Parse(dataArray [2]),3f));
        //strings [3] =
        ////	"CAPITOL\t\n" + //+ ": " + dataArray [3] + "\n" +
        //	"Negotiate with the local inhabitants on a variety of subjects";
        //"Increases effectivity" + "\n" +
        //"Multiplier\t\t" + ": " +(dataArray[3]+1) + "\n\n" +
        //"Upgrade" + "\n" +
        //	"Cost\t\t" + ": " + (float.Parse(dataArray[3])+1)*1000;
        //strings [4] =
        //"SHIPYARD\t\t" + ": " + dataArray [4] + "\n" +
        //	"Builds private and military vessels"; //+ "\n" //+

        /*strings [10] =
			"SHIPYARD\t\t" + ": " + dataArray [4] + "\n" +
			"The backbone of any planet, come to trade weapons, equipment, and ships" + "\n\n\n" +
			"Upgrade" + "\n" +
			"Cost\t\t" + ": " + (Mathf.Pow(float.Parse(dataArray [2]),3f));


		strings [11] =
			"Buy Items\t" + "\n" +
			"Trade credits for new ships and items"; //+ "\n" //+
		strings [12] =
			"Sell Items\t" + "\n" +
			"Trade non-damaged ships and items for credits"; //+ "\n" //+

		//strings [13] =
			//"Sell Ships\t" + "\n" +
			//"Large military vessels"; //+ "\n" //+


		strings [20] =
			"FIGHTER\t" + "\n" +
			"Effective in swarms, it takes hits and damages smaller targets"; //+ "\n" //+
		strings [21] =
			"BOMBER\t" + "\n" +
			"Armed with missiles, it is adept at taking out large targets if in range"; //+ "\n" //+
		strings [22] =
			"ATTACKER\t" + "\n" +
			"An upgraded fighter specialized in intercepting small targets"; //+ "\n" //+


		strings [30] =
			"TRADER\t" + "\n" +
			"Carrying supplies to sell, but fragile to enemy attacks"; //+ "\n" //+
		strings [31] =
			"ARMED_TRADER\t" + "\n" +
			"Carrying supplies to sell, it turns a profit while being protected"; //+ "\n" //+
		strings [32] =
			"TROOP_TRANSPORT\t" + "\n" +
			"Loaded with soldiers, it is able to colonize and invade planets"; //+ "\n" //+
		strings [33] =
			"CORVETTE\t" + "\n" +
			"A supersized fighter perfect for chasing down pesky bombers and other small targets"; //+ "\n" //+
		strings [34] =
			"CRUISER\t" + "\n" +
			"Exotic in shape and weapon placement, it takes down smaller ships with efficient ease."; //+ "\n" //+
		strings [35] =
			"FRIGATE\t" + "\n" +
			"With a powerful plasma cannon, it packs high damage potentials in a small blueprint."; //+ "\n" //+
		strings [36] =
			"DESTROYER\t" + "\n" +
			"Designed to unload multitudes of missile, it can bring down even the biggest ships."; //+ "\n" //+
		strings [40] =
			"BATTLESHIP\t" + "\n" +
			"Well-rounded to destroy targets big and small. It can hold out against many more targets."; //+ "\n" //+
		strings [41] =
			"CARRIER\t" + "\n" +
			"However lacking in raw fire, it is capable of sending out swarms of smaller ships.";// What it lacks in raw firepoint is made up by its hanger."; //+ "\n" //+
		strings [42] =
			"DREADNOUGHT\t" + "\n" +
			"Bulky and slow, but packs a punch. It can take on most anything and win."; //+ "\n" //+
		strings [43] =
			"FLAGSHIP\t" + "\n" +
			"The biggest and baddest of them all, a fleet is not complete without one of these at its center."; //+ "\n" //+




		//if enemy 	:sneak onto surface - fail :: battle/fine/ect
		//			:	
/*

		strings [0] =
			"SHIPYARD\t\n" + //": " + dataArray [0] + "\n" +
			"Buy and sell ships and ship parts. Damaged products not tendered";
			//"Production\t\t" + ": " +(Mathf.Pow(float.Parse(dataArray [0]),3f)) + "\n\n" +
			//"Upgrade" + "\n" +
			//"Cost\t\t" + ": " + (Mathf.Pow(float.Parse(dataArray [0]),3f));
		strings [1] =
			"SCRAPYARD\t\t\t\n" + //": " + dataArray [1] + "\n" +
			"Trade scrap materials and damaged ships and ship parts";
			//"Production\t\t" + ": " +(Mathf.Pow(float.Parse(dataArray [1]),3f)) + "\n\n" +
			//"Upgrade" + "\n" +
			//"Cost\t\t" + ": " + (Mathf.Pow(float.Parse(dataArray [1]),3f));
		strings [2] =
			"CANTINA\t\t\n" + //": " + dataArray [2] + "\n" +
			"Hire employees, find missions, and hear the latest gossip and trade information";
			//"Production\t\t" + ": " +(Mathf.Pow(float.Parse(dataArray [2]),3f)) + "\n\n" +
			//"Upgrade" + "\n" +
			//"Cost\t\t" + ": " + (Mathf.Pow(float.Parse(dataArray [2]),3f));
		strings [3] =
			"CAPITOL\t\n" + //+ ": " + dataArray [3] + "\n" +
			"Negotiate with the local inhabitants on a variety of subjects";
			//"Increases effectivity" + "\n" +
			//"Multiplier\t\t" + ": " +(dataArray[3]+1) + "\n\n" +
			//"Upgrade" + "\n" +
			//	"Cost\t\t" + ": " + (float.Parse(dataArray[3])+1)*1000;
		//strings [4] =
			//"SHIPYARD\t\t" + ": " + dataArray [4] + "\n" +
		//	"Builds private and military vessels"; //+ "\n" //+

		strings [10] =
			"SHIPYARD\t\t" + ": " + dataArray [4] + "\n" +
			"The backbone of any planet, come to trade weapons, equipment, and ships" + "\n\n\n" +
			"Upgrade" + "\n" +
			"Cost\t\t" + ": " + (Mathf.Pow(float.Parse(dataArray [2]),3f));


		strings [11] =
			"Buy Items\t" + "\n" +
			"Trade credits for new ships and items"; //+ "\n" //+
		strings [12] =
			"Sell Items\t" + "\n" +
			"Trade non-damaged ships and items for credits"; //+ "\n" //+

		//strings [13] =
			//"Sell Ships\t" + "\n" +
			//"Large military vessels"; //+ "\n" //+


		strings [20] =
			"FIGHTER\t" + "\n" +
			"Effective in swarms, it takes hits and damages smaller targets"; //+ "\n" //+
		strings [21] =
			"BOMBER\t" + "\n" +
			"Armed with missiles, it is adept at taking out large targets if in range"; //+ "\n" //+
		strings [22] =
			"ATTACKER\t" + "\n" +
			"An upgraded fighter specialized in intercepting small targets"; //+ "\n" //+


		strings [30] =
			"TRADER\t" + "\n" +
			"Carrying supplies to sell, but fragile to enemy attacks"; //+ "\n" //+
		strings [31] =
			"ARMED_TRADER\t" + "\n" +
			"Carrying supplies to sell, it turns a profit while being protected"; //+ "\n" //+
		strings [32] =
			"TROOP_TRANSPORT\t" + "\n" +
			"Loaded with soldiers, it is able to colonize and invade planets"; //+ "\n" //+
		strings [33] =
			"CORVETTE\t" + "\n" +
			"A supersized fighter perfect for chasing down pesky bombers and other small targets"; //+ "\n" //+
		strings [34] =
			"CRUISER\t" + "\n" +
			"Exotic in shape and weapon placement, it takes down smaller ships with efficient ease."; //+ "\n" //+
		strings [35] =
			"FRIGATE\t" + "\n" +
			"With a powerful plasma cannon, it packs high damage potentials in a small blueprint."; //+ "\n" //+
		strings [36] =
			"DESTROYER\t" + "\n" +
			"Designed to unload multitudes of missile, it can bring down even the biggest ships."; //+ "\n" //+
		strings [40] =
			"BATTLESHIP\t" + "\n" +
			"Well-rounded to destroy targets big and small. It can hold out against many more targets."; //+ "\n" //+
		strings [41] =
			"CARRIER\t" + "\n" +
			"However lacking in raw fire, it is capable of sending out swarms of smaller ships.";// What it lacks in raw firepoint is made up by its hanger."; //+ "\n" //+
		strings [42] =
			"DREADNOUGHT\t" + "\n" +
			"Bulky and slow, but packs a punch. It can take on most anything and win."; //+ "\n" //+
		strings [43] =
			"FLAGSHIP\t" + "\n" +
			"The biggest and baddest of them all, a fleet is not complete without one of these at its center."; //+ "\n" //+


*/

        
        updateText ();
	}

	public void arrow(bool up)
	{
		if (formSolar.GetComponent<solarReport>().buttonTier == 0) numberEntries = 2;
		if (formSolar.GetComponent<solarReport>().buttonTier == 10) numberEntries = 1 + 5;
		if (formSolar.GetComponent<solarReport>().buttonTier == 20) numberEntries = 2;
		if (formSolar.GetComponent<solarReport>().buttonTier == 30) numberEntries = 6;
        if (formSolar.GetComponent<solarReport>().buttonTier == 40) numberEntries = starInfo.planets[planetSelected].buildings.Count-1;


        if (up && index > 0) {index--;	updateText ();}
		else if (!up && index < numberEntries-2) {index++;	updateText ();}
		if (index == 0)	arrowUp.SetActive (false);
		else arrowUp.SetActive (true);
		if (index == numberEntries-2) arrowDown.SetActive (false);
		else arrowDown.SetActive (true);
	}

	public void updateText()
	{
		print (index + formSolar.GetComponent<solarReport> ().buttonTier);
		for (int i = 0; i < 3; i++)	texts [i].GetComponent<textAnimator> ().changeText(strings [i+index+formSolar.GetComponent<solarReport>().buttonTier]);
		for (int i = 0; i < 3; i++) images [i].GetComponent<Image> ().sprite = (Resources.Load ("sprites\\UIchoices\\" + strings [i + index+formSolar.GetComponent<solarReport>().buttonTier].Split ('\t') [0].ToLower()) as GameObject).GetComponent<SpriteRenderer> ().sprite;
	}

	

}
