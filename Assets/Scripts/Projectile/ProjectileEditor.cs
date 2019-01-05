using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		this.transform.Translate (new Vector2 (0, Input.GetAxis ("Vertical") * .05f));
		this.transform.Rotate (new Vector3 (0, 0, -Input.GetAxis ("Horizontal")));
    }
}
