﻿using System.Collections;
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
            "class ExampleClass {\n" +
            "static void Main() {\n" +
            "int angle = 1;\n" +
            "for (int x = 0; x < 10; x++) {\n" +
            "for (int y = 0; y < 10; y++) {\n" +
            "for (int z = 0; z < 10; z++) {\n" +
            "angle = angle + 1;\n" +
            "Print();\n" +
            "}\n" +
            "}\n" +
            "}\n" +
            "}\n" +
            "void Print() {\n" +
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