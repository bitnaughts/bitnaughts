using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Main : MonoBehaviour
{
    public int divisions = 0;
    public float radius = 1f;

    private void Awake()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Sphere sphere = SphereCreator.Create(divisions, radius, "World");
        meshFilter.mesh = sphere.Render();
    }
}

public class Sphere
{
    public Vector3[] sNormals;
    public Triangle[] sTriangles;
    public Vector3[] sVertices;
    public string sName;

    public Sphere(Vector3[] aNormals, Triangle[] aTriangles, Vector3[] aVertices, string aName)
    {
        sNormals = aNormals;
        sTriangles = aTriangles;
        sVertices = aVertices;
        sName = aName;
    }

    public Mesh Render()
    {
        // Turn Triangle array into int array of vertices
        int[] triangles = new int[sTriangles.Length * 3];

        int i = 0;
        foreach (Triangle triangle in sTriangles)
        {
            triangles[i + 0] = triangle.vertices[0];
            triangles[i + 1] = triangle.vertices[1];
            triangles[i + 2] = triangle.vertices[2];
            i += 3;
        }

        // Create the Mesh
        Mesh mesh = new Mesh
        {
            name = sName,
            vertices = sVertices,
            triangles = triangles,
            normals = sNormals
        };

        return mesh;
    }
}

public class Triangle
{
    public int[] vertices;
    public Triangle(int a, int b, int c)
    {
        vertices = new int[]
        {
            a, b, c
        };
    }
}

public class SphereCreator
{
    public static Vector3[] normals;
    public static Triangle[] triangles;
    private static List<Vector3> vertices;

    public static int GetMidPointIndex(Dictionary<int, int> cache, int indexA, int indexB, List<Vector3> vertices)
    {
        // Checks if vertice has already been made and creates it if it hasn't
        int smallerIndex = Mathf.Min(indexA, indexB);
        int greaterIndex = Mathf.Max(indexA, indexB);
        int key = (smallerIndex << 16) + greaterIndex;

        if (cache.TryGetValue(key, out int ret))
            return ret;

        Vector3 p1 = vertices[indexA];
        Vector3 p2 = vertices[indexB];
        Vector3 middle = Vector3.Lerp(p1, p2, 0.5f).normalized;

        ret = vertices.Count;

        vertices.Add(middle);

        cache.Add(key, ret);
        return ret;
    }

    public static Sphere Create(int divisions, float radius, string name)
    {
        float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        // Initial Vertices and Triangles of Icosahedron
        vertices = new List<Vector3>
        {
            new Vector3(-1, t, 0).normalized,
            new Vector3(1, t, 0).normalized,
            new Vector3(-1, -t, 0).normalized,
            new Vector3(1, -t, 0).normalized,
            new Vector3(0, -1, t).normalized,
            new Vector3(0, 1, t).normalized,
            new Vector3(0, -1, -t).normalized,
            new Vector3(0, 1, -t).normalized,
            new Vector3(t, 0, -1).normalized,
            new Vector3(t, 0, 1).normalized,
            new Vector3(-t, 0, -1).normalized,
            new Vector3(-t, 0, 1).normalized
        };
        triangles = new Triangle[]
        {
            new Triangle(0, 11, 5),
            new Triangle(0, 1, 7),
            new Triangle(0, 5, 1),
            new Triangle(0, 7, 10),
            new Triangle(0, 10, 11),
            new Triangle(1, 5, 9),
            new Triangle(5, 11, 4),
            new Triangle(11, 10, 2),
            new Triangle(10, 7, 6),
            new Triangle(7, 1, 8),
            new Triangle(3, 9, 4),
            new Triangle(3, 4, 2),
            new Triangle(3, 2, 6),
            new Triangle(3, 6, 8),
            new Triangle(3, 8, 9),
            new Triangle(4, 9, 5),
            new Triangle(2, 4, 11),
            new Triangle(6, 2, 10),
            new Triangle(8, 6, 7),
            new Triangle(9, 8, 1)
        };

        // Divide faces
        var midPointCache = new Dictionary<int, int>();
        for (int d = 0; d < divisions; d++)
        {
            var newTriangles = new List<Triangle>();
            for (int f = 0; f < triangles.Length; f++)
            {
                // Get current triangle
                Triangle triangle = triangles[f];

                // Get current triangle vertices
                int a = triangle.vertices[0];
                int b = triangle.vertices[1];
                int c = triangle.vertices[2];

                // Find vertice at centre of each edge
                int ab = GetMidPointIndex(midPointCache, a, b, vertices);
                int bc = GetMidPointIndex(midPointCache, b, c, vertices);
                int ca = GetMidPointIndex(midPointCache, c, a, vertices);

                // Create new Triangles
                newTriangles.Add(new Triangle(a, ab, ca));
                newTriangles.Add(new Triangle(b, bc, ab));
                newTriangles.Add(new Triangle(c, ca, bc));
                newTriangles.Add(new Triangle(ab, bc, ca));
            }
            triangles = newTriangles.ToArray();
        }

        // Create Normals
        normals = new Vector3[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            normals[i] = vertices[i] = vertices[i].normalized;
        }

        Sphere sphere = new Sphere(normals, triangles, vertices.ToArray(), name);

        return sphere;
    }
}