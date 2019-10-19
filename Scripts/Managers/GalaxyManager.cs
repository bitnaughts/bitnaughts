using System;
using System.Collections;
using UnityEngine;

public class GalaxyManager : MonoBehaviour {

    public GalaxyObject galaxy;

    public GameObject[] stars;

    void Start () { 
        galaxy = new GalaxyObject();
        print(galaxy);



    }

    void Update () {



    }
}