using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {


    int opening_speed = 0;
    bool opened = false;

    public GameObject TitleMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!opened && opening_speed > 0) TitleMenu.GetComponent<RectTransform>().Translate(new Vector2(0, opening_speed--));

        if (opened && opening_speed > 0) TitleMenu.GetComponent<RectTransform>().Translate(new Vector2(0, -opening_speed--));
    }

    public void OpenTitleMenu()
    {
        opened = !opened;
        opening_speed = 15;
    }
}
