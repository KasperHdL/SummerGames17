using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private Camera camera;

	void Start () {
        camera = Camera.main;

	}
	
	void Update () {

//        transform.LookAt(transform.position - (camera.transform.position - transform.position));
 //       return;

        Vector3 delta = camera.transform.position - transform.position;
        delta.y = 0;

        delta = delta.normalized;

        float rot = Mathf.Atan2(-delta.z, delta.x);

        transform.rotation = Quaternion.Euler(0, rot * Mathf.Rad2Deg - 90, 0);
		
	}
}
