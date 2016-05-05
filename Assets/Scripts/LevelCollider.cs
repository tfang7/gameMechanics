using UnityEngine;
using System.Collections;
public class LevelCollider : MonoBehaviour {
    public GameObject spawnTarget;
    public GameObject clock;
    public GameState gameState;
    public float time;
    private GameTime gameTimer;
	// Use this for initialization
	void Start () {
        gameTimer = clock.GetComponent<GameTime>();
        time = gameTimer.getTime();
        print(time);
	}
	
	// Update is called once per frame
	void Update () {
        time = gameTimer.getTime();
        gameState = GameObject.Find("GameController").GetComponent<GameState>();
	
	}
    void OnTriggerEnter(Collider c)
    {
        if (time < gameTimer.getGameLength())
        {
            if (c.gameObject.tag == "Player")
            {
                c.gameObject.transform.position = spawnTarget.transform.position;
                var startPosition = c.transform.position;
                Camera.main.transform.position = spawnTarget.transform.position;
            }
        } else if (time > gameTimer.getGameLength() && this.tag == "FinalLevel")
        {
            gameState.quitGame();
        }


    }
}
