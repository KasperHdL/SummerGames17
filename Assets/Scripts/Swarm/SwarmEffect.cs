using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEffect : MonoBehaviour {

    static int id_counter = 0;
    int id;

    public enum Type{
        Direction,
        Push,
        Pull
    };

    public Type type;

    [Range(0,1)]public float persuasion;

    public Vector3 direction;

    [Header("Trigger Type")]
    public bool on_enter;
    public bool on_stay;
    public bool on_exit;

    void Start(){
        id = id_counter++;
    }

    public void OnTriggerEnter(Collider coll){
        if(!on_enter)return;

        SwarmClient client = coll.GetComponent<SwarmClient>();
        if(client == null) return;
        client.Effect(CalcEffect(client));

    }

    public void OnTriggerStay(Collider coll){
        if(!on_stay)return;

        SwarmClient client = coll.GetComponent<SwarmClient>();
        if(client == null) return;
        client.Effect(CalcEffect(client));

    }

    public void OnTriggerExit(Collider coll){
        if(!on_exit)return;

        SwarmClient client = coll.GetComponent<SwarmClient>();

        if(client == null) return;

        client.Effect(CalcEffect(client));

    }

    public Vector3 CalcEffect(SwarmClient client){

        Vector3 v = Vector3.zero;

        switch(type){
            case Type.Direction:{
                v = direction;
            }break;
            case Type.Push:{
                v = client.transform.position - transform.position;

            }break;
            case Type.Pull:{
                v =  transform.position - client.transform.position;
            }break;
        }


        v = Vector3.Lerp(client.effect_direction, v, persuasion);
        return v;
    }
}
