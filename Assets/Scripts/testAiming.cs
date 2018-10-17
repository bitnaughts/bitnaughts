using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testAiming : MonoBehaviour {

	public static GameObject[] enemies = new GameObject[0];
	public static GameObject[] friendlies = new GameObject[0];

	GameObject codeBase;
	// Use this for initialization
	void Start () {
		GameObject codeBase = GameObject.Find("CodeBase");
	}
	
	// Update is called once per frame
	void Update () {
		//enemies = GameObject.FindGameObjectsWithTag("enemy");
		//friendlies = GameObject.FindGameObjectsWithTag ("friend");
		//for (int i = 0; i < enemies.Length-1; i++) if (enemies[i].transform == null) print ("err");
		//print (enemies.Length);
	}

	/*public static GameObject checkCollision(bool friendly, Vector2 position)
	{
		if (friendly)
		{
			for (int i = 0; i < enemies.Length; i++)
			{
				if (position.x > enemies[i].transform.position.x - (enemies[i].GetComponent<ship>().size.x) && position.x < enemies[i].transform.position.x + (enemies[i].GetComponent<ship>().size.x))
				{
					if (position.y > enemies[i].transform.position.y - (enemies[i].GetComponent<ship>().size.y) && position.y < enemies[i].transform.position.y + (enemies[i].GetComponent<ship>().size.y))
					{
						return enemies[i];
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < friendlies.Length; i++)
			{
				if (position.x > friendlies[i].transform.position.x - (friendlies[i].GetComponent<ship>().size.x) && position.x < friendlies[i].transform.position.x + (friendlies[i].GetComponent<ship>().size.x))
				{
					if (position.y > friendlies[i].transform.position.y - (friendlies[i].GetComponent<ship>().size.y) && position.y < friendlies[i].transform.position.y + (friendlies[i].GetComponent<ship>().size.y))
					{
						return friendlies[i];
						
					}
				}
			}
		}
		return null;
	}*/

	public static GameObject findTarget(Vector2 shipLocation, int team)
	{
		//GameObject temp = GameObject.Find("CodeBase");
		float closestShipDistance = 100f;
		int closestShipID = -1;
		int closestShipTeam = -1;
		for (int otherTeams = 0; otherTeams < 10; otherTeams++)
		{
			if (TeamManager.getStanding(team, otherTeams) == -1)
			{
                List<GameObject> possibleTargets = TeamManager.team_lists[otherTeams];//FleetManager.getShipsInTeam(otherTeams);
				for (int i = 0; i < possibleTargets.Count; i++)
				{
					if (possibleTargets[i] != null)
					{
						float distance = Mathf.Sqrt(Mathf.Pow(shipLocation.x-possibleTargets[i].transform.position.x,2) + Mathf.Pow(shipLocation.y-possibleTargets[i].transform.position.y,2));  
						if (distance < closestShipDistance)
						{
							closestShipDistance = distance;
							closestShipID = i;
							closestShipTeam = otherTeams;
						}
					}
				}
			}
		}
		if (closestShipID != -1) return TeamManager.team_lists[closestShipTeam][closestShipID];
		else return null;
	}


	

	public static GameObject findTarget(Vector2 shipLocation, float leftAimAngle, float rightAimAngle, int team)
	{
        
        GameObject temp = GameObject.Find("CodeBase");
		float closestShipDistance = 100000f;
		int closestShipID = -1;
		int closestShipTeam = -1;
		for (int otherTeams = 0; otherTeams < 10; otherTeams++)
		{
			if (TeamManager.getStanding(team, otherTeams) == -1)
			{
                List<GameObject> possibleTargets = TeamManager.team_lists[otherTeams];
                for (int i = 0; i < possibleTargets.Count; i++)
				{
					if (possibleTargets[i] != null)
					{
                        float angle = findAngle (shipLocation, possibleTargets[i].transform.position);

						if (((rightAimAngle <  leftAimAngle) && (angle < leftAimAngle && angle > rightAimAngle)) || ((rightAimAngle >  leftAimAngle) && (angle < leftAimAngle || angle > rightAimAngle) )   ) 
						{
							float distance = Mathf.Sqrt(Mathf.Pow(shipLocation.x-possibleTargets[i].transform.position.x,2) + Mathf.Pow(shipLocation.y-possibleTargets[i].transform.position.y,2));  
							if (distance < closestShipDistance)
							{
								closestShipDistance = distance;
								closestShipID = i;
								closestShipTeam = otherTeams;
							}
						}
					}
				}
			}
		}
		if (closestShipID != -1) return TeamManager.team_lists[closestShipTeam][closestShipID];
        else return null;
	}

	public static float findAngle(Vector2 item, Vector2 target)
	{
        //return GetAngle( target.x, target.y, item.x, item.y);
		//Finding angle through tan^-1
		float angle = Mathf.Rad2Deg*Mathf.Atan((target.y-item.y)/(target.x-item.x));
		
       /* if (target.y > item.y && target.x < item.x)
        {
            return angle + 360;
        }
        if (target.y < item.y && target.x < item.x)
        {
            return angle + 0;
        }
        if (target.y < item.y && target.x > item.x)
        {
            return angle + 180;
        }
        if (target.y > item.y && target.x > item.x)
        {
            return angle + 180;
        }*/
        //Configuring produced angle to 0-360
        if (target.y < item.y)
        {
        	if(angle < 0) angle += 180f;
        	angle += 180f;			 
        }
        else if(angle < 0) angle += 180f;				
        return angle;

	}


    public static float GetAngle(float X1, float Y1, float X2, float Y2)
    {

        // take care of special cases - if the angle
        // is along any axis, it will return NaN,
        // or Not A Number.  This is a Very Bad Thing(tm).
        if (Y2 == Y1)
        {
            return (X1 > X2) ? 180 : 0;
        }
        if (X2 == X1)
        {
            return (Y2 > Y1) ? 90 : 270;
        }

        float tangent = (X2 - X1) / (Y2 - Y1);
        // convert from radians to degrees
        double ang = (float)Mathf.Atan(tangent) * 57.2958;
        // the arctangent function is non-deterministic,
        // which means that there are two possible answers
        // for any given input.  We decide which one here.
        if (Y2 - Y1 < 0) ang -= 180;


        // NOTE that this does NOT need to be normalised.  Arctangent
        // always returns an angle that is within the 0-360 range.


        // barf it back to the calling function
        return (float)ang;

    }


}
