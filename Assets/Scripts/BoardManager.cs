using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour
{

    public GameObject[] asteroidsPlay;
    public GameObject[] asteroidsExit;
    public GameObject[] asteroidsMenu;
    public GameObject[] asteroidsColorMenu;
    public GameObject[] asteroidsColorPicker;
    public GameObject[] planets;
    public GameObject[] reticule;
    public GameObject toolTip;
    public GameObject back;
    public GameObject Sun;
    public GameObject Ship;
    public GameObject BG;
    private Transform boardHolder;

    public void SetupScene(int level)
    {
        boardHolder = new GameObject("Board").transform;
        switch (level)
        {
            case 0:
                {
                    AsteroidCreate();
                    PlaceShip();
                    PlaceBackground();
                    break;
                }
            case 1:
                {
                    ColorMenuCreate();
                    PlaceShip();
                    PlaceBackground();
                    break;
                }
            case 2:
                {
                    PlayMenuCreate();
                    PlaceShip();
                    PlaceBackground();
                    break;
                }
        }
    }

    void PlayMenuCreate()
    {
        GameObject instance = Instantiate(back, new Vector3(Random.Range(-5f, 8f), Random.Range(-4f, 4f), 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(Sun, new Vector3(-10.88f, 0, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(planets[0], new Vector3(2f, 1.83f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(planets[1], new Vector3(-1f, -1f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(planets[2], new Vector3(-3.75f, 1f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(planets[3], new Vector3(6.45f, -3.51f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(planets[4], new Vector3(-3.25f, -3.25f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    void ColorMenuCreate()
    {
        GameObject instance = Instantiate(back, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        foreach (GameObject g in asteroidsColorPicker)
        {
            instance = Instantiate(g, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }

    }

    void AsteroidCreate()
    {
        GameObject instance = Instantiate(asteroidsPlay[Random.Range(0, asteroidsPlay.Length)], new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(asteroidsExit[Random.Range(0, asteroidsExit.Length)], new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(asteroidsMenu[Random.Range(0, asteroidsMenu.Length)], new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
        instance = Instantiate(asteroidsColorMenu[Random.Range(0, asteroidsColorMenu.Length)], new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    void PlaceShip()
    {
        GameObject instance = Instantiate(Ship, new Vector3(0f, -5.082f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }

    void PlaceBackground()
    {
        GameObject instance = Instantiate(BG, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
    }
}
