using UnityEngine;
using System.Collections;

public class HardpointObject
{

	Vector2 localPosition;
	int mainAngle;
	int leftAngle;
	int rightAngle;
	int numberAngle;

	string size;
	string equippedItem;
	public ItemObject item;

    string ammoType;
    int ammoAmount;

	public HardpointObject (Vector2 localPosition, int mainAngle, int leftAngle, int rightAngle, int numberAngle, string size)
	{
		this.localPosition = localPosition;
		this.mainAngle = mainAngle;
		this.leftAngle = leftAngle;
		this.rightAngle = rightAngle;
		this.numberAngle = numberAngle;
		this.size = size;
	}

	public void equip(string[] input)
	{
        item = new ItemObject(input);
		//item = new ItemObject(input, size);
		//if (input.Substring(1) == "M") this.leftAngle = -1;
	}
//	public string getItem()
//	{
//		return item.getShortName();
//	}

	public int[] getValues()
	{
		return new int[] {mainAngle, leftAngle, rightAngle, numberAngle};
	}
	public Vector3 getPosition()
	{
		return new Vector3 (localPosition.x, localPosition.y, -1);
	}
	public int getMainAngle()
	{
		return mainAngle;
	}
	public string getSize()
	{
		return size;
	}
    public string getEquippedAmmo()
    {
        return ammoType;
    }
    public int getEquippedAmmoAmount()
    {
        return ammoAmount;
    }
    public void load(string ammo, int amount)
    {
        ammoType = ammo;
        ammoAmount = amount;
    }

//    public string getEquippedItem()
	//{
	//	return item.getShortName();
		/*switch (equippedItem) 
		{
			case "CR": return "ConventionalRifle";
			case "CC": return "ConventionalCannon";
			case "CF": return "ConventionalFlakCannon";
			case "CM": return "ConventionalMissile";
			case "CO": return "ConventionalRocket";
			case "CL": return "ConventionalClusterMissile";
			case "LR": return "LaserBeam";
			case "LC": return "LaserCannon";
			case "PC": return "PlasmaCannon";
			case "PR": return "PlasmaRifle";
			case "PM": return "PlasmaMissile";
			case "PO": return "PlasmaRocket";
			case "PL": return "PlasmaClusterMissile";

		}
		return "NotRegistered";*/
	//}//
}
