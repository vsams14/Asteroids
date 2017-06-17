using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public GameObject minerReference;
    [HideInInspector]
    public bool isSelected;

	// Use this for initialization
	void Start () {
        transform.Find("reticule").gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //give the asteroids counterclockwise rotation
        //transform.forward = transform.forward + new Vector3(0f, 0f, 20f);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-20f, Random.Range(3f, 6f), 0f), Time.deltaTime);
        if(transform.position.x < -10.5)
        {
            Destroy(transform.gameObject);
            if (isSelected)
            {
                minerReference.GetComponent<Miner>().MoveTo(new Vector3(0f, -1f, 0f));
                return;
            }
        }
        if (isSelected)
        {
            minerReference.GetComponent<Miner>().MoveTo(transform.position + new Vector3(0.5f, 0f, 0f));
        }
	}

    private void OnMouseDown()
    {
        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        foreach (GameObject GO in allObjects)
        {
            if (GO.GetComponent<Asteroid>() != null)
            {
                GO.transform.Find("reticule").gameObject.SetActive(false);
                GO.GetComponent<Asteroid>().isSelected = false;
            }
        }
        transform.Find("reticule").gameObject.SetActive(true);
        isSelected = true;
    }
}
