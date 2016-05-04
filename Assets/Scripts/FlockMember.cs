using UnityEngine;
using System.Collections;

public class FlockMember : MonoBehaviour
{
    public float speed = 1.0f;
    public float magnetSpeed = 10.0f;
    public float lifetime = 30.0f;
    public bool inFlock = false;
    float startTime;
    float lifeTimer;
    public Rigidbody rb;
    public GameObject gameState;
    private float scoreValue = 50.0f;
    public RigidBodyController player;
    public GameObject playerObject;
    public bool alive;
    public bool attracted;
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
        startTime = Time.time;
        alive = true;
        attracted = false;
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * speed * -1;
        lifeTimer = startTime + lifetime;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<RigidBodyController>();
        RandomState();
        print(state.ToString());
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
      //  state = State.BLUE;
        
        if (random == 1)
        {
            state = State.BLUE;
            Blue();
        } else if (random == 2)
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
        float playerdist = Vector3.Distance(transform.position, player.transform.position);
        if (playerdist > 100)
        {
            transform.LookAt(player.transform);
            rb.AddRelativeForce(Vector3.forward * (speed + magnetSpeed), ForceMode.Force);
        }

    }
    void Update()
    {

        if (attracted)
        {
            //Vector3 field = playerObject.transform.position - transform.position;
            //float index = (5f - field.magnitude) / 5f;
           // rb.AddForce(magnetSpeed * field * index);
            //  Transform dir = transform.TransformDirection - player.transform.TransformDirection;
        }
        

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

            // print(player.state);
            scoreValue = 0.0f;  
            if (StateCheck())
            {
                attracted = true;
                //Debug.Log(playerState);
                if (gameObject != null)
                    {
                        scoreValue = 100.0f;//player.updateScore(scoreValue);
                        Destroy(gameObject);
                    }
                
            }
            else
            {
                if (gameObject != null)
                {
                    //scoreValue = 50.0f;
                    //player.updateScore(scoreValue);
                    //Destroy(gameObject);
                }
            }
            player.updateScore(scoreValue);

        }
    }
    public bool StateCheck()
    {
        string playerState = (player.returnState());
        
        return playerState == state.ToString() || playerState == "NEUTRAL";
    }
}
 
