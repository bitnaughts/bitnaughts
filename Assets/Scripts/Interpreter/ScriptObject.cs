using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
public class ScriptObject {

	public GameObject obj; 
	short pointer; //tracks what line is being processed
	Stack<short> backlog; 
	string[] script;

	ProcessorObject processor;
	float time = 0;

	public ScriptObject (GameObject obj) {
		this.obj = obj;
		script = null;
		
		processor = new ProcessorObject();
	}
	public ScriptObject (string text) {
		this.script = text.Split('\n');
	}
	public ScriptObject (string[] script) {
		this.script = script;
	}

	public void tick(float deltaTime) {
		time += deltaTime;
		if (time >= processor.tick_speed) {
			time -= processor.tick_speed;
			/* Execute a line */
			Interpreter.interpret(script[pointer++]);
		}
	}
}
