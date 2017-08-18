using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmClient : MonoBehaviour {

    [HideInInspector]public static int id_counter;
    [HideInInspector]public int id;

    public bool debug;

    public float move_force;

    public Vector3 direction;

    public float min_change;
    public float direction_change_force;

    [Header("Effect decay")]
    public Vector3 effect_direction;
    public AnimationCurve curve;

    private Dictionary<int, EffectInfo> effects;

    private Dictionary<int, Vector3> last_surrounders;

    [Header("Surroundings")]
    public float range;
    public float forward_range;
    public float surrounding_effect;




    private Rigidbody body;

    void Start(){
        id = id_counter++;
        body = GetComponent<Rigidbody>();

        effects = new Dictionary<int, EffectInfo>();
        last_surrounders = new Dictionary<int, Vector3>();
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

            if(debug){
                Debug.DrawRay(transform.position + v.normalized, v * info.persuasion, Color.yellow);
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

        float force_mult = 1;
        for(int i = 0; i < hits.Length; i++){ 
            if(hits[i].transform == transform) continue; 

            SwarmClient client = hits[i].collider.GetComponent<SwarmClient>(); 
            if(client == null) continue;

            //get last velo for client
            Vector3 last_velo = Vector3.zero;
            bool is_new = true;
            if(last_surrounders.ContainsKey(client.id)){
                last_velo = last_surrounders[client.id];
                is_new = false;
            }

            Vector3 velo = hits[i].rigidbody.velocity;;

            if(!is_new){
                Vector3 delta = velo - last_velo;


                if(delta.magnitude > min_change){
                    dir = Vector3.Lerp(dir, velo, .5f);
                    force_mult = direction_change_force;
                }else{
                    dir += velo;
                }


            }else{
                dir += velo;
            }


            if(debug){
                Debug.DrawRay(hits[i].transform.position, velo, Color.red);
            }

            //upsert entry
            if(!last_surrounders.ContainsKey(client.id)){
                last_surrounders.Add(client.id, velo);
            }else{
                last_surrounders[client.id] = velo;
            }
        } 


        return dir.normalized * force_mult;
    }

}
