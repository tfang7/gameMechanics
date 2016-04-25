using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class TommyFangPlatformer : MonoBehaviour
{
    public float JumpHeight;
    public float JumpTime;
    public float attackTime = 1f;
    public float maxSpeed = 3f;

    float jumpBurst;
    float jumpStart;
    float jumpTimer;
    bool jumpTrigger = false;
    float yvel;
    float xvel;
    float zvel;

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
    // Use this for initialization
    void Start()
    {
        gravity = -9.81f;
        bonusGravity = -10;
        xpos = transform.position.x;
        ypos = transform.position.y;
        zpos = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        jumpBurst = 2 * JumpHeight / JumpTime;
        gravity = (-2 * JumpHeight) / (Mathf.Pow(JumpTime, 2));

        xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        zAxis = CrossPlatformInputManager.GetAxis("Vertical");
        jump = CrossPlatformInputManager.GetButtonDown("Jump");

        if (xAxis > 0 || xAxis < 0)
        {
            if (trigger == false)
            {
                trigger = true;
                startTime = Time.time;
            }

        }
        else if (zAxis > 0 || xAxis < 0)
        {
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
            t = Mathf.Clamp01((Time.time - startTime) / attackTime);
            t = t * t;
            xvel = t * maxSpeed;
            zvel = t * maxSpeed;

            xpos += xvel * Time.deltaTime * xAxis;
            zpos += zvel * Time.deltaTime * zAxis;
        }
        /* if (zAxis > 0 || zAxis < 0)
         {
             zpos += zAxis * 2;
         }*/
        if (jump)
        {
            jumpTrigger = true;
            jumpStart = Time.time;
            jumpTimer = jumpStart;
            yvel = jumpBurst;
        }
        
        if (jumpTrigger)
        {
            jumpTimer += Time.deltaTime;

            //Increase gravity @ peak
            if (jumpTimer - jumpStart > JumpTime)
            {
                gravity += -1000;
                yvel += (gravity) * Time.deltaTime;
                jumpTrigger = false;
            }
         
        }

        else
        {
            if (!jump)
            jumpTrigger = false;
            gravity += -10000;
            yvel += (gravity) * Time.deltaTime;

            /* if (jumpTimer - jumpStart < JumpTime)
             {
                 gravity += -1000;
                 yvel += (gravity) * Time.deltaTime;
             }
             else
             {
                 gravity = -9.81f;
                 yvel += (gravity) * Time.deltaTime;
             }*/

        }
        ypos += yvel * Time.deltaTime + gravity / 2 * Time.deltaTime * Time.deltaTime;
        ypos = ypos < 0f ? 0f : ypos;
        transform.position = new Vector3(xpos, ypos, zpos);
    }
}

/*




using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Platformer : MonoBehaviour
{
    public float JumpHeight;
    public float JumpTime;

    float jumpBurst;
    float jumpStart;
    bool jumpTrigger = false;
    float yvel;
    float ypos;
    float xpos;
    float gravity;
    float bonusGravity;
    bool trigger = false;
    // Use this for initialization
    void Start()
    {
        gravity = -9.81f;
        bonusGravity = -10;
        xpos = transform.position.x;
        ypos = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        jumpBurst = 2 * JumpHeight / JumpTime;
        gravity = (-2 * JumpHeight) / (Mathf.Pow(JumpTime, 2));
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            
            jumpStart = Time.time;
            yvel = jumpBurst;
            JumpTime -= Time.deltaTime;
            float peak = JumpTime - Time.deltaTime;
            float testme = Time.deltaTime;

            float totalGravity = gravity * bonusGravity;


            print("This is jump start: " + jumpStart);
            print("This is jump time: " + JumpTime);

            if (jumpStart - Time.deltaTime < JumpTime )
            {

                float test = Time.time - jumpStart + Time.deltaTime;
                print(test);
                yvel += (gravity) * Time.deltaTime;
            }
            if (jumpStart - Time.deltaTime > 0.5)
            {
                yvel += (gravity) * Time.deltaTime;
            }
        }
        else
        {
            //gravity = -9.81f;
            yvel += (gravity) * Time.deltaTime;
        }
        ypos += yvel * Time.deltaTime + gravity / 2 * Time.deltaTime * Time.deltaTime;
        ypos = ypos < 0f ? 0f : ypos;
        transform.position = new Vector3(xpos, ypos, transform.position.z);
    }
}

    */