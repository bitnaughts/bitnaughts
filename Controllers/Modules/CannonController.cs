using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

public class Cannon : Module {

    public bool loaded;

    public Cannon (dynamic json) : base ((object) json) { }
    public Cannon (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.action_speed = 10; //Depending on size, type...?
        this.prefab_path = "Prefabs/Modules/Thruster";
    }
}

public class CannonController : ModuleController<Cannon> {

    Projectile ammunition;

    public override void Visualize () {
        // obj.angle += .01f;
        // parts["turret"].transform.rotation = ConversionHandler.ToQuaternion (obj.angle);
    }
    public override void Initialize (Cannon obj) {
        this.obj = obj;

        ammunition = new Projectile (
            obj.center,
            obj.size,
            obj.rotation
        );
        // parts = new Dictionary<string, GameObject> { { "cannon", Referencer.prefab_controller.Add ("Prefabs/Modules/Cannon", PointF.zero, obj.size, obj.angle, this.transform) } };
    }
    public bool Fire () {
        if (Action ()) {
            Referencer.prefab_controller.Add (ammunition);
            return true;
        }
        return false;
    }
    /* 
        OnReload trigger, calls interpreter's function to execute code when this cannon has reloaded?
        Benefits include showing more dynamic code execution without defaulting to while(true) clocks that don't scale well with increased execution speeds
     */
}