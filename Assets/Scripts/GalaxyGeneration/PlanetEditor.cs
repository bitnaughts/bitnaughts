using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEditor : MonoBehaviour {
    // Start is called before the first frame update
    List<TriangleHolder> triangles = new List<TriangleHolder> ();
    public GameObject marker;
    void Start () {
        
        print (CreateNSphere (1));
        for (int i = 0; i < triangles.Count; i++) {
            Instantiate(marker, triangles[i].p1, this.transform.rotation);
        }
          triangles = new List<TriangleHolder> ();
        print (CreateNSphere (2));
        for (int i = 0; i < triangles.Count; i++) {
            triangles[i].p1.x += 5;
            Instantiate(marker, triangles[i].p1, this.transform.rotation);
        }
         triangles = new List<TriangleHolder> ();
        print (CreateNSphere (3));
        for (int i = 0; i < triangles.Count; i++) {
            triangles[i].p1.x += 10;
            Instantiate(marker, triangles[i].p1, this.transform.rotation);
        }
        triangles = new List<TriangleHolder> ();
        print (CreateNSphere (4));
        for (int i = 0; i < triangles.Count; i++) {
            triangles[i].p1.x += 15;
            Instantiate(marker, triangles[i].p1, this.transform.rotation);
        }
        triangles = new List<TriangleHolder> ();
        print (CreateNSphere (5));
        for (int i = 0; i < triangles.Count; i++) {
            triangles[i].p1.x += 20;
            Instantiate(marker, triangles[i].p1, this.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update () {

    }

    /* http://paulbourke.net/geometry/circlesphere/csource3.c
     * FROM https://bendwavy.org/sphere.htm
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * Create a triangular facet approximation to a sphere
     * Return the number of facets created.
     * The number of facets will be (4^iterations) * 8
     */
    int CreateNSphere (int iterations) {

        int i, it;
        float a;
        Vector3[] p = { new Vector3 (0, 0, 1), new Vector3 (0, 0, -1), new Vector3 (-1, -1, 0), new Vector3 (1, -1, 0), new Vector3 (1, 1, 0), new Vector3 (-1, 1, 0) };
        Vector3 pa, pb, pc;
        int nt = 0, ntold;

        /* Create the level 0 object */
        a = 1 / Mathf.Sqrt (2.0f);
        for (i = 0; i < 6; i++) {
            p[i].x *= a;
            p[i].y *= a;
        }
        triangles.Add (new TriangleHolder (p[0], p[3], p[4]));
        triangles.Add (new TriangleHolder (p[0], p[4], p[5]));
        triangles.Add (new TriangleHolder (p[0], p[5], p[2]));
        triangles.Add (new TriangleHolder (p[0], p[2], p[3]));
        triangles.Add (new TriangleHolder (p[1], p[4], p[3]));
        triangles.Add (new TriangleHolder (p[1], p[5], p[4]));
        triangles.Add (new TriangleHolder (p[1], p[2], p[5]));
        triangles.Add (new TriangleHolder (p[1], p[3], p[2]));
        nt = 8;

        if (iterations < 1) return nt;

        /* Bisect each edge and move to the surface of a unit sphere */
        for (it = 0; it < iterations; it++) {
            ntold = nt;
            for (i = 0; i < ntold; i++) {
                pa.x = (triangles[i].p1.x + triangles[i].p2.x) / 2;
                pa.y = (triangles[i].p1.y + triangles[i].p2.y) / 2;
                pa.z = (triangles[i].p1.z + triangles[i].p2.z) / 2;
                pb.x = (triangles[i].p2.x + triangles[i].p3.x) / 2;
                pb.y = (triangles[i].p2.y + triangles[i].p3.y) / 2;
                pb.z = (triangles[i].p2.z + triangles[i].p3.z) / 2;
                pc.x = (triangles[i].p3.x + triangles[i].p1.x) / 2;
                pc.y = (triangles[i].p3.y + triangles[i].p1.y) / 2;
                pc.z = (triangles[i].p3.z + triangles[i].p1.z) / 2;
                pa = Vector3.Normalize (pa);
                pb = Vector3.Normalize (pb);
                pc = Vector3.Normalize (pc);
                print (nt);

                makeTriangle (nt++, new TriangleHolder (triangles[i].p1, pa, pc));
                makeTriangle (nt++, new TriangleHolder (pa, triangles[i].p2, pb));
                makeTriangle (nt++, new TriangleHolder (pb, triangles[i].p3, pc));
                makeTriangle (i, new TriangleHolder (pa, pb, pc));
            }
        }

        return nt;
    }
    public void makeTriangle (int index, TriangleHolder triangle) {
        if (index >= triangles.Count) {
            triangles.Add (triangle);
        } else triangles[index] = triangle;
    }

}
public class TriangleHolder {
    public Vector3 p1, p2, p3;
    public TriangleHolder (Vector3 p1, Vector3 p2, Vector3 p3) {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }
}