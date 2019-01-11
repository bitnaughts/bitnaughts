using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour {
    public GameObject console;
    public Text console_input;

    ScriptObject script_to_view;
    Vector2 minimum_window_size = new Vector2 (330, 220);
    void Start () {
        console = GameObject.Find ("Console");
        console_input = console.transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ();

        console.SetActive (false);
    }
    void Update () {
        if (script_to_view != null) {
            //print(script_to_view.getCurrentLine());
        }
    }
    public void execute (string function_name, string function_parameters, GameObject obj) {
        switch (function_name) {
            case "WriteLine":
                console.SetActive (true);
                console_input.text = function_parameters;
                break;
            case "Open":
                console.SetActive (true);
                script_to_view = obj.GetComponent<ScriptEditor> ().script;
                console_input.text = script_to_view.getFormattedScript();
                break;
            case "Close":
                console.SetActive (false);
                break;
        }
    }
    public void execute (string function_name) {
        execute (function_name, "", null);
    }
}