using UnityEngine;
using System.Collections;

public class FlockMember : MonoBehaviour {
    public float speed = 1.0f;
    public float lifetime = 30.0f;
    public bool inFlock = false;
    float startTime;
    float lifeTimer;
<<<<<<< HEAD:Assets/FlockMember.cs
    public Rigidbody rb;
    public GameObject gameState;
    private float scoreValue = 50.0f;
    public RigidBodyController player;
    public bool alive;
    public enum State
    {
        NEUTRAL,
        BLUE,
        RED
    }
    public MeshRenderer mr;
    public Material[] mats;
    public State state;
    // Use this for initialization
    void Start () {
=======
    GameObject Player;
	// Use this for initialization
	void Start () {
>>>>>>> 9d26a941986e14bfef2da2d259cc5c19ec48e388:Assets/Scripts/FlockMember.cs
        startTime = Time.time;
        alive = true;
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * speed * -1;
        lifeTimer = startTime + lifetime;
<<<<<<< HEAD:Assets/FlockMember.cs
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RigidBodyController>();
        StartCoroutine("FSM");
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
=======
        Player = GameObject.FindGameObjectWithTag("Player");
        
        
    }
	
	// Update is called once per frame
	void Update () {
>>>>>>> 9d26a941986e14bfef2da2d259cc5c19ec48e388:Assets/Scripts/FlockMember.cs


            }
            yield return null;

        }
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
        float random = Random.Range(0,4);
        state = State.NEUTRAL;
        /*
        if (random == 1)
        {
            state = State.BLUE;
        } else if (random == 2)
        {
            state = State.NEUTRAL;
        }
        else
        {
            state = State.RED;
        }
        */
    }
    void Update () {
        if (lifeTimer <= Time.time)
        {
            lifeTimer = Time.time + lifetime;
            Destroy(gameObject);
            //Destroy.gameObject);

        }
<<<<<<< HEAD:Assets/FlockMember.cs
	}
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {

            player.updateScore(scoreValue);
            print(player.state);
            string playerState = (player.state).ToString();
            if (playerState == "BLUE")
            {
                Debug.Log(playerState);
            }
=======

    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {            
>>>>>>> 9d26a941986e14bfef2da2d259cc5c19ec48e388:Assets/Scripts/FlockMember.cs
            if (gameObject != null)
            {
                Destroy(gameObject);

            }

            //c.gameObject.SendMessage("updateScore", scoreValue, SendMessageOptions.DontRequireReceiver);
        }
    }
}
