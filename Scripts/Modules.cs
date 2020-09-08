using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Structure : Module {

    public Structure (dynamic json) : base ((object) json) { }
    public Structure (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.action_speed = 10; //...
        this.prefab_path = "Prefabs/Modules/Beam"; //this.prefab_path = "Prefabs/Modules/Grid"; //upgraded version
    }
}

public class Engine : Module {

    public Engine (dynamic json) : base ((object) json) { }
    public Engine (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.action_speed = 10; //thrust vector determined by engine size, fuel flow?
        this.prefab_path = "Prefabs/Modules/Engine";
    }
}

public class Thruster : Module {

    public Thruster (dynamic json) : base ((object) json) { }
    public Thruster (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.action_speed = 10; //thrust vector determined by engine size, fuel flow?
        this.prefab_path = "Prefabs/Modules/Thruster";
    }
}

public class Tractor : Module {

    public Tractor (dynamic json) : base ((object) json) { }
    public Tractor (PointF center, SizeF size, int rotation) : base (center, size, rotation) {
        this.action_speed = 10;
        this.prefab_path = "Prefabs/Modules/Hardpoint";
    }
}


