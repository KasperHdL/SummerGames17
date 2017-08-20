using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

    public static bool game_started = false;


    public float max_time;
    private float start_time;
    public Text timer;
    public Canvas start_ui;

	// Use this for initialization
	void Start () {
        start_ui.enabled = true;
		
        timer.text = Mathf.RoundToInt(max_time) + "";
	}
	
	// Update is called once per frame
	void Update () {
        if(!game_started){
            if(Input.GetKeyDown(KeyCode.Space)){
                StartGame();
            }
        }else{
            timer.text = Mathf.RoundToInt(max_time - (Time.time - start_time)) + "";
        }
	}


    public void StartGame(){
        start_time = Time.time;
        game_started = true;
        start_ui.enabled = false;

    }
}
