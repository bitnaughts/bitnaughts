using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public class Gimbal : Module {

    public float angle;
    public int angle_high, angle_low; /* More limited gimbals support more weight */
    public List<Module> modules;

    public Gimbal (dynamic json) : base ((object) json) { }
    public Gimbal (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.angle = 0;
        this.action_speed = 10; /* Will be a calculation of less weight of modules, smaller size of gimbal */
        this.prefab_path = "Prefabs/Modules/Cache";
        modules = new List<Module> ();
    }
    public Gimbal (PointF center, SizeF size, int rotation, List<Module> modules) : base (center, size, rotation) {
        this.angle = 0;
        this.action_speed = 10; /* Will be a calculation of less weight of modules, smaller size of gimbal */
        this.prefab_path = "Prefabs/Modules/Cache";
        this.modules = modules;
    }

    /* API interfaces for scripting */
    public void Rotate (int amount) {

    }
}

public class GimbalController : ModuleController<Gimbal> {
    public override void Visualize () {
        obj.angle += .05f;
        parts["turret"].transform.rotation = ConversionHandler.ToQuaternion (obj.angle);
    }
    public override void Initialize (Gimbal obj) {
        this.obj = obj;
        parts = new Dictionary<string, GameObject> { { "turret", Referencer.prefab_controller.Add ("Prefabs/Modules/GimbalHead", PointF.zero, new SizeF (2, 2), obj.angle, this.transform) } };
    }
}