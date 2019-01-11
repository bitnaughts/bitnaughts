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
            GameObject.Find ("Output").GetComponent<Text> ().text = script.ToString ();
            GameObject.Find ("Output2").GetComponent<Text> ().text = script.getFormattedScript();
        }
	}
}