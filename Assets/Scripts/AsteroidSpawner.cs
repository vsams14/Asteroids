using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject asteroid, minerReference;
    private int frameCount = 0;
    
    // Update is called once per frame
    void Update () {
    	if (frameCount >= (int)Random.Range(60f,180f))
        {
            frameCount = 0;
            GameObject instance = Instantiate(asteroid, new Vector3(10.5f, Random.Range(3f, 6f), 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(transform);
            instance.transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
            instance.GetComponent<Asteroid>().minerReference = minerReference;
        }
        else
        {
            frameCount++;
        }
	}
}
