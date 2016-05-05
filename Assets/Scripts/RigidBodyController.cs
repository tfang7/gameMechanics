using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof(Rigidbody))]
public class RigidBodyController : MonoBehaviour {
    public Rigidbody rbd;
    public GameObject gameState;
    public float speed = 10.0f;
    public float defaultSpeed = 10.0f;
    public float maxSpeed = 50.0f;
    public float score = 0.0f;
    public float scoreMultiplier = 1.0f;
    public int combo = 0;
    private float xInput,yInput;
    public bool alive;
    public MeshRenderer mr;
    public Material[] mats;
    public bool powerUp;
    public float powerUpTime = 10.0f;
    public int hitsCounter = 0;

    // Use this for initialization
    public enum State
    {
        NEUTRAL,
        BLUE,
        RED
    }
    private State state;
	void Start () {
        mr = GetComponent<MeshRenderer>();
        rbd = GetComponent<Rigidbody>();
        rbd.useGravity = false;
        rbd.velocity = transform.forward * speed;
        gameState = GameObject.FindGameObjectWithTag("GameController");
        //gameState.SendMessage()
        StartCoroutine("FSM");
        alive = true;
        state = State.RED;
        

    }
    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.NEUTRAL:
                    break;
                case State.BLUE:
                    Blue();
                    break;
                case State.RED:
                    Red();
                    break;
                
            }
            yield return null;

        }
    }
    // Update is called once per frame
    void Update () {
        inputHandler();
        SpeedHandler();
        scoreMultiplier = combo * 0.1f;
        
        if (powerUp)
        {
            if (powerUpTime <= Time.time)
            {
                toggleInvuln();
            }
        }
	}

    void inputHandler()
    {
        xInput =  Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(xInput, yInput, transform.forward.z);
        rbd.velocity = movement * speed;
        StateController();

    }
    public void updateScore(float value)
    {
        score += value;
        string display = "Score: ";
        display += score.ToString();
    }
    public string returnState()
    {
        return state.ToString();
    }
    public void SpeedHandler()
    {
        if (combo == 0)
        {
            speed = defaultSpeed;
        }
        else if (speed < defaultSpeed)
        {
            speed = defaultSpeed;
        }
        else if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
        else
        {
            speed = defaultSpeed;
            if (combo > 0)
            {
               speed = defaultSpeed + combo * (0.1f);
            }
        }
    }
    public void StateController()
    {

        bool changeState = Input.GetButtonDown("Fire1");
        bool changeState2 = Input.GetButtonDown("Fire2");


       if (changeState)
        {
            state = State.BLUE;
            mr.material = mats[0];

        }
        else if (changeState2)
        {
            state = State.RED;
            mr.material = mats[1];

        }
        

    }
    public void toggleInvuln()
    {
        powerUp = !powerUp;
        if (powerUp)
        {
            powerUpTime = Time.time + powerUpTime;
            mr.material = mats[2];
        }
        else
        {
            if (state == State.BLUE)
            {
                mr.material = mats[0];
            }
            else
            {
                mr.material = mats[1];

            }

        }
    }
    public void takeHit()
    {
        hitsCounter++;
    }
    public void Blue()
    {
        mr.material = mats[0];
    }
    public void Red()
    {
        mr.material = mats[1];
    }

}
