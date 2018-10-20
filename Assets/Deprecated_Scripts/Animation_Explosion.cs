using UnityEngine;
using System.Collections;

public class Animation_Explosion : MonoBehaviour {
    int runtime_count;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        runtime_count++;
        if (runtime_count == 200)
        {
            this.gameObject.SetActive(false);
            runtime_count = 0;
        }


    }
}
