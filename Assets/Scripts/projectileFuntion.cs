using UnityEngine;
using System.Collections;

public class projectileFuntion : MonoBehaviour {
	public string type;
	
	public float speed = .1f;
	public float turnSpeed = 20f;
	public float damage = 1;
	public float lifeTime = 1;
	public float life = 0f;
	private int bugTest = 0;

	public float detectionRange = .1f;

	public bool fireOnDeath = false;
	public bool fireOverTime = false;
	bool firing = false;
	public string deathFireType;

	public float fireAmount = 10;


	public bool friendly = true;


	public bool on = false;

	public int team = 0;

    float swayCounter;
    bool swayDir;

	public GameObject target;

    bool dead = false;
	
	void Start ()
    {

		if (type=="missile")
		{
            target = testAiming.findTarget(this.transform.position, team);
		}

	}

    void death()
    {
        this.GetComponent<ParticleSystem>().Stop();
        this.gameObject.SetActive(false);

    }

	void Update () {
        if (dead)
        {

            dead = false;
        }
		life+=.01f;
		if (life > lifeTime)
		{
            death();
		}

        if (!firing)
        {
            GameObject ship_hit = null;
            RaycastHit2D hitInfo = Physics2D.Raycast(this.transform.position, new Vector2(Mathf.Cos(Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z + 90f)), Mathf.Sin(Mathf.Deg2Rad * (this.transform.rotation.eulerAngles.z + 90f))), detectionRange);             //,LayerMask.NameToLayer("enemy"));//this.transform.position, Vector3.forward, out hitInfo, .1f, LayerMask.NameToLayer("enemy")))//, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
            if (hitInfo.collider != null)
            {
                ship_hit = hitInfo.collider.gameObject;
           /* }
             MANUAL DETECTION/COLLISION
            for (int i = 0; i < 10; i++)
            {
                if (TeamManager.getStanding(i, team) == -1)
                {
                    for (int j = 0; j < TeamManager.team_lists[i].Count; j++)
                    {
                        float radius = TeamManager.team_lists[i][j].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                        if (speed > radius) radius = speed;
                        if (Values.isWithinBounds(this.transform.position, TeamManager.team_lists[i][j].transform.position, radius))
                        {
                            ship_hit = TeamManager.team_lists[i][j];
                        }
                    }
                }
            }
            if (ship_hit != null)
            {*/
                if (TeamManager.getStanding(team, ship_hit.GetComponent<Ship>().team) == -1)
                {
                    ship_hit.GetComponent<life>().hurt(damage);
                    if (fireOnDeath)
                    {
                        if (!fireOverTime)
                        {
                            for (int i = 0; i < fireAmount; i++) Instantiate(Resources.Load("projectiles/" + deathFireType), this.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.value * 360f)));
                            death();
                        }
                        else
                        {
                            firing = true;
                            life = lifeTime - (fireAmount / 100f);
                        }
                    }
                    else
                    {
                        if (damage / 10 < Random.value) ProjectileManager.getExplosion(this.transform.position, new Vector2(.01f, .01f), damage * 4);
                        death();
                    }
                }
            }
        }
        if (firing)
        {
            Instantiate(Resources.Load(deathFireType), this.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.value * 360f)));
            bugTest++;
            if (bugTest > fireAmount)
            {
                print("closed manually");
                death(); this.gameObject.SetActive(false);
            }
            print(bugTest);

        }
        this.transform.Translate(new Vector2(0f, speed));		
        if (type == "rocket")
        {
            if (life < .2f)
            {
                swayCounter = Random.Range(-1f, 1f);
                swayDir = (Random.Range(0, 1f) < .5f) ? true : false;
            }
            else
            {
                this.transform.Translate(new Vector2(0f, (speed * life) / 20f) );

                this.transform.Rotate(new Vector3(0, 0, swayCounter));
                if (swayDir) swayCounter += Random.Range(0, life / 2);
                else swayCounter += -Random.Range(0, life / 2);
                if (swayCounter > life/20) swayDir = false;
                else if (swayCounter < -life/20) swayDir = true;
            }

        }	
		if (type=="missile")
		{
            if (life > .2f)
            {
                this.transform.Translate(new Vector2(0f, (speed * life) / 20f));
                // if (this.transform.position.z != -5f) this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -5f);
                //Vector2 clickPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
               // Vector3 targetPoint;
                //.transform.position;
                if (target == null)
				{
					target = testAiming.findTarget(this.transform.position, team);
				}
               // {
//
               //     targetPoint = new Vector2(0, 0);
               //     target = testAiming.findTarget(this.transform.position, team).transform.position;
               // }
               // else targetPoint = target.transform.position;
      

                transform.up = target.transform.position - this.transform.position;
            }
		}
        if (type == "cathod")
        {
           // transform.up = target - this.transform.position;



        }
	}

}
