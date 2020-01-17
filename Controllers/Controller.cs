using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Controller<T> : MonoBehaviour {

    public T obj;

    void Start () { }
    void Update () { }

    public abstract void Visualize ();
    public abstract void Initialize (T obj);
}