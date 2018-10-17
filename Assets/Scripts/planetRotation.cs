using UnityEngine;
using System.Collections;

public class planetRotation : MonoBehaviour {

	public float rotationSpeed = .1f;
	public GameObject formSolar;

	public float rotation = 1000f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate ( new Vector3( 0, 0, rotationSpeed*Time.deltaTime));

		//for (int i = 0; i < 7; i++)
		//	formSolar.GetComponent<solarReport> ().planets [i].GetComponent<RectTransform> ().Rotate (0, 0, rotationSpeed / rotation);
	}
	public void randomizeRotation ()
	{
		this.transform.Rotate (new Vector3 (0, 0, Random.value * 360f));
	
		////{
	
		//TODO ADD FEATURE TO CORRECTLY ROTATE PLANETS AROUND STAR TO MAKE PLANETS SEEM CORRECTLY LIT


	}
}
