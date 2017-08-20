using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

    public static bool game_started = false;
    public static bool end_game = false;

    public static float score = 0;


    public AnimationCurve score_count_percent_curve;
    public float start_score_count_delay;
    public float score_duration;
    private float score_count = 0;
    private float start_score_count_time;

    public float max_time;
    public float current_time;
    private float start_time;

    public Text timer;
    public Text score_text;

    public Canvas start_ui;
    public Canvas end_ui;
    public EnterBus enter_bus;

	// Use this for initialization
	void Start () {
        start_ui.enabled = true;
		
        timer.text = Mathf.RoundToInt(max_time) + "";
	}
	
	// Update is called once per frame
	void Update () {
        if(end_game && start_score_count_time > Time.time){
            float t = score_count_percent_curve.Evaluate((Time.time - start_score_count_time) / score_duration);
            if(t >= 1) t = 1;
            score_text.text = (int)(score * t) + "";
            print(score_count);

            if( Time.time < start_score_count_time + score_duration){
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

        end_ui.enabled = true;

        //show points
        start_score_count_time = Time.time + start_score_count_delay;

    }

    public void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

    }
}
