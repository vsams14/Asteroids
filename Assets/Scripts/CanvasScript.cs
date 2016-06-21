using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance = null;
    public Text planetText;
    public GameObject[] Keyboard;
    public bool keyboard_up = false;
    public Button[] radialB;
    public Text textField;
    private string pName = "";
    private string valid = "1234567890QWERTYUIOPASDFGHJKL#ZXCVBNM-_.qwertyuiopasdfghjklzxcvbnm";
    public Sprite radial, radial_selected;
    private int diff = 1;
    public int callingPlanet;

    void Awake()
    {
        if (instance == null)
        {
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        planetText.GetComponent<Text>().enabled = false;
        hideKeyboard();
    }

    public void hideKeyboard()
    {
        foreach (GameObject g in Keyboard)
        {
            g.gameObject.SetActive(false);
        }
        keyboard_up = false;
    }

    public IEnumerator showKeyboard(int i)
    {
        keyboard_up = true;
        yield return new WaitForSeconds(.05f);
        foreach (GameObject g in Keyboard)
        {
            g.gameObject.SetActive(true);
        }
        pName = "";
        callingPlanet = i;
        diff = 1;
    }

    public void typeChar(string c)
    {
        if (c == "*")
        {
            if (pName.Length > 0)
            {
                pName = pName.Substring(0, pName.Length - 1);
            }
        }
        else
        {
            if (pName.Length < 7)
            {
                pName += c;
            }
        }        
    }

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == "\b"[0])
            {
                if (pName.Length > 0)
                {
                    pName = pName.Substring(0, pName.Length - 1);
                }
            }
            else if (c == "\n"[0] || c == "\r"[0])
            {
            }
            else
            {
                if (pName.Length < 7 && valid.Contains(c.ToString()))
                {
                    pName += c;
                }
            }
        }
        textField.text = "NAME:" + pName.ToUpper();
    }

    public void setDifficulty(int i)
    {
        switch(i)
        {
            case 0:
                radialB[0].GetComponent<Image>().sprite = radial_selected;
                radialB[1].GetComponent<Image>().sprite = radial;
                radialB[2].GetComponent<Image>().sprite = radial;
                break;
            case 1:
                radialB[1].GetComponent<Image>().sprite = radial_selected;
                radialB[0].GetComponent<Image>().sprite = radial;
                radialB[2].GetComponent<Image>().sprite = radial;
                break;
            case 2:
                radialB[2].GetComponent<Image>().sprite = radial_selected;
                radialB[1].GetComponent<Image>().sprite = radial;
                radialB[0].GetComponent<Image>().sprite = radial;
                break;
        }
        diff = i;
    }

    public void createSave()
    {
        if(pName != "")
        {
            hideKeyboard();
            GameManager.instance.save[callingPlanet].difficulty = diff;
            GameManager.instance.save[callingPlanet].planetName = pName;
            GameManager.instance.save[callingPlanet].percent = 0;
            GameManager.instance.copySaveToMenuPrefs(callingPlanet);
            GameManager.instance.StartGame(callingPlanet);
        }
    }
}