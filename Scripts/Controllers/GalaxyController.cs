using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyController : MonoBehaviour {

    public GalaxyObject galaxy;

    GameObject[] star_prefabs = new GameObject[4];
    GameObject line_prefab;

    Database database;

    Task<string> fetch_galaxy;

    void Start () {

        database = GameObject.Find ("CodeManager").GetComponent<Database> ();

        line_prefab = Resources.Load<GameObject> ("Prefabs/GalaxyElements/Lines/line");
        star_prefabs.Each ((star, index) => { star_prefabs[index] = Resources.Load<GameObject> ("Prefabs/GalaxyElements/Stars/star" + (index + 1)); });

        if (Database.LOAD_FROM_SERVER) {
            //Get simple systems info, connected systems, not full object 
            fetch_galaxy = database.Get<GalaxyObject> (0);

        } else {
            /* Procedurally generates galaxy, including all systems, their planets, stars, asteroids, etc. */
            galaxy = new GalaxyObject (10);
            Visualize ();
        }
        // Debug.Log (galaxy.ToString ());
        // System.IO.File.WriteAllText (@"C:\test.txt", galaxy.ToString ());

        // save_result = database.Set<GalaxyObject> (galaxy);
        // save_result = database.Add<ShipObject> (new ShipObject(5));

        
    }

    int system_id = 0;

    void FixedUpdate () {
        if (fetch_galaxy == null) {

        } else {
            if (fetch_galaxy.IsCompleted) {
                Debug.Log (fetch_galaxy.Result);

                galaxy = new GalaxyObject (fetch_galaxy.Result);
                Visualize ();

            } else {
                Debug.Log (fetch_galaxy.Status);
            }
        }
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
                    PointHandler.GetDistance (connected_system.position, system.position),
                    .05f,
                    .05f
                );
            }
        }
    }
}