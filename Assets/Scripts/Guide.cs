using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour {

    public float move_force;

    private Rigidbody body;

    public GuideAction[] actions;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {

        //movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        body.AddForce(move * move_force * Time.deltaTime);

        //rotate

        Vector2 input = Input.mousePosition;
        Vector3 delta = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(input);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            delta = hit.point - transform.position;
            delta.y = 0;
            Debug.DrawRay(transform.position, delta, Color.red);
            delta = delta.normalized;


            float rot = Mathf.Atan2(-delta.z, delta.x);

            transform.rotation = Quaternion.Euler(0, rot * Mathf.Rad2Deg + 90, 0);

        }

        for(int i = 0; i < actions.Length; i++){
            GuideAction a = actions[i];

            //activate
            if(Input.GetKey(a.key)){
                a.effect.allow_triggers = true;
            }else{
                a.effect.allow_triggers = false;
            }

        }

		
	}
}

[System.Serializable]
public class GuideAction{
    public KeyCode key;
    public SwarmEffect effect;
    [HideInInspector] public float end = 0;
    [HideInInspector] public float next = 0;
}
