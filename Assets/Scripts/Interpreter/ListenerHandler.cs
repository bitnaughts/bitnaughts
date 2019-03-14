using System.Collections.Generic;

public class ListenerHandler {
    public List<string> listeners = new List<string> ();

    public void addListener (string[] line_parts) {
        listeners.Add (Evaluator.scrubSymbols (line_parts[1]));
        switch (Evaluator.scrubSymbols (line_parts[1])) {
            case Classes.CONSOLE:
                Referencer.consoleManager.execute (Console.OPEN, "", obj);
                break;
            case Classes.PLOTTER:

                break;
        }
        break;
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