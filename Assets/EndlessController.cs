using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
public class EndlessController : MonoBehaviour {

    
    public float speed;

    public float minVel, maxVel;
    
    private List<GameObject> flockList = new List<GameObject>();
    public List<GameObject> PickUpItems;
    float x, y, z;
    int s;
    bool Jump;
    float zAxis, yAxis;
    float xRotation;
    Vector3 targetRot;
    Vector3 startRot;
    float barrelRollSpeed = 5.0f;
    Quaternion rotx;
    // Use this for initialization
    void Start () {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

    }

    // Update is called once per frame
    void Update() {
        targetRot.x += barrelRollSpeed;
        xRotation = transform.rotation.x;
        zAxis = -CrossPlatformInputManager.GetAxis("Horizontal");
        yAxis = CrossPlatformInputManager.GetAxis("Vertical");
        Jump = CrossPlatformInputManager.GetButton("Jump");
        //print(flockList.Count);
        x += speed;

        if (zAxis != 0)
        {
            z += speed * Time.deltaTime * zAxis * 100;
        }
        if (yAxis != 0)
        {
            y += speed * Time.deltaTime * yAxis * 100;
        }  
        if (Jump)
        {
            // transform.Rotate(Vector3.Lerp(transform.rotation, transform.r);
           // transform.rotation = Vector3.Lerp(transform.rotation, targetRot, Time.deltaTime);
        }
        transform.position = new Vector3(x, y, z);
	}
    void LateUpdate ()
    {

    }
    public void addFlockMember(GameObject obj)
    {
        //obj.= gameObject;
        s += 1;
        flockList.Add(obj);
      
    }
    void GetInactiveInRadius()
    {
        float dist = 0;
        foreach (GameObject obj in PickUpItems)
        {
            dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist > 100)
                obj.SetActive(false);
            
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "FlockMember")
        {
           // c.gameObject.SetActive(false);
        }
    }
    void InputHandler()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {

        }
    }
}
