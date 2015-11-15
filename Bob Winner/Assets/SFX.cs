using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour {

    public AudioClip[] Sounds;
	public bool destroyable = true;
	
	// Use this for initialization
	void Start () {
        int rand = Random.Range(0,Sounds.Length);
        GetComponent<AudioSource>().clip = Sounds[rand];
        GetComponent<AudioSource>().Play();

        if(destroyable)
            Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
