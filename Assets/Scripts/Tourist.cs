using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourist : MonoBehaviour {

    private Camera camera;


    public Material[] materials;


	// Use this for initialization
	void Start () {
        camera = Camera.main;

        GetComponent<MeshRenderer>().material = materials[Random.Range(0,materials.Length)];
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(transform.position - (camera.transform.position - transform.position));
        return;

        Vector3 delta = camera.transform.position - transform.position;
        delta.y = 0;

        delta = delta.normalized;

        float rot = Mathf.Atan2(-delta.z, delta.x);

        transform.rotation = Quaternion.Euler(0, rot * Mathf.Rad2Deg - 90, 0);
		
	}
}
