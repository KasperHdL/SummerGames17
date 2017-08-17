using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmClient : MonoBehaviour {

    public bool debug;

    public float move_force;

    public Vector3 direction;

    [Header("Effect decay")]
    public Vector3 effect_direction;
    public float effect_weight;

    public AnimationCurve effect_decay;
    private float last_effect_time;

    [Header("Surroundings")]
    public float range;
    public float forward_range;




    private Rigidbody body;

    void Start(){
        body = GetComponent<Rigidbody>();
    }

    void Update(){

        float decay = effect_decay.Evaluate(Time.time - last_effect_time);
        if(debug)print("Decay " + decay);

        Vector3 surroundings = CalcSurroundEffect();



        direction = (surroundings/effect_weight) * (1-decay) + effect_direction * decay;
        body.AddForce(direction * Time.deltaTime * move_force);

        Debug.DrawRay(transform.position, direction.normalized, Color.green);
        Debug.DrawRay(transform.position, effect_direction, Color.yellow);
        Debug.DrawRay(transform.position, surroundings, Color.red);
    }

    public void Effect(Vector3 direction){
        last_effect_time = Time.time;

        effect_direction = direction;
        Debug.DrawRay(transform.position, direction, Color.red);

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

    public struct EffectInfo{
        int id;
        float timestamp;
        AnimationCurve decay;
        float force;
        Vector3 direction;
    }
}
