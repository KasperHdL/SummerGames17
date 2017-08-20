using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {


    public bool timer_started = false;
    public float max_time;
    private float start_time;
    public Text timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(timer_started){
            timer.text = Mathf.RoundToInt(max_time - (Time.time - start_time)) + "";
        }
	}


    public void Start_Timer(){
        start_time = Time.time;
        timer_started = true;

    }
}
