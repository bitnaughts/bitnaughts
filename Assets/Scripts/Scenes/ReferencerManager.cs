using UnityEngine;
using UnityEngine.UI;

public class ReferencerManager : MonoBehaviour {
	// public ConsoleManager console;
	// public Slider codeSpeedTester;
	void Start () {
		/* CodeManager-derived scripts */
		GameObject codeManager = GameObject.Find("CodeManager");
		Referencer.consoleManager = codeManager.GetComponent<ConsoleManager>();
		Referencer.shipManager = codeManager.GetComponent<ShipManager>();

		Referencer.codeSpeedTester = GameObject.Find("12345").GetComponent<Slider>();
	}
}


