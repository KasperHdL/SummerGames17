using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excitement : MonoBehaviour {


    public Material good;
    public Material bad;

    public float seconds_alive;

    public float movement;

    private float end;

    private Vector3 position;

    private Material mat;
    
	void Start () {
        end = Time.time + seconds_alive;
        position = transform.position;
		
	}
	
	void Update () {
        if(end > Time.time){

            transform.LookAt(transform.position - (Camera.main.transform.position - transform.position));

            float t = (end - Time.time) / seconds_alive;
            transform.position = position + Vector3.up * movement * (1-t);

            Color c = Color.white;
            c.a = t;
            mat.SetColor("_TintColor", c);


        }else{
            Destroy(gameObject);
        }

		
	}

    public void Init(float change){
        bool isHappy = change > 0f;

        mat = new Material(isHappy ? good : bad);
        GetComponent<MeshRenderer>().material = mat;

    }
}
