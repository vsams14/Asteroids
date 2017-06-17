using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [HideInInspector]
    public BoardManager boardReference;
    public float abundance, initial;
    private bool isSelected;

    // Use this for initialization
    void Start() {
        transform.Find("reticule").gameObject.SetActive(false);
        abundance = Random.Range(0f, 100f);
        initial = abundance;
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-20f, Random.Range(3f, 6f), 0f), Time.deltaTime);
        if (transform.position.x < -10.5) {
            Destroy(transform.gameObject);
        }
    }

    private void OnMouseDown() {
        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        foreach (GameObject GO in allObjects) {
            if (GO.GetComponent<Asteroid>() != null) {
                GO.transform.Find("reticule").gameObject.SetActive(false);
            }
        }
        isSelected = !isSelected;
        transform.Find("reticule").gameObject.SetActive(isSelected);
        boardReference.SetSelectedAst(isSelected ? this : null);
    }

    public void Deselect() {
        transform.Find("reticule").gameObject.SetActive(false);
    }
}
