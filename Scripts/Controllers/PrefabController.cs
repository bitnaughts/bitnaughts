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

	public GameObject Instantiate (string prefab_path, PointF position, int orientation, SizeF scale, Transform parent) {
		return Instantiate (
			Get<GameObject> (prefab_path),
			ConversionHandler.ToVector3 (position),
			ConversionHandler.ToQuaternionY (orientation),
			ConversionHandler.ToVector3 (scale),
			parent
		);
	}

	public GameObject Instantiate (string prefab_path, Vector3 position, Quaternion orientation, Vector3 scale, Transform parent) {
		return Instantiate (Get<GameObject> (prefab_path), position, orientation, scale, parent);
	}

	public GameObject Instantiate (GameObject prefab, Vector3 position, Quaternion orientation, Vector3 scale, Transform parent) {
		GameObject obj = Instantiate (prefab, Vector3.zero, orientation, parent) as GameObject;
		obj.transform.GetChild(0).localScale = scale;
		obj.transform.localPosition = position;
		return obj;
	}

	void Start () { }
}