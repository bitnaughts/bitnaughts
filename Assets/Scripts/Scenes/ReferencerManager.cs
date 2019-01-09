using UnityEngine;

public class ReferencerManager : MonoBehaviour {
	public ConsoleManager console;
	void Start () {
		Referencer.consoleManager = GameObject.Find("CodeManager").GetComponent<ConsoleManager>();
	}
}


