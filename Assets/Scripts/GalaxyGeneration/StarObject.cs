using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarObject
{
    public Vector2 position;

    public List<PlanetObject> planets = new List<PlanetObject>();
    public List<StarObject> connectedStars = new List<StarObject>();
    public int seed;
    public int radius = 5;

    public StarObject(Vector2 position, int seed)
    {
        this.seed = seed;
        this.position = position;
        buildPlanets();
    }

    void buildPlanets()
    {
        Random.seed = seed;
        int numPlanets = (int)(Random.value * 9);

        float angle = 360 / numPlanets;

        for(int i = 0; i < numPlanets; i++)
        {
            Vector2 planetLocation = new Vector2(position.x + Mathf.Cos(angle * i) * radius, position.y + Mathf.Sin(angle * i) * radius);
            planets.Add(new PlanetObject(seed, planetLocation));
        }
    }

}
