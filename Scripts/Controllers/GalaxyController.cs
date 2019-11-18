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
            // Visualize ();
        }
        // Debug.Log (galaxy.ToString ());
        // System.IO.File.WriteAllText (@"C:\test.txt", galaxy.ToString ());

        // save_result = database.Set<GalaxyObject> (galaxy);
        // save_result = database.Add<ShipObject> (new ShipObject(5));
        Create ();
    }

    int system_id = 0;

    void FixedUpdate () {
        // if (fetch_galaxy == null) {

        // } else {
        //     if (fetch_galaxy.IsCompleted) {
        //         Debug.Log (fetch_galaxy.Result);

        //         galaxy = new GalaxyObject (fetch_galaxy.Result);
        //         Visualize ();

        //     } else {
        //         Debug.Log (fetch_galaxy.Status);
        //     }
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
                    PointHandler.GetDistance (connected_system.position, system.position),
                    .05f,
                    .05f
                );
            }
        }
    }

    private struct TriangleIndices {
        public int v1;
        public int v2;
        public int v3;

        public TriangleIndices (int v1, int v2, int v3) {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }

    // return index of point in the middle of p1 and p2
    private int getMiddlePoint (int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius) {
        // first check if we have it already
        bool firstIsSmaller = p1 < p2;
        long smallerIndex = firstIsSmaller ? p1 : p2;
        long greaterIndex = firstIsSmaller ? p2 : p1;
        long key = (smallerIndex << 32) + greaterIndex;

        int ret;
        if (cache.TryGetValue (key, out ret)) {
            return ret;
        }

        // not in cache, calculate it
        Vector3 point1 = vertices[p1];
        Vector3 point2 = vertices[p2];
        Vector3 middle = new Vector3 (
            (point1.x + point2.x) / 2f,
            (point1.y + point2.y) / 2f,
            (point1.z + point2.z) / 2f
        );

        // add vertex makes sure point is on unit sphere
        int i = vertices.Count;
        vertices.Add (middle.normalized * radius);

        // store it, return index
        cache.Add (key, i);

        return i;
    }

    public void Create () {
        MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
        Mesh mesh = filter.mesh;
        mesh.Clear ();

        List<Vector3> vertList = new List<Vector3> ();
        Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int> ();
        int index = 0;

        int recursionLevel = 3;
        float radius = 1f;

        // create 12 vertices of a icosahedron
        float t = (1f + Mathf.Sqrt (5f)) / 2f;

        vertList.Add (new Vector3 (-1f, t, 0f).normalized * radius);
        vertList.Add (new Vector3 (1f, t, 0f).normalized * radius);
        vertList.Add (new Vector3 (-1f, -t, 0f).normalized * radius);
        vertList.Add (new Vector3 (1f, -t, 0f).normalized * radius);

        vertList.Add (new Vector3 (0f, -1f, t).normalized * radius);
        vertList.Add (new Vector3 (0f, 1f, t).normalized * radius);
        vertList.Add (new Vector3 (0f, -1f, -t).normalized * radius);
        vertList.Add (new Vector3 (0f, 1f, -t).normalized * radius);

        vertList.Add (new Vector3 (t, 0f, -1f).normalized * radius);
        vertList.Add (new Vector3 (t, 0f, 1f).normalized * radius);
        vertList.Add (new Vector3 (-t, 0f, -1f).normalized * radius);
        vertList.Add (new Vector3 (-t, 0f, 1f).normalized * radius);

        // create 20 triangles of the icosahedron
        List<TriangleIndices> faces = new List<TriangleIndices> ();

        // 5 faces around point 0
        faces.Add (new TriangleIndices (0, 11, 5));
        faces.Add (new TriangleIndices (0, 5, 1));
        faces.Add (new TriangleIndices (0, 1, 7));
        faces.Add (new TriangleIndices (0, 7, 10));
        faces.Add (new TriangleIndices (0, 10, 11));

        // 5 adjacent faces 
        faces.Add (new TriangleIndices (1, 5, 9));
        faces.Add (new TriangleIndices (5, 11, 4));
        faces.Add (new TriangleIndices (11, 10, 2));
        faces.Add (new TriangleIndices (10, 7, 6));
        faces.Add (new TriangleIndices (7, 1, 8));

        // 5 faces around point 3
        faces.Add (new TriangleIndices (3, 9, 4));
        faces.Add (new TriangleIndices (3, 4, 2));
        faces.Add (new TriangleIndices (3, 2, 6));
        faces.Add (new TriangleIndices (3, 6, 8));
        faces.Add (new TriangleIndices (3, 8, 9));

        // 5 adjacent faces 
        faces.Add (new TriangleIndices (4, 9, 5));
        faces.Add (new TriangleIndices (2, 4, 11));
        faces.Add (new TriangleIndices (6, 2, 10));
        faces.Add (new TriangleIndices (8, 6, 7));
        faces.Add (new TriangleIndices (9, 8, 1));

        // refine triangles
        for (int i = 0; i < recursionLevel; i++) {
            List<TriangleIndices> faces2 = new List<TriangleIndices> ();
            foreach (var tri in faces) {
                // replace triangle by 4 triangles
                int a = getMiddlePoint (tri.v1, tri.v2, ref vertList, ref middlePointIndexCache, radius);
                int b = getMiddlePoint (tri.v2, tri.v3, ref vertList, ref middlePointIndexCache, radius);
                int c = getMiddlePoint (tri.v3, tri.v1, ref vertList, ref middlePointIndexCache, radius);

                faces2.Add (new TriangleIndices (tri.v1, a, c));
                faces2.Add (new TriangleIndices (tri.v2, b, a));
                faces2.Add (new TriangleIndices (tri.v3, c, b));
                faces2.Add (new TriangleIndices (a, b, c));
            }
            faces = faces2;
        }

        mesh.vertices = vertList.ToArray ();

        List<int> triList = new List<int> ();
        for (int i = 0; i < faces.Count; i++) {
            triList.Add (faces[i].v1);
            triList.Add (faces[i].v2);
            triList.Add (faces[i].v3);
        }
        mesh.triangles = triList.ToArray ();
        mesh.uv = new Vector2[mesh.vertices.Length];

        Vector3[] normales = new Vector3[vertList.Count];
        for (int i = 0; i < normales.Length; i++)
            normales[i] = vertList[i].normalized;

        mesh.normals = normales;

        mesh.RecalculateBounds ();

        filter.mesh = mesh;
    }
}