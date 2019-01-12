using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetObject
{
    //Incorporate ideas like Different things that can be done on a planet
    //Elements, Resources, Friendly, Market...Etc
    public Vector2 position;

    public PlanetObject(int seed, Vector2 position)
    {
        Random.seed = seed;
        this.position = position;

        //buildPlanet((int)(Random.value * 100000), position);
    }
}
