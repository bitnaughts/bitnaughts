using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lagDispaly : MonoBehaviour {
	float total;
	int count = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		total += (1 / Time.deltaTime);
		count++;			
		float average = total / count;
		
		this.GetComponent<Text>().text = "FPScurrent:" + (1 / Time.deltaTime) + "\tFPSaverage:" + average;
		
		if (count == 1000) {
			total = 0;
			count = 0;
		}
	}
}
