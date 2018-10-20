using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingObject
{
    public string name;
    public string description;

    List<ItemObject> tradeableItems = new List<ItemObject>();


    public BuildingObject(string type, int seed)
    {
        name = type;

        description = setDescription();
    }

    public string setDescription()
    {
        switch (name)
        {
            case "ARMORY": return "Purchase weapons and ordnances from mostly legal sources";
            case "CANTINA": return "Hire employees, find missions, and hear the latest gossip and trade information";
            case "SHIPYARD": return "Order the construction of ships, buy modules, and outfit your fleet";
            case "SCRAPYARD": return "Trade in large quantities of scrap metal, typically from space battles";
            case "MARKET": return "Bargain for goods and credits, prices based on the planet's economy";
            case "CAPITOL": return "Negotiate with the local inhabitants on a variety of subjects";
        }
        return "";
    }





    //things to be "purchased" use possible IDs determined by seed
    //IDs organized to have a #-###
    // (type of item)-(###) item
    // shipyard-corvette (maybe a "luxury" modifier, others ect)



}
