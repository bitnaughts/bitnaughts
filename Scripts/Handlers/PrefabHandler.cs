using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabHandler {

    //1:1 between class and prefab to represent class

    static Dictionary<string, UnityEngine.Object> cache = new Dictionary<string,  UnityEngine.Object> ();

    public static T Load<T> (string path) where T : UnityEngine.Object {
        if (!cache.ContainsKey (path))
            cache[path] = Resources.Load<T> (path);
        return (T) cache[path];
    }
    

    // public static Object Get (string type) {
    //     structure_prefabs.Each ((star, index) => { structure_prefabs[index] = Resources.Load<GameObject> ("Prefabs/Ship/Structure" + (index + 1)); });
    //     gimbal_prefab = Resources.Load<GameObject> ("Prefabs/Ship/Gimbal");
    //     switch (type) {
    //         case "":
    //             return Load<GameObject>(type);
    //     }
    //     return null;
    // }

}