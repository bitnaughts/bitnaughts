using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GUIManager_Inventory : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    public void updateText(List<ItemObject> inventory)
    {
        string output = "";
        for (int i = 0; i < inventory.Count; i++)
        {
          //  output += "\t\t" + inventory[i].getDescription() + "\n\n";

        }
        this.transform.GetChild(1).GetComponent<textAnimator>().changeText(output);//GetComponent<Text>().text = output;
    }
}
