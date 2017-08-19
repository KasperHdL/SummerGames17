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
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 camera_delta = transform.position - Camera.main.transform.position;
        camera_delta.y = 0;
        camera_delta = camera_delta.normalized;

        Vector3 perp = Vector3.Cross(camera_delta, -Vector3.up);

        Vector3 move = perp * input.x + camera_delta * input.y;
        move = move.normalized;

        body.AddForce(move * move_force * Time.deltaTime);

        //rotate

        input = Input.mousePosition;
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
