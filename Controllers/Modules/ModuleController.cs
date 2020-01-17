using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public abstract class Module {

    public PointF center;
    public SizeF size;
    public int rotation;

    public float hitpoints;

    public float action_speed;
    public float action_cooldown;

    public string prefab_path;
    public ColorF color;
    public GameObject obj;

    public Module (PointF center, SizeF size, int rotation) {
        this.center = center;
        this.size = size;
        this.rotation = rotation;
        this.hitpoints = 1;
        this.action_speed = 10;
        this.prefab_path = "Prefabs/Modules/Hardpoint";
        this.color = ColorF.white;
    }
    public Module (dynamic json) {
        this.center = new PointF (json.center);
        this.size = new SizeF (json.size);
        this.rotation = json.rotation;
        this.hitpoints = json.hitpoints;
        this.action_speed = json.action_speed;
        this.action_speed = json.action_cooldown;
        this.prefab_path = json.prefab_path;
        this.color = new ColorF (json.color);
    }
    public JObject ToJObject () {
        dynamic json = new JObject ();
        json.type = this.GetType ().ToString ();
        json.center = center.ToJObject ();
        json.size = size.ToJObject ();
        json.rotation = rotation;
        json.hitpoints = hitpoints;
        json.action_speed = action_speed;
        json.prefab_path = prefab_path;
        json.color = color.ToJObject ();
        return json;
    }
    public override string ToString () { return ToJObject ().ToString (); }
}

public abstract class ModuleController<T> : Controller<T> where T : Module {

    public PrefabController prefab_controller;
    public Dictionary<string, GameObject> parts;

    void Update () {
        if (obj.action_cooldown > 0) { /* Updates Module's action cooldown timer */
            obj.action_cooldown -= Time.deltaTime;
            obj.action_cooldown = Mathf.Clamp (obj.action_cooldown, 0, obj.action_speed);
        }
        Visualize (); /* Updates Module's visual representation to current frame */
    }

    // public abstract void Visualize ();
    // public abstract void Initialize (T obj);

    public bool Action () {
        if (obj.action_cooldown == 0) { /* Module has finished cooling down from previous action */
            obj.action_cooldown = obj.action_speed;
            return true;
        }
        return false;
    }
    public void Remove () {
        foreach (var part in parts) Destroy (part.Value);
        Destroy (this.gameObject);
    }
    // void GetGameObject() {
    //     return this.gameObject;
    // }
}