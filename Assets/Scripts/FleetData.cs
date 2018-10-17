using UnityEngine;
using System.Collections;

public class FleetData
{
	ShipObject[] ships;
	public int numberShips;

	public FleetData()
	{

	}

	public FleetData(string[] ships)
	{
		numberShips = ships.Length;
		this.ships = new ShipObject[ships.Length];
		for (int i = 0; i < ships.Length; i++)
		{
			this.ships[i] = new ShipObject(ships[i]);
		}
	}
	
	public ShipObject getShip (int index)
	{
		return ships [index];
	}
}
