using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GalaxyManager
{
    public float size = 10f;
    public float declineRate = .5f;
    public float width = 100f;
    

    GalaxyObject galaxy = new GalaxyObject();
    bool GalaxyUI_visualizing = false;
    int GalaxyUI_visualizatingStarCount = 0;
    bool GalaxyUI_visualizatingText = true;

    GalaxyManager()
    {
        int seed = 12345;

        galaxy.build(seed, size, declineRate, width);
        GalaxyUI_visualizing = true;
    }
}
