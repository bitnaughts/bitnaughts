using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GalaxyObject
{
    public List<StarObject> stars = new List<StarObject>();


    public void build(int seed, float size, float declineValue, float dispersion)
    {
        Random.seed = seed;

        Vector2 systemLocation = new Vector2(0, 0);
        float tempSize = size;
        
        //Add Stars
        while(tempSize < size)
        {
            tempSize -= declineValue;

            if(tempSize < 0)
            {
                tempSize += size;
                systemLocation = new Vector2(Random.value * dispersion - (dispersion / 2), Random.value * dispersion - (dispersion / 2));
            }

            Vector2 potentialLocation = new Vector2(Random.value * tempSize - (tempSize / 2) + systemLocation.x, Random.value * tempSize - (tempSize / 2) + systemLocation.y; 
            bool available = true;
            for (int i = 0; i < stars.Count; i++)
            {
                if (Mathf.Abs(potentialLocation.x - stars[i].position.x) + Mathf.Abs(potentialLocation.y - stars[i].position.y) < .6f) //.06 is closest two stars can be to eachother (based on sprite size*)
                {
                    available = false;
                    break;
                }
            }
            if (available)
            {
                stars.Add(new StarObject(potentialLocation, seed));
            }
        }
    }
}
