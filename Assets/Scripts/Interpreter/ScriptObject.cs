using System.Collections;
using System.Collections.Generic;

public class ScriptObject {

	short pointer; //tracks what line is being processed
	Stack<short> backlog; 
	string[] script;

	public ScriptObject (string text) {
		this.script = text.Split('\n');
	}
	public ScriptObject (string[] script) {
		this.script = script;
	}
}
