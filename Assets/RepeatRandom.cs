using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRandom : MonoBehaviour {

    public AudioClip[] clips;
    public float delay;
    private AudioSource source;
    private float next;
    

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
		
        PlayRandom();
	}
	
	// Update is called once per frame
	void Update () {
        if(next < Time.time){
            PlayRandom();
        }
	}

    void PlayRandom(){
        source.clip = clips[Random.Range(0,clips.Length)];
        next = Time.time + source.clip.length + delay;

        source.Play();

    }
}
