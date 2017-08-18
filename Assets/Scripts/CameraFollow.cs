using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float min_distance;
    public float max_distance;

    public float max_move;
    public float move_force;


    private Rigidbody body;
	void Start () {
        body = GetComponent<Rigidbody>();

		
	}
	
	void Update () {

        //rotation
        transform.LookAt(target);

        //move

        Vector3 delta = target.position - transform.position;
        delta.y = 0;

        Vector3 move = Vector3.zero;
        if(delta.magnitude < min_distance){
            move = -delta.normalized * (min_distance - delta.magnitude) * move_force;

        }

        if(delta.magnitude > max_distance){
            move = delta.normalized * (delta.magnitude - max_distance) * move_force;
        }

        if(move.magnitude > max_move){
            move = move.normalized * max_move;
        }


        body.AddForce(move * Time.deltaTime);


		
	}
}
