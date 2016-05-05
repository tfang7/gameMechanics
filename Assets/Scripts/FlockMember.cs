using UnityEngine;
using System.Collections;

public class FlockMember : MonoBehaviour
{
    public float speed = 1.0f;
    public float magnetSpeed = 10.0f;
    public float magnetRadius = 30.0f;
    public float lifetime = 30.0f;
    public bool inFlock = false;
    float startTime;
    float lifeTimer;
    public GameObject gameState;
    private float scoreValue = 50.0f;
    public RigidBodyController player;
    public GameObject playerObject;
    public GameObject clock;
    public float actualTime;
    public bool alive;
    public bool attracted;
    private GameTime timer;

    public enum State
    {
        NEUTRAL,
        BLUE,
        RED
    }
    public MeshRenderer mr;
    public Material[] mats;
    private State state;
    // Use this for initialization
    GameObject Player;
    // Use this for initialization
    void Start()
    {
        clock = GameObject.Find("GameTime");
        timer = clock.GetComponent<GameTime>();
        startTime = Time.time;
        alive = true;
        attracted = false;
        mr = GetComponent<MeshRenderer>();
        //rb.useGravity = false;
        //rb.velocity = transform.forward * speed * -1;
        lifeTimer = startTime + lifetime;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<RigidBodyController>();
        RandomState();
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.RED:
                    Red();
                    break;
                case State.NEUTRAL:
                    White();
                    break;
                case State.BLUE:
                    Blue();
                    break;

            }
            yield return null;
        }
        StartCoroutine("FSM");
    }

    // Update is called once per frame
    public void Blue()
    {
        mr.material = mats[2];
    }
    public void Red()
    {
        mr.material = mats[0];
    }
    public void White()
    {
        mr.material = mats[1];
    }
    public void RandomState()
    {
        float random = Random.Range(0, 4);
        float timeOfDay = timer.getTime();
        if (timeOfDay >= 180)
        {
            transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
            random = Random.Range(0, 7);
            if (random <= 4)
            {
                state = State.RED;
                Red();
            }
            else
            {
                state = State.BLUE;
                Blue();
            }
        }
        else if (timeOfDay >= 120)
        {
            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            random = Random.Range(0, 7);
            if (random <= 2)
            {
                state = State.NEUTRAL;
                White();
            } else if (random > 3 && random < 5) {
                state = State.RED;
                Red();
            }
            else
            {
                state = State.BLUE;
                Blue();
            }
        }
        else if (timeOfDay >= 60)
        {

            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            if (random <= 1)
            {
                state = State.NEUTRAL;
                White();
            }
            else
            {
                state = State.BLUE;
                Blue();
            }
        }
        else if (timeOfDay >= 0)
        {
            state = State.NEUTRAL;
            White();
        }
        else
        {
            state = State.RED;
            Red();
        }
        
    }
    void FixedUpdate()
    {
         magnetCheck();
    }
    void magnetCheck()
    {
        Vector3 move = new Vector3(0, 0, speed * Time.deltaTime * -1);
        float playerdist = Vector3.Distance(transform.position, player.transform.position);
        if (playerdist < magnetRadius && StateCheck())
        {
            attracted = true;
            transform.LookAt(player.transform);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, magnetSpeed * Time.deltaTime * 1);
        }
        else
        {
            transform.Translate(move);
            attracted = false;
        }
        
    }
    void Update()
    {
        speed = speed + (0.001f) * timer.getTime();
        if (lifeTimer <= Time.time)
        {
            lifeTimer = Time.time + lifetime;
            Destroy(gameObject);
            //Destroy.gameObject);

        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            CalculateScore();
            if (!StateCheck())
            {
                player.combo = 0;
                player.score /= 10;
                player.takeHit();
            }
        }
    }
    public void CalculateScore()
    {
        // print(player.state);
        scoreValue = 0.0f;
        float scoreMultiplier = player.scoreMultiplier;
        if (StateCheck())
        {
            attracted = true;
            //Debug.Log(playerState);
            if (gameObject != null)
            {
                scoreValue = 100.0f * scoreMultiplier;//player.updateScore(scoreValue);
                Destroy(gameObject);
            }

        }
        else
        {
            if (gameObject != null)
            {
                attracted = true;
            }
        }
        player.combo++;
        player.updateScore(scoreValue);

    }
    public bool StateCheck()
    {
        string playerState = (player.returnState());        
        return playerState == state.ToString() || state.ToString() == "NEUTRAL" || player.powerUp == true;
    }
}
 
