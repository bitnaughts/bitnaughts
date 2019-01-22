using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //temporary

public class ProjectileEditor : MonoBehaviour
{
    ScriptObject script;

    ProjectileObject projectile;

    string testScript = "int i = 120 + 2;";

    void Start () {
        testScript = "using Console;\nusing Plotter;\nusing Grapher;\nint angle = 1;\nfor (int x = 0; x < 10; x++) {\nfor (int y = 0; y < 10; y++) {\nConsole.WriteLine(x + \"*\" + y + \"=\" + x * y);\n}\n}\n";
        GetComponent<ScriptEditor>().script =  new ScriptObject (this.gameObject, testScript);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Output").GetComponent<Text>().text = script.ToString();
        script.tick(Time.deltaTime);

		//this.transform.Translate (new Vector2 (0, .5f));
		//this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
    }
}
