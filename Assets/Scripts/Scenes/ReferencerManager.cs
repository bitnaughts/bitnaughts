using UnityEngine;
using UnityEngine.UI;

public class ReferencerManager : MonoBehaviour {
	// public ConsoleManager console;
	// public Slider codeSpeedTester;
	void Start () {
		Referencer.consoleManager = GameObject.Find("CodeManager").GetComponent<ConsoleManager>();
		Referencer.codeSpeedTester = GameObject.Find("12345").GetComponent<Slider>();
		
	}
}


