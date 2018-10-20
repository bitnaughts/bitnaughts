using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarObject
{
    public Vector2 position;
    public List<PlanetObject> planets = new List<PlanetObject>();

    public List<StarObject> connectedStars = new List<StarObject>();

    public StarObject(Vector2 position)
    {
        this.position = position;
        buildPlanets();
    }

    void buildPlanets()
    {
        int numberPlanets = (int)(UnityEngine.Random.value * 9);

        for (int i = 0; i < numberPlanets; i++)
        {
            planets.Add(new PlanetObject());
        }

    }

    public override string ToString()
    {
        string output = "new Star(" + position + ":" + planets.Count + ");\n";
        for (int i = 0; i < planets.Count; i++) output += "\tnew Planet(" + planets[i].ToString() + "); ";
        output += "\nStar.ConnectTo(";
        for (int i = 0; i < connectedStars.Count; i++) output += connectedStars[i].position + ",";
        output += ");\n";
        return output;
    }

}
