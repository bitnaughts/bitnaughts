using UnityEngine;
using System.Collections;

public class AudioClipHolder : MonoBehaviour {
    public AudioClip[] item;// = new AudioClip[6];
	public AudioClip GetItem()
	{

        return item[((int)(Random.value * item.Length))];	                  
	}

}
