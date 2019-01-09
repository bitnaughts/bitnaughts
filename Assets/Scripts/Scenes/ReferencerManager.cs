using UnityEngine;

public class ReferencerManager : MonoBehaviour {
	public ConsoleManager console;
	void Start () {
		Referencer.consoleManager = console;
	}
}