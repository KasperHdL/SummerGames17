using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

    public static bool game_started = false;
    public static bool end_game = false;

    private bool finished_audio = false;
    public float score = 0;
    public int num_tourist = 0;
    public bool force_end = false;
    public Color minus_color;


    public AnimationCurve score_count_percent_curve;
    public float start_score_count_delay;
    public float score_duration;
    private float start_score_count_time;

    public float max_time;
    public float current_time;
    private float start_time;

    public Text minus;
    public Text timer;
    public Text score_text;
    public Text num_tourist_text;
    public Text press_to_play;

    public Canvas start_ui;
    public Canvas end_ui;
    public EnterBus enter_bus;

	// Use this for initialization
	void Start () {
        start_ui.enabled = true;
        end_ui.enabled = false;
		
        timer.text = Mathf.RoundToInt(max_time) + "";
	}
	
	// Update is called once per frame
	void Update () {
        if(end_game && start_score_count_time < Time.time){
            float t = score_count_percent_curve.Evaluate((Time.time - start_score_count_time) / score_duration);
            if(t >= 1) t = 1;
            score_text.text = (int)Mathf.Abs(score * t) + "";
            if(score < 0){
                minus.enabled = true;
                score_text.color = minus_color;
                minus.color = minus_color;
            }
            num_tourist_text.text = (int)(num_tourist * t) + "";

            if( Time.time > start_score_count_time + score_duration){
                if(!finished_audio){
                    //AudioSource.PlayClipAtPoint(
                    finished_audio = true;

                }


                float a = (Time.time - (start_score_count_time + score_duration));
                if(a > 1) a = 1;
                Color c = press_to_play.color;
                c.a = a;
                press_to_play.color = c;

                if(     Input.GetKeyDown ( KeyCode.X) ||
                        Input.GetKeyDown ( KeyCode.Z) ||
                        Input.GetKeyDown ( KeyCode.UpArrow) ||
                        Input.GetKeyDown ( KeyCode.DownArrow) ||
                        Input.GetKeyDown ( KeyCode.LeftArrow) ||
                        Input.GetKeyDown ( KeyCode.RightArrow)){
                    RestartGame    ( );
                }
            }

        }else if(!game_started){
            
            if( Input.GetKeyDown ( KeyCode.X) ||
                Input.GetKeyDown ( KeyCode.Z) ||
                Input.GetKeyDown ( KeyCode.UpArrow) ||
                Input.GetKeyDown ( KeyCode.DownArrow) ||
                Input.GetKeyDown ( KeyCode.LeftArrow) ||
                Input.GetKeyDown ( KeyCode.RightArrow)){
                    StartGame    ( );
            }
        }else if(!end_game){
            current_time = Time.time - start_time;
            timer.text = Mathf.RoundToInt(max_time - current_time) + "";

            if(current_time <= 0){
                EndGame();
            }
        }
        if(force_end){
            force_end = false;
            EndGame();
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


        score_text.text = "0";
        num_tourist_text.text = "0";
        minus.enabled = false;
        end_ui.enabled = true;

        //show points
        start_score_count_time = Time.time + start_score_count_delay;

    }

    public void RestartGame(){
        end_game = false;
        game_started = false;
        Application.LoadLevel(0);

    }
}
