using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public class Sensor : Module {

    public Sensor (dynamic json) : base ((object) json) { }
    public Sensor (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.action_speed = 10; //Sampling rate, depending on size, 
        this.prefab_path = "Prefabs/Modules/Hardpoint";
    }
}

public class SensorController : ModuleController<Sensor> {
    public override void Visualize () {
        // obj.angle += .01f;
        // parts["turret"].transform.rotation = ConversionHandler.ToQuaternion (obj.angle);
    }
    public override void Initialize (Sensor obj) {
        this.obj = obj;
        // parts = new Dictionary<string, GameObject> { { "cannon", Referencer.prefab_controller.Add ("Prefabs/Modules/Sensor", PointF.zero, obj.size, obj.angle, this.transform) } };
    }
}