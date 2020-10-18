using UnityEngine;
using UnityEngine.UI;

public class ReferencerManager : MonoBehaviour {
	// public ConsoleManager console;
	// public Slider codeSpeedTester;
	void Start () {
		/* CodeManager-derived scripts */

		// Referencer.consoleManager = GameObject.Find("InnerWindow").GetComponent<ConsoleObject>();//codeManager.GetComponent<ConsoleManager>();
		Referencer.prefab_controller = this.GetComponent<PrefabController>();
		// Referencer.interaction_controller = this.GetComponent<InteractionController>();
		// Referencer.database = this.GetComponent<Database>();

		Referencer.world_space = GameObject.Find("WorldSpace").transform;

		// Referencer.shipManager = codeManager.GetComponent<ShipManager>();

		// Referencer.codeSpeedTester = GameObject.Find("12345").GetComponent<Slider>();
	}
}


