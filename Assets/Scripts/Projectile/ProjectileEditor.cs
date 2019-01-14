using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEditor : MonoBehaviour {
    ScriptObject script;

    ProjectileObject projectile;

    public string testScript; 

    void Start () {
        testScript = "using Console;\nusing Plotter;\nusing Grapher;\nint angle = 1;\nfor (int i = 0; i < 10; i++) {\nfor (int j = 0; j < 10; j++) {\nConsole.WriteLine(i * j);\n}\n}\n";
        GetComponent<ScriptEditor>().script =  new ScriptObject (this.gameObject, testScript);
    }

    void Update () {
        //this.transform.Translate (new Vector2 (0, .5f));
        //this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
    }
}