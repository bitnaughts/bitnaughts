using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour {

    public float size;
    public string snap_direction;

    public string type; //Console.NAME, Plotter.NAME, etc

    public UIRectangle rect;
    public GameObject obj;

    //UIStyle style;

    public UIPanel (string snap_direction, float size, string type, GameObject obj) {
        this.snap_direction = snap_direction;
        this.size = size;
        this.type = type;
        this.obj = obj;
    }
    public void setObj (GameObject obj) {
        this.obj = obj;
    }
    public void setRect (UIRectangle rect) {
        if (rect != null) {
            this.rect = rect;
            obj.GetComponent<RectTransform> ().localPosition = rect.getMidpoint ();
            obj.GetComponent<RectTransform> ().sizeDelta = rect.getSize ();
        }
    }

    public void updateListener (int line) {

        /* The question is what data do panels need to update with...? */

        switch (type) {
            case Plotter.NAME:
                obj.GetComponent<PlotterHandler> ().updateListener (line);
                break;
            case Console.NAME:
                obj.GetComponent<ConsoleHandler> ().updateListener (line);
                break;
        }
    }
}