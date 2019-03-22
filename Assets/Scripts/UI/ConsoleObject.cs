using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleObject : MonoBehaviour {

    List<UIPanel> panels = new List<UIPanel> ();
    GameObject inner_window;

    Vector2 window_size;

    public GameObject prefab_panel;

    //UIPanel
    //ScriptPanel
    //ConsolePanel
    //PlotterPanel
    //...

    // public GameObject console;
    // public Text console_input;
    // public Text console_output;

    // public GameObject console_highlighter;

    // ScriptObject script_to_view;

    int line;

    void Start () {
        //inner_window = ...
        // panels.Add (new UIPanel (UIStyle.SNAP_BOTTOM, .25f));

        // panels.Add (new UIPanel (UIStyle.SNAP_BOTTOM, .1f));
        panels.Add (new UIPanel (UIStyle.SNAP_LEFT, .5f));
        
        panels.Add (new UIPanel (UIStyle.SNAP_LEFT, .5f));
        // panels.Add (new UIPanel (UIStyle.SNAP_RIGHT, .5f));

        for (int i = 0; i < panels.Count; i++) {
            panels[i].setObj (Instantiate (prefab_panel, this.transform));

        }
    }
    void Update () {


        //when needing to update...
        float total_width = 1f, total_height = 1f;
        // for (int i = 0; i < panels.Count; i++) {
        //     switch (panels[i].snap_direction) {
        //         case UIStyle.SNAP_TOP:
        //         case UIStyle.SNAP_BOTTOM:
        //             total_height += panels[i].size;
        //             break;
        //         case UIStyle.SNAP_LEFT:
        //         case UIStyle.SNAP_RIGHT:
        //             total_width += panels[i].size;
        //             break;
        //     }
        // }

        // ... or ...
        UIRectangle moving_window_points = new UIRectangle (-this.GetComponent<RectTransform> ().rect.width / 2, this.GetComponent<RectTransform> ().rect.width / 2, -this.GetComponent<RectTransform> ().rect.height / 2, this.GetComponent<RectTransform> ().rect.height / 2);
        for (int i = 0; i < panels.Count - 1; i++) {
            panels[i].setRect (UIRectangle.partition (ref moving_window_points, panels[i].snap_direction, panels[i].size, total_width, total_height));
        }
        panels[panels.Count - 1].setRect (moving_window_points);
    }

    // public void execute (string function_name, string function_parameters, GameObject obj) {
    //     switch (function_name) {
    //         case Console.OPEN:
    //             console.SetActive (true);
    //             script_to_view = obj.GetComponent<ScriptEditor> ().script;
    //             console_input.text = script_to_view.getFormattedScript ();
    //             break;
    //         case Console.WRITE_LINE:
    //             if (console_output.text.Split ('\n').Length > 2) {
    //                 console_output.text = console_output.text.Substring (console_output.text.IndexOf ("\n") + 1);
    //             }
    //             console_output.text += DateTime.Now.ToString ("HH:mm:ss:  ");
    //             console_output.text += function_parameters + "\n";

    //             break;
    //         case Console.WRITE:
    //             console_output.text += DateTime.Now + ":\t";
    //             console_output.text += function_parameters;
    //             break;
    //         case Console.UPDATE:
    //             updateListener (int.Parse (function_parameters));
    //             break;
    //         case Console.CLOSE:
    //             console.SetActive (false);
    //             break;
    //     }
    // }
    // public void execute (string function_name) {
    //     execute (function_name, "", null);
    // }

    // //Generalize to support multiple consoles live... parameter of which script it is
    // public void updateListener (int line) {
    //     console_highlighter.GetComponent<RectTransform> ().localPosition = new Vector2 (350 / 2, -line * console_input.fontSize);
    //     console_highlighter.GetComponent<RectTransform> ().sizeDelta = new Vector2 (350, console_input.fontSize);
    // }
}