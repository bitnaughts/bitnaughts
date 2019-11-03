using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyManager : MonoBehaviour {

    public GalaxyObject galaxy;

    public GameObject[] star_prefabs;
    public GameObject line_prefab;

    async void Start () {
        // print (.ToString());

        if (DatabaseHandler.LOAD_FROM_DATABASE) {

            /* Loads galaxy object from database, only including necessary objects for visualization in galaxy view */
            Task<string> task = Task.Run<string> (async () => await DatabaseHandler.Get<GalaxyObject> (666)); // 666 == current galaxy_id

            /* Loads results from database into Galaxy object */
            galaxy = new GalaxyObject (task.Result);
            print (task.Result + "\n" + galaxy.ToString ());

        } else {

            /* Procedurally generates galaxy, including all systems, their planets, stars, asteroids, etc. */
            galaxy = new GalaxyObject (4);

            // debugger.GetComponent<Text>().text += await DatabaseHandler.Set<GalaxyObject> (galaxy);

            /* Saves Galaxy objects to DB, destructively overriding old data */
            // Task<string> task = Task.Run<string> (async () => await DatabaseHandler.Set<GalaxyObject> (galaxy));
            // print ("Save result:" + task.Result);
        }
        Visualize ();
        // string output = await DatabaseHandler.Reset();
        // debugger.GetComponent<Text>().text += output;
        // print (output);
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

    public void Visualize () {
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
                    ConversionHandler.ToVector2 (
                        PointHandler.GetMidpoint (connected_system.position, system.position)
                    ),
                    ConversionHandler.ToQuaternion (
                        PointHandler.GetAngle (connected_system.position, system.position)
                    )
                ) as GameObject;

                /* Sets line's length equal to distance between systems */
                line.transform.localScale = new Vector3 (
                    PointHandler.GetDistance(connected_system.position, system.position),
                    .05f,
                    .05f
                );
            }
        }
    }
}