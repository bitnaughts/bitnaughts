using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour {

	Dictionary<string, UnityEngine.Object> prefab_cache = new Dictionary<string, UnityEngine.Object> ();
	Dictionary<string, List<UnityEngine.Object>> prefab_pool = new Dictionary<string, List<UnityEngine.Object>> (); //Separate keys for "Projectile Active 1->n" and "Projectile Inactive 1->n" or check by tag per projectile 

	public T Get<T> (string path) where T : UnityEngine.Object {
		if (!prefab_cache.ContainsKey (path)) {
			prefab_cache[path] = Resources.Load<T> (path);
		}
		return (T) prefab_cache[path];
	}

#region Add Overrides
	public GameObject Add (Panel panel, Transform parent) => Add (
		panel.prefab_path,
		panel.position,
		panel.size,
		0,
		parent
	);
	public GameObject Add (Module module) => Add (
		module,
		Referencer.world_space
	);
	public GameObject Add (Module module, Transform parent) => Add (
		module.prefab_path,
		module.center,
		module.size,
		module.rotation,
		parent
	);
	public GameObject Add (string prefab_path, PointF position, SizeF scale, float rotation, Transform parent) => Add (
		Get<GameObject> (prefab_path),
		ConversionHandler.ToVector3 (position),
		ConversionHandler.ToVector3 (scale),
		ConversionHandler.ToQuaternion (rotation),
		parent
	);
	public GameObject Add (string prefab_path, Vector3 position, Vector3 scale, Quaternion rotation, Transform parent) => Add (
		Get<GameObject> (prefab_path),
		position,
		scale,
		rotation,
		parent
	);
#endregion

#region Add Implementation with Instantiate
	public GameObject Add (GameObject prefab, Vector3 position, Vector3 scale, Quaternion rotation, Transform parent) {
		GameObject obj = Instantiate (
			prefab,
			Vector3.zero,
			rotation,
			parent
		) as GameObject;
		if (obj.GetComponent<SpriteRenderer> ().size.x == 0) obj.transform.localScale = scale;
		else obj.GetComponent<SpriteRenderer> ().size = scale;
		obj.transform.localPosition = position;
		return obj;
	}
#endregion

	void Start () { }

	void Update () {
		// zoom_modifier = Mathf.Sqrt (Camera.main.orthographicSize);

		// for (int i = 0; i < projectiles.Length; i++) {
		// 	if (projectiles[i].tag == "Used") {

		// 		projectiles[i].tag = "Unused";
		// 		projectiles[i].GetComponent<ParticleSystem> ().Stop ();
		// 		projectiles[i].SetActive (false);

		// 	} else if (projectiles[i].activeSelf) {
		// 		if (StepProjectile (projectiles[i])) {
		// 			projectiles[i].tag = "Used";
		// 		}
		// 	}
		// }
	}

	// public static bool StepProjectile (GameObject projectile) {
	// GameObject ship_hit = null;
	// RaycastHit2D hitInfo = Physics2D.Raycast (projectile.transform.position, new Vector2 (Mathf.Cos (Mathf.Deg2Rad * (projectile.transform.rotation.eulerAngles.z + 90f)), Mathf.Sin (Mathf.Deg2Rad * (projectile.transform.rotation.eulerAngles.z + 90f))), projectile_info.speed); //,LayerMask.NameToLayer("enemy"));//this.transform.position, Vector3.forward, out hitInfo, .1f, LayerMask.NameToLayer("enemy")))//, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers);
	// if (hitInfo.collider != null) {
	//     ship_hit = hitInfo.collider.gameObject;
	//     if (TeamManager.getStanding (projectile_info.team, ship_hit.GetComponent<Ship> ().team) == -1) {
	//         ship_hit.GetComponent<life> ().hurt (projectile_info.damage);
	//         if (projectile_info.damage > Random.value * 50) ProjectileManager.getExplosion (ship_hit.transform.position, new Vector2 (.01f, .01f), projectile_info.damage);
	//         //    print(projectile_info.damage);
	//         //if (weapon.parts[Values.AMMO][0] == 'L') projectile.transform.Translate(new Vector2(0f, Values.distanceBetweenObjects(projectile.transform.position, ship_hit.transform.position)));
	//         return true;
	//     }
	// }
	// projectile.transform.Translate (new Vector2 (0f, projectile_info.speed));
	// return false;
	// }

	// public static void ScaleProjectilesToZooming (float zoom_level) {
	// zoom_modifier = zoom_level;
	// for (int i = 0; i < projectiles.Length; i++) {
	//     if (projectiles[i].activeSelf) {
	//         //projectiles[i].transform.localScale = new Vector2(2, 3f) * projectiles_info[i].size * Mathf.Sqrt(zoom_modifier);
	//     }
	// }
	// }

	// public static void getProjectile (GameObject target, Vector3 position, Quaternion rotation, int team) //string firingType, string weaponType, GameObject target, Vector3 position, Quaternion rotation, int team)
	// {
	// GameObject projectile = projectiles[projectileCount]; //If you want an easy way to keep track of total shots in a battle, use MOD instead of resetting to 0, then simply show projectile count as total shots without MOD

	// //TRANSFORM VALUES
	// projectile.transform.position = position;
	// projectile.transform.rotation = rotation;
	// projectile.transform.localScale = new Vector2 (2, 3f) * Values.barrelSize (weapon.parts[Values.BARREL]) * Mathf.Sqrt (zoom_modifier); // * weapon.coefficients[Values.BARREL];//projectileInfo.Projectile_Visuals.Transform_Scale * equipmentInfo.Projectile_SizeModifier;

	// //PARTICLE SYSTEM VALUES
	// projectile.GetComponent<ParticleSystem> ().Play ();
	// //projectile.GetComponent<ParticleSystem>().startLifetime = projectileInfo.Projectile_Visuals.ParticleSystem_Lifetime;
	// //projectile.GetComponent<ParticleSystem>().startSize = projectileInfo.Projectile_Visuals.ParticleSystem_Size * equipmentInfo.Projectile_SizeModifier * .5f;
	// //projectile.GetComponent<ParticleSystem>().startColor = projectileInfo.Projectile_Visuals.Sprite_Color;

	// //TRAIL RENDERER VALUES
	// // if (weapon.parts[Values.AMMO][0] == 'L') {
	// //     projectile.GetComponent<TrailRenderer> ().enabled = true;
	// //     projectile.GetComponent<TrailRenderer> ().material.SetColor ("_Color", new Color (1, 0, 0)); // projectileInfo.Projectile_Visuals.Sprite_Color);
	// // } else projectile.GetComponent<TrailRenderer> ().enabled = false;
	// projectile.SetActive (true);

	// projectiles_info[projectileCount].team = team;
	// projectiles_info[projectileCount].speed = weapon.coefficients[Values.VELOCITY]; //projectileInfo.Projectile_Speed;
	// //velocity*mass*energycoefficient
	// projectiles_info[projectileCount].damage = weapon.coefficients[Values.VELOCITY] * weapon.coefficients[Values.MASS]; //;//projectileInfo.Projectile_Damage * equipmentInfo.Projectile_DamageModifier;
	// projectiles_info[projectileCount].size = Values.barrelSize (weapon.parts[Values.BARREL]);
	// if (++projectileCount == projectileAmount) projectileCount = 0;
	// }

	// public static void getPlayer (string input, string weaponInput, Vector2 position) {
	// GameObject item = audioPlayer[audioPlayerCount++ % 50];
	// item.GetComponent<AudioSource>().clip = (Resources.Load("sounds/" + inputInitials) as GameObject).GetComponent<AudioClipHolder>().GetItem();
	// item.GetComponent<AudioSource>().volume = (projectileInfo.Projectile_Visuals.Transform_Scale.y * equipmentInfo.Projectile_SizeModifier) / 25f - .125f;  //1.25f + (Random.value/2f);
	// item.GetComponent<AudioSource>().pitch = 10f / (projectileInfo.Projectile_Visuals.Transform_Scale.y * equipmentInfo.Projectile_SizeModifier);  //1.25f + (Random.value/2f);
	// item.SetActive(true);
	// item.GetComponent<AudioSource>().Play();
	// }
	// public static void getExplosion (Vector2 position, Vector3 shipSize, float damage) {
	// 	GameObject item = explosions[explosionsCount++];
	// 	if (explosionsCount == 50)
	// 		explosionsCount = 0;
	// 	float x = (Random.value >.5f) ? (shipSize.x) * Random.value : (shipSize.x) * -Random.value;
	// 	float y = (Random.value >.5f) ? (shipSize.y) * Random.value : (shipSize.y) * -Random.value;
	// 	item.transform.position = new Vector2 (position.x + x / 4, position.y + y / 4);
	// 	// item.GetComponent<Animator> ().runtimeAnimatorController = (Resources.Load ("animations/explosion" + (int) Random.Range (1, 5)) as GameObject).GetComponent<ControllerHolder> ().controller;
	// 	item.transform.localScale = new Vector2 (damage * shipSize.x * 4f, damage * shipSize.x * 4f);
	// 	item.SetActive (true);
	// }
}