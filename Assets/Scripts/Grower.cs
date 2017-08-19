using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grower : MonoBehaviour {

    public float end_scale;
    public float duration;

    private float start_scale;
    private float end;

	// Use this for initialization
	void Start () {
        end = Time.time + duration;
        start_scale = transform.localScale.x;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(end > Time.time){
            float t = (end - Time.time) / duration;
            transform.localScale = Vector3.one * (start_scale + (end_scale - start_scale) * (1-t));

        }else{
            Destroy(gameObject);
        }

		
	}
}
