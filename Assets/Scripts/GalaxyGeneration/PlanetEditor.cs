using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEditor : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {

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
    int CreateNSphere (FACET3 * f, int iterations) {
        int i, it;
        float a;
        Vector3[] p = { new Vector3 (0, 0, 1), new Vector3 (0, 0, -1), new Vector3 (-1, -1, 0), new Vector3 (1, -1, 0), new Vector3 (1, 1, 0), new Vector3 (-1, 1, 0) };
        Vector3 pa, pb, pc;
        int nt = 0, ntold;

        /* Create the level 0 object */
        a = 1 / Mathf.Sqrt (2.0);
        for (i = 0; i < 6; i++) {
            p[i].x *= a;
            p[i].y *= a;
        }
        f[0].p1 = p[0];
        f[0].p2 = p[3];
        f[0].p3 = p[4];
        f[1].p1 = p[0];
        f[1].p2 = p[4];
        f[1].p3 = p[5];
        f[2].p1 = p[0];
        f[2].p2 = p[5];
        f[2].p3 = p[2];
        f[3].p1 = p[0];
        f[3].p2 = p[2];
        f[3].p3 = p[3];
        f[4].p1 = p[1];
        f[4].p2 = p[4];
        f[4].p3 = p[3];
        f[5].p1 = p[1];
        f[5].p2 = p[5];
        f[5].p3 = p[4];
        f[6].p1 = p[1];
        f[6].p2 = p[2];
        f[6].p3 = p[5];
        f[7].p1 = p[1];
        f[7].p2 = p[3];
        f[7].p3 = p[2];
        nt = 8;

        if (iterations < 1)
            return (nt);

        /* Bisect each edge and move to the surface of a unit sphere */
        for (it = 0; it < iterations; it++) {
            ntold = nt;
            for (i = 0; i < ntold; i++) {
                pa.x = (f[i].p1.x + f[i].p2.x) / 2;
                pa.y = (f[i].p1.y + f[i].p2.y) / 2;
                pa.z = (f[i].p1.z + f[i].p2.z) / 2;
                pb.x = (f[i].p2.x + f[i].p3.x) / 2;
                pb.y = (f[i].p2.y + f[i].p3.y) / 2;
                pb.z = (f[i].p2.z + f[i].p3.z) / 2;
                pc.x = (f[i].p3.x + f[i].p1.x) / 2;
                pc.y = (f[i].p3.y + f[i].p1.y) / 2;
                pc.z = (f[i].p3.z + f[i].p1.z) / 2;
                Normalise ( & pa);
                Normalise ( & pb);
                Normalise ( & pc);
                f[nt].p1 = f[i].p1;
                f[nt].p2 = pa;
                f[nt].p3 = pc;
                nt++;
                f[nt].p1 = pa;
                f[nt].p2 = f[i].p2;
                f[nt].p3 = pb;
                nt++;
                f[nt].p1 = pb;
                f[nt].p2 = f[i].p3;
                f[nt].p3 = pc;
                nt++;
                f[i].p1 = pa;
                f[i].p2 = pb;
                f[i].p3 = pc;
            }
        }

        return (nt);
    }

}