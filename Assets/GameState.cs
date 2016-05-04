using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public float Score = 0.0f;
    public float Time;
    public GameObject ScoreText;
    public GUIText text;
    private Text textRef;
    private Text displayText;
    public RigidBodyController player;
	// Use this for initialization
	void Start () {
        ScoreText = GameObject.Find("Score");
        ScoreText.GetComponent<Text>().text = "Score" + Score.ToString();
        player = GameObject.Find("PhysicsController").GetComponent<RigidBodyController>();
    }
	
	// Update is called once per frame
	void Update () {
        //ScoreText.GetComponent<Text>().text = displayText.text;
        updateScore();

    }
    void updateScore()
    {
        Score = player.score;
        ScoreText.GetComponent<Text>().text = "Score" + Score.ToString() + "\n" + "Combo:";

    }
}
