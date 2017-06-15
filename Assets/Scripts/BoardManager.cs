using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BoardManager : MonoBehaviour
{

    public GameObject BG;
    public Planet planet;
    public GameObject selectCanvas;
    [HideInInspector]
    public Planet planetInstance;
    private Transform boardHolder;
    private GameObject UI;

    public void SetupScene(int level)
    {
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
            default:
                {
                    break;
                }
        }
    }

    private void PlaceBackground()
    {
        GameObject instance = Instantiate(BG, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    private void PlacePlanet()
    {
        Planet instance = Instantiate(planet, new Vector3(1.28f, 0f, 0f), Quaternion.identity) as Planet;
        instance.transform.SetParent(boardHolder);
        instance.Generate();
        planetInstance = instance;
        updateSidebar();
    }

    private void PlaceCanvas()
    {
        GameObject instance = Instantiate(selectCanvas, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        UI = instance;
        ShowControls();
        HideAudio();
        HideVideo();
    }

    public void updateSidebar()
    {
        UI.transform.Find("Sidebar").Find("PLANETNAME").GetComponent<Text>().text = planetInstance.getName();
    }

    public void ShowControls()
    {
        UI.transform.Find("Options_Menu").Find("CONTROLS_UI").gameObject.SetActive(true);
    }
    public void ShowAudio()
    {
        UI.transform.Find("Options_Menu").Find("AUDIO_UI").gameObject.SetActive(true);
    }
    public void ShowVideo()
    {
        UI.transform.Find("Options_Menu").Find("VIDEO_UI").gameObject.SetActive(true);
    }
    public void HideControls()
    {
        UI.transform.Find("Options_Menu").Find("CONTROLS_UI").gameObject.SetActive(false);
    }
    public void HideAudio()
    {
        UI.transform.Find("Options_Menu").Find("AUDIO_UI").gameObject.SetActive(false);
    }
    public void HideVideo()
    {
        UI.transform.Find("Options_Menu").Find("VIDEO_UI").gameObject.SetActive(false);
    }

}
