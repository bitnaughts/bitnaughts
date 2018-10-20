using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GameObject Element_ShipFolder;
    public GameObject Element_Background;


	public GameObject GUIElement_Minimap;
	GameObject[] GUIElementGroup_Minimap;

	int GUIVariable_ShipViewIndex;

	Vector2 mouseLocation;

	bool firstRun = true;

	// Use this for initialization
	void Start () 
	{
		GUIElementGroup_Minimap = new GameObject[10];
		GUIElementGroup_Minimap[0] = GUIElement_Minimap.transform.Find("GUIElement_ShipViewer").Find("GUIPart_Ship").gameObject;

    }


    //RUNS GENERATION METHOD AND CHECKS MOUSE 
    void Update()
    {
        if (firstRun) GUI_UpdateShipView();
        firstRun = false;

        if (Input.GetMouseButtonDown(0))
        {
            mouseLocation = Input.mousePosition;
            //IF DATA X
            if (clickOn(-14f, 14f, -13f, 13f, mouseLocation, new Vector2(GUIElement_Minimap.GetComponent<RectTransform>().transform.position.x - 60f, GUIElement_Minimap.GetComponent<RectTransform>().transform.position.y - 132f)))
            {
                GUI_UpdateShipView();
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 newMouseLocation = Input.mousePosition;
            mouseLocation = newMouseLocation;
        }
        if (Input.GetMouseButtonUp(0))
        {

        }

        //Update Graphics
        if (Element_ShipFolder.transform.childCount > 0)
        {
            if (GUIVariable_ShipViewIndex >= Element_ShipFolder.transform.childCount)
                GUIVariable_ShipViewIndex = Element_ShipFolder.transform.childCount-1;
            GUIElementGroup_Minimap[0].transform.rotation = Element_ShipFolder.transform.GetChild(GUIVariable_ShipViewIndex).transform.rotation;
        }
        

        //Background Movement
        Vector2 ship_position = TeamManager.team_lists[0][0].transform.position;//GameObject.Find("Holder").transform.GetChild(0).position;
        //Element_Background.transform.position = new Vector3(ship_position.x*20, ship_position.y*20, 10f);
    }
	



	public bool clickOn(float xMin, float xMax, float yMin, float yMax, Vector2 mousePosition, Vector2 otherPosition)
	{
		if (mousePosition.x + xMin < otherPosition.x && mousePosition.x + xMax > otherPosition.x) if (mousePosition.y + yMin < otherPosition.y && mousePosition.y + yMax > otherPosition.y) return true;
		return false;
	}

	//CONSISTENT TIMER FOR DETERMINING BETWEEN CLICK AND DRAG
	void FixedUpdate()
	{
		//if (Input.GetMouseButton (0)) clickDuration++;
	}

	public void GUI_UpdateShipView()
	{
		GUIVariable_ShipViewIndex++;
		if (GUIVariable_ShipViewIndex == Element_ShipFolder.transform.childCount) GUIVariable_ShipViewIndex = 0;
       // Sprite GUISprite_Ship = Element_ShipFolder.transform.GetChild(GUIVariable_ShipViewIndex).GetComponent<MeshRenderer>().material.mainTexture;// sprite;
	//	GUIElementGroup_Minimap[0].GetComponent<Image>().sprite = GUISprite_Ship;

	//	float shipRatio = GUISprite_Ship.rect.width / GUISprite_Ship.rect.height;
	///	if (GUISprite_Ship.rect.height > 90) GUIElementGroup_Minimap[0].GetComponent<RectTransform>().sizeDelta = new Vector2(shipRatio*90f,90f/*GUISprite_Ship.rect.width/4f, GUISprite_Ship.rect.height/4f*/);
	//	else GUIElementGroup_Minimap[0].GetComponent<RectTransform>().sizeDelta = new Vector2(GUISprite_Ship.rect.width, GUISprite_Ship.rect.height);

	}

    public void tester()
    {
        string input = GameObject.Find("TEST").GetComponent<InputField>().text;
        ItemObject lel = new ItemObject(input.Split(','));
        //for (int i = 0; i < lel.coefficients.Length; i++)
        //{
        GameObject.Find("TEXT").GetComponent<Text>().text = "";
            GameObject.Find("TEXT").GetComponent<Text>().text += "Velocity: " + lel.coefficients[0] + "\n";
            GameObject.Find("TEXT").GetComponent<Text>().text += "Firerate: " + lel.coefficients[1] + "\n";
            GameObject.Find("TEXT").GetComponent<Text>().text += "Accuracy: " + lel.coefficients[2] + "\n";
            GameObject.Find("TEXT").GetComponent<Text>().text += "Rotation: " + lel.coefficients[3] + "\n";
            GameObject.Find("TEXT").GetComponent<Text>().text += "Clipsize: " + lel.coefficients[4] + "\n";



        //}

    }
}
