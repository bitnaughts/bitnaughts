using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InterpreterConstants {
    static string[] VARIABLE_TYPES = {
        "bool",
        "int",
        "float",
        "string",
        "Vector2"
    };
    static string[] EVALUATORS = { "==", ">", "<", "<=", ">=", "!=" };
    static string[] OPERATORS = { "=", "++", "--", "+=", "-=", "*=", "/=", "%=" };
}

public class VariableObject {

    private string type;
    private string name;
    /* For Primitive Data Types, value == value, otherwise value holds ToString() */
    private string value;

    VariableObject[] fields;

    public VariableObject (string type) {
        init (type, "", "");
    }
    public VariableObject (string type, string name) {
        init (type, name, "");
    }
    public VariableObject (string type, string name, string value) {
        init (type, name, value);
    }
    public void init (string type, string name, string value) {
        this.type = type;
        this.name = name;
        this.value = value;

        switch (type) {
            case "Vector2":
                fields = new VariableObject[] {
                    new VariableObject ("float", "x", value.Split (',') [0]),
                    new VariableObject ("float", "y", value.Split (',') [1])
                };
                break;
            default:
                fields = null;
                break;
        }
    }

}
public class Interpreter {

    private GameObject obj;

    private List<VariableObject> variables;
    private string[] script;

    private short pointer; //tracks what line is being processed

    // Stack<short> backlog; 

    public Interpreter (string[] script, GameObject obj) {
        this.script = script;
        this.obj = obj;

        pointer = 0;
        variables = new List<VariableObject> ();
    }

    /* Parsing each line of text into code (a.k.a. where the magic happens) */
    public void interpretLine () {
        string line = script[pointer];
        string[] line_parts = line.Split (' ');

        switch (line_parts[0]) {
            case "{": //this shouldn't be possible if linting to "if () {" lines
            case "}":
                //scope control:
                //update visible variables?
                pointer = pointer + 1; //find new pointer position, might be going back to loop, skip else, etc
                interpretLine (); //does not cost anything in interpreter, recalls
                break;
            case "if":
            case "for":
            case "while":
                /* e.g. "for (int i = 0; i < 10; i++) {" */
                string parameter;
                for (int i = 1; i < line_parts.Length - 1; i++) {
                    parameter += line_parts[i];
                }
                /* e.g. "(int i = 0; i < 10; i++)" */

                break;
            case "break":
            case "continue":
                //scope-flow control?
                //break => go to current loop's } 
                //continue => go to current loop's { [where ++ and conditionals are also checked]
                break;
            case "bool":
            case "int":
            case "float":
            case "string":
            case "Vector2":
                string variable_type = line_parts[0];
                string variable_name = line_parts[1];
                if (line_parts.Length == 2) {
                    /* e.g. "int i;" */
                    variables.Add (new VariableObject (variable_type, variable_name, ""));
                } else {
                    /* e.g. "int i = 122;" */
                    string variable_value = "";
                    for (int i = 3; i < line_parts.Length; i++) {
                        variable_value += line_parts[i];
                    }
                    /* e.g. "122;" */
                    variable_value = parse (variable_type, variable_value);
                    variables.Add (new VariableObject (variable_type, variable_name, value));
                }

                break;
            default:

                break;
        }

    }
    public string parse(string cast_type, string input) {
        /* e.g. "valueX + function(valueX);" */
        string output = input;



        return output;
    }

}
// string[] parameter;
// switch (function) {
//     case "Abs":
//         return Mathf.Abs(parameter[0]);
//     case "Round":
//         return Mathf.Round(parameter[0]);
//     case "Floor":
//         return Mathf.Floor(parameter[0]);
//     case "Ceil":
//         return Mathf.Ceil(parameter[0]);
// }
// Fire();
// Fire(10);
// Subroutines...
// print(line);
/* if (line.IndexOf("while (") == 0)
 {
     string condition = ("(" + Parser_splitStringBetween(line, "(", ")") + ")");
     //  print(condition);
     if (Compiler_booleanEvaluation(condition))
     {
         tasks.Push(new TaskObject(current_line, Compiler_findMatchingBracket(current_line), condition, ""));
     }
     else
     {
         current_line = Compiler_findNextInstanceOf(current_line, "}");
     }
 }
 if (line.IndexOf("for (") == 0)
 {
     Compiler_addVariable(Parser_splitStringBetween(line, "(", ";") + ";");
     string condition = ("(" + Parser_splitStringBetween(line, "; ", ";") + ")");
     if (Compiler_booleanEvaluation(condition))
     {
         tasks.Push(new TaskObject(current_line, Compiler_findMatchingBracket(current_line), condition, (Parser_splitStringBetween(line, Parser_splitStringBetween(line, "; ", ";"), ")") + ";").Substring(2)));
     }
     else
     {
         current_line = Compiler_findNextInstanceOf(current_line, "}");
     }
 }
 if (tasks.Count != 0)
 {
     if (current_line == tasks.Peek().ending_line)
     {
         current_line = Compiler_doEndOfTask(tasks.Peek());
         if (current_line == tasks.Peek().ending_line) tasks.Pop();
     }
 }

 if (line.IndexOf("if (") == 0)
 {
     next_else = false; //Used to know if an "else" is linked to an if/will be executed           
     if (!Compiler_booleanEvaluation(line))
     {
         //Jump past If Statement
         current_line = Compiler_findMatchingBracket(current_line);//Compiler_findNextInstanceOf(current_line, "}");
         //Enter next else if available
         next_else = true;
     }
 }
 if (line.IndexOf("else") == 0)
 {
     if (next_else == false)
     {
         current_line = Compiler_findMatchingBracket(current_line);
     }

 }

 //Variable statements
 for (int types_of_PDTs = 0; types_of_PDTs < list_of_PDTs.Length; types_of_PDTs++)
 {
     //DECLARING A NEW VARIABLE OF TYPE (list_of_PDTs[types_of_PDTs])
     if (line.IndexOf(list_of_PDTs[types_of_PDTs] + " ") == 0)
     {
         Compiler_addVariable(line);
     }
 }
 //Looks for a simple statement that is modifying variables
 Compiler_modifyVariable(line);

 if (line.IndexOf("System.out.print") == 0)
 {
     string output = Parser_splitStringBetween(line, "(", ")");
     print(output);
     output = Compiler_evaluateExpression(output);
     print(output);
     if (line.IndexOf("System.out.println") == 0)
     {
         Display_pushConsoleText(output + "\n");
     }
     else Display_pushConsoleText(output);
 }
 if (line.IndexOf("Iterate(") == 0)
 {
     //Iterate(Phone.Vibrate);
     if (line.Contains("Vibrate"))
     {
         if (LevelManager.vibrating) Vibration.Vibrate(10);
     }
     else if (line.Contains("Position"))
     {

         string type = Parser_splitStringBetween(line, "(", ",");
         type = type.Substring(1, type.Length - 2);
         string pos = Parser_splitStringBetween(line, ", new Position(", ")");

         string x = Compiler_evaluateExpression(pos.Remove(pos.IndexOf(",")));
         string y = Compiler_evaluateExpression(pos.Substring(pos.IndexOf(",") + 1));

         Vector2 position = new Vector2(float.Parse(x), float.Parse(y));
         position /= 6;
         GameObject obj = Instantiate(Resources.Load(type), position, this.transform.rotation) as GameObject;
         obj.transform.SetParent(this.transform);

         //obj.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
     }
     else
     {
         string output = Parser_splitStringBetween(line, "(", ")");
         output = output.Substring(1, output.Length - 2);
         Instantiate(Resources.Load(output), this.transform.position, this.transform.rotation);
     }
 }
 if (line.IndexOf("ClearIterations();") == 0)
 {
     for (int i = 0; i < this.transform.childCount; i++)
     {
         Destroy(this.transform.GetChild(i).gameObject);
     }
 }
 if (line.IndexOf("IterateArduino(new Color(") == 0)
 {//lines.Add("IterateArduino(new Color(1,1,1));");

     string message = Parser_splitStringBetween(line, "(new Color(", ")");//1,1,1
     string[] values = message.Split(',');
     for (int i = 0; i < values.Length; i++)
     {
         int val = int.Parse(Compiler_evaluateExpression(values[i]));
         if (val > 255) val = 255;
         if (val < 0) val = 0;
         bluetooth_module.message = byte.Parse(val.ToString());
         bluetooth_module.send();
     }
 }*/