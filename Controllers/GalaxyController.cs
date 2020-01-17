using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class Galaxy {

    public static int GalaxyID = 0, SystemID = 0, StarID, PlanetID = 0, AsteroidID = 0, ShipID = 0;

    public int seed;

    public List<SolarSystem> systems = new List<SolarSystem> ();

    public Galaxy (string json) {
        // ToObject (json);
    }

    public Galaxy (int seed) {

        /* Pre-seeds random number generator */
        this.seed = seed;
        RandomHandler.Seed(seed);

        /* Randomly generates systems with own seeds */
        for (int number_systems = RandomHandler.NextInt (GalaxyConstants.MIN_SYSTEMS, GalaxyConstants.MAX_SYSTEMS); number_systems > 0; number_systems--) {
            systems.Add (new SolarSystem (RandomHandler.NextSeed ()));
        }

        /* Connects systems to each other */
        foreach (SolarSystem system in systems) {

            /* Larger systems connect more to other systems */
            int number_connections = system.stars.Count * 2;

            /* Initializes arrays to be populated with optimal solution */
            SolarSystem[] closest_systems = new SolarSystem[number_connections];
            float[] closest_distances = new float[number_connections];

            for (int i = 0; i < number_connections; i++) {
                closest_distances[i] = Single.MaxValue;
            }

            /* Iterates over all other systems to find closest systems to current system */
            foreach (SolarSystem other_system in systems) {
                if (system == other_system) continue;

                float distance_between = PointF.GetDistance (system.position, other_system.position);

                for (int i = 0; i < number_connections; i++) {
                    if (closest_distances[i] > distance_between) {

                        /* Bubbles less optimal connections out of array */
                        for (int j = number_connections - 1; j > i; j--) {
                            closest_distances[j] = closest_distances[j - 1];
                            closest_systems[j] = closest_systems[j - 1];
                        }

                        closest_distances[i] = distance_between;
                        closest_systems[i] = other_system;
                        break;
                    }
                }
            }
            system.connected_systems = new List<SolarSystem> (closest_systems);
        }
    }

    public override string ToString () {
        return JSONHandler.ToJSON (
            new Dictionary<string, string> { { "id", GalaxyID.ToString () },
                { "seed", seed.ToString () },
                { "systems", JSONHandler.ToJSONArray<SolarSystem>(systems) }
            }
        );
    }
}

public class GalaxyController : MonoBehaviour {

    GameObject[] star_prefabs = new GameObject[4];
    GameObject line_prefab;

    Galaxy galaxy;

    Task<string> fetch_obj { get; set; }

    void Visualize () {
        /* Generates systems, represented by sprites */
        foreach (SolarSystem system in galaxy.systems) {
            int size = 1;
            if (system.stars != null) size = system.stars.Count;

            GameObject star = Referencer.prefab_controller.Add (
                "Prefabs/GalaxyElements/Stars/star" + size,
                ConversionHandler.ToVector2 (system.position),
                this.transform.localScale,
                this.transform.rotation,
                this.transform
            );

            /* Add on-click trigger for obj */
            Referencer.interaction_controller.Add (system.position.x, system.position.y, .1f, "" + system.id, star);

            /* Generates connection lines between neighboring systems */
            foreach (SolarSystem connected_system in system.connected_systems) {

                /* Creates a new line between two systems, pointed between the two systems */
                GameObject line = Referencer.prefab_controller.Add (
                    "Prefabs/GalaxyElements/Lines/line",
                    ConversionHandler.ToVector2 (
                        PointF.GetMidpoint (connected_system.position, system.position)
                    ),
                    new Vector3 (
                        PointF.GetDistance (connected_system.position, system.position),
                        .05f,
                        .05f
                    ),
                    ConversionHandler.ToQuaternion (
                        PointF.GetAngle (connected_system.position, system.position)
                    ),
                    this.transform
                );
            }
        }
    }

    bool Fetch () {
        // if (fetch_obj != null && fetch_obj.IsCompleted) {
        //     galaxy = new Galaxy (fetch_obj.Result);
        //     Visualize ();
        //     return true;
        // }
        return false;
    }

    void Start () {
        if (Database.LOAD_FROM_SERVER) {
            // fetch_obj = database.Get (0);
        } else {
            galaxy = new Galaxy (10); /* Procedurally generates obj, including all systems, their planets, stars, asteroids, etc. */
            Visualize ();
        }
        fetch_obj = Referencer.database.Get<Galaxy> (0);
        // fetch_obj = Referencer.database.Set<Galaxy> (galaxy);
        // save_result = database.Add<ShipObject> (new ShipObject(5));
        Create ();
    }

    int system_id = 0;

    bool has_visualized = true;
    void FixedUpdate () {
        if (fetch_obj != null) {
            if (fetch_obj.IsCompleted) {
                // galaxy = new Galaxy (fetcsh_obj.Result);
                if (has_visualized) has_visualized = false;
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