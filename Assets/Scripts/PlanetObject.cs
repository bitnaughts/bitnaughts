using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetObject
{
    public int habitability;

    public List<BuildingObject> buildings = new List<BuildingObject>();
    /*			
            SHIPYARD (new ships, weapons, equipment)
			SCRAPYARD (metal, destroyed ship loot)
            LAB (upgrades?)
			MARKET (raw goods, tradeable items)
			CANTINA (people, missions, ect)
			CAPITOL	*/

    //shipyard, scrapyard, ect objects (BuildingObject, set ids of things to sell, ect)
    //
    //give starting seed for generation, then keep data in object, save with .ToString() info
    //

    //government info through capitol object

    int team;
    int favorability;

    public PlanetObject()
    {


        buildPlanet();
    }

    void buildPlanet()
    {
        habitability = (int)(UnityEngine.Random.value * 10) + 1;

        //weapons, ammo
        buildings.Add(new BuildingObject("ARMORY", 100));
        //mercenaries, crew, missions, gossip, ect
        buildings.Add(new BuildingObject("CANTINA", 100));
        //ships, modules, repair, equipping
        buildings.Add(new BuildingObject("SHIPYARD", 100));
        //selling metal/scrap materials
        buildings.Add(new BuildingObject("SCRAPYARD", 100));
        //selling trade items (items whose only value is in gaming trade markets)
        buildings.Add(new BuildingObject("MARKET", 100));
        //info on government, other info
        buildings.Add(new BuildingObject("CAPITOL", 100));

        // if (habitability >= 7) //Only habitable planets are part of bigger groups?
        //{
        //    buildings.Add(new BuildingObject("CAPITOL", 100));
        //}
    }

    
    public override string ToString()
    {
        return habitability + "," + team;
    }


    /*

         //	SHIPYARD
     // SCRAPYARD
     // MARKET
     // CANTINA
     //  CAPITOL

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

        */


}
