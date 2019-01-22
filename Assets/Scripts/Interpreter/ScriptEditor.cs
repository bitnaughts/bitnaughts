using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScriptEditor : MonoBehaviour {

	public ScriptObject script;

	// public ScriptObject getScript () {
	// 	return script;
	// }
	void Update() {
		if (script != null && script.tick (Time.deltaTime, Referencer.codeSpeedTester.value)) {
			//Any visualization/updating while script executes
		}
	}
}