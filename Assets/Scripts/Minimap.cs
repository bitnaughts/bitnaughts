using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {


	public GameObject holder;

	public GameObject[] points;

	public float magnifier;

	public GameObject camera;

	// Use this for initialization
	void Start () 
	{
		points = new GameObject[100];
		for (int i = 0; i < 100; i++)
		{
			points [i] = Instantiate(Resources.Load("point"),this.transform.position,this.transform.rotation) as GameObject;
			points [i].transform.SetParent(this.transform);
			points [i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		magnifier = 50/camera.GetComponent<Camera>().orthographicSize;

		for (int i = 0; i < 100; i++)
		{
			points [i].SetActive(false);
		}


		Transform[] ships = new Transform[holder.transform.childCount];
		for (int i = 1; i < holder.transform.childCount; i++)
		{
			ships[i] = holder.transform.GetChild(i);
		}
		Transform center = holder.transform.GetChild(0);

		//Vector2[] positions = new Vector2[holder.transform.childCount-1];
		for (int i = 0; i < holder.transform.childCount-1; i++)
		{
			Vector2 position = new Vector2();
			position.x = (ships[i+1].position.x - center.position.x) * magnifier;
			position.y = (ships[i+1].position.y - center.position.y) * magnifier;

			//positions[i] = position;
			if (Mathf.Abs(position.x) < 104f+2 && Mathf.Abs(position.y) < 84f+2)
			{
				points[i].SetActive(true);
				points[i].GetComponent<RectTransform> ().localPosition = position;
			}

		}

	




	}
}
