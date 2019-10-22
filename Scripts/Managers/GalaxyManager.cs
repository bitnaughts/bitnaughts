using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GalaxyManager : MonoBehaviour {

    public GalaxyObject galaxy;

    public GameObject[] star_prefabs;
    public GameObject line_prefab;

    void Start () {
        // print (.ToString());

        /* Procedurally generates galaxy, including all systems, their planets, stars, asteroids, etc. */
        if (DatabaseHandler.LOAD_FROM_DATABASE == false) {
            galaxy = new GalaxyObject (4);

            // print (galaxy);

            /* Generates systems, represented by sprites */
            foreach (SystemObject system in galaxy.systems) {
                Instantiate (
                    star_prefabs[system.stars.Count - 1],
                    ConversionHandler.ToVector2 (system.position),
                    this.transform.rotation
                );

                /* Generates connection lines between neighboring systems */
                foreach (SystemObject connected_system in system.connected_systems) {

                    /* Creates a new line between two systems, pointed between the two systems */
                    GameObject line = Instantiate (
                        line_prefab,
                        new Vector2 (
                            (connected_system.position.X + system.position.X) / 2,
                            (connected_system.position.Y + system.position.Y) / 2
                        ),
                        Quaternion.Euler (
                            new Vector3 (
                                0,
                                0,
                                Mathf.Rad2Deg * Mathf.Atan (
                                    (connected_system.position.Y - system.position.Y) / (connected_system.position.X - system.position.X)
                                )
                            )
                        )
                    ) as GameObject;

                    /* Sets line's length equal to distance between systems */
                    line.transform.localScale = new Vector3 (
                        Mathf.Sqrt (
                            Mathf.Pow (connected_system.position.X - system.position.X, 2) + Mathf.Pow (connected_system.position.Y - system.position.Y, 2)
                        ),
                        .05f,
                        .05f
                    );
                }
            }

            /* Save Galaxy objects to DB */
            Task<string> task = Task.Run<string> (async () => await DatabaseHandler.Create (galaxy));
            print ("Save result:" + task.Result);
        }
    }

    int system_id = 0;
    void Update () {
        // if (system_id < galaxy.systems.Count) {
        if (system_id++ == 400) {
            // print();
            // System.IO.File.WriteAllText (@"C:\test.txt", galaxy.ToString ());
            // 
        }
        //     system_id++;
        // }
    }
}