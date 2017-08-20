using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

    public static bool game_started = false;
    public static bool end_game = false;
    public static float score = 0;


    public float max_time;
    public float current_time;
    private float start_time;
    public Text timer;
    public Canvas start_ui;
    public EnterBus enter_bus;

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
        }else if(!end_game){
            current_time = Time.time - start_time;
            timer.text = Mathf.RoundToInt(max_time - current_time) + "";

            if(current_time <= 0 || end_game){
                EndGame();
            }
        }
	}


    public void StartGame(){
        start_time = Time.time;
        game_started = true;
        start_ui.enabled = false;

    }
    public void EndGame(){
        end_game = true;
        enter_bus.allow_passengers = false;

        print(score);

        //show points


    }
}
