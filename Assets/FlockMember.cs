using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class FlockMember : MonoBehaviour {
    public float speed = 1.0f;
    public float lifetime = 30.0f;
    public bool inFlock = false;
    float startTime;
    float lifeTimer;
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
        startTime = Time.time;
        alive = true;
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * speed * -1;
        lifeTimer = startTime + lifetime;
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
        }
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
            if (gameObject != null)
            {
                Destroy(gameObject);

            }

            //c.gameObject.SendMessage("updateScore", scoreValue, SendMessageOptions.DontRequireReceiver);
        }
    }
}
