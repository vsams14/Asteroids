using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmos : MonoBehaviour {

    public BoardManager boardReference;
    public float cargo;
    private bool isSelected;

	// Use this for initialization
	void Start () {
        transform.Find("reticule").gameObject.SetActive(false);
	}

    private void OnMouseDown() {
        isSelected = !isSelected;
        if (isSelected) {
            transform.Find("reticule").gameObject.SetActive(true);
        }
        else {
            transform.Find("reticule").gameObject.SetActive(false);
        }
    }

    public void AddCargo(float toAdd) {
        cargo += toAdd;
    }
}
