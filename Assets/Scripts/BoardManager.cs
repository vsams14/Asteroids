using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BoardManager : MonoBehaviour {

    public GameObject bg, menuUI, gameGround, atmos, asteroidSpawner, miner, gameUI;
    public Planet planet;
    [HideInInspector]
    public Planet planetInstance;
    private Transform boardHolder;
    private GameObject ui;
    private Miner minerInstance;
    private int level;
    private Asteroid selectedAst;
    private Atmos atmosRef;

    public void SetupScene(int level) {
        this.level = level;
        boardHolder = new GameObject("Board").transform;
        switch (level) {
            case 0: {
                    PlaceBackground();
                    PlaceCanvas();
                    PlacePlanet();
                    break;
                }
            case 1: {
                    PlaceGameUI();
                    PlaceGameGround();
                    PlaceAtmos();
                    PlaceMiner();
                    PlaceASpawn();
                    break;
                }
            default: {
                    break;
                }
        }
    }

    private void PlaceBackground() {
        GameObject instance = Instantiate(bg, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    private void PlaceGameGround() {
        GameObject instance = Instantiate(gameGround, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    private void PlacePlanet() {
        Planet instance = Instantiate(planet, new Vector3(1.28f, 0f, 0f), Quaternion.identity) as Planet;
        instance.transform.SetParent(boardHolder);
        instance.Generate();
        planetInstance = instance;
        UpdateSidebar();
    }

    private void PlaceCanvas() {
        GameObject instance = Instantiate(menuUI, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        ui = instance;
        ShowControls();
        HideAudio();
        HideVideo();
    }

    private void PlaceGameUI() {
        GameObject instance = Instantiate(gameUI, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        ui = instance;
        ui.SetActive(false);
    }

    private void PlaceAtmos() {
        GameObject instance = Instantiate(atmos, new Vector3(0f, -1.5f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        atmosRef = instance.GetComponent<Atmos>();
        atmosRef.boardReference = this;
    }

    private void PlaceASpawn() {
        GameObject instance = Instantiate(asteroidSpawner, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance.GetComponent<AsteroidSpawner>().boardReference = this;
    }

    private void PlaceMiner() {
        GameObject instance = Instantiate(miner, new Vector3(0f, -1f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        minerInstance = instance.GetComponent<Miner>();
        minerInstance.boardReference = this;
    }

    private void Update() {
        if (level == 1) {
            if (selectedAst != null || minerInstance.isSelected) {
                ui.SetActive(true);
                ui.transform.Find("Sidebar").Find("MINER").gameObject.SetActive(false);
                ui.transform.Find("Sidebar").Find("ASTEROID").gameObject.SetActive(false);
                if (selectedAst != null) {
                    ShowAstData();
                }
                if (minerInstance.isSelected) {
                    ShowMinerData();
                }
            } else {
                ui.SetActive(false);
            }
        }
    }

    private void ShowAstData() {
        ui.transform.Find("Sidebar").Find("ASTEROID").gameObject.SetActive(true);
        ui.transform.Find("Sidebar").Find("ASTEROID").Find("MINED").gameObject.GetComponent<Text>().text = "MINED " + (int)(((selectedAst.initial - selectedAst.abundance) / selectedAst.initial) * 100f) + "%";
    }

    private void ShowMinerData() {
        ui.transform.Find("Sidebar").Find("MINER").gameObject.SetActive(true);
        ui.transform.Find("Sidebar").Find("MINER").Find("SPEED").gameObject.GetComponent<Text>().text = "SPEED: " + minerInstance.miningSpeed;
        ui.transform.Find("Sidebar").Find("MINER").Find("CARGO").gameObject.GetComponent<Text>().text = "CARGO: " + (int)minerInstance.cargo;
        ui.transform.Find("Sidebar").Find("MINER").Find("MAXCARGO").gameObject.GetComponent<Text>().text = "MAX: " + minerInstance.maxCargo;
    }

    public void DestroyBoard() {
        Destroy(boardHolder.gameObject);
    }

    public void UpdateSidebar() {
        ui.transform.Find("Sidebar").Find("PLANETNAME").GetComponent<Text>().text = planetInstance.getName();
    }

    public void ShowControls() {
        ui.transform.Find("Options_Menu").Find("CONTROLS_UI").gameObject.SetActive(true);
    }
    public void ShowAudio() {
        ui.transform.Find("Options_Menu").Find("AUDIO_UI").gameObject.SetActive(true);
    }
    public void ShowVideo() {
        ui.transform.Find("Options_Menu").Find("VIDEO_UI").gameObject.SetActive(true);
    }
    public void HideControls() {
        ui.transform.Find("Options_Menu").Find("CONTROLS_UI").gameObject.SetActive(false);
    }
    public void HideAudio() {
        ui.transform.Find("Options_Menu").Find("AUDIO_UI").gameObject.SetActive(false);
    }
    public void HideVideo() {
        ui.transform.Find("Options_Menu").Find("VIDEO_UI").gameObject.SetActive(false);
    }

    public void SetSelectedAst(Asteroid ast) {
        selectedAst = ast;
        minerInstance.SetAst(ast);
    }

    public void DeselectAst() {
        if (selectedAst != null) {
            selectedAst.Deselect();
            selectedAst = null;
            minerInstance.SetAst(null);
        }
    }

    public void SendCargoToAtmos(float cargo) {
        atmosRef.AddCargo(cargo);
    }
}
