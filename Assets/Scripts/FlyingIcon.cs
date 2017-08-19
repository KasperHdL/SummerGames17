using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingIcon : MonoBehaviour {

    public float seconds_alive;

    public float movement;

    private float end;

    private Vector3 position;

    private Material mat;
    
	
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

    public void Init(Material icon){
        mat = new Material(icon);
        GetComponent<MeshRenderer>().material = mat;

        end = Time.time + seconds_alive;
        position = transform.position;
		
    }
}
