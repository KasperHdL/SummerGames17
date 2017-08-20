using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll){
        if(coll.GetComponent<SwarmClient>() == null) return;

        //count excitement

        Destroy(coll.gameObject);



    }
}
