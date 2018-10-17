using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class planetAimToStar : MonoBehaviour {

	public GameObject star;
	public float modifyAmount;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        transform.up = star.transform.position;
        /*if (this.GetComponent<Image>().enabled)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan((this.transform.position.y - star.transform.position.y) / (this.transform.position.x - star.transform.position.x));
            //Configuring produced angle to 0-360
            if (this.transform.position.y < star.transform.position.y)
            {
                if (angle < 0) angle += 180f;
                angle += 180f;
            }
            else if (angle < 0) angle += 180f;
            print(angle);

            this.GetComponent<RectTransform>().transform.localRotation.eulerAngles.Set(0, 0, 10); //.rotation.eulerAngles.Set (0,0,angle);//this.GetComponent<RectTransform>().rotation.eulerAngles.z);
        }*/
    }
}
