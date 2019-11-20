using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class ScriptController : MonoBehaviour {

    public ScriptObject script;

    void Start () {
        string testScript =
            // "using Console;\n" +
            // "using Plotter;\n" +
            // "using Grapher;\n" +
            // "using IO;\n" +
            // "using Scanner;\n" +    //Scanner.GetClosestShip();
            "class ExampleClass {\n" +
            // "int tester = 10;\n" +
            "static void Main() {\n" +
            "2 + 4 * 200 % 2 - 200 / 10 + 250 * 5 + 20 - 10 == 200 || true\n" +
            // //
            // "Console console1 = new Console(500, 500, this.script);\n" +  // two overloads: "this.script" adds a panel for the code viewing, otherwise blank
            // //
            // "Plotter plot1 = new Plotter(ref x);\n" + //Construct with a ref == Watch variable
            // "Plotter plot2 = new Plotter(ref x);\n" + //Construct with a ref == Watch variable
            // "Plotter plot3 = new Plotter(ref x);\n" + //Construct with a ref == Watch variable
            // "console1.Add(plot1, \"bottom\", .7);\n" + //Snap to the left 30% width. If "top", would be 30% height.
            // "console1.Add(plot2, \"left\", .7);\n" + //Snap to the left 30% width. If "top", would be 30% height.
            // "console1.Add(plot3, \"top\", .7);\n" + //Snap to the left 30% width. If "top", would be 30% height.
            // //
            // "int angle = 1;\n" +
            // "for (int x = 0; x < 10; x++) {\n" +
            // "for (int y = 0; y < 10; y++) {\n" +
            // "for (int z = 0; z < 10; z++) {\n" +
            // "Print2();\n" +
            // "angle = angle + 1;\n" +
            // "Print();\n" +
            "}\n" +
            // "}\n" +
            // "}\n" +
            // "}\n" +
            // "void Print() {\n" +
            // "Print2();\n" +
            // "}\n" +
            // "void Print2() {\n" +
            // "Console.WriteLine(\"hello\");\n" +
            // "}\n" +
            "}";
        script = new ScriptObject (testScript);
    }

    void Update () {
        if (script != null && script.tick (Time.deltaTime, /* Referencer.codeSpeedTester.value */ 1)) {
            GameObject.Find ("Text 1").GetComponent<Text> ().text = script.ToString ();
            //Any visualization/updating while script executes
        }
        // GameObject.Find ("DEBUGGER").GetComponent<Text> ().text = GetComponent<ScriptEditor> ().script.getFormattedScript () + "\n\n\n" + GetComponent<ScriptEditor> ().script.ToString ();
    }

    public ScriptObject getScript () {
        return script;
    }

}