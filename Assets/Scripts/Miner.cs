using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {

    public bool isSelected, isMining;
    public float miningSpeed = 0.01f, cargo = 0f, maxCargo = 100f;
    public BoardManager boardReference;
    private Asteroid selectedAst;

    private void Start() {
        transform.Find("reticule").gameObject.SetActive(false);
        transform.Rotate(new Vector3(0f, 0f, -180f));
    }

    private void OnMouseDown() {
        isSelected = !isSelected;
        if (isSelected) {
            transform.Find("reticule").gameObject.SetActive(true);
        } else {
            transform.Find("reticule").gameObject.SetActive(false);
        }
    }

    public void SetAst(Asteroid ast) {
        selectedAst = ast;
    }

    private void Update() {
        if (isSelected) {
            //bring up sidebar
        }

        //move to selected ast or home
        Vector3 oldPos = transform.position;
        if (selectedAst != null) {
            transform.position = Vector3.MoveTowards(oldPos, selectedAst.transform.position + new Vector3(0.5f, 0f, 0f), 3 * Time.deltaTime);
        } else {
            transform.position = Vector3.MoveTowards(oldPos, new Vector3(0f, -1f, 0f), 3 * Time.deltaTime);
        }
        Vector3 newPos = transform.position;
        //float theta = Mathf.Atan2(newPos.y - oldPos.y, newPos.x - oldPos.x);
        //this doesn't work correctly
        //transform.Rotate(new Vector3(0f, 0f, theta));

        //mining code
        if (selectedAst != null && (selectedAst.transform.position - newPos).magnitude < 1f && cargo <= maxCargo) {
            isMining = true;
        }
        else {
            isMining = false;
        }

        if (isMining) {
            cargo += miningSpeed * selectedAst.abundance;
            selectedAst.abundance -= miningSpeed * selectedAst.abundance;
        }

        if (cargo > maxCargo) {
            selectedAst = null;
            boardReference.DeselectAst();
        }
        if (transform.position == new Vector3(0f, -1f, 0f)) {
            boardReference.SendCargoToAtmos(miningSpeed * cargo);
            cargo -= miningSpeed * cargo;
        }
    }
}
