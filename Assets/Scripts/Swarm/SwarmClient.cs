using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmClient : MonoBehaviour {

    public bool debug;

    public float move_force;

    public Vector3 direction;

    [Header("Effect decay")]
    public Vector3 effect_direction;
    public AnimationCurve curve;

    private Dictionary<int, EffectInfo> effects;

    [Header("Surroundings")]
    public float range;
    public float forward_range;
    public float surrounding_effect;




    private Rigidbody body;

    void Start(){
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

            float t = Time.time - info.timestamp;
            float c = 0;

            c = curve.Evaluate(1 - (t / info.fade_out_length));


            Vector3 v = info.direction * c;

            effect_direction += v * info.persuasion;
        }


        direction = (surroundings * surrounding_effect) + effect_direction;
        body.AddForce(direction * Time.deltaTime * move_force);

        Debug.DrawRay(transform.position, direction.normalized, Color.green);
        Debug.DrawRay(transform.position, effect_direction, Color.yellow);
        Debug.DrawRay(transform.position, surroundings, Color.red);
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

            dir += hits[i].rigidbody.velocity;
        } 

        return dir.normalized;


    }

}
