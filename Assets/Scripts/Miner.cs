using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {

    public bool isSelected;
    private Vector3 selectedAst = new Vector3(0f, -1f, 0f);

    private void Start()
    {
        transform.Find("reticule").gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;
        if (isSelected)
        {
            transform.Find("reticule").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("reticule").gameObject.SetActive(false);

        }

    }

    public void MoveTo(Vector3 pos)
    {
        selectedAst = pos;
    }

    private void Update()
    {
        if (isSelected)
        {
            transform.position = Vector3.MoveTowards(transform.position, selectedAst, 3 * Time.deltaTime);
        }
    }
}
