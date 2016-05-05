using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameState : MonoBehaviour {
    public float Score = 0.0f;
    public string State = "NEUTRAL";
    public int Combo = 0;
    public float Time;
    public bool EndGame;
    private GameObject scoreObject;
    private GameObject hitsObject;
    public GUIText text;
    private Text textRef;
    public GameObject clock;
    private GameTime timeRef;
    public RigidBodyController player;
    public GameObject GameOverPanel;
    private float currentTime;
    public int hitsLimit;
	// Use this for initialization
	void Start () {
        EndGame = false;
        hitsObject = GameObject.Find("Hits");
        hitsObject.GetComponent<Text>().text = "Hits Taken: " + player.hitsCounter.ToString();
        timeRef = clock.GetComponent<GameTime>();
        scoreObject = GameObject.Find("Score");
        scoreObject.GetComponent<Text>().text = "Score" + Score.ToString();
        player = GameObject.Find("PhysicsController").GetComponent<RigidBodyController>();
        GameOverPanel = GameObject.Find("GameOverPanel");
        GameOverPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //ScoreText.GetComponent<Text>().text = displayText.text;
        updateGUI();
        if (timeRef.getTime() > (timeRef.getGameLength() + 40.0f) || player.hitsCounter > hitsLimit)
        {
            EndGame = true;
        }
        if (EndGame) 
        {
            quitGame();
        }

    }
    void updateGUI()
    {
        Score = player.score;
        State = player.returnState();
        Combo = player.combo;
        float displayTime = timeRef.getTime();
        int day = (int) displayTime / 60;

        scoreObject.GetComponent<Text>().text = "Score: " + Score.ToString() + "\n" + "State: " + State + "\n" + "Day: " + day.ToString() + "\n" +  "Combo: " + Combo.ToString();
        hitsObject.GetComponent<Text>().text = "Hits Taken: " + player.hitsCounter.ToString();
        
    }
    public void quitGame()
    {
        GameOverPanel.SetActive(true);
    }

}
