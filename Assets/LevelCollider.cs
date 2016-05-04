using UnityEngine;
using System.Collections;
public class LevelCollider : MonoBehaviour {
    public GameObject spawnTarget;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.transform.position = spawnTarget.transform.position;
            var startPosition = c.transform.position;
            Camera.main.transform.position = spawnTarget.transform.position;
        }

    }
}
