using UnityEngine;
using System.Collections;

public static class Formations 
{
	public static Vector2 getFormationType(string type, int index, int amount, int distance)
	{
		float value = 0;
		switch (type)
		{
		case "LC":
			value = 360f/amount;
			return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (((index + 1 - 1) * value))) * distance, Mathf.Sin (Mathf.Deg2Rad * (((index + 1 - 1) * value))) * distance);
		case "HC":
			value = 180f/amount;
			return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * ((((index* value)-90)))) * distance, Mathf.Sin (Mathf.Deg2Rad * ((((index* value) +90)))) * distance);
		case "CC":
			if (amount < 8) value = 360f/(amount);
			else value = 360f/8f;
			if (index < 8)	return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (((index + 1 - 1) * value))) * distance, Mathf.Sin (Mathf.Deg2Rad * (((index + 1 - 1) * value))) * distance);
			else { distance *= 2; value = 360f/(amount-8);	return new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (((index + 1 - 1) * value))) * distance, Mathf.Sin (Mathf.Deg2Rad * (((index + 1 - 1) * value))) * distance); }
		case "CO":
			if (index % 2 == 0) return new Vector2 (((int)(index/2))*distance,0);
			else return new Vector2 ((((int)(index+1)/2))*-1*distance,0);
		case "RO":
			if (index % 2 == 0) return new Vector2 (0,((int)(index/2))*distance);
			else return new Vector2 (0,(((int)(index+1)/2))*-1*distance);
		}
		return new Vector2 (0, 0);
	}
}
