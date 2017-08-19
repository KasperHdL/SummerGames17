using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEffect : MonoBehaviour {

    static int id_counter = 0;
    int id;

    public bool allow_triggers = true;

    public EffectType type;

    [Range(0,1)]public float persuasion;
    public float duration;

    public float max_excitement_per_tourist;

    public float excitement_per_second;

    public AnimationCurve range_effect;
    public float multiplier = 1;

    [Header("Trigger Type")]
    public bool on_enter;
    public bool on_stay;
    public bool on_exit;

    [Header("Only used for Type Direction")]
    public Vector3 direction;

    void Start(){
        id = id_counter++;
    }

    public void OnTriggerEnter(Collider coll){
        if(!on_enter)return;

        SwarmClient client = coll.GetComponent<SwarmClient>();
        if(client == null || !allow_triggers) return;

        client.Effect(CalcEffect(client));

    }

    public void OnTriggerStay(Collider coll){
        if(!on_stay)return;

        SwarmClient client = coll.GetComponent<SwarmClient>();
        if(client == null || !allow_triggers) return;

        client.Effect(CalcEffect(client));

    }

    public void OnTriggerExit(Collider coll){
        if(!on_exit)return;

        SwarmClient client = coll.GetComponent<SwarmClient>();

        if(client == null || !allow_triggers) return;

        client.Effect(CalcEffect(client));

    }

    public EffectInfo CalcEffect(SwarmClient client){

        EffectInfo info = new EffectInfo();
        info.id = id;
        info.timestamp_end = Time.time + duration;
        info.effector = this;


        return info;
    }
}
