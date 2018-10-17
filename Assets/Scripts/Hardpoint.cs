using UnityEngine;
using System.Collections;

public class Hardpoint : MonoBehaviour 
{
	public float mainAngle;
	public float sizeAngle;
	public float leftAngle;
	public float rightAngle;
	public int numberAngle;
	public bool angleLimit = true;
	public bool turnable = true;

	void Start () 
	{
		if (numberAngle == 0) numberAngle = (int)Mathf.Abs (leftAngle - rightAngle);
	}

	public void setValues(int[] values)
	{
		mainAngle = values [0];
		sizeAngle = 60f;
		leftAngle = values[1];
		rightAngle = values [2];
		numberAngle = values [3];
		if (leftAngle == -1)
			angleLimit = false;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			this.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
			if (leftAngle == -1 && turnable)
			{
				for (int i = 0; i < 360; i+=5)
				{
					GameObject swag = Instantiate(Resources.Load("GunMax"),new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,270))) as GameObject;
					swag.transform.parent = this.transform;
					swag.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,-1f);
					swag.transform.rotation = this.transform.rotation;
					swag.transform.Rotate(new Vector3(0,0,0));
					swag.transform.Rotate(new Vector3(0,0,0 - i));
				}
			}
			else
			{
				this.transform.GetChild(1).transform.localRotation = Quaternion.Euler(new Vector3(0,0,leftAngle-90));
				this.transform.GetChild(2).transform.localRotation = Quaternion.Euler(new Vector3(0,0,rightAngle-90));
				this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
				this.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;

				for (int i = 0; i < (leftAngle-mainAngle)+5f; i+=5)
				{
					GameObject swag = Instantiate(Resources.Load("GunMax"),new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,270))) as GameObject;
					swag.transform.parent = this.transform;
					swag.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,-1f);
					swag.transform.rotation = this.transform.rotation;
					swag.transform.Rotate(new Vector3(0,0,270-this.transform.localRotation.eulerAngles.z));
					swag.transform.Rotate(new Vector3(0,0,mainAngle + (i - 2.5f)));
				}
				float tempRightAngle = 0;
				if (mainAngle == 0) tempRightAngle = rightAngle-360;
				else tempRightAngle = rightAngle;
				for (int i = 0; i < (mainAngle-tempRightAngle)+5f; i+=5)
				{
					GameObject swag = Instantiate(Resources.Load("GunMax"),new Vector3(0,0,0),Quaternion.Euler(new Vector3(0,0,270))) as GameObject;
					swag.transform.parent = this.transform;
					swag.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,-1f);
					swag.transform.rotation = this.transform.rotation;
					swag.transform.Rotate(new Vector3(0,0,270-this.transform.localRotation.eulerAngles.z));
					swag.transform.Rotate(new Vector3(0,0,mainAngle - (i - 2.5f)));
				}
			}	
		}
		if (Input.GetKeyUp(KeyCode.F))
		{
			this.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
			this.transform.GetChild(1).transform.Rotate(new Vector3(0,0,-leftAngle));//rotation.eulerAngles.z = leftAngle;
			this.transform.GetChild(2).transform.Rotate(new Vector3(0,0,-rightAngle));
			this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
			this.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
			for (int i = 0; i < this.transform.childCount - 3; i++)
			{
				Destroy (this.transform.GetChild(i+3).gameObject);
			}
		}
	}
}
