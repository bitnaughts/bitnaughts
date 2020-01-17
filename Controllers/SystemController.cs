using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystem {

    public int id;
    public string name;
    public int seed;
    public PointF position;

    /* All stars, planets, asteroids, debris, etc. */
    public List<SolarSystem> connected_systems;
    public List<Star> stars;
    public List<Planet> planets;
    public List<Asteroid> asteroids;
    public List<Ship> ships;

    public SolarSystem (int seed) {
        this.id = Galaxy.SystemID++;

        /* Pre-seeds random number generator */
        this.seed = seed;
        RandomHandler.Seed (seed);

        /* Randonmly generates position in galaxy */
        this.position = RandomHandler.NextPosition (20);

        /* Randomly generates stars with own seeds */
        stars = new List<Star> ();
        for (int number_stars = RandomHandler.NextInt (GalaxyConstants.Systems.MIN_STARS, GalaxyConstants.Systems.MAX_STARS); number_stars > 0; number_stars--) {
            stars.Add (new Star (RandomHandler.NextSeed ()));
        }

        /* Randomly generates planets with own seeds */
        planets = new List<Planet> ();
        for (int number_planets = RandomHandler.NextInt (GalaxyConstants.Systems.MIN_PLANETS, GalaxyConstants.Systems.MAX_PLANETS); number_planets > 0; number_planets--) {
            planets.Add (new Planet (RandomHandler.NextSeed ()));
        }

        /* Randomly generates planets with own seeds */
        asteroids = new List<Asteroid> ();
        for (int number_asteroids = RandomHandler.NextInt (GalaxyConstants.Systems.MIN_ASTEROIDS, GalaxyConstants.Systems.MAX_ASTEROIDS); number_asteroids > 0; number_asteroids--) {
            asteroids.Add (new Asteroid (RandomHandler.NextSeed ()));
        }

        ships = new List<Ship> ();
    }

    public SolarSystem (string json) {

        dynamic data = JSONHandler.ToDynamic (json);

        string[] fields = data.system.ToString ().Split (',');

        this.id = int.Parse (fields[0]);
        this.seed = int.Parse (fields[2]);
        this.position = new PointF (float.Parse (fields[3]), float.Parse (fields[4]));

        stars = new List<Star> ();

        planets = new List<Planet> ();
        foreach (string planet_serial in data.planets.ToString ().Split ('\n')) {
            planets.Add (new Planet (planet_serial));
        }

        asteroids = new List<Asteroid> ();
        foreach (string asteroid_serial in data.asteroids.ToString ().Split ('\n')) {
            asteroids.Add (new Asteroid (asteroid_serial));
        }

        ships = new List<Ship> ();
        foreach (string ship_serial in data.ships.ToString ().Split ('\n')) {
            ships.Add (new Ship (ship_serial));
        }
    }

    public override string ToString () {
        return JSONHandler.ToJSON (
            new Dictionary<string, string> { { "id", id.ToString () },
                { "seed", seed.ToString () },
                { "position_x", position.x.ToString ("F") },
                { "position_y", position.y.ToString ("F") },
                { "connected_systems", JSONHandler.ToJSONArray<int> (connected_systems.Select (connected_system => connected_system.id).ToList ()) },
                { "stars", JSONHandler.ToJSONArray<Star> (stars) },
                { "planets", JSONHandler.ToJSONArray<Planet> (planets) },
                { "asteroids", JSONHandler.ToJSONArray<Asteroid> (asteroids) },
                // { "ships", "[" + String.Join (",", ships.Select (x => x.ToString ()).ToArray ()) + "]" }
            }
        );
    }
}

public class SystemController : MonoBehaviour {

    // GameObject[] star_prefabs = new GameObject[4];
    // GameObject line_prefab;

    SolarSystem system;

    Task<string> fetch_obj { get; set; }
    Database database { get; set; }

    bool RENDERING_SYSTEM = false;

    void Visualize () {

        List<Module> modules = new List<Module> ();
        List<Module> g_modules = new List<Module> ();
        g_modules.Add (new Structure (
            new PointF (0, 0),
            new SizeF (1, 7),
            0 /* rotation */
        ));
        g_modules.Add (new Structure (
            new PointF (0, -3),
            new SizeF (3, 3),
            0 /* rotation */
        ));
        g_modules.Add (new Cannon (
            new PointF (0, 3),
            new SizeF (1, 2),
            0 /* rotation */
        ));
        modules.Add (new Mainframe (
            new PointF (0, 0),
            new SizeF (7, 7),
            0 /* rotation */
        ));
        modules.Add (new Printer (
            new PointF (-7, 0),
            new SizeF (7, 7),
            0, /* rotation */
            "Prefabs/Modules/Cache"
        ));
        modules.Add (new Gimbal (
            new PointF (7f, 0),
            new SizeF (7, 7),
            0, /* rotation */
            g_modules
        ));

        string data = @"
{
  'id': 0,
  'player_id': 0,
  'debug': null,
  'modules': [
    {
      'type': 'Printer',
      'center': {
        'x': 0.0,
        'y': 0.0,
        'z': 0.0
      },
      'size': {
        'x': 7.0,
        'y': 7.0,
        'z': 0.0
      },
      'rotation': 0,
      'action_speed': 10.0,
      'hitpoints': 1.0,
      'color': {
        'x': 0.0,
        'y': 0.0,
        'z': 0.0
      }
    },
    {
      'type': 'Printer',
      'center': {
        'x': -7.0,
        'y': 0.0,
        'z': 0.0
      },
      'size': {
        'x': 7.0,
        'y': 7.0,
        'z': 0.0
      },
      'rotation': 0,
      'action_speed': 10.0,
      'hitpoints': 1.0,
      'color': {
        'x': 0.0,
        'y': 0.0,
        'z': 0.0
      }
    },
    {
      'type': 'Gimbal',
      'center': {
        'x': 7.0,
        'y': 0.0,
        'z': 0.0
      },
      'size': {
        'x': 7.0,
        'y': 7.0,
        'z': 0.0
      },
      'rotation': 0,
      'action_speed': 10.0,
      'hitpoints': 1.0,
      'color': {
        'x': 0.0,
        'y': 0.0,
        'z': 0.0
      }
    }
  ]
}
";
        dynamic json = JSONHandler.ToDynamic (data);
        print (json);

        system.ships.Add (new Ship (modules));

        foreach (var ship in system.ships) {
            ship.obj = Referencer.prefab_controller.Add (ship);
            ship.obj.AddComponent<ShipController> ().Initialize (ship);
        }

        if (RENDERING_SYSTEM) {
            /* Generates system */
            foreach (Planet planet in system.planets) {
                GameObject planet_obj = Referencer.prefab_controller.Add (
                    "Prefabs/GalaxyElements/Planets/planet" + ((planet.seed % 10) + 1),
                    ConversionHandler.ToVector2 (planet.orbit),
                    new Vector2 (planet.size, planet.size),
                    this.transform.rotation,
                    this.transform
                );
                /* Add on-click trigger for obj */
                Referencer.interaction_controller.Add (ConversionHandler.ToPointF (planet.orbit), planet.size / 4f, "Planet " + planet.id, planet_obj);
            }

            foreach (Asteroid asteroid in system.asteroids) {
                GameObject asteroid_obj = Referencer.prefab_controller.Add (
                    "Prefabs/GalaxyElements/Asteroids/asteroid" + ((asteroid.seed % 2) + 1),
                    ConversionHandler.ToVector2 (asteroid.orbit),
                    new Vector2 (asteroid.size / 5f, asteroid.size / 5f),
                    this.transform.rotation,
                    this.transform
                );
                /* Add on-click trigger for obj */
                Referencer.interaction_controller.Add (ConversionHandler.ToPointF (asteroid.orbit), asteroid.size / 4f, "Asteroid " + asteroid.id, asteroid_obj);
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

        system = new SolarSystem (4);

        // fetch_obj = Referencer.database.Get<SolarSystem> (Referencer.system_id);
        // line_prefab = Resources.Load<GameObject> ("Prefabs/GalaxyElements/Lines/line");
        // star_prefabs.Each ((star, index) => { star_prefabs[index] = Resources.Load<GameObject> ("Prefabs/GalaxyElements/Stars/star" + (index + 1)); });

        // if (Database.LOAD_FROM_SERVER) {
        //Get simple systems info, connected systems, not full object 
        // fetch_obj = database.Get (0);

        // } else {
        /* Procedurally generates obj, including all systems, their planets, stars, asteroids, etc. */
        // galaxy = new Galaxy (10);
        // Visualize ();
        // }
        // Debug.Log (obj.ToString ());
        // System.IO.File.WriteAllText (@"C:\test.txt", obj.ToString ());

        // save_result = database.Set (obj);
        // save_result = database.Add<ShipObject> (new ShipObject(5));
        // Create ();
        Visualize ();
    }

    int system_id = 0;

    void Update () {
        // if (fetch_obj == null) { } else {
        //     if (fetch_obj.IsCompleted) {

        //         Debug.Log (fetch_obj.Result);

        //         system = new SolarSystem (fetch_obj.Result);

        //         Visualize ();

        //         fetch_obj = null;
        //     } else {
        //         Debug.Log (fetch_obj.Status);
        //     }
        // }
    }

    void FixedUpdate () {
        // if (fetch_obj == null) {

        // } else {
        //     if (fetch_obj.IsCompleted) {
        //         Debug.Log (fetch_obj.Result);

        //         obj = new Galaxy (fetch_obj.Result);
        //         Visualize ();

        //     } else {
        //         Debug.Log (fetch_obj.Status);
        //     }
        // }
    }

    // private struct TriangleIndices {
    //     public int v1;
    //     public int v2;
    //     public int v3;

    //     public TriangleIndices (int v1, int v2, int v3) {
    //         this.v1 = v1;
    //         this.v2 = v2;
    //         this.v3 = v3;
    //     }
    // }

    // // return index of point in the middle of p1 and p2
    // private int getMiddlePoint (int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius) {
    //     // first check if we have it already
    //     bool firstIsSmaller = p1 < p2;
    //     long smallerIndex = firstIsSmaller ? p1 : p2;
    //     long greaterIndex = firstIsSmaller ? p2 : p1;
    //     long key = (smallerIndex << 32) + greaterIndex;

    //     int ret;
    //     if (cache.TryGetValue (key, out ret)) {
    //         return ret;
    //     }

    //     // not in cache, calculate it
    //     Vector3 point1 = vertices[p1];
    //     Vector3 point2 = vertices[p2];
    //     Vector3 middle = new Vector3 (
    //         (point1.x + point2.x) / 2f,
    //         (point1.y + point2.y) / 2f,
    //         (point1.z + point2.z) / 2f
    //     );

    //     // add vertex makes sure point is on unit sphere
    //     int i = vertices.Count;
    //     vertices.Add (middle.normalized * radius);

    //     // store it, return index
    //     cache.Add (key, i);

    //     return i;
    // }

    // public void Create () {
    //     MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
    //     Mesh mesh = filter.mesh;
    //     mesh.Clear ();

    //     List<Vector3> vertList = new List<Vector3> ();
    //     Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int> ();
    //     int index = 0;

    //     int recursionLevel = 3;
    //     float radius = 1f;

    //     // create 12 vertices of a icosahedron
    //     float t = (1f + Mathf.Sqrt (5f)) / 2f;

    //     vertList.Add (new Vector3 (-1f, t, 0f).normalized * radius);
    //     vertList.Add (new Vector3 (1f, t, 0f).normalized * radius);
    //     vertList.Add (new Vector3 (-1f, -t, 0f).normalized * radius);
    //     vertList.Add (new Vector3 (1f, -t, 0f).normalized * radius);

    //     vertList.Add (new Vector3 (0f, -1f, t).normalized * radius);
    //     vertList.Add (new Vector3 (0f, 1f, t).normalized * radius);
    //     vertList.Add (new Vector3 (0f, -1f, -t).normalized * radius);
    //     vertList.Add (new Vector3 (0f, 1f, -t).normalized * radius);

    //     vertList.Add (new Vector3 (t, 0f, -1f).normalized * radius);
    //     vertList.Add (new Vector3 (t, 0f, 1f).normalized * radius);
    //     vertList.Add (new Vector3 (-t, 0f, -1f).normalized * radius);
    //     vertList.Add (new Vector3 (-t, 0f, 1f).normalized * radius);

    //     // create 20 triangles of the icosahedron
    //     List<TriangleIndices> faces = new List<TriangleIndices> ();

    //     // 5 faces around point 0
    //     faces.Add (new TriangleIndices (0, 11, 5));
    //     faces.Add (new TriangleIndices (0, 5, 1));
    //     faces.Add (new TriangleIndices (0, 1, 7));
    //     faces.Add (new TriangleIndices (0, 7, 10));
    //     faces.Add (new TriangleIndices (0, 10, 11));

    //     // 5 adjacent faces 
    //     faces.Add (new TriangleIndices (1, 5, 9));
    //     faces.Add (new TriangleIndices (5, 11, 4));
    //     faces.Add (new TriangleIndices (11, 10, 2));
    //     faces.Add (new TriangleIndices (10, 7, 6));
    //     faces.Add (new TriangleIndices (7, 1, 8));

    //     // 5 faces around point 3
    //     faces.Add (new TriangleIndices (3, 9, 4));
    //     faces.Add (new TriangleIndices (3, 4, 2));
    //     faces.Add (new TriangleIndices (3, 2, 6));
    //     faces.Add (new TriangleIndices (3, 6, 8));
    //     faces.Add (new TriangleIndices (3, 8, 9));

    //     // 5 adjacent faces 
    //     faces.Add (new TriangleIndices (4, 9, 5));
    //     faces.Add (new TriangleIndices (2, 4, 11));
    //     faces.Add (new TriangleIndices (6, 2, 10));
    //     faces.Add (new TriangleIndices (8, 6, 7));
    //     faces.Add (new TriangleIndices (9, 8, 1));

    //     // refine triangles
    //     for (int i = 0; i < recursionLevel; i++) {
    //         List<TriangleIndices> faces2 = new List<TriangleIndices> ();
    //         foreach (var tri in faces) {
    //             // replace triangle by 4 triangles
    //             int a = getMiddlePoint (tri.v1, tri.v2, ref vertList, ref middlePointIndexCache, radius);
    //             int b = getMiddlePoint (tri.v2, tri.v3, ref vertList, ref middlePointIndexCache, radius);
    //             int c = getMiddlePoint (tri.v3, tri.v1, ref vertList, ref middlePointIndexCache, radius);

    //             faces2.Add (new TriangleIndices (tri.v1, a, c));
    //             faces2.Add (new TriangleIndices (tri.v2, b, a));
    //             faces2.Add (new TriangleIndices (tri.v3, c, b));
    //             faces2.Add (new TriangleIndices (a, b, c));
    //         }
    //         faces = faces2;
    //     }

    //     mesh.vertices = vertList.ToArray ();

    //     List<int> triList = new List<int> ();
    //     for (int i = 0; i < faces.Count; i++) {
    //         triList.Add (faces[i].v1);
    //         triList.Add (faces[i].v2);
    //         triList.Add (faces[i].v3);
    //     }
    //     mesh.triangles = triList.ToArray ();
    //     mesh.uv = new Vector2[mesh.vertices.Length];

    //     Vector3[] normales = new Vector3[vertList.Count];
    //     for (int i = 0; i < normales.Length; i++)
    //         normales[i] = vertList[i].normalized;

    //     mesh.normals = normales;

    //     mesh.RecalculateBounds ();

    //     filter.mesh = mesh;
    // }
}