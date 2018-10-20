using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour {

	public static int[,] standings = new int[10,10];
    public static List<GameObject>[] team_lists = new List<GameObject>[10];
    

    public static void updateStanding(int team1, int team2, int standing)
	{

		standings[team1,team2] = standing;
		//standings[team2,team1] = standing;	//Only once they are attacked?

	}  
	public static int getStanding(int team1, int team2)
	{
		updateStanding(0,1,-1);
		updateStanding(1,0,-1);
		return standings[team1,team2];
	}
}
