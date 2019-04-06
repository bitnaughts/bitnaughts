using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour {
    public Dictionary<string, ConsoleObject> consoles = new Dictionary<string, ConsoleObject> ();
    private ConsoleObject console_reference;
    // public Text console_input;
    // public Text console_output;

    // public GameObject console_highlighter;

    // ScriptObject script_to_view;
    // Vector2 minimum_window_size = new Vector2 (330, 220);

    // int line;
    void Start () {
        // console = GameObject.Find ("Console");
        // console_input = console.transform.GetChild (0).GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ();
        // console_output = console.transform.GetChild (2).GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ();
        // console_highlighter = console_input.transform.GetChild (0).gameObject;
        // console.SetActive (false);  
    }
    public void execute (string class_name, string function_name, VariableObject console_variable, GameObject obj) {

        if (consoles.TryGetValue (console_variable.value, out console_reference)) {
            console_reference.execute ();
        } else {
            consoles.Add (console_variable.value, Instantiate (prefab_console, GameObject.Find ("GUI")));
        }
    }

    public void execute (string function_name, string[] function_parameters, GameObject obj) {
        switch (function_name) {
            case Console.OPEN:
                // console.SetActive (true);
                // script_to_view = obj.GetComponent<ScriptEditor> ().script;
                // console_input.text = script_to_view.getFormattedScript ();
                break;
            case Console.WRITE_LINE:
                // if (console_output.text.Split ('\n').Length > 2) {
                //     console_output.text = console_output.text.Substring (console_output.text.IndexOf ("\n") + 1);
                // }
                // console_output.text += DateTime.Now.ToString("HH:mm:ss:  ");
                // console_output.text += function_parameters + "\n";

                break;
            case Console.WRITE:
                // console_output.text += DateTime.Now + ":\t";
                // console_output.text += function_parameters;
                break;
            case Console.UPDATE:
                // updateListener (int.Parse (function_parameters));
                break;
            case Console.CLOSE:
                // console.SetActive (false);
                break;
        }
    }
    public void execute (string function_name) {
        execute (function_name, new string[] { "" }, null);
    }

    //Generalize to support multiple consoles live... parameter of which script it is
    public void updateListener (int line) {
        // console_highlighter.GetComponent<RectTransform> ().localPosition = new Vector2 (350 / 2, -line * console_input.fontSize);
        // console_highlighter.GetComponent<RectTransform> ().sizeDelta = new Vector2 (350, console_input.fontSize);
    }
}