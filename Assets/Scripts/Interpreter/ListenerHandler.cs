using System.Collections.Generic;
using UnityEngine;

public class ListenerHandler {

    private List<string> listeners;
    private GameObject obj;

    private bool has_been_added = false;

    public ListenerHandler (List<string> listeners) {
        this.listeners = listeners;
        // this.obj = obj;

    }
    public void addListeners (GameObject obj) {
        for (int listener = 0; listener < listeners.Count; listener++) {
            addListener (listeners[listener], obj);
        }
    }

    public void addListener (string class_name, GameObject obj) {
        switch (class_name) {
            case Classes.CONSOLE:
                Referencer.consoleManager.execute (Console.OPEN, "", obj);
                break;
            case Classes.PLOTTER:

                break;
        }
    }

    public void updateListeners (int line, GameObject obj) {
        if (has_been_added) {
            for (int listener = 0; listener < listeners.Count; listener++) {
                switch (listeners[listener]) {
                    case Classes.CONSOLE:
                        Referencer.consoleManager.execute (Console.UPDATE, line + "", obj);
                        break;
                    case Classes.PLOTTER:
                        break;
                        //...
                }
            }
        }
        else {
            addListeners(obj);
            has_been_added = true;
        }
    }
}