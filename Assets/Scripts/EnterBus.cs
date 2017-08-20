using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBus : MonoBehaviour {

    public bool allow_passengers = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll){
        if(!allow_passengers) return;
        if(coll.GetComponent<SwarmClient>() != null){

            //count excitement

            GameHandler.score += coll.GetComponent<SwarmClient>().excitement;

            Destroy(coll.gameObject);

        }else if(coll.GetComponent<Guide>() != null){
            //end game

            GameHandler.end_game = true;
            Destroy(coll.gameObject);

        }


    }
}
