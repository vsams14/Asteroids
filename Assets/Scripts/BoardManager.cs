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

    void PlaceBackground()
    {
        GameObject instance = Instantiate(BG, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    void PlacePlanet()
    {
        Planet instance = Instantiate(planet, new Vector3(1.28f, 0f, 0f), Quaternion.identity) as Planet;
        instance.transform.SetParent(boardHolder);
        instance.Generate();
        planetInstance = instance;
        updateSidebar();
    }

    internal void updateSidebar()
    {
        UI.transform.Find("Sidebar").Find("PLANETNAME").GetComponent<Text>().text = planetInstance.getName();
    }

    void PlaceCanvas()
    {
        GameObject instance = Instantiate(selectCanvas, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        UI = instance;
    }
}
