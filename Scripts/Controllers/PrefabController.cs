using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour {

	Dictionary<string, UnityEngine.Object> cache = new Dictionary<string, UnityEngine.Object> ();

	public T Get<T> (string path) where T : UnityEngine.Object {
		if (!cache.ContainsKey (path)) {
			cache[path] = Resources.Load<T> (path);
		}
		return (T) cache[path];
	}
	public GameObject Add (Panel panel, Transform parent) {
		return Add (
			panel.prefab_path,
			panel.position,
			panel.size,
			0,
			parent
		);
	}
	public GameObject Add (Module module, Transform parent) {
		return Add (
			module.prefab_path,
			module.center,
			module.size,
			module.rotation,
			parent
		);
	}
	public GameObject Add (string prefab_path, PointF position, SizeF scale, int rotation, Transform parent) {
		return Add (
			Get<GameObject> (prefab_path),
			ConversionHandler.ToVector3 (position),
			ConversionHandler.ToVector3 (scale),
			ConversionHandler.ToQuaternion (rotation),
			parent
		);
	}
	public GameObject Add (string prefab_path, Vector3 position, Vector3 scale, Quaternion rotation, Transform parent) {
		print ("initilizing: " + prefab_path);
		return Add (
			Get<GameObject> (prefab_path),
			position,
			scale,
			rotation,
			parent
		);
	}
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

	void Start () { }
}