using UnityEngine;
using System.Collections;

public class SpawnVolume : MonoBehaviour {
    public GameObject spawnObject;
    public float spawnRate = 0.5f;
    GameObject player;
    Transform playerTransform;
    float nextSpawn = 0;
    Vector3 spawnBox;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;




    }

    // Update is called once per frame
    void Update () {
        playerTransform = player.transform;
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            spawnBox = transform.localScale;
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);

            GameObject clone = (GameObject)Instantiate(spawnObject, rndPosWithin, transform.rotation);

            


        }
	
	}
}
