using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class VariableObject {

    public string type;
    public string name;
    /* For Primitive Data Types, value == value, otherwise value holds ToString() */
    public string value;
    public int line_defined;
    VariableObject[] fields;
    public VariableObject (string type, string name, string value, int line_defined) {
        init (type, name, value, line_defined);
    }
    public void init (string type, string name, string value, int line_defined) {
        this.type = type;
        this.name = name;
        this.value = value;
        this.line_defined = line_defined;

        switch (type) {
            case "Vector2":
                fields = new VariableObject[] {
                    new VariableObject ("float", "x", value.Split (',') [0], line_defined),
                    new VariableObject ("float", "y", value.Split (',') [1], line_defined)
                };
                break;
            default:
                fields = null;
                break;
        }
    }
    public override string ToString () {
        string output = type + " " + name + " = " + value;
        if (fields != null) {

            for (int i = 0; i < fields.Length; i++) {
                output += "-> -> Field(" + fields[i].ToString () + ")\n";
            }
        }
        return output;
    }
}