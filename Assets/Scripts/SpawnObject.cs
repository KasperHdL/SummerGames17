using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject prefab; 

    public bool on_trigger;
    public bool on_collision;

    public float delay;

    private float next = 0;


    void OnCollisionEnter(Collision coll){
        if(!on_collision) return;
        Trigger(coll.transform);
    }

    void OnTriggerEnter(Collider coll){
        if(!on_trigger)return;
        Trigger(coll.transform);
    }

    void Trigger(Transform t){
        if(next > Time.time) return;
        if(t.GetComponent<SwarmClient>() == null) return;
        next = Time.time + delay;
        Instantiate(prefab, t.position, Quaternion.identity);
    }
}
