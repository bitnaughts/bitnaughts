using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector {
    public static List<VariableObject> removeBetween (int startLine, int endLine, List<VariableObject> variables) {
        for (int i = 0; i < variables.Count; i++) {
            if (variables[i].line_defined >= startLine && variables[i].line_defined <= endLine) {
                variables.RemoveAt(i); 
            }
        }
        return variables;
    }
}