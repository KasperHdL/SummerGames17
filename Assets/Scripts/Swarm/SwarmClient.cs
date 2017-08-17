using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmClient : MonoBehaviour {

    public bool debug;

    public float range;
    public float forward_range;

    public float move_force;
    public float swarm_force;
    public float swarm_decay;

    public float swarm_velo_minimum;


    private Rigidbody body;

    void Start(){
        body = GetComponent<Rigidbody>();
    }

    void Update(){

        RaycastHit[] hits = Physics.CapsuleCastAll(transform.position, transform.position + Vector3.up, range, transform.forward, forward_range);

        for(int i = 0; i < hits.Length; i++){
            if(hits[i].transform == transform) continue;

            SwarmClient client = hits[i].collider.GetComponent<SwarmClient>();


            if(hits[i].rigidbody.velocity.magnitude > swarm_velo_minimum){
                body.AddForce(hits[i].rigidbody.velocity.normalized * swarm_force * Time.deltaTime * swarm_decay);

                if(debug){
                    Debug.DrawRay(hits[i].transform.position, hits[i].rigidbody.velocity, Color.green);
                    Debug.DrawRay(transform.position, body.velocity, Color.green);
                }

            }else if(debug){
                Debug.DrawRay(hits[i].transform.position, hits[i].rigidbody.velocity, Color.red);
                print(i + ": " + hits[i].rigidbody.velocity);

            }
        }
    }
}
