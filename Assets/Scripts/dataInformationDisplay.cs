using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class dataInformationDisplay : MonoBehaviour {
	public string[] strings;
	public GameObject text;
	public GameObject image;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void display(string input)
	{
		strings [0] =
			"FIGHTER" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t1" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t10" + "\n" +
			"-Cargo:\t\t\t\t\t25T" + "\n" +
			"-Hitpoints:\t\t\t20" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t45,000C"; 
		strings [1] =
			"BOMBER" + "\n" +
			"Hardpoints:" + "\n" +
			"-Medium Fixed:\t1" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t8" + "\n" +
			"-Cargo:\t\t\t\t\t135T" + "\n" +
			"-Hitpoints:\t\t\t30" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t235,000C"; 
		strings [2] =
			"ATTACKER" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t1" + "\n" +
			"-Small Turret:\t\t1" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t7" + "\n" +
			"-Cargo:\t\t\t\t\t85T" + "\n" +
			"-Hitpoints:\t\t\t50" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t85,000C"; 
		strings [3] =
			"TRADER" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t4" + "\n" +
			"-Cargo:\t\t\t\t\t3,750T" + "\n" +
			"-Hitpoints:\t\t\t2,250" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t385,000C"; 
		strings [4] =
			"ARMED_TRADER" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t2" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t4" + "\n" +
			"-Cargo:\t\t\t\t\t3,500T" + "\n" +
			"-Hitpoints:\t\t\t2,750" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t575,000C"; 
		strings [5] =
			"TROOP_TRANSPORT" + "\n" +
			"Hardpoints:" + "\n" +
			"-Barracks:\t\t\t1" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t4" + "\n" +
			"-Cargo:\t\t\t\t\t300T" + "\n" +
			"-Hitpoints:\t\t\t2,550" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t400,000C"; 
		strings [6] =
			"CORVETTE" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t5" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t5" + "\n" +
			"-Cargo:\t\t\t\t\t250T" + "\n" +
			"-Hitpoints:\t\t\t1,350" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t525,000C"; 
		strings [7] =
			"CRUISER" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t10" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t4" + "\n" +
			"-Cargo:\t\t\t\t\t650T" + "\n" +
			"-Hitpoints:\t\t\t3,850" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t2,950,0000C"; 
		strings [8] =
			"FRIGATE" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Turret:\t\t1" + "\n" +
			"-Medium Fixed:\t2" + "\n" +
			"-Large Fixed:\t\t1" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t3" + "\n" +
			"-Cargo:\t\t\t\t\t1,250T" + "\n" +
			"-Hitpoints:\t\t\t6,000" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t16,500C"; 
		strings [9] =
			"DESTROYER" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t3" + "\n" +
			"-Medium Fixed:\t6" + "\n" +
			"-Large Fixed:\t\t1" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t4" + "\n" +
			"-Cargo:\t\t\t\t\t825T" + "\n" +
			"-Hitpoints:\t\t\t4,750" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t3,550,000C"; 
		strings [10] =
			"BATTLESHIP" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t5" + "\n" +
			"-Medium Fixed:\t4" + "\n" +
			"-Large Fixed:\t\t1" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t3" + "\n" +
			"-Cargo:\t\t\t\t\t750T" + "\n" +
			"-Hitpoints:\t\t\t10,250" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t4,750,000C"; 
		strings [11] =
			"CARRIER" + "\n" +
			"Hardpoints:" + "\n" +
			"-Ship Bay:\t\t\t\t1" + "\n" +
			"-Small Fixed:\t\t4" + "\n" +
			"-Medium Fixed:\t2" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t3" + "\n" +
			"-Cargo:\t\t\t\t\t2,750T" + "\n" +
			"-Hitpoints:\t\t\t14,250" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t5,525,000C"; 
		strings [12] =
			"DREADNOUGHT" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Turret:\t\t1" + "\n" +
			"-Medium Fixed:\t2" + "\n" +
			"-Large Fixed:\t\t5" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t2" + "\n" +
			"-Cargo:\t\t\t\t\t3,000T" + "\n" +
			"-Hitpoints:\t\t\t14,750" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t8,250,000C"; 
		strings [13] =
			"FLAGSHIP" + "\n" +
			"Hardpoints:" + "\n" +
			"-Small Fixed:\t\t\t4" + "\n" +
			"-Medium Fixed:\t\t3" + "\n" +
			"-Medium Turret:\t2" + "\n" +
			"-Large Fixed:\t\t\t4" + "\n" +
			"\n" +
			"Statistics:" + "\n" +
			"-Speed:\t\t\t\t\t\t3" + "\n" +
			"-Cargo:\t\t\t\t\t\t2,250T" + "\n" +
			"-Hitpoints:\t\t\t\t12,500" + "\n" +
			"\n" +
			"Cost:\t\t\t\t\t\t\t12,000,000C"; 
		int index = 0;
		print (input);
		for (int i = 0; i < 14; i++)
			if (strings [i].Split ('\n') [0].Equals (input))
				index = i;
		text.GetComponent<textAnimator> ().changeText(strings [index]);

		image.GetComponent<Image> ().sprite = (Resources.Load (input.ToLower ()) as GameObject).GetComponent<SpriteRenderer> ().sprite;
		//image.GetComponent<RectTransform>().sizeDelta = new Vector2((Resources.Load (input.ToLower ()) as GameObject).GetComponent<SpriteRenderer> ().sprite.rect.x, (Resources.Load (input.ToLower ()) as GameObject).GetComponent<SpriteRenderer> ().sprite.rect.y);
	

	}

}
