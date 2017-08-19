using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmClient : MonoBehaviour {

    [HideInInspector]public static int id_counter;
    [HideInInspector]public int id;

    [Range(-1,1)] public float excitement = 0;

    public bool debug;

    public GameObject icon_prefab;
    public int amount_change_spawn_icon;
    public Vector3 icon_offset;

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


    private int type;
    public Material[] materials;
    public AudioClip[] clips_happy;
    public AudioClip[] clips_sad;
    [Range(0,1)]public float volume;
    private static int num_clips_per_type = 3;

    public MeshRenderer model;

    public Material icon_happy;
    public Material icon_sad;

    private Rigidbody body;

    void Start(){
        id = id_counter++;
        body = GetComponent<Rigidbody>();

        effects = new Dictionary<int, EffectInfo>();


        type = Random.Range(0,materials.Length);
        model.material = materials[type];
    }

    void Update(){

        Vector3 surroundings = CalcSurroundEffect();

        //calculate effects;

        float before = excitement;
        effect_direction = Vector3.zero;
        foreach (KeyValuePair<int, EffectInfo> pair in effects){
            EffectInfo info = pair.Value;

            if(info.timestamp_end < Time.time){
                continue;
            }


            //calculate effect
            SwarmEffect effector = info.effector;
            if(effector == null)
                continue;

            if(Mathf.Abs(effector.max_excitement_per_tourist) < Mathf.Abs(info.excitement_received)){
                continue;
            }else{
                float change = effector.excitement_per_second * Time.deltaTime;
                excitement += change;
                info.excitement_received += change;


            }

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


        //Spawn Icon
        if(Mathf.Abs(Mathf.Round(excitement*10) - Mathf.Round(before * 10)) >= amount_change_spawn_icon){
            float change = excitement - before;

            GameObject g = Instantiate(icon_prefab, transform.position + icon_offset, Quaternion.identity) as GameObject;

            bool isHappy = change > 0;

            g.GetComponent<FlyingIcon>().Init(isHappy ? icon_happy : icon_sad);

            //play audio
            int index = Random.Range(0, num_clips_per_type) + type * num_clips_per_type;
            AudioSource.PlayClipAtPoint(change > 0 ? clips_happy[index] : clips_sad[index], transform.position, volume);

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
            effects[effectInfo.id].timestamp_end = effectInfo.timestamp_end;
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
