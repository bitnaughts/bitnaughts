using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour {
    float avg;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (avg == 0) avg = 1 / Time.deltaTime;
        avg = (.3f * (1 / Time.deltaTime)) + (.7f) * avg;
        
        this.GetComponent<Text>().text = "FPS:" + Mathf.Round(1 / Time.deltaTime) + "\nAVG:" + Mathf.Round(avg);
    }
}
