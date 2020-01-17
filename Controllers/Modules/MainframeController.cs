using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public class Mainframe : Module {

    Script script;
    Tractor tractor; //Has tractor beam to interact with nearby objects, place, move, remove modules on own ship

    public Mainframe (dynamic json) : base ((object) json) { }
    public Mainframe (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.action_speed = 10; //interpreter speed, quality of cpu
        this.prefab_path = "Prefabs/Modules/Mainframe";
        // this.script = new Script ();
    }
}

public class MainframeController : ModuleController<Mainframe> {
    public override void Visualize () {
        // obj.angle += .01f;
        // parts["turret"].transform.rotation = ConversionHandler.ToQuaternion (obj.angle);
    }
    public override void Initialize (Mainframe obj) {
        this.obj = obj;
        // parts = new Dictionary<string, GameObject> { { "cannon", Referencer.prefab_controller.Add ("Prefabs/Modules/Sensor", PointF.zero, obj.size, obj.angle, this.transform) } };
    }
}