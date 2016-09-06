using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetMenu : MonoBehaviour
{
    private Vector3[] bounds = new Vector3[4];
    private Vector3 tooltipPos;
    private GameObject[] ret = new GameObject[4];
    private GameObject tooltip;
    private float planetRadius;
    private float tick;
    private float tock;
    private int planetNumber = 0;
    private Text planetText;

    void Start()
    {
        planetRadius = GetComponent<CircleCollider2D>().radius;
        Vector3 pos = transform.position;
        bounds[0] = pos + new Vector3(-planetRadius - .11f, planetRadius + .05f, 0f);
        bounds[1] = pos + new Vector3(planetRadius - .11f, planetRadius + .05f, 0f);
        bounds[2] = pos + new Vector3(planetRadius - .11f, -planetRadius + .05f, 0f);
        bounds[3] = pos + new Vector3(-planetRadius - .11f, -planetRadius + .05f, 0f);
        tooltipPos = pos + new Vector3(0f, planetRadius + 1f, 0f);
        planetNumber = int.Parse(gameObject.name);
        planetText = CanvasScript.instance.planetTextObject.GetComponent<Text>();
    }

    void OnMouseEnter()
    {
        if (!CanvasScript.instance.keyboard_up)
        {
            createReticule();
            tick = Time.time;
            tock = tick;
        }        
    }

    void OnMouseOver()
    {
        if (!CanvasScript.instance.keyboard_up)
        {
            if (tick != 0)
            {
                GameObject tool = GameManager.instance.boardScript.toolTip;
                tooltip = Instantiate(tool, tooltipPos, Quaternion.identity) as GameObject;
                tick = 0;
            }
            if ((Time.time - tock) > 1.75f && tock != 0)
            {
                planetText.GetComponent<RectTransform>().localPosition = (tooltipPos) * 100;
                if (GameManager.instance.data.save[planetNumber])
                {
                    planetText.text = GameManager.instance.data.planetName[planetNumber] + "\nTerrain: " +
                        GameManager.instance.data.diff[GameManager.instance.data.planetDiff[planetNumber]] + "\nPercent: " +
                        GameManager.instance.data.percent[planetNumber] + "%";
                }
                else
                {
                    planetText.text = "CLICK TO\nCREATE NEW SAVE";
                }

                planetText.enabled = true;
                tock = 0;
            }
        }
    }

    void OnMouseDown()
    {
        if (!CanvasScript.instance.keyboard_up)
        {
            if (GameManager.instance.data.save[planetNumber])
            {
                planetText.enabled = false;
                GameManager.instance.StartGame(planetNumber);
            }
            else
            {
                CanvasScript.instance.showKeyboard(planetNumber);
            }
        }        
    }

    void OnMouseExit()
    {
        destroyReticule();
        tick = 0;
        tock = 0;
        planetText.enabled = false;
    }

    private void createReticule()
    {
        GameObject tl = GameManager.instance.boardScript.reticule[0];
        GameObject tr = GameManager.instance.boardScript.reticule[1];
        GameObject br = GameManager.instance.boardScript.reticule[2];
        GameObject bl = GameManager.instance.boardScript.reticule[3];
        ret[0] = Instantiate(tl, bounds[0], Quaternion.identity) as GameObject;
        ret[1] = Instantiate(tr, bounds[1], Quaternion.identity) as GameObject;
        ret[2] = Instantiate(br, bounds[2], Quaternion.identity) as GameObject;
        ret[3] = Instantiate(bl, bounds[3], Quaternion.identity) as GameObject;
    }

    private void destroyReticule()
    {
        Destroy(ret[0]);
        Destroy(ret[1]);
        Destroy(ret[2]);
        Destroy(ret[3]);
        Destroy(tooltip);
    }
}