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
    public GameObject prefab_script_viewer;

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
        inner_window = this.gameObject;
    }
    void Update () {

        //when needing to update...
        float total_width = 1f, total_height = 1f;

        // ... or ...
        if (panels.Count > 0) {
            print (panels.Count);
            UIRectangle moving_window_points = new UIRectangle (-this.GetComponent<RectTransform> ().rect.width / 2, this.GetComponent<RectTransform> ().rect.width / 2, -this.GetComponent<RectTransform> ().rect.height / 2, this.GetComponent<RectTransform> ().rect.height / 2);
            for (int i = 0; i < panels.Count - 1; i++) {
                panels[i].setRect (UIRectangle.partition (ref moving_window_points, panels[i].snap_direction, panels[i].size, total_width, total_height));
            }
            panels[panels.Count - 1].setRect (moving_window_points);
        }
    }

    public void execute (string class_name, string function_name, string[] function_parameters, GameObject obj) {
        switch (class_name) {
            case Plotter.NAME:
                // switch (function_name) {
                //     case Console.ADD:
                //         string value_of_interest = function_parameters[0];
                //         panels.Add (new UIPanel (function_parameters[1], function_parameters[2]));
                //         break;
                // }
                break;
            case Console.NAME:
                switch (function_name) {
                    //     case Console.OPEN:
                    //         console.SetActive (true);
                    //         script_to_view = obj.GetComponent<ScriptEditor> ().script;
                    //         console_input.text = script_to_view.getFormattedScript ();
                    //         break;
                    case Console.ADD:
                        print ("yes");
                        // string value_of_interest = function_parameters[0];
                        panels.Add (new UIPanel (function_parameters[1], float.Parse (function_parameters[2]), class_name, Instantiate (prefab_panel, this.transform)));
                        if (function_parameters[0] == "ScriptViewer") {
                            print ("add script viewer");
                            Instantiate(prefab_script_viewer, panels[panels.Count - 1].obj.transform);
                        } 
                        break;
                        // case Console.WRITE_LINE:
                        //     if (console_output.text.Split ('\n').Length > 2) {
                        //         console_output.text = console_output.text.Substring (console_output.text.IndexOf ("\n") + 1);
                        //     }
                        //     console_output.text += DateTime.Now.ToString ("HH:mm:ss:  ");
                        //     console_output.text += function_parameters + "\n";

                        //     break;
                        // case Console.WRITE:
                        //     console_output.text += DateTime.Now + ":\t";
                        //     console_output.text += function_parameters;
                        //     break;
                    case Console.UPDATE:
                        for (int i = 0; i < panels.Count; i++) {

                            panels[i].updateListener (int.Parse (function_parameters[0]));

                        }
                        // updateListener (int.Parse (function_parameters));
                        break;
                        // case Console.CLOSE:
                        //     console.SetActive (false);
                        //     break;
                }
                break;
        }
    }
}