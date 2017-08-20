using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBus : MonoBehaviour {

    public AudioClip enterClip;
    public GameHandler gameHandler;
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

            gameHandler.score += coll.GetComponent<SwarmClient>().excitement;
            gameHandler.num_tourist++;

            AudioSource.PlayClipAtPoint(enterClip, coll.transform.position);

            Destroy(coll.gameObject);

        }else if(coll.GetComponent<Guide>() != null){
            //end game

            gameHandler.force_end = true;
//            Destroy(coll.gameObject);

        }


    }
}
