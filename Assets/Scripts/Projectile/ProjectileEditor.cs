using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEditor : MonoBehaviour {
    ScriptObject script;

    ProjectileObject projectile;

    public string testScript;

    void Start () {
        testScript =
            "using Console;\n" +
            "using Plotter;\n" +
            "using Grapher;\n" +
            "using IO;\n" +
            "class ExampleClass {\n" +
            "int tester = 10;\n" +
            "static void Main() {\n" +
            "Plotter.Open(\"left\", .3);\n" +       //Snap to the left 30% width. If "top", would be 30% height.
            "int angle = 1;\n" +
            "for (int x = 0; x < 10; x++) {\n" +
            "for (int y = 0; y < 10; y++) {\n" +
            "for (int z = 0; z < 10; z++) {\n" +
            "Print2();\n" +
            "angle = angle + 1;\n" +
            "Print();\n" +
            "}\n" +
            "}\n" +
            "}\n" +
            "}\n" +
            "void Print() {\n" +
            "Print2();\n" +
            "}\n" +
            "void Print2() {\n" +
            "Console.WriteLine(\"hello\");\n" +
            "}\n" +
            "}";
        GetComponent<ScriptEditor> ().script = new ScriptObject (this.gameObject, testScript);
    }

    void Update () {
        //this.transform.Translate (new Vector2 (0, .5f));
        //this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
    }
}