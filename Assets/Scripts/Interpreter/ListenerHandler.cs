using System.Collections.Generic;
using UnityEngine;

public class ListenerHandler {

    private List<string> listeners;
    private GameObject obj; 

    public ListenerHandler (List<string> listeners, GameObject obj) {
        this.listeners = listeners;
        this.obj = obj;
        for (int listener = 0; listener < listeners.Count; listener++) {
            addListener (listeners[listener]);
        }
    }

    public void addListener (string class_name) {
        switch (class_name) {
            case Classes.CONSOLE:
                Referencer.consoleManager.execute (Console.OPEN, "", obj);
                break;
            case Classes.PLOTTER:

                break;
        }
    }

    public void updateListeners (int line) {
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
}