using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof(Rigidbody))]
public class RigidBodyController : MonoBehaviour {
    public Rigidbody rbd;
    public GameObject gameState;
    public float speed = 10.0f;
    public float score = 0.0f;
    public float scoreMultiplier = 1.0f;
    public int combo = 0;
    public float velocity,acceleration;
    public float xInput,yInput;
    public bool alive;
    public MeshRenderer mr;
    public Material[] mats;
    public LayerMask pickupLayerMask;
    public Vector3 FieldPosition;
    public int FieldRadius;
    public int FieldForce;
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
        FieldRadius = 20;
        FieldPosition = transform.position;
        pickupLayerMask = LayerMask.NameToLayer("FlockMember");
        StartCoroutine("FSM");
        alive = true;
        

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
        scoreMultiplier = combo * 0.1f;
        print(scoreMultiplier);
        //print(state);
	}
    void FixedUpdate()
    {
        /*
        Collider[] cols = Physics.OverlapSphere(transform.position + FieldPosition, FieldRadius, pickupLayerMask);
        Rigidbody temp_rb = null;
        for (int i = 0; i < cols.Length; i++)
        {
            temp_rb = (cols[i].GetComponent<Rigidbody>());
            if (temp_rb == null)
            {
                continue;
            }
            temp_rb.AddExplosionForce(FieldForce * -1, transform.position + FieldPosition, FieldRadius);
        }*/
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
        if (speed < 100.0f)
        {
            speed++;
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
    public void Blue()
    {
        mr.material = mats[0];
    }
    public void Red()
    {
        mr.material = mats[1];
    }

}
