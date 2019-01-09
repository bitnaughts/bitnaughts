using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{
    public GameObject console;
    Vector2 minimum_window_size = new Vector2(330, 220);
    void Start() {
        console = GameObject.Find("Console");
    }
    public void execute (string function_name, string function_parameters) {
		switch (function_name) {
			case "WriteLine":
				console.SetActive(true);
				break;
            case "Open":
                console.SetActive(true);
                break;
            case "Close":
                console.SetActive(false);
                break;
		}
	}
    public void execute(string function_name) {
        execute(function_name, "");
    }
}
