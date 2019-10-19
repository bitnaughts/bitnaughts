using System;
using System.Collections;
using UnityEngine;

public class GalaxyManager : MonoBehaviour {

    public GalaxyObject galaxy;

    public GameObject[] star_prefabs;
    public GameObject line_prefab;

    void Start () {

        /* Procedurally generates galaxy, including all systems, their planets, stars, asteroids, etc. */
        galaxy = new GalaxyObject ();
        print (galaxy);

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
    }

    void Update () {

    }
}