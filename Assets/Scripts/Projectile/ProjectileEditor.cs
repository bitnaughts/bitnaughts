using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEditor : MonoBehaviour
{
    ScriptObject script;

    ProjectileObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        script = new ScriptObject(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        script.tick(Time.deltaTime);

		//this.transform.Translate (new Vector2 (0, .5f));
		//this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
    }
}
