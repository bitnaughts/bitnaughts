using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScriptObject {

	public GameObject obj;
	private string[] script;

	private ProcessorObject processor;
	private Interpreter interpreter;
	float time = 0;

	bool ran_this_tick = false;
	bool finished = false;

	public ScriptObject (GameObject obj, string text) {
		init (obj, text.Split ('\n'));
	}
	public ScriptObject (GameObject obj, string[] script) {
		init (obj, script);
	}
	public void init (GameObject obj, string[] script) {
		this.obj = obj;
		this.script = script;
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
	public bool tick (float delta_time, float tick_speed) {
		processor.setSpeed(tick_speed);
		if (finished) return false;
		ran_this_tick = false;		 
		time += delta_time;
		while (time >= processor.tick_speed) {
			time -= processor.tick_speed;
			/* Execute a line */
			finished = interpreter.interpretLine ();
			ran_this_tick = true;
		}
		return ran_this_tick;
	}
	public int getCurrentLine() {
		return interpreter.getPointer();
	}
	public string[] getScript()  {
		return script;
	}
	public string getFormattedScript() {
		string output = "";
		for (int i = 0; i < script.Length; i++) {
			output += i + " " + script[i] + "\n";
		}
		return output;
	}
	
	public override string ToString () {
		string output = "";
		output += interpreter.ToString();    
        return output;
    }
}