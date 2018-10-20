 using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    public float radius = 2f;
	public AudioClip test;
	int count = 0;

	public int position = 0;

	public bool demo;

	public Vector2 size;

	
	public float speed = .05f;
    public float currentSpeed;
	public float turnSpeed = 1f;

	public static float counter;
	
	//private GameObject fleet;
///
	public bool friendly = true;

	bool firstRun = true;
	




	public int team;














	void Start () 
	{
        if (firstRun && team == 0) 
	 	{
	 		GameObject.Find ("Main Camera").GetComponent<cameraControl>().follow_target = this.gameObject.transform;
	 		firstRun = false;
	 	}
	}

	//public void playSound (string input)
	//{

			//projectilePool.getPlayer(input,this.transform.position);
	//}
   

	void Update ()
	 {

        if (team == 0)
        {
            currentSpeed = speed * Input.GetAxis("Vertical");
            this.transform.Translate(new Vector2(0, speed * (Input.GetAxis("Vertical"))));
            this.transform.Rotate(new Vector3(0, 0, .5f * -Input.GetAxis("Horizontal")));

            //  Debug.DrawRay(this.transform.position, new Vector3(0, 0, -10), new Color(255, 0, 0));
            if (false)// || Input.GetKeyDown(KeyCode.Space))
            {
                int x = 0;
                int y = 0;
                x = (int)((Random.value * radius*2) - radius);
                y = (int)((Random.value * radius*2) - radius);
                if (Mathf.Sqrt((x * x) + (y * y)) < radius) 
                //for (int x = -1; x <= 1; x++)
                {
                    //for (int y = -1; y <= 1; y++)
                    {
                        RaycastHit hit;
                        Ray ray = new Ray(new Vector3(this.transform.position.x + (1f * x), this.transform.position.y + (1f * y), this.transform.position.z -10f), new Vector3(0, 0, 1));//Camera.main.ScreenPointToRay(new Vector2(this.transform.position.x + (.5f * x), this.transform.position.y + (.5f * y)));
                        Debug.DrawRay(new Vector3(this.transform.position.x + (1f * x), this.transform.position.y + (1f * y), this.transform.position.z - 10f), new Vector3(0, 0, 10), new Color(255, 0, 0));
                        if (Physics.Raycast(ray, out hit, 10f))
                        {
                            int index = hit.triangleIndex;
                            // print(hit.collider.gameObject.name);
                            if (hit.triangleIndex != -1)
                            {
                             
                                //print("hit");
                                Destroy(hit.collider.gameObject.GetComponent<MeshCollider>());
                                Mesh mesh = hit.collider.gameObject.transform.GetComponent<MeshFilter>().mesh;
                                int[] old = mesh.triangles;
                                int[] newTris = new int[mesh.triangles.Length - 3];
                                int i = 0;
                                int j = 0;
                                while (j < mesh.triangles.Length)
                                {
                                    if (j != index * 3)
                                    { 

                                        newTris[i++] = old[j++];
                                        newTris[i++] = old[j++];
                                        newTris[i++] = old[j++];
                                    }
                                    else j += 3;
                                }
                                hit.collider.gameObject.transform.GetComponent<MeshFilter>().mesh.triangles = newTris;
                                hit.collider.gameObject.AddComponent<MeshCollider>();
                            }
                            //else print("distinct lack of triangles");
                        }
                        else
                        {
                            //redo
                        }
                        
                    }
                }
            }
            
        }
        else
        {
            currentSpeed = speed;
            //TARGETTING SYSTEM, if closer to point, go slower,
            //Find turning radius with speeds, match speed that hits target point*******
            Vector3 targetPoint = new Vector2(Random.value * 100f, Random.value * 100f);
            Vector3 turretPoint = this.transform.position;
            //Insert AI here
            float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - targetPoint.x, 2) + Mathf.Pow(transform.position.y - targetPoint.y, 2));
            this.transform.Translate(new Vector2(0, Mathf.Clamp(distance / 3, 0f, speed)));//Time.deltaTime * 100 * speed)));


            //Finding angle through tan^-1
            float angle = Mathf.Rad2Deg * Mathf.Atan((targetPoint.y - turretPoint.y) / (targetPoint.x - turretPoint.x));
            //Configuring produced angle to 0-360
            if (targetPoint.y < turretPoint.y)
            {
                if (angle < 0) angle += 180f;
                angle += 180f;
            }
            else if (angle < 0) angle += 180f;
            //finding current 0-360 angle
            float current = this.transform.rotation.eulerAngles.z + 90f;
            float reverseDirectionMultiplier = 1f;
            if (Mathf.Abs(current - angle) > 180f && Mathf.Abs(current - angle) < 360f) reverseDirectionMultiplier = -1f;
            //Random turn speed
            if (current > angle) this.transform.Rotate(0, 0, -Time.deltaTime * 100 * turnSpeed * reverseDirectionMultiplier);
            else transform.Rotate(0, 0, Time.deltaTime * 100 * turnSpeed * reverseDirectionMultiplier);


        }
	}
}
