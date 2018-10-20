using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShipObject
{
	public HardpointObject[] externalPoints;

    public List<ItemObject> inventory = new List<ItemObject>();



	float lifePoints;
	string type;
	int team;

	public ShipObject(string data)
	{

		this.type = data.Substring(0,data.IndexOf(":"));
		setHardpoints();
		this.team = int.Parse(data.Substring(data.IndexOf(":")+1,1));
        //fighter:0:{M5,C5,V5,P5,N5,C}&{}
        string items = data.Substring(data.IndexOf(":") + 2);
        //{M5,C5,V5,P5,N5,C}&{}
        string hardpoints = items.Substring(0, items.IndexOf("&"));
        //{M5,C5,V5,P5,N5,C}
        equipHardpoints(hardpoints);
        //string modules = items.Substring(items.IndexOf(":"), items.IndexOf("*"));

        // string ammo = items.Substring(items.IndexOf("*"));


    }

    private void equipHardpoints(string values)
	{
        string[] items = values.Split('}');
        //{M5,C5,V5,P5,N5,C
        items[0] = items[0].Substring(1);
        //M5,C5,V5,P5,N5,C
        string[] parts = new string[6];
        for (int i = 0; i < items.Length-1; i++)
		{
            items[i] = items[i].Substring(1);
            parts = items[i].Split(',');

            externalPoints[i].equip(parts);//items[i].Substring(1,2)); //R1
            //externalPoints[i].load(items[i].Substring(items[i].IndexOf(":")+1, 2), int.Parse(items[i].Substring(items[i].IndexOf("-")+1))); //C1, 100
        }
    }

    private void setHardpoints()
	{
		switch (type)
		{
			case "fighter":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,.1f),90,110,70,0,"small")};
				lifePoints = 20f;
			break;
			case "shuttle":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,.1f),90,120,60,0,"small")};
				lifePoints = 30f;
			break;
			case "assaulter":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,.175f),90,110,70,0,"small"),new HardpointObject(new Vector2(0f,-.06f),90,-1,-1,-1,"small")};
				lifePoints = 60f;
			break;
			case "bomber":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,.285f),90,135,45,0,"medium")};
				lifePoints = 100f;
			break;
			case "gunboat":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,.42f),90,100,80,0,"large"),new HardpointObject(new Vector2(-.27f,0),120,150,90,0,"small"),new HardpointObject(new Vector2(.27f,0),60,90,30,0,"small")};
				lifePoints = 250f;
			break;
			case "corvette":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,.505f),90,110,70,0,"small"),new HardpointObject(new Vector2(-.155f,.395f),90,135,60,0,"small"),new HardpointObject(new Vector2(.155f,.395f),90,120,45,0,"small"),new HardpointObject(new Vector2(-.335f,.095f),135,135,70,0,"small"),new HardpointObject(new Vector2(.335f,.095f),45,110,45,0,"small")};
				lifePoints = 550f;
			break;
			case "liner":
				externalPoints = new HardpointObject[] {};
				lifePoints = 500f;
			break;
			case "freighter":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,.635f),90,135,45,0,"small")};
				lifePoints = 600f;
			break;
			case "frigate":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(0f,1.135f),90,110,70,0,"large"),new HardpointObject(new Vector2(-.385f,.575f),90,135,60,0,"medium"),new HardpointObject(new Vector2(.385f,.575f),90,120,45,0,"medium"),new HardpointObject(new Vector2(0f,-.725f),90,-1,-1,-1,"small")};
				lifePoints = 3000f;
			break;
			case "cruiser":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(-.255f,1.105f),90,135,60,0,"small"),new HardpointObject(new Vector2(.255f,1.105f),90,120,45,0,"small"),new HardpointObject(new Vector2(-.295f,.965f),100,145,90,0,"small"),new HardpointObject(new Vector2(.305f,.965f),80,90,35,0,"small"),new HardpointObject(new Vector2(-.315f,.827f),110,155,90,0,"small"),new HardpointObject(new Vector2(.325f,.825f),70,90,25,0,"small"),new HardpointObject(new Vector2(-.555f,.175f),100,150,80,0,"small"),new HardpointObject(new Vector2(.555f,.175f),80,110,30,0,"small"),new HardpointObject(new Vector2(-.425f,-.575f),180,225,135,0,"medium"),new HardpointObject(new Vector2(.435f,-.575f),0,45,315,90,"medium")};
				lifePoints = 3250f;
			break;
			case "destroyer":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(-.3f,1.06f),90,120,60,0,"medium"),new HardpointObject(new Vector2(.31f,1f),90,110,70,0,"medium"),new HardpointObject(new Vector2(.43f,.85f),90,120,45,0,"small"),new HardpointObject(new Vector2(-.43f,.48f),90,100,80,0,"medium"),new HardpointObject(new Vector2(-.43f,.18f),180,210,150,0,"medium"),new HardpointObject(new Vector2(.43f,.18f),0,30,330,60,"medium"),new HardpointObject(new Vector2(-.43f,-.18f),180,210,150,0,"medium"),new HardpointObject(new Vector2(.43f,-.18f),0,30,330,60,"medium"),new HardpointObject(new Vector2(-.43f,-.54f),180,210,150,0,"medium"),new HardpointObject(new Vector2(.43f,-.54f),0,30,330,60,"medium")};
				lifePoints = 3750;
			break;
			case "battleship":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(-.47f,1.165f),90,135,70,0,"large"),new HardpointObject(new Vector2(.47f,1.13f),90,110,45,0,"large"),new HardpointObject(new Vector2(-.5f,.4f),90,120,60,0,"medium"),new HardpointObject(new Vector2(.49f,.42f),90,120,60,0,"medium"),new HardpointObject(new Vector2(0f,-.03f),90,100,80,0,"large"),new HardpointObject(new Vector2(-.66f,-.15f),180,260,110,0,"small"),new HardpointObject(new Vector2(.66f,-.15f),0,80,280,160,"small"),new HardpointObject(new Vector2(-.66f,-.265f),180,260,110,0,"small"),new HardpointObject(new Vector2(.66f,-.265f),0,80,280,160,"small")};
				lifePoints = 6500;
			break;
			case "carrier":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(-.33f,1.26f),90,120,60,0,"medium"),new HardpointObject(new Vector2(.33f,1.26f),90,120,60,0,"medium"),new HardpointObject(new Vector2(-.51f,1.02f),90,100,80,0,"small"),new HardpointObject(new Vector2(.51f,1.02f),90,100,80,0,"small"),new HardpointObject(new Vector2(-.6f,-.07f),120,110,70,0,"small"),new HardpointObject(new Vector2(.6f,-.07f),60,110,70,0,"small")};
				lifePoints = 6750;
			break;
			case "dreadnought":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(-.44f,1.345f),90,120,60,0,"medium"),new HardpointObject(new Vector2(.44f,1.345f),90,120,60,0,"medium"),new HardpointObject(new Vector2(-.605f,1.035f),90,160,70,0,"small"),new HardpointObject(new Vector2(.595f,1.035f),90,110,20,0,"small"),new HardpointObject(new Vector2(0,-.035f),90,100,80,0,"medium"),new HardpointObject(new Vector2(-.585f,-.1f),90,120,60,0,"medium"),new HardpointObject(new Vector2(.565f,-.1f),90,110,70,0,"large"),new HardpointObject(new Vector2(.69f,-.76f),0,30,330,60,"large")};
				lifePoints = 8000;
			break;
			case "flagship":
				externalPoints = new HardpointObject[] {new HardpointObject(new Vector2(-.235f,1.55f),90,150,30,0,"small"),new HardpointObject(new Vector2(.235f,1.55f),90,150,30,0,"small"),new HardpointObject(new Vector2(-.405f,1.42f),90,135,45,0,"medium"),new HardpointObject(new Vector2(.405f,1.42f),90,135,45,0,"medium"),new HardpointObject(new Vector2(-.615f,.5f),90,120,60,0,"large"),new HardpointObject(new Vector2(.615f,.5f),90,120,60,0,"large"),new HardpointObject(new Vector2(0,.52f),90,110,70,0,"large"),new HardpointObject(new Vector2(-.085f,.025f),90,-1,-1,-1,"small"),new HardpointObject(new Vector2(.085f,.025f),90,-1,-1,-1,"small"),new HardpointObject(new Vector2(-.675f,-.87f),180,265,95,0,"small"),new HardpointObject(new Vector2(.675f,-.87f),0,85,275,170,"small"),new HardpointObject(new Vector2(-.675f,-.97f),180,265,95,0,"small"),new HardpointObject(new Vector2(.675f,-.97f),0,85,275,170,"small")};
				lifePoints = 10000;
			break;
		}
	}
	public HardpointObject getHardpoint(int index)
	{
		return externalPoints [index];
	}
	public string getType()
	{
		return type;
	}
	public int getTeam()
	{
		return team;
	}

	public float getLifePoints()
	{
		return lifePoints;
	}

}
