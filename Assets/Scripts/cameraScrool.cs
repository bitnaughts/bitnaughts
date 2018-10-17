


using UnityEngine;
using System.Collections;

public class cameraScrool : MonoBehaviour {
	public float orthoZoomSpeed = 0.25f;
	Vector3 dragLocation;
	Vector3 newLocation;
	public GameObject player;
	public bool overUI = false;
	bool autoCorrect = false;
	// Use this for initialization
	void Start () {
		dragLocation  = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		newLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
	
	// Update is called once per frame
	void Update () {




		if (Input.GetMouseButtonDown (0)) {
			autoCorrect = false;
			dragLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			newLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
		if (Input.GetMouseButton (0)) 
		{
			newLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			if (Input.touchCount <= 1 && !overUI) this.transform.Translate(new Vector2((dragLocation.x - newLocation.x), (dragLocation.y - newLocation.y)));//position = 
			dragLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
		if (Input.GetMouseButtonUp (0)) {
			dragLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			newLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}

		if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
		{
			autoCorrect = true;

		}
		if (autoCorrect)
		{
			Vector2 targetPoint = player.transform.position;//positions[position].position;
			float xSpeed = 0f;
			float ySpeed = 0f;
			xSpeed = Mathf.Abs(this.transform.position.x - targetPoint.x)/10f;
			//if (counter%10 == 0) xSpeed += (Random.value / 100) -.005f;
			ySpeed = Mathf.Abs(this.transform.position.y - targetPoint.y)/10f;
			//if (counter%10 == 0) ySpeed += (Random.value / 100) -.005f;
			if (this.transform.position.x < targetPoint.x) this.transform.position = new Vector3(this.transform.position.x + xSpeed, this.transform.position.y,-20f);
			else if (this.transform.position.x > targetPoint.x) this.transform.position = new Vector3(this.transform.position.x - xSpeed, this.transform.position.y,-20f);
			if (this.transform.position.y < targetPoint.y) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + ySpeed,-20f);
			else if (this.transform.position.y > targetPoint.y) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - ySpeed,-20f);

			if (xSpeed + ySpeed < .0001f)
			{
				autoCorrect = false;
				this.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-20f);
			}
		}

		this.GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y;
		this.GetComponent<Camera>().orthographicSize -= -Input.GetAxis("Mouse ScrollWheel");
		if (Input.GetAxis("Mouse ScrollWheel") != 0) GameObject.Find("CodeBase").GetComponent<LineRendererHolder>().updateLines(this.GetComponent<Camera>().orthographicSize);
		if (Input.GetKeyDown (KeyCode.Joystick1Button2)) 
			this.GetComponent<Camera> ().orthographicSize += 1;


		//Pinch zoom
		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			this.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
			this.GetComponent<Camera>().orthographicSize = Mathf.Max(this.GetComponent<Camera>().orthographicSize, 0.1f);
		}
		if (this.GetComponent<Camera> ().orthographicSize < 2.5f)
			this.GetComponent<Camera> ().orthographicSize = 2.5f;
		if (this.GetComponent<Camera> ().orthographicSize > 100f)
			this.GetComponent<Camera> ().orthographicSize = 100f;

	}
}
