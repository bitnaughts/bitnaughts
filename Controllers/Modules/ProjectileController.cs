using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class Projectile : Module {

    public Projectile (dynamic json) : base ((object) json) { }
    public Projectile (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        //action_speed controls speed of projectile
        this.prefab_path = "Prefabs/Modules/Hardpoint";
    }
}

public class Rocket : Projectile {

    float stability; //0-1, how choatic/accurate it is. e.g. hellfire missile from starwars clone wars

    public Rocket (dynamic json) : base ((object) json) { }
    public Rocket (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        //action_speed controls speed of projectile
        this.prefab_path = "Prefabs/Modules/Hardpoint";
    }
}

public class Missile : Rocket {

    Script script;

    public Missile (dynamic json) : base ((object) json) { }
    public Missile (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        //action_speed controls speed of projectile
        this.prefab_path = "Prefabs/Modules/Hardpoint";
    }
}

public class ProjectileController : ModuleController<Projectile> {
    
    public override void Visualize () {
        // obj.transform.Translate(new Vector2(0, obj.speed));
    }
    public override void Initialize (Projectile obj) {
        this.obj = obj;
        // parts = new Dictionary<string, GameObject> { { "cannon", Referencer.prefab_controller.Add ("Prefabs/Modules/Cannon", PointF.zero, obj.size, obj.angle, this.transform) } };
    }
 }