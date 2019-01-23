using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPlanetEditor : MonoBehaviour
{
    public Material m_Material;
    GameObject m_PlanetMesh;

    List<Polygon> m_Polygons;
    List<Vector3> m_Vertices;

    public int m_NumberOfContinents = 10;
    public float m_ContinentSizeMax = 10.0f;
    public float m_ContinentSizeMin = 5.0f;

    public int m_NumberOfHills = 10;
    public float m_HillSizeMax = 10.0f;
    public float m_HillSizeMin = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Creating icosahedron and subdivide
        InitAsIcosohedron();
        Subdivide(3);

        //Calculate poly neighbors for extruding
        CalculateNeighbors();

        //Primary color is before extrusion
        Color32 primaryColor = new Color32(0, 170, 108, 1);
        Color32 colorGrass = new Color32(100, 20, 0, 0);
        Color32 colorDirt = new Color32(180, 100, 20, 0);

        foreach (Polygon p in m_Polygons)
            p.m_Color = primaryColor;

        PolySet landPolys = new PolySet();

        for (int i = 0; i < m_NumberOfContinents; i++)
        {
            float continentSize = Random.Range(m_ContinentSizeMin, m_ContinentSizeMax);

            PolySet newLand = GetPolysInSphere(Random.onUnitSphere, continentSize, m_Polygons);

            landPolys.UnionWith(newLand);
        }

        foreach (Polygon landPoly in landPolys)
        {
            landPoly.m_Color = colorGrass;
        }

        // The Extrude function will raise the land Polygons up out of the water.
        // It also generates a strip of new Polygons to connect the newly raised land
        // back down to the water level. We can color this vertical strip of land brown like dirt.

        PolySet sides = Extrude(landPolys, 0.005f);

        foreach (Polygon side in sides)
        {
            side.m_Color = colorDirt;
        }

        // Grab additional polygons to generate hills, but only from the set of polygons that are land.

        PolySet hillPolys = new PolySet();

        for (int i = 0; i < m_NumberOfHills; i++)
        {
            float hillSize = Random.Range(m_HillSizeMin, m_HillSizeMax);

            PolySet newHill = GetPolysInSphere(Random.onUnitSphere, hillSize, landPolys);

            hillPolys.UnionWith(newHill);
        }

        sides = Extrude(hillPolys, 0.05f);

        foreach (Polygon side in sides)
        {
            side.m_Color = colorDirt;
        }



        GenerateMesh();
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InitAsIcosohedron()
    {
        m_Polygons = new List<Polygon>();
        m_Vertices = new List<Vector3>();

        // An icosahedron has 12 vertices, and
        // since they're completely symmetrical the
        // formula for calculating them is kind of
        // symmetrical too:

        float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;

        m_Vertices.Add(new Vector3(-1, t, 0).normalized);
        m_Vertices.Add(new Vector3(1, t, 0).normalized);
        m_Vertices.Add(new Vector3(-1, -t, 0).normalized);
        m_Vertices.Add(new Vector3(1, -t, 0).normalized);
        m_Vertices.Add(new Vector3(0, -1, t).normalized);
        m_Vertices.Add(new Vector3(0, 1, t).normalized);
        m_Vertices.Add(new Vector3(0, -1, -t).normalized);
        m_Vertices.Add(new Vector3(0, 1, -t).normalized);
        m_Vertices.Add(new Vector3(t, 0, -1).normalized);
        m_Vertices.Add(new Vector3(t, 0, 1).normalized);
        m_Vertices.Add(new Vector3(-t, 0, -1).normalized);
        m_Vertices.Add(new Vector3(-t, 0, 1).normalized);

        // And here's the formula for the 20 sides,
        // referencing the 12 vertices we just created.

        m_Polygons.Add(new Polygon(0, 11, 5));
        m_Polygons.Add(new Polygon(0, 5, 1));
        m_Polygons.Add(new Polygon(0, 1, 7));
        m_Polygons.Add(new Polygon(0, 7, 10));
        m_Polygons.Add(new Polygon(0, 10, 11));
        m_Polygons.Add(new Polygon(1, 5, 9));
        m_Polygons.Add(new Polygon(5, 11, 4));
        m_Polygons.Add(new Polygon(11, 10, 2));
        m_Polygons.Add(new Polygon(10, 7, 6));
        m_Polygons.Add(new Polygon(7, 1, 8));
        m_Polygons.Add(new Polygon(3, 9, 4));
        m_Polygons.Add(new Polygon(3, 4, 2));
        m_Polygons.Add(new Polygon(3, 2, 6));
        m_Polygons.Add(new Polygon(3, 6, 8));
        m_Polygons.Add(new Polygon(3, 8, 9));
        m_Polygons.Add(new Polygon(4, 9, 5));
        m_Polygons.Add(new Polygon(2, 4, 11));
        m_Polygons.Add(new Polygon(6, 2, 10));
        m_Polygons.Add(new Polygon(8, 6, 7));
        m_Polygons.Add(new Polygon(9, 8, 1));
    }
    public void Subdivide(int recursions)
    {
        var midPointCache = new Dictionary<int, int>();

        for (int i = 0; i < recursions; i++)
        {
            var newPolys = new List<Polygon>();
            foreach (var poly in m_Polygons)
            {
                int a = poly.m_Vertices[0];
                int b = poly.m_Vertices[1];
                int c = poly.m_Vertices[2];

                // Use GetMidPointIndex to either create a
                // new vertex between two old vertices, or
                // find the one that was already created.

                int ab = GetMidPointIndex(midPointCache, a, b);
                int bc = GetMidPointIndex(midPointCache, b, c);
                int ca = GetMidPointIndex(midPointCache, c, a);

                // Create the four new polygons using our original
                // three vertices, and the three new midpoints.

                newPolys.Add(new Polygon(a, ab, ca));
                newPolys.Add(new Polygon(b, bc, ab));
                newPolys.Add(new Polygon(c, ca, bc));
                newPolys.Add(new Polygon(ab, bc, ca));
            }

            // Replace all our old polygons with the new set of
            // subdivided ones.

            m_Polygons = newPolys;
        }
    }

    public int GetMidPointIndex(Dictionary<int, int> cache, int indexA, int indexB)
    {
        // We create a key out of the two original indices
        // by storing the smaller index in the upper two bytes
        // of an integer, and the larger index in the lower two
        // bytes. By sorting them according to whichever is smaller
        // we ensure that this function returns the same result
        // whether you call
        // GetMidPointIndex(cache, 5, 9)
        // or...
        // GetMidPointIndex(cache, 9, 5)

        int smallerIndex = Mathf.Min(indexA, indexB);
        int greaterIndex = Mathf.Max(indexA, indexB);
        int key = (smallerIndex << 16) + greaterIndex;

        // If a midpoint is already defined, just return it.

        int ret;
        if (cache.TryGetValue(key, out ret))
            return ret;

        // If we're here, it's because a midpoint for these two
        // vertices hasn't been created yet. Let's do that now!

        Vector3 p1 = m_Vertices[indexA];
        Vector3 p2 = m_Vertices[indexB];
        Vector3 middle = Vector3.Lerp(p1, p2, 0.5f).normalized;

        ret = m_Vertices.Count;
        m_Vertices.Add(middle);

        cache.Add(key, ret);
        return ret;
    }
    public void GenerateMesh()
    {
        // We'll store our planet's mesh in the m_PlanetMesh
        // variable so that we can delete the old copy when
        // we want to generate a new one.

        if (m_PlanetMesh)
            Destroy(m_PlanetMesh);

        m_PlanetMesh = new GameObject("Planet Mesh");

        MeshRenderer surfaceRenderer =
          m_PlanetMesh.AddComponent<MeshRenderer>();
        surfaceRenderer.material = m_Material;

        Mesh terrainMesh = new Mesh();

        int vertexCount = m_Polygons.Count * 3;

        int[] indices = new int[vertexCount];

        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];
        Color32[] colors = new Color32[vertexCount];

        for (int i = 0; i < m_Polygons.Count; i++)
        {
            var poly = m_Polygons[i];

            indices[i * 3 + 0] = i * 3 + 0;
            indices[i * 3 + 1] = i * 3 + 1;
            indices[i * 3 + 2] = i * 3 + 2;

            vertices[i * 3 + 0] = m_Vertices[poly.m_Vertices[0]];
            vertices[i * 3 + 1] = m_Vertices[poly.m_Vertices[1]];
            vertices[i * 3 + 2] = m_Vertices[poly.m_Vertices[2]];
   

            colors[i * 3 + 0] = poly.m_Color;
            colors[i * 3 + 1] = poly.m_Color;
            colors[i * 3 + 2] = poly.m_Color;

            // For now our planet is still perfectly spherical, so
            // so the normal of each vertex is just like the vertex
            // itself: pointing away from the origin.

            normals[i * 3 + 0] = m_Vertices[poly.m_Vertices[0]];
            normals[i * 3 + 1] = m_Vertices[poly.m_Vertices[1]];
            normals[i * 3 + 2] = m_Vertices[poly.m_Vertices[2]];
        }

        terrainMesh.vertices = vertices;
        terrainMesh.normals = normals;
        terrainMesh.colors32 = colors;

        terrainMesh.SetTriangles(indices, 0);

        MeshFilter terrainFilter = m_PlanetMesh.AddComponent<MeshFilter>();
        terrainFilter.mesh = terrainMesh;
    }
    public void CalculateNeighbors()
    {
        foreach (Polygon poly in m_Polygons)
        {
            foreach (Polygon other_poly in m_Polygons)
            {
                if (poly == other_poly)
                    continue;

                if (poly.IsNeighborOf(other_poly))
                    poly.m_Neighbors.Add(other_poly);
            }
        }
    }
    public List<int> CloneVertices(List<int> old_verts)
    {
        List<int> new_verts = new List<int>();

        foreach (int old_vert in old_verts)
        {
            Vector3 cloned_vert = m_Vertices[old_vert];
            new_verts.Add(m_Vertices.Count);
            m_Vertices.Add(cloned_vert);
        }
        return new_verts;
    }
    public PolySet StitchPolys(PolySet polys)
    {
        PolySet stichedPolys = new PolySet();

        var edgeSet = polys.CreateEdgeSet();

        var originalVerts = edgeSet.GetUniqueVertices();

        var newVerts = CloneVertices(originalVerts);

        edgeSet.Split(originalVerts, newVerts);

        foreach (Edge edge in edgeSet)
        {
            // Create new polys along the stitched edge. These
            // will connect the original poly to its former
            // neighbor.

            var stitch_poly1 = new Polygon(edge.m_OuterVerts[0],
                                           edge.m_OuterVerts[1],
                                           edge.m_InnerVerts[0]);

            var stitch_poly2 = new Polygon(edge.m_OuterVerts[1],
                                           edge.m_InnerVerts[1],
                                           edge.m_InnerVerts[0]);

            // Add the new stitched faces as neighbors to
            // the original Polys.

            edge.m_InnerPoly.ReplaceNeighbor(edge.m_OuterPoly,
                                             stitch_poly2);

            edge.m_OuterPoly.ReplaceNeighbor(edge.m_InnerPoly,
                                             stitch_poly1);

            m_Polygons.Add(stitch_poly1);
            m_Polygons.Add(stitch_poly2);

            stichedPolys.Add(stitch_poly1);
            stichedPolys.Add(stitch_poly2);
        }

        //Swap to the new vertices on the inner polys.
        foreach (Polygon poly in polys)
        {
            for (int i = 0; i < 3; i++)
            {
                int vert_id = poly.m_Vertices[i];
                if (!originalVerts.Contains(vert_id))
                    continue;

                int vert_index = originalVerts.IndexOf(vert_id);
                poly.m_Vertices[i] = newVerts[vert_index];
            }
        }
        return stichedPolys;
    }
    public PolySet Extrude(PolySet polys, float height)
    {
        PolySet stitchedPolys = StitchPolys(polys);
        List<int> verts = polys.GetUniqueVertices();

        // Take each vertex in this list of polys, and push it
        // away from the center of the Planet by the height
        // parameter.

        foreach (int vert in verts)
        {
            Vector3 v = m_Vertices[vert];
            v = v.normalized * (v.magnitude + height);
            m_Vertices[vert] = v;
        }
        return stitchedPolys;
    }
    public PolySet Inset(PolySet polys, float interpolation)
    {
        PolySet stitchedPolys = StitchPolys(polys);
        List<int> verts = polys.GetUniqueVertices();

        //Calculate the average center of all the vertices
        //in these Polygons.

        Vector3 center = Vector3.zero;
        foreach (int vert in verts)
            center += m_Vertices[vert];
        center /= verts.Count;

        // Pull each vertex towards the center, then correct
        // it's height so that it's as far from the center of
        // the planet as it was before.

        foreach (int vert in verts)
        {
            Vector3 v = m_Vertices[vert];
            float height = v.magnitude;
            v = Vector3.Lerp(v, center, interpolation);
            v = v.normalized * height;
            m_Vertices[vert] = v;
        }
        return stitchedPolys;
    }
    public PolySet GetPolysInSphere(Vector3 center,
                                  float radius,
                                  IEnumerable<Polygon> source)
    {
        PolySet newSet = new PolySet();
        foreach (Polygon p in source)
        {
            foreach (int vertexIndex in p.m_Vertices)
            {
                float distanceToSphere = Vector3.Distance(center,
                                         m_Vertices[vertexIndex]);

                if (distanceToSphere <= radius)
                {
                    newSet.Add(p);
                    break;
                }
            }
        }
        return newSet;
    }
}

public class Polygon
{
    public List<int> m_Vertices;
    public List<Polygon> m_Neighbors;
    public Color32 m_Color;
    public bool m_SmoothNormals;

    public Polygon(int a, int b, int c)
    {
        m_Vertices = new List<int> () { a, b, c};
        m_Neighbors = new List<Polygon>();

        m_SmoothNormals = true;

        m_Color = new Color32(255, 0, 255, 255);
    }
    public bool IsNeighborOf(Polygon other_poly)
    {
        int shared_vertices = 0;
        foreach (int vertex in m_Vertices)
        {
            if (other_poly.m_Vertices.Contains(vertex))
                shared_vertices++;
        }

        // A polygon and its neighbor will share exactly
        // two vertices. Ergo, if this poly shares two
        // vertices with the other, then they are neighbors.

        return shared_vertices == 2;
    }
    public void ReplaceNeighbor(Polygon oldNeighbor,
                              Polygon newNeighbor)
    {
        for (int i = 0; i < m_Neighbors.Count; i++)
        {
            if (oldNeighbor == m_Neighbors[i])
            {
                m_Neighbors[i] = newNeighbor;
                return;
            }
        }
    }
}
public class PolySet : HashSet<Polygon>
{
    //Given a set of Polys, calculate the set of Edges
    //that surround them.

    public EdgeSet CreateEdgeSet()
    {
        EdgeSet edgeSet = new EdgeSet();
        foreach (Polygon poly in this)
        {
            foreach (Polygon neighbor in poly.m_Neighbors)
            {
                if (this.Contains(neighbor))
                    continue;

                // If our neighbor isn't in our PolySet, then
                // the edge between us and our neighbor is one
                // of the edges of this PolySet.

                Edge edge = new Edge(poly, neighbor);
                edgeSet.Add(edge);
            }
        }
        return edgeSet;
    }

    // GetUniqueVertices calculates a list of the vertex indices 
    // used by these Polygons with no duplicates.

    public List<int> GetUniqueVertices()
    {
        List<int> verts = new List<int>();
        foreach (Polygon poly in this)
        {
            foreach (int vert in poly.m_Vertices)
            {
                if (!verts.Contains(vert))
                    verts.Add(vert);
            }
        }
        return verts;
    }
}
public class Edge
{
    // The Poly that's inside the Edge. This is the one 
    // we'll be extruding or insetting.
    public Polygon m_InnerPoly;

    // The Poly that's outside the Edge. We'll be leaving 
    // this one alone.
    public Polygon m_OuterPoly;

    //The vertices along this edge, according to the Outer poly.
    public List<int> m_OuterVerts;

    //The vertices along this edge, according to the Inner poly.
    public List<int> m_InnerVerts;

    public Edge(Polygon inner_poly, Polygon outer_poly)
    {
        m_InnerPoly = inner_poly;
        m_OuterPoly = outer_poly;
        m_OuterVerts = new List<int>(2);
        m_InnerVerts = new List<int>(2);

        //Find which vertices these polys share.
        foreach (int vertex in inner_poly.m_Vertices)
        {
            if (outer_poly.m_Vertices.Contains(vertex))
                m_InnerVerts.Add(vertex);
        }

        // For consistency, we want the 'winding order' of the 
        // edge to be the same as that of the inner polygon.
        // So the vertices in the edge are stored in the same order
        // that you would encounter them if you were walking clockwise
        // around the polygon. That means the pair of edge vertices 
        // will be:
        // [1st inner poly vertex, 2nd inner poly vertex] or
        // [2nd inner poly vertex, 3rd inner poly vertex] or
        // [3rd inner poly vertex, 1st inner poly vertex]
        //
        // The formula above will give us [1st inner poly vertex, 
        // 3rd inner poly vertex] though, so we check for that 
        // situation and reverse the vertices.

        if (m_InnerVerts[0] == inner_poly.m_Vertices[0] &&
           m_InnerVerts[1] == inner_poly.m_Vertices[2])
        {
            int temp = m_InnerVerts[0];
            m_InnerVerts[0] = m_InnerVerts[1];
            m_InnerVerts[1] = temp;
        }

        // No manipulations have happened yet, so the outer and 
        // inner Polygons still share the same vertices.
        // We can instantiate m_OuterVerts as a copy of m_InnerVerts.
        m_OuterVerts = new List<int>(m_InnerVerts);
    }
}
public class EdgeSet : HashSet<Edge>
{
    // Split - Given a list of original vertex indices and a list of
    // replacements, update m_InnerVerts to use the new replacement
    // vertices.

    public void Split(List<int> oldVertices, List<int> newVertices)
    {
        foreach (Edge edge in this)
        {
            for (int i = 0; i < 2; i++)
            {
                edge.m_InnerVerts[i] = newVertices[oldVertices.IndexOf(
                                       edge.m_OuterVerts[i])];
            }
        }
    }

    // GetUniqueVertices - Get a list of all the vertices referenced
    // in this edge loop, with no duplicates.

    public List<int> GetUniqueVertices()
    {
        List<int> vertices = new List<int>();
        foreach (Edge edge in this)
        {
            foreach (int vert in edge.m_OuterVerts)
            {
                if (!vertices.Contains(vert))
                    vertices.Add(vert);
            }
        }
        return vertices;
    }
}