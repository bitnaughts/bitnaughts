using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineRendererHolder : MonoBehaviour {
    //LineRenderer[] lines;

    public List<Vector4> links = new List<Vector4>();

    public Material lineColorer;

    
	
	int lineCount = 0;
	// Use this for initialization
	void Start () {
		
		
		
		
	}
	

	public void updateLines(float orthoSize)
	{
		for (int i = 0; i < transform.childCount; i++)
			this.transform.GetChild(i).gameObject.GetComponent<LineRenderer>().SetWidth(Mathf.Clamp(.0125f * orthoSize, .05f, 10),Mathf.Clamp(.0125f * orthoSize, .05f, 10));
	}
	public void setLine(Vector2 start, Vector2 end)
	{
        bool line_created = false;
        for (int i = 0; i < links.Count; i++)
        {
            if (links[i] == new Vector4(end.x, end.y, start.x, start.y))
            {
                line_created = true;
            }
        }
        if (line_created == false)
        {
            links.Add(new Vector4(start.x, start.y, end.x, end.y));


            Material mat = lineColorer;
            //mat.color = 
            GameObject temp = new GameObject();
            temp.transform.SetParent(this.gameObject.transform);
            LineRenderer lines = temp.AddComponent<LineRenderer>();
            lines.material = mat;
            lines.material.color = (Random.value < .5f) ? Color.red : Color.green;
            lines.SetWidth(.25f, .25f);
            lines.SetPosition(0, start);
            lines.SetPosition(1, end);
        }
	}
}
