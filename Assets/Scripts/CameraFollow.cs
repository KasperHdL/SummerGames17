using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    
    private Rigidbody body;

	void Start () {
        body = target.GetComponent<Rigidbody>();

		
	}
	
	void Update () {

        transform.position = target.position - offset;

        //rotation
        transform.LookAt(target);

		
	}
}
