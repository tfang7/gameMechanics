using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Zelda : MonoBehaviour
{

    public float distanceToTravel;
    public float travelTime;
    public enum moveType { linear, easeIn, easeOut, easeInOut, arch, custom }
    public moveType style = moveType.linear;
    public AnimationCurve ac;
    public float frequency;
    public float amplitude;

    bool triggered = false;
    float startTime;
    float startx;
    float starty;
    float startz;
    float t; //interpolation parameter
    float x, y, z;

    float target;
    float ydistanceToTravel;
    // Use this for initialization
    void Start()
    {
        startx = transform.position.x;
        starty = transform.position.y;
        startz = transform.position.z;
        ydistanceToTravel = distanceToTravel;
    }

    // Update is called once per frame
    void Update()
    {
        y = transform.position.y;
        x = transform.position.x;
        z = transform.position.z;
        /*   if (CrossPlatformInputManager.GetAxis("Horizontal") > 0f || CrossPlatformInputManager.GetAxis("Horizontal") < 0f || CrossPlatformInputManager.GetAxis("Vertical") > 0f || CrossPlatformInputManager.GetAxis("Vertical") < 0f) ;
           {
               triggered = true;
               startTime = Time.time;
           }*/
        if (CrossPlatformInputManager.GetAxis("Horizontal") > 0f)
        {
            ydistanceToTravel = 0;
            if (distanceToTravel < 0)
            {
                distanceToTravel *= -1;
            }
            triggered = true;
            startTime = Time.time;
        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal") < 0f)
        {

            ydistanceToTravel = 0;
            triggered = true;
            startTime = Time.time;
            if (distanceToTravel > 0)
            {
                distanceToTravel *= -1;
            }
        }
        else if (CrossPlatformInputManager.GetAxis("Vertical") > 0f)
        {
            ydistanceToTravel = distanceToTravel;
            if (ydistanceToTravel < 0)
            {
                ydistanceToTravel *= -1;
            }
            triggered = true;
            startTime = Time.time;
        }
        else if (CrossPlatformInputManager.GetAxis("Vertical") < 0f)
        {
            ydistanceToTravel = distanceToTravel;
            if (distanceToTravel > 0)
            {
                distanceToTravel *= -1;
            }
            triggered = true;
            startTime = Time.time;
        }
        if (triggered)
        {
            t = Mathf.Clamp01((Time.time - startTime) / travelTime);
            if (t == 1)
            {

                if (ydistanceToTravel > 0 || ydistanceToTravel < 0)
                {
                    startz += ydistanceToTravel;
                }
                else
                {
                    startx += distanceToTravel;
                }
                t = 0;
                triggered = false;
            }

            switch (style)
            {
                case moveType.linear:
                    //pass
                    break;
                case moveType.easeIn: // "soft" attack
                    t = t * t;  // t^2
                    break;
                case moveType.easeOut: // "tight" attack
                    t = 2 * t - t * t; // 2t - t^2
                    break;
                case moveType.easeInOut: // sigmoid, a.k.a 's' curve
                    t = 3 * t * t - 2 * t * t * t; // 3t^2 - 2t^3
                    break;
                case moveType.arch:
                    t = t * (1 - t);
                    break;
                case moveType.custom:
                    t = ac.Evaluate(t);
                    break;
            }

            //    Debug.Log(CrossPlatformInputManager.GetAxis("Horizontal"));
            float xAxis = CrossPlatformInputManager.GetAxis("Horizontal") * distanceToTravel * t;
            float zAxis = ydistanceToTravel * t;

            float tempx = (startx + (t * distanceToTravel));
            float tempy = starty + Mathf.Sin(Time.time * frequency) * amplitude;
            float tempz = (startz);
            if (zAxis > 0 || zAxis < 0)
            {
                tempx = x;
                tempz += zAxis;
            }
            Debug.Log(tempx);
            transform.position = new Vector3(tempx, tempy, tempz);
            transform.localScale = new Vector3(1 + t, 1 + t, 1 + t);
        }

    }
}
