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

        // switch (snap_direction) {
        //     case UIStyle.SNAP_TOP:
        //     case UIStyle.SNAP_BOTTOM:
        //         width = -1;
        //         height = size;
        //         break;
        //     case UIStyle.SNAP_LEFT:
        //     case UIStyle.SNAP_RIGHT:
        //         break;
        // }
    }
    public void setObj(GameObject obj) {
        this.obj = obj;
    }
    public void setRect(UIRectangle rect) {
        this.rect = rect;
        obj.GetComponent<RectTransform>().localPosition = rect.getMidpoint();
        obj.GetComponent<RectTransform>().sizeDelta = rect.getSize();
    }

    public void updateListener(int line) {
        switch (type) {
            case Plotter.NAME: 
                obj.GetComponent<PlotterHandler>().updateListener(line);
                break;
            case Console.NAME: 
                obj.GetComponent<ConsoleHandler>().updateListener(line);
                break;    
        }
    }



    // public float getWidth() {
    //     return width;
    // }
    // public float getHeight() {
    //     return height;
    // }

}