using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GalaxyObject
{
    public List<StarObject> stars = new List<StarObject>();


    public void build(float size, float declineRate, float dispersion)
    {
        Vector2 systemLocation = new Vector2(0, 0);
        float tempSize = size;

        //Make Stars
        while (declineRate < size)
        {
            tempSize -= declineRate;
            if (tempSize < 0)
            {
                tempSize = size;
                systemLocation = new Vector2(UnityEngine.Random.value * dispersion - (dispersion / 2), UnityEngine.Random.value * dispersion - (dispersion / 2));
                declineRate *= 3 / 2f;
            }
            Vector2 potentialLocation = new Vector2(UnityEngine.Random.value * tempSize - (tempSize / 2) + systemLocation.x, UnityEngine.Random.value * tempSize - (tempSize / 2) + systemLocation.y);
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
                stars.Add(new StarObject(potentialLocation));
            }
        }
        //Make Star Connections
        int closestAmount = 4;
        float closestStarDistance = 10;
        int[] closestStarID = new int[closestAmount];
        bool chosen = false;
        float distance = 0;
        for (int i = 0; i < stars.Count; i++)
        {
            closestAmount = 4;
            closestStarDistance = 10;
            closestStarID = new int[closestAmount];
            for (int m = 0; m < closestAmount; m++) closestStarID[m] = -1;
            for (int k = 0; k < closestAmount; k++)
            {
                for (int j = 0; j < stars.Count; j++)
                {
                    chosen = false;
                    for (int n = 0; n < closestAmount; n++) if (j == closestStarID[n]) chosen = true;
                    if (j == i) chosen = true;
                    if (!chosen)
                    {
                        distance = Values.distanceBetweenObjects(stars[i].position, stars[j].position);//Mathf.Sqrt(Mathf.Pow(stars[i].position.x - stars[j].position.x, 2) + Mathf.Pow(stars[i].position.y - stars[j].position.y, 2));
                        if (distance < closestStarDistance)
                        {
                            closestStarDistance = distance;
                            closestStarID[k] = j;
                        }
                    }
                }
                if (closestStarID[k] != -1)
                {
                    stars[i].connectedStars.Add(stars[closestStarID[k]]);
                    closestStarDistance = 10;
                }
            }
        }


    }
}
