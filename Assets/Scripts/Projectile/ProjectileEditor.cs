using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEditor : MonoBehaviour {
    ScriptObject script;

    ProjectileObject projectile;

    public string testScript;

    void Start () {
        testScript = 
            "class ExampleClass {\n"+
            "using Console;\n" +
            "using Plotter;\n" +
            "using Grapher;\n" +
            "static void Main() {\n" +
            "int angle = 1;\n" +
            "for (int x = 0; x < 10; x++) {\n" +
            "for (int y = 0; y < 10; y++) {\n" +
            "Console.WriteLine(x + \"*\" + y + \"=\" + x * y);\n" +
            "}\n" +
            "}\n" +
            "}\n" +
            "}\n";
        GetComponent<ScriptEditor> ().script = new ScriptObject (this.gameObject, testScript);
    }

    void Update () {
        //this.transform.Translate (new Vector2 (0, .5f));
        //this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
    }
}