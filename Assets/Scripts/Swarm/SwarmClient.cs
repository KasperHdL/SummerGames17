using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmClient : MonoBehaviour {

    [HideInInspector]public static int id_counter;
    [HideInInspector]public int id;

    public bool debug;

    public float move_force;

    [HideInInspector]public Vector3 direction;

    [Header("Effect decay")]
    public AnimationCurve curve;

    [HideInInspector]public Vector3 effect_direction;
    private Dictionary<int, EffectInfo> effects;

    [Header("Surroundings")]
    public float range;
    public AnimationCurve range_effect;
    public float forward_range;
    public float surrounding_effect;




    private Rigidbody body;

    void Start(){
        id = id_counter++;
        body = GetComponent<Rigidbody>();

        effects = new Dictionary<int, EffectInfo>();
    }

    void Update(){

        Vector3 surroundings = CalcSurroundEffect();

        //calculate effects;

        effect_direction = Vector3.zero;
        foreach (KeyValuePair<int, EffectInfo> pair in effects){
            EffectInfo info = pair.Value;

            if(info.timestamp_end < Time.time){
                continue;
            }

            //calculate effect
            SwarmEffect effector = info.effector;

            Vector3 delta = transform.position - effector.transform.position;
            float effect_distance = delta.magnitude;
            float effect_mult = effector.range_effect.Evaluate(effect_distance) * effector.multiplier;

            if(effect_mult == 0) continue;

            Vector3 v = Vector3.zero;

            switch(effector.type){
                case EffectType.Direction:{
                    v = effector.direction;
                }break;
                case EffectType.Push:{
                    v = delta;
                }break;
                case EffectType.Pull:{
                    v = -delta;
                }break;
                case EffectType.Stop:{
                    v = Vector3.zero;
                }break;
            }


            //effect_direction += v * info.persuasion;
            effect_direction = Vector3.Lerp(effect_direction, v * effect_mult, effector.persuasion);

            if(debug){
                Debug.DrawRay(transform.position + v.normalized, v * effector.persuasion * effect_mult, Color.yellow);
            }
        }


        direction = (surroundings * surrounding_effect) + effect_direction;
        body.AddForce(direction * Time.deltaTime * move_force);

        if(debug){
            Debug.DrawRay(transform.position, direction.normalized, Color.green);
            Debug.DrawRay(transform.position, effect_direction, Color.yellow);
            Debug.DrawRay(transform.position, surroundings, Color.red);
        }
    }

    public void Effect(EffectInfo effectInfo){
        if(!effects.ContainsKey(effectInfo.id)){
            effects.Add(effectInfo.id, effectInfo);
        }else{
            effects[effectInfo.id] = effectInfo;
        }
    }

    public Vector3 CalcSurroundEffect(){

        Vector3 dir = Vector3.zero;

        RaycastHit[] hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up, range, body.velocity.normalized, forward_range); 

        for(int i = 0; i < hits.Length; i++){ 
            if(hits[i].transform == transform) continue; 

            SwarmClient client = hits[i].collider.GetComponent<SwarmClient>(); 
            if(client == null) continue;

            Vector3 delta = client.transform.position - transform.position;
            
            dir += delta.normalized * range_effect.Evaluate(delta.magnitude);

        }

        return dir.normalized;
    }

}
