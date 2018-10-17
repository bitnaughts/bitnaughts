using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GalaxyManager : MonoBehaviour
{

    public float limit = 0f;
    int counter = 0;
    bool generating = true;

    public float size = 10f;
    float tempSize = 0;
    Vector2 systemLocation = new Vector2(0, 0);
    public float declineRate = .5f;
    public float width = 100f;
    public bool after = true;

    public GameObject testText;

    int starCount = 0;

    public GameObject camera;
    public GameObject title;

    Vector2 dragLocation;
    Vector2 newLocation;
    Vector2 mouseLocation;



    string savedGalaxy;
    int amountSavedPlanets = 0;

    int clickDuration = 0;
    public bool fromSave;

    int planetSelected;

    LineRenderer testLine;
    int testCount = 0;

    int lineCount;


    public GameObject solarInformation;
    public GameObject dataInformation;
    public GameObject politicInformation;

    public GameObject GUI_interfacePanel;

    GameObject codeBase;
    GameObject test;


    Text outputText;

    GalaxyObject galaxy = new GalaxyObject();
    bool GalaxyUI_visualizing = false;
    int GalaxyUI_visualizatingStarCount = 0;
    bool GalaxyUI_visualizatingText = true;

    public List<FleetObject> fleets = new List<FleetObject>();







    void Start()
    {
        // title.GetComponent<TitleDisplay>().updateMessage("GalaxyViewzzzzzzzzzzzzzz");
        // print((int)'A' + " " + (int)'a' + " " + (int)'Z');
        print(Values.camelCaseParser("helloWorldTest"));
        //Create and start visualizing galaxy
        galaxy.build(size, declineRate, width);
        GalaxyUI_visualizing = true;

        //List<ShipObject> shipTest = new List<ShipObject>();
        //
        //
        //
        // ShipObject temp = new ShipObject("fighter:1:{M3:C1-1000}&{}{}{}");//"flagship:0:{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}&{}{}{}");
        //temp.inventory.Add(new ItemObject("R2"));
        //  temp.inventory.Add(new ItemObject("C2"));
        //    temp.inventory.Add(new ItemObject("O2"));
        //      temp.inventory.Add(new ItemObject("R4"));

        //shipTest.Add(temp);
        //Create fleets 
        //fleets.Add(new FleetObject(shipTest));

        //GameObject.Find("GUIElement_Inventory").GetComponent<GUIManager_Inventory>().updateText(fleets[0].ships[0].inventory);



        outputText = GameObject.Find("Output").GetComponent<Text>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Overview");
        }
        if (GalaxyUI_visualizing)
        {
            GameObject test = (Instantiate(Resources.Load("star" + (((galaxy.stars[GalaxyUI_visualizatingStarCount].planets.Count - 1) / 2))), galaxy.stars[GalaxyUI_visualizatingStarCount].position, this.transform.rotation)) as GameObject;
            //this.GetComponent<AudioSource>().Play();
            //this.GetComponent<AudioSource>().pitch = Random.value;

            if (GalaxyUI_visualizatingText) outputText.text += galaxy.stars[GalaxyUI_visualizatingStarCount].ToString() + "\n";

            for (int i = 0; i < galaxy.stars[GalaxyUI_visualizatingStarCount].connectedStars.Count; i++)
            {
                this.GetComponent<LineRendererHolder>().setLine(galaxy.stars[GalaxyUI_visualizatingStarCount].position, galaxy.stars[GalaxyUI_visualizatingStarCount].connectedStars[i].position);
            }

            GalaxyUI_visualizatingStarCount++;
            if (GalaxyUI_visualizatingStarCount == galaxy.stars.Count)
            {
                GalaxyUI_visualizing = false;
            }

            if (GalaxyUI_visualizatingText && GalaxyUI_visualizatingStarCount > 102 / 4)
            {
                outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n") + 1);
                outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n") + 1);
                outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n") + 1);
                outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n") + 1);
            }

        }
        else
        {
            if (GalaxyUI_visualizatingText)
            {
                outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n") + 1);
                outputText.text = outputText.text.Substring(outputText.text.IndexOf("\n") + 1);
            }



            if (Input.GetMouseButtonDown(0))
            {
                //Application.LoadLevel("Overview");
                dragLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                print(Input.mousePosition);
                mouseLocation = Input.mousePosition;
                //if on a ship
                GUI_interfacePanel.GetComponent<GUIManager_InterfacePanel>().over = false;
                solarInformation.GetComponent<solarReport>().over = false;
                dataInformation.GetComponent<dataReport>().over = false;
                politicInformation.GetComponent<politicsReport>().over = false;
                camera.GetComponent<cameraScrool>().overUI = false;
                //if (politicInformation
                if (GUI_interfacePanel.GetComponent<GUIManager_InterfacePanel>().opened)
                {
                    if (clickOn(-300f, 300f, -300f, 300f, mouseLocation, GUI_interfacePanel.GetComponent<RectTransform>().transform.position))
                    {
                        GUI_interfacePanel.GetComponent<GUIManager_InterfacePanel>().over = true;
                        camera.GetComponent<cameraScrool>().overUI = true;
                    }

                }
                if (politicInformation.GetComponent<politicsReport>().opened)
                {
                    //IF OVER FORM
                    if (clickOn(-189f, 189f, -121f, 121f, mouseLocation, politicInformation.GetComponent<RectTransform>().transform.position))
                    { politicInformation.GetComponent<politicsReport>().over = true; camera.GetComponent<cameraScrool>().overUI = true; }
                    //IF DATA X
                    if (clickOn(-14f, 14f, -13f, 13f, mouseLocation, new Vector2(politicInformation.GetComponent<RectTransform>().transform.position.x + 163f, politicInformation.GetComponent<RectTransform>().transform.position.y + 100f))) politicInformation.GetComponent<politicsReport>().open(false, "");
                }
                if (dataInformation.GetComponent<dataReport>().opened)
                {
                    //IF OVER FORM
                    if (clickOn(-163f, 163f, -121f, 121f, mouseLocation, dataInformation.GetComponent<RectTransform>().transform.position))
                    { dataInformation.GetComponent<dataReport>().over = true; camera.GetComponent<cameraScrool>().overUI = true; }
                    //IF DATA X
                    if (clickOn(-14f, 14f, -13f, 13f, mouseLocation, new Vector2(dataInformation.GetComponent<RectTransform>().transform.position.x + 137.35f, dataInformation.GetComponent<RectTransform>().transform.position.y + 100f))) dataInformation.GetComponent<dataReport>().open(false, "");
                    //IF BUILD
                    // if (clickOn(-54f, 54f, -23f, 23f, mouseLocation, new Vector2(dataInformation.GetComponent<RectTransform>().transform.position.x + 8f, dataInformation.GetComponent<RectTransform>().transform.position.y - 105f)))
                    //{ buildShip(dataInformation.GetComponent<dataReport>().build()); dataInformation.GetComponent<dataReport>().open(false, ""); }
                }
                //SOLAR REPORT FORM
                if (solarInformation.GetComponent<solarReport>().opened)
                {
                    //IF OVER FORM
                    if (clickOn(-215f, 215f, -223f, 223f, mouseLocation, solarInformation.GetComponent<RectTransform>().transform.position))
                    { solarInformation.GetComponent<solarReport>().over = true; camera.GetComponent<cameraScrool>().overUI = true; }
                    /*add condition: only check this if the panel is open*/
                    if (clickOn(-128f, 128f, -170f, 170f, mouseLocation, new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x + 284, solarInformation.GetComponent<RectTransform>().transform.position.y - 6)))
                    { solarInformation.GetComponent<solarReport>().over = true; camera.GetComponent<cameraScrool>().overUI = true; }
                    //IF OVER PLANET 
                    for (int i = 0; i < solarInformation.GetComponent<solarReport>().numberPlanets; i++) if (clickOn(-30f, 30f, -30f, 30f, mouseLocation, solarInformation.GetComponent<solarReport>().planets[i].transform.position)) solarInformation.GetComponent<solarReport>().planetClick(true, i);
                    //IF SIDEBAR BUTTONS
                    for (int i = 0; i < 3; i++) if (clickOn(-30f, 30f, -30f, 30f, mouseLocation, new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x + 362, solarInformation.GetComponent<RectTransform>().transform.position.y + 56 - (76 * i)))) solarInformation.GetComponent<solarReport>().planetButtonClick(i);
                    //IF SIDEBAR ARROW UP
                    if (clickOn(-11f, 11f, -12f, 12f, mouseLocation, new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x + 402, solarInformation.GetComponent<RectTransform>().transform.position.y + 54.25f))) solarInformation.GetComponent<solarReport>().sideBar.GetComponent<informationDisplay>().arrow(true);
                    //IF SIDEBAR ARROW DOWN
                    if (clickOn(-11f, 11f, -12f, 12f, mouseLocation, new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x + 402, solarInformation.GetComponent<RectTransform>().transform.position.y - 97.75f))) solarInformation.GetComponent<solarReport>().sideBar.GetComponent<informationDisplay>().arrow(false);
                    //IF SIDEBAR X
                    if (clickOn(-19f, 19f, -17f, 17f, mouseLocation, new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x + 371, solarInformation.GetComponent<RectTransform>().transform.position.y + 108.9f))) solarInformation.GetComponent<solarReport>().planetClick(false, -1);//..Equals),"");
                                                                                                                                                                                                                                                                                                                //IF REPORT X
                    if (clickOn(-14f, 14f, -13f, 13f, mouseLocation, new Vector2(solarInformation.GetComponent<RectTransform>().transform.position.x + 189, solarInformation.GetComponent<RectTransform>().transform.position.y + 202))) solarInformation.GetComponent<solarReport>().open(false, null);
                }
            }
            if (Input.GetKeyDown(KeyCode.P)) politicInformation.GetComponent<politicsReport>().open(true, "");


            if (Input.GetMouseButton(0))
            {

                newLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newMouseLocation = Input.mousePosition;
                if (solarInformation.GetComponent<solarReport>().over)
                {
                    solarInformation.transform.Translate(new Vector2(-(mouseLocation.x - newMouseLocation.x), -(mouseLocation.y - newMouseLocation.y)));
                }
                else if (GUI_interfacePanel.GetComponent<GUIManager_InterfacePanel>().over)
                {
                    GUI_interfacePanel.transform.Translate(new Vector2(-(mouseLocation.x - newMouseLocation.x), -(mouseLocation.y - newMouseLocation.y)));
                }
                else if (politicInformation.GetComponent<politicsReport>().over)
                {

                    politicInformation.transform.Translate(new Vector2(-(mouseLocation.x - newMouseLocation.x), -(mouseLocation.y - newMouseLocation.y)));
                }
                else if (dataInformation.GetComponent<dataReport>().over)
                {
                    dataInformation.transform.Translate(new Vector2(-(mouseLocation.x - newMouseLocation.x), -(mouseLocation.y - newMouseLocation.y)));
                }
                //else if (Input.touchCount <= 1) this.transform.Translate(new Vector2(-(dragLocation.x - newLocation.x), -(dragLocation.y - newLocation.y)));//position = 

                dragLocation = newLocation;
                mouseLocation = newMouseLocation;
            }
            if (Input.GetMouseButtonUp(0))
            {

                camera.GetComponent<cameraScrool>().overUI = false;
                dragLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (clickDuration < 10)//originally 10
                {
                    //testText.GetComponent<Text>().text = "up";
                    Vector2 mapLocation = new Vector2(newLocation.x - this.transform.position.x, newLocation.y - this.transform.position.y);
                    if (!solarInformation.GetComponent<solarReport>().over) findPlanet(mapLocation.x, mapLocation.y);
                }
                clickDuration = 0;
            }
        }
    }

    //CHECKS IF CLICK IS WITHIN GIVEN BOUNDS
    public bool clickOn(float xMin, float xMax, float yMin, float yMax, Vector2 mousePosition, Vector2 otherPosition)
    {
        if (mousePosition.x + xMin < otherPosition.x && mousePosition.x + xMax > otherPosition.x) if (mousePosition.y + yMin < otherPosition.y && mousePosition.y + yMax > otherPosition.y) return true;
        return false;
    }

    //CONSISTENT TIMER FOR DETERMINING BETWEEN CLICK AND DRAG
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            clickDuration++;
        }
    }

    //RUNS THROUGH ALL POSITIONS TO SEE WHERE USER CLICKED, RUNS NECESSARY METHODS PER ITEM
    public void findPlanet(float x, float y)
    {
        //look for being on a ship or gui or stars
        for (int i = 0; i < galaxy.stars.Count; i++)
        {
            if (x - .3f < galaxy.stars[i].position.x && x + .3f > galaxy.stars[i].position.x)
            {
                if (y - .3f < galaxy.stars[i].position.y && y + .3f > galaxy.stars[i].position.y)
                {
                    //resets
                    for (int g = 0; g < 5; g++) solarInformation.GetComponent<solarReport>().planetClick(false, -1);
                    //displays correct data (or else previous openings will show up
                    solarInformation.GetComponent<solarReport>().open(false, null);
                    solarInformation.GetComponent<solarReport>().open(true, galaxy.stars[i]);
                    // solarInformation.GetComponent<solarReport>().open(true, File.ReadAllLines(Application.persistentDataPath + "galaxyData.txt")[i]);///savedPlanets[i]);
                    planetSelected = i;
                    break;
                }
            }
        }
    }
}
