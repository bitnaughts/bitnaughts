using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour {

    public Dictionary<string, ConsoleObject> consoles = new Dictionary<string, ConsoleObject> ();
    private ConsoleObject console_reference;

    public GameObject prefab_console;

    // Vector2 minimum_window_size = new Vector2 (330, 220);

    public void execute (string class_name, string function_name, VariableObject[] console_variables, GameObject obj) {

        //TODO::
        //for each console in consoles dictionary, if there is a console that is not in console_variables, delete that from dictionary

        foreach (VariableObject console_variable in console_variables) {

            if (consoles.TryGetValue (console_variable.value, out console_reference)) {
                //If console exists, execute the desired command
                print (class_name + function_name);
                console_reference.execute (class_name, function_name, new string[] { "", "left", ".5" }, null);
            } else {
                //Make desired console
                consoles.Add (console_variable.value, (Instantiate (prefab_console, GameObject.Find ("GUI").transform) as GameObject).transform.GetChild (0).GetComponent<ConsoleObject>());
                execute(console_variable, "Add", new String[] {"ScriptViewer", "bottom", ".7"}, null);
            }
        }
    }

    public void execute (VariableObject variable, string function_name, string[] function_parameters, GameObject obj) {
        print (variable + function_name + function_parameters[0] + function_parameters[1] + function_parameters[2] + obj);
        if (consoles.TryGetValue (variable.value, out console_reference)) {
            console_reference.execute (variable.type, function_name, function_parameters, obj);
        } else {
            print ("not valid \n\n\n");
        }
    }
}