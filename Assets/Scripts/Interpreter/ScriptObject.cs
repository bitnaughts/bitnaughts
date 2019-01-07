using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScriptObject {

	public GameObject obj;
	private string[] script;

	private ProcessorObject processor;
	private Interpreter interpreter;
	float time = 0;

	public ScriptObject (GameObject obj, string text) {
		init (null, text.Split ('\n'));
	}
	public ScriptObject (GameObject obj, string[] script) {
		init (null, script);
	}
	public void init (GameObject obj, string[] script) {
		this.obj = obj;
		interpreter = new Interpreter (script, obj);
		processor = new ProcessorObject ();
	}
	public void setScript (string text) {
		setScript (text.Split ('\n'));
	}
	public void setScript (string[] script) {
		interpreter = new Interpreter (script, obj);
	}
	public void setProcessor (ProcessorObject processor) {
		this.processor = processor;
	}
	public bool tick (float deltaTime) {
		time += deltaTime;
		if (time >= processor.tick_speed) {
			time -= processor.tick_speed;
			/* Execute a line */
			interpreter.interpretLine ();
			return true;
		}
		return false;
	}
	public override string ToString () {
		string output = "";
		output += interpreter.ToString();    
        return output;
    }
}