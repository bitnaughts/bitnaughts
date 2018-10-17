using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {
    public float rad = 0f;
    public int index = 0;
    Mesh mesh;
    List<int> oldTrianglesList;

    float distance(Vector3 point1)
    {
        return (point1.x * point1.x) + (point1.y * point1.y) + (point1.z * point1.z);
    }
	// Use this for initialization
	void Start ()
    {
        mesh = transform.GetComponent<MeshFilter>().mesh;
        oldTrianglesList = new List<int>(mesh.triangles);
    }
	
	// Update is called once per frame
	void Update ()
    {
       // if (Random.value < .1f)
        {

            cut(rad, index);

        }
    }
    void cut(float radius, int i)
    {
        bool within_range = false;
        for (int j = 0; j <= 2; j++)
        {
            Vector3 vertex = mesh.vertices[oldTrianglesList[i + j]];
            if (distance(vertex) < radius * radius)
            {

                within_range = true;
            }
        }
        if (within_range)
        {
            for (int j = 0; j <= 2; j++)
            {
                oldTrianglesList.RemoveAt(i);
            }
        }
        index += 3;
        if (index >= oldTrianglesList.Count) index = 0;
        this.gameObject.transform.GetComponent<MeshFilter>().mesh.triangles = oldTrianglesList.ToArray();
    }
}
