using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour {
	/*How many max of each type there are */
	private static int projectileAmount = 250;
	/* GameObjects of each type */
	public static GameObject[] projectiles = new GameObject[projectileAmount];
    public static ProjectileData[] projectiles_info = new ProjectileData[projectileAmount];
	/* Which GameObject is to be used next */
	public static int projectileCount;

	public static GameObject[] audioPlayer = new GameObject[50];
	public static int audioPlayerCount;

    public static GameObject[] explosions = new GameObject[50];
    public static int explosionsCount;

    public static float zoom_modifier = 32;


	// Use this for initialization
	void Start () 
	{
        zoom_modifier = Mathf.Sqrt(Camera.main.orthographicSize);

        for (int i = 0; i < projectileAmount; i++) 
		{
			projectiles [i] = Instantiate (Resources.Load ("projectiles/template"), this.transform.position, this.transform.rotation) as GameObject;
			projectiles [i].transform.SetParent(this.transform);
			projectiles [i].SetActive(false);

            projectiles_info[i] = new ProjectileData(-1, -1, -1, -1);
		}
        for (int i = 0; i < audioPlayer.Length; i++)
        {
            audioPlayer[i] = Instantiate(Resources.Load("AudioPlayer"), this.transform.position, this.transform.rotation) as GameObject;
            audioPlayer[i].transform.SetParent(this.transform);
            audioPlayer[i].SetActive(false);
        }
        for (int i = 0; i < explosions.Length; i++)
        {
            explosions[i] = Instantiate(Resources.Load("Explosion"), this.transform.position, this.transform.rotation) as GameObject;
            explosions[i].transform.SetParent(this.transform);
            explosions[i].SetActive(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (projectiles[i].tag == "Used")
            {

                projectiles[i].tag = "Unused";
                projectiles[i].GetComponent<ParticleSystem>().Stop();
                projectiles[i].SetActive(false);

            }
            else if (projectiles[i].activeSelf)
            {
                if (StepProjectile(projectiles[i], projectiles_info[i]))
                {
                    projectiles[i].tag = "Used";
                }
            }
        }





    }

    public static bool StepProjectile(GameObject projectile, ProjectileData projectile_info)
    {
        GameObject ship_hit = null;
        RaycastHit2D hitInfo = Physics2D.Raycast(projectile.transform.position, new Vector2(Mathf.Cos(Mathf.Deg2Rad * (projectile.transform.rotation.eulerAngles.z + 90f)), Mathf.Sin(Mathf.Deg2Rad * (projectile.transform.rotation.eulerAngles.z + 90f))), projectile_info.speed);             //,LayerMask.NameToLayer("enemy"));//this.transform.position, Vector3.forward, out hitInfo, .1f, LayerMask.NameToLayer("enemy")))//, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
        if (hitInfo.collider != null)
        {
            ship_hit = hitInfo.collider.gameObject;
            if (TeamManager.getStanding(projectile_info.team, ship_hit.GetComponent<Ship>().team) == -1)
            {
                ship_hit.GetComponent<life>().hurt(projectile_info.damage);
                if (projectile_info.damage > Random.value*50) ProjectileManager.getExplosion(ship_hit.transform.position, new Vector2(.01f, .01f), projectile_info.damage);
            //    print(projectile_info.damage);
                //if (weapon.parts[Values.AMMO][0] == 'L') projectile.transform.Translate(new Vector2(0f, Values.distanceBetweenObjects(projectile.transform.position, ship_hit.transform.position)));
                return true;
            }
        }
        projectile.transform.Translate(new Vector2(0f, projectile_info.speed));
        return false;
    }

    public static void ScaleProjectilesToZooming(float zoom_level)
    {
        zoom_modifier = zoom_level;
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (projectiles[i].activeSelf)
            {
                //projectiles[i].transform.localScale = new Vector2(2, 3f) * projectiles_info[i].size * Mathf.Sqrt(zoom_modifier);
            }
        }
    }

    public static void getProjectile(ItemObject weapon, GameObject target, Vector3 position, Quaternion rotation, int team)//string firingType, string weaponType, GameObject target, Vector3 position, Quaternion rotation, int team)
    {
        GameObject projectile = projectiles[projectileCount];         //If you want an easy way to keep track of total shots in a battle, use MOD instead of resetting to 0, then simply show projectile count as total shots without MOD


        // VisualDataWrapper projectileInfo = Values.getVisualElements(firingType);
        //ProjectileDataWrapper projectileInfo = Values.getProjectileElements(firingType);
        //EquipmentDataWrapper equipmentInfo = Values.getEquipmentElements(weaponType);

        //TRANSFORM VALUES
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;
        projectile.transform.localScale = new Vector2(2, 3f) * Values.barrelSize(weapon.parts[Values.BARREL]) * Mathf.Sqrt(zoom_modifier);// * weapon.coefficients[Values.BARREL];//projectileInfo.Projectile_Visuals.Transform_Scale * equipmentInfo.Projectile_SizeModifier;
        
        //SPRITE VALUES
        //projectile.GetComponent<SpriteRenderer>().color = projectileInfo.Projectile_Visuals.Sprite_Color;

        //PROJECTILE FUNCTION VALUES
        // projectile.GetComponent<projectileFuntion>().type = (weaponType.Contains("O")) ? "missile" : "laser";
        // print(projectile.GetComponent<projectileFuntion>().type);
        //if (weaponType.Contains("O")) projectile.GetComponent<projectileFuntion>().type = "rocket";
        //if (weaponType.Contains("M")) projectile.GetComponent<projectileFuntion>().type = "missile";
        //if (firingType.Equals("L1")) projectile.GetComponent<projectileFuntion>().type = "cathod"; 
        //projectile.GetComponent<projectileFuntion>().speed = projectileInfo.Projectile_Speed * equipmentInfo.Projectile_SpeedModifier;
        //projectile.GetComponent<projectileFuntion>().detectionRange = projectileInfo.Projectile_Speed;
        //projectile.GetComponent<projectileFuntion>().damage = projectileInfo.Projectile_Damage * equipmentInfo.Projectile_DamageModifier;
        //projectile.GetComponent<projectileFuntion>().target = target;


        //PARTICLE SYSTEM VALUES
        projectile.GetComponent<ParticleSystem>().Play();
        //projectile.GetComponent<ParticleSystem>().startLifetime = projectileInfo.Projectile_Visuals.ParticleSystem_Lifetime;
        //projectile.GetComponent<ParticleSystem>().startSize = projectileInfo.Projectile_Visuals.ParticleSystem_Size * equipmentInfo.Projectile_SizeModifier * .5f;
        //projectile.GetComponent<ParticleSystem>().startColor = projectileInfo.Projectile_Visuals.Sprite_Color;

        //TRAIL RENDERER VALUES
        if (weapon.parts[Values.AMMO][0] == 'L')
        {
            projectile.GetComponent<TrailRenderer>().enabled = true;
            projectile.GetComponent<TrailRenderer>().material.SetColor("_Color", new Color(1, 0, 0));// projectileInfo.Projectile_Visuals.Sprite_Color);

        }
        else projectile.GetComponent<TrailRenderer>().enabled = false;
        // projectile.GetComponent<projectileFuntion>().life = 0f;
        projectile.SetActive(true);
        //projectile.GetComponent<projectileFuntion>().life = 0;

        //projectile.GetComponent<projectileFuntion>().team = team;

        projectiles_info[projectileCount].team = team;
        projectiles_info[projectileCount].speed = weapon.coefficients[Values.VELOCITY];//projectileInfo.Projectile_Speed;
        //velocity*mass*energycoefficient
        projectiles_info[projectileCount].damage = weapon.coefficients[Values.VELOCITY] * weapon.coefficients[Values.MASS];//;//projectileInfo.Projectile_Damage * equipmentInfo.Projectile_DamageModifier;
        projectiles_info[projectileCount].size = Values.barrelSize(weapon.parts[Values.BARREL]);
        if (++projectileCount == projectileAmount) projectileCount = 0;
    }

    public static void getPlayer(string input, string weaponInput, Vector2 position)
    {
        GameObject item = audioPlayer[audioPlayerCount++];
        if (audioPlayerCount == 50)
            audioPlayerCount = 0;
        //Ammo type + weapon type

        ProjectileDataWrapper projectileInfo = Values.getProjectileElements(input);
        EquipmentDataWrapper equipmentInfo = Values.getEquipmentElements(weaponInput);
        string inputInitials = input.Substring(0, 1) + weaponInput.Substring(0, 1);// + input.Substring(2, 1);
                                                                                   //  print(inputInitials + "_" + input);
        //print(inputInitials);
        if ((Resources.Load("sounds/" + inputInitials) as GameObject) == null)
        {

        }
        else
        {
            item.GetComponent<AudioSource>().clip = (Resources.Load("sounds/" + inputInitials) as GameObject).GetComponent<AudioClipHolder>().GetItem();

            item.GetComponent<AudioSource>().volume = (projectileInfo.Projectile_Visuals.Transform_Scale.y * equipmentInfo.Projectile_SizeModifier) / 25f - .125f;  //1.25f + (Random.value/2f);
                                                                                                                                                            //if (input.Contains("Cannon")) item.GetComponent<AudioSource> ().volume = Random.value/4 + .25f;
            item.GetComponent<AudioSource>().pitch = 10f / (projectileInfo.Projectile_Visuals.Transform_Scale.y * equipmentInfo.Projectile_SizeModifier);  //1.25f + (Random.value/2f);
                                                                                                                                                           //if (input != "LaserCannon" && input.Contains ("Cannon") || input.Contains("Rocket"))
                                                                                                                                                           //	item.GetComponent<AudioSource> ().pitch = 1f;
                                                                                                                                                           //if (input.Contains("Cannon")) item.GetComponent<AudioSource>().volume = .10f;

            item.SetActive(true);

            item.GetComponent<AudioSource>().Play();
        }
    }
    public static void getExplosion(Vector2 position, Vector3 shipSize, float damage)
    {
        GameObject item = explosions[explosionsCount++];
        if (explosionsCount == 50)
            explosionsCount = 0;
        float x = (Random.value > .5f) ? (shipSize.x) * Random.value : (shipSize.x) * -Random.value;
        float y = (Random.value > .5f) ? (shipSize.y) * Random.value : (shipSize.y) * -Random.value;
        item.transform.position = new Vector2(position.x + x / 4, position.y + y / 4);
        item.GetComponent<Animator>().runtimeAnimatorController = (Resources.Load("animations/explosion" + (int)Random.Range(1, 5)) as GameObject).GetComponent<ControllerHolder>().controller;

        item.transform.localScale = new Vector2(damage*shipSize.x*4f,damage*shipSize.x*4f);

        item.SetActive(true);
    }

}
