using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public float Score = 0.0f;
    public string State = "NEUTRAL";
    public float Time;
    public GameObject ScoreText;
    public GUIText text;
    private Text textRef;
    private Text displayText;
    public GameObject clock;
    private GameTime timeRef;
    public RigidBodyController player;
	// Use this for initialization
	void Start () {
        timeRef = clock.GetComponent<GameTime>();
        ScoreText = GameObject.Find("Score");
        ScoreText.GetComponent<Text>().text = "Score" + Score.ToString();
        player = GameObject.Find("PhysicsController").GetComponent<RigidBodyController>();
    }
	
	// Update is called once per frame
	void Update () {
        //ScoreText.GetComponent<Text>().text = displayText.text;
        updateGUI();

    }
    void updateGUI()
    {
        Score = player.score;
        State = player.returnState();
        float displayTime = Mathf.Round(timeRef.getTime());
         // Debug.Log((timeRef.getTime()));
        ScoreText.GetComponent<Text>().text = "Score" + Score.ToString() + "\n" + State + "\n" + displayTime.ToString();

    }

}
