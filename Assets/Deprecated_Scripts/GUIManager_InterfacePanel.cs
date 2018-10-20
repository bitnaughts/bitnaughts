using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager_InterfacePanel : MonoBehaviour {

    public bool opened = false;
    public bool over = false;
	// Use this for initialization
	void Start ()
    { 
        
        this.transform.GetChild(0).gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.GetChild(0).gameObject.SetActive(opened);

    }


    public void loadShipyard (ShipObject input)
    {
        opened = true;
        this.transform.GetChild(1).gameObject.SetActive(true);
        GameObject buttons = this.transform.GetChild(1).GetChild(0).GetChild(1).gameObject;

        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(9).GetComponent<Image>().sprite = (Resources.Load("sprites/" + input.getType()) as GameObject).GetComponent<SpriteRenderer>().sprite;
        this.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(9).GetComponent<Image>().rectTransform.sizeDelta = (Resources.Load("sprites/" + input.getType()) as GameObject).GetComponent<SpriteRenderer>().sprite.textureRect.size;
        for (int i = 0; i < input.externalPoints.Length; i++)
        {

            // string[] output = Values.camelCaseSplitter(input.externalPoints[i].item.name);
            buttons.transform.GetChild(i).gameObject.SetActive(true);
           // buttons.transform.GetChild(i).gameObject.GetComponent<GUIManager_ButtonTextAnimator>().updateBottomMessage(input.externalPoints[i].item.name);//output[1]);//Values.camelCaseParser(input.externalPoints[i].item.name));
            //buttons.transform.GetChild(i).gameObject.GetComponent<GUIManager_ButtonTextAnimator>().updateTopMessage(Values.getProjectileName(input.externalPoints[i].getEquippedAmmo()));//output[0]);//Values.camelCaseParser(Values.getProjectileName(input.externalPoints[i].getEquippedAmmo())));
        }
        for (int i = input.externalPoints.Length; i < 14; i++)
        {
            buttons.transform.GetChild(i).gameObject.SetActive(false);
        }


    }


}
