using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BoardManager : MonoBehaviour
{

    public GameObject bg, selectCanvas, gameGround, atmos, asteroidSpawner, miner;
    public Planet planet;
    [HideInInspector]
    public Planet planetInstance;
    private Transform boardHolder;
    private GameObject ui;
    private GameObject minerInstance;
    private int level;

    public void SetupScene(int level)
    {
        this.level = level;
        boardHolder = new GameObject("Board").transform;
        switch (level)
        {
            case 0:
                {
                    PlaceBackground();
                    PlaceCanvas();
                    PlacePlanet();                    
                    break;
                }
            case 1:
                {
                    PlaceGameGround();
                    PlaceAtmos();
                    PlaceMiner();
                    PlaceASpawn();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void PlaceBackground()
    {
        GameObject instance = Instantiate(bg, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    private void PlaceGameGround()
    {
        GameObject instance = Instantiate(gameGround, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    private void PlacePlanet()
    {
        Planet instance = Instantiate(planet, new Vector3(1.28f, 0f, 0f), Quaternion.identity) as Planet;
        instance.transform.SetParent(boardHolder);
        instance.Generate();
        planetInstance = instance;
        UpdateSidebar();
    }

    private void PlaceCanvas()
    {
        GameObject instance = Instantiate(selectCanvas, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        ui = instance;
        ShowControls();
        HideAudio();
        HideVideo();
    }

    private void PlaceAtmos()
    {
        GameObject instance = Instantiate(atmos, new Vector3(0f, -1.5f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    private void PlaceASpawn()
    {
        GameObject instance = Instantiate(asteroidSpawner, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance.GetComponent<AsteroidSpawner>().minerReference = minerInstance;
    }

    private void PlaceMiner()
    {
        GameObject instance = Instantiate(miner, new Vector3(0f, -1f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        minerInstance = instance;
    }

    private void Update()
    {
        if (level == 1 && minerInstance.GetComponent<Miner>().isSelected)
        {

        }
    }

    public void DestroyBoard()
    {
        Destroy(boardHolder.gameObject);
    }

    public void UpdateSidebar()
    {
        ui.transform.Find("Sidebar").Find("PLANETNAME").GetComponent<Text>().text = planetInstance.getName();
    }

    public void ShowControls()
    {
        ui.transform.Find("Options_Menu").Find("CONTROLS_UI").gameObject.SetActive(true);
    }
    public void ShowAudio()
    {
        ui.transform.Find("Options_Menu").Find("AUDIO_UI").gameObject.SetActive(true);
    }
    public void ShowVideo()
    {
        ui.transform.Find("Options_Menu").Find("VIDEO_UI").gameObject.SetActive(true);
    }
    public void HideControls()
    {
        ui.transform.Find("Options_Menu").Find("CONTROLS_UI").gameObject.SetActive(false);
    }
    public void HideAudio()
    {
        ui.transform.Find("Options_Menu").Find("AUDIO_UI").gameObject.SetActive(false);
    }
    public void HideVideo()
    {
        ui.transform.Find("Options_Menu").Find("VIDEO_UI").gameObject.SetActive(false);
    }
    public GameObject GetMiner()
    {
        return minerInstance;
    }

}
