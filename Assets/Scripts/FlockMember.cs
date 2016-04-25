using UnityEngine;
using System.Collections;

public class FlockMember : MonoBehaviour {
    public float lifetime = 1.0f;
    public bool inFlock = false;
    float startTime;
    float lifeTimer;
    GameObject Player;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        lifeTimer = startTime + lifetime;
        Player = GameObject.FindGameObjectWithTag("Player");
        
        
    }
	
	// Update is called once per frame
	void Update () {

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
            if (gameObject != null)
            {
                if (inFlock == false)
                {
                    inFlock = true;
                }
            }
        }
    }
}
