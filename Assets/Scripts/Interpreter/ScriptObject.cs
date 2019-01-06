using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class ScriptObject {

	public GameObject obj; 
	short pointer; //tracks what line is being processed
	Stack<short> backlog; 
	string[] script;

	float tick_rate = 10;
	float time = 0;

	public ScriptObject (GameObject obj) {
		this.obj = obj;
		script = null;
	}
	public ScriptObject (string text) {
		this.script = text.Split('\n');
	}
	public ScriptObject (string[] script) {
		this.script = script;
	}

	public void tick(float deltaTime) {
		time += deltaTime;
		if (time >= tick_rate) {
			time -= tick_rate;
			/* Execute a line */
			Interpreter.interpret(script[pointer++]);
		}
	}
}
