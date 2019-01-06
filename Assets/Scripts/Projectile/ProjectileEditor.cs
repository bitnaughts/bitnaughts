using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //temporary

public class ProjectileEditor : MonoBehaviour
{
    ScriptObject script;

    ProjectileObject projectile;

    string testScript = "int i = 120 + 2;";

    // Start is called before the first frame update
    void Start()
    {
        script = new ScriptObject(this.gameObject, testScript);
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
