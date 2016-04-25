using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Platformer : MonoBehaviour
{
    public float JumpHeight = 10f;
    public float JumpTime = 1f;
    public float attackTime = 1f;
    public float maxSpeed = 3f;

    float jumpBurst;
    float jumpStart;
    float jumpTimer;
    bool jumpTrigger = false;
    float yvel;
    float xvel;
    float zvel;

    float defaultMaxSpeed;
    float t;
    float ypos;
    float xpos;
    float zpos;
    float startTime;
    float xAxis, zAxis;
    float gravity;
    float bonusGravity;
    bool trigger = false;
    bool jump = false;
    bool jumpHeld = false;

    // Use this for initialization
    void Start()
    {
        gravity = -9.81f;
        xpos = transform.position.x;
        ypos = transform.position.y;
        zpos = transform.position.z;
        defaultMaxSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        jumpBurst = 2 * JumpHeight / JumpTime;
        gravity = (-2 * JumpHeight) / (Mathf.Pow(JumpTime, 2));

        xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        zAxis = CrossPlatformInputManager.GetAxis("Vertical");
        jump = CrossPlatformInputManager.GetButtonDown("Jump");
        jumpHeld = CrossPlatformInputManager.GetButton("Jump");


        //Strafing speeds 
        if ((xAxis > 0 && zAxis > 0) || (xAxis < 0 && zAxis < 0) || 
                 (xAxis > 0 && zAxis < 0) || (xAxis < 0 && zAxis > 0))
        {
            //if strafing, cut speed
            maxSpeed = defaultMaxSpeed * 0.5f;

            if (trigger == false)
            {
                trigger = true;
                startTime = Time.time;
            }
        }
        else if (xAxis > 0 || xAxis < 0 || zAxis > 0 || zAxis < 0)
        {
            maxSpeed = defaultMaxSpeed;
            if (trigger == false)
            {
                trigger = true;
                startTime = Time.time;
            }
        }
        else
        {
            trigger = false;
        }

        if (trigger)
        {
            //we are running
            t = Mathf.Clamp01((Time.time - startTime) / attackTime);
            t = t * t;
            xvel = t * maxSpeed;
            zvel = t * maxSpeed;
            xpos += xvel * Time.deltaTime * xAxis;
            zpos += zvel * Time.deltaTime * zAxis;
        }
        if (jump)
        {
            //we are jumping
            jumpTrigger = true;
            jumpStart = Time.time;
            jumpTimer = jumpStart;
            yvel = jumpBurst;
        }

        if (jumpTrigger)
        {
            jumpTimer += Time.deltaTime;
            if (!jumpHeld)
            {
                gravity += -1000.0f;
                yvel += (gravity) * Time.deltaTime;
                print(yvel);

            }
            if (jumpTimer - jumpStart > JumpTime)
            {
                gravity += -1000.0f;
                yvel += (gravity) * Time.deltaTime;
                print(yvel);
            }
            //Normal gravity when jumping

            //Increase gravity in middle of jump if jump not held
            else if (jumpTimer - jumpStart > JumpTime - (JumpTime/2))
            {

                print("hat");
                gravity += -2000.0f;
                yvel += (gravity) * Time.deltaTime;

            }
            
        }
        //set gravity back to normal
        else
        {
            jumpTrigger = false;
            gravity = -9.81f;
            yvel += (gravity) * Time.deltaTime;
        }
        ypos += yvel * Time.deltaTime + gravity / 2 * Time.deltaTime * Time.deltaTime;
        ypos = ypos < 0f ? 0f : ypos;
        transform.position = new Vector3(xpos, ypos, zpos);
    }
}