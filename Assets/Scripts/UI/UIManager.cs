using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {


    int opening_speed = 0;
    bool opened = false;

    public GameObject TitleMenu;


    int counter = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (counter++ % 3 == 0)
        {
            if (!opened && opening_speed > 0)
            {
                TitleMenu.GetComponent<RectTransform>().Translate(new Vector2(0, opening_speed));
                opening_speed -= 2;
            }
            if (opened && opening_speed > 0)
            {
                TitleMenu.GetComponent<RectTransform>().Translate(new Vector2(0, -opening_speed));
                opening_speed -= 2;
            }
        }
    }

    public void OpenTitleMenu()
    {
        opened = !opened;
        opening_speed = 22;
    }
}
