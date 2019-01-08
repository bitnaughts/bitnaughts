﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //temporary

public class ProjectileEditor : MonoBehaviour {
    ScriptObject script;

    ProjectileObject projectile;

    string testScript = "int angle = 1;\n\nwhile (true) {\nangle = angle + 10;\n}\n";


    // Start is called before the first frame update
    void Start () {
        script = new ScriptObject (this.gameObject, testScript);
    }

    // Update is called once per frame
    void Update () {

        if (script.tick (Time.deltaTime)) {
            GameObject.Find ("Output").GetComponent<Text> ().text = script.ToString ();
            GameObject.Find ("Output2").GetComponent<Text> ().text = testScript;
        }

        //this.transform.Translate (new Vector2 (0, .5f));
        //this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
    }
}