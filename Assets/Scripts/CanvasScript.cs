using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance = null;
    public Text planetTextObject; //The text which shows planet name/progress
    public Text keyboardTextObject; //NAME: *******
    public GameObject Keyboard; //keyboard object reference
    public GameObject Menu; //menu object reference
    public GameObject[] menuObjectHolders; //keys_obj, audio_obj, video_obj;
    public Scrollbar[] menuScrollbars; //keysScrollbar;
    public Text[] audioPercentTexts; //master,game,sfx
    public Sprite[] sprites;
    //radial_sprite, radial_selected_sprite, 
    //keys_sprite, keys_selected_sprite, 
    //audio_sprite, audio_selected_sprite, 
    //video_sprite, video_selected_sprite;
    
    public Button[] radialButtonObjects; //difficulty select buttons
    public Button[] tabObjects; //setting tabs
    //public Button[] menuButtonObjects;

    [HideInInspector]
    public bool keyboard_up = false;
    [HideInInspector]
    public bool menu_up = false;
    [HideInInspector]
    public int callingPlanet;

    private int activeTab = 0;
    private int diff = 1;
    private string pName = "";
    private string valid = "1234567890QWERTYUIOPASDFGHJKL#ZXCVBNM-_.qwertyuiopasdfghjklzxcvbnm";

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
        planetTextObject.GetComponent<Text>().enabled = false;
        hideKeyboard();
        hideMenu();
    }

    //New Planet

    public void hideKeyboard()
    {
        Keyboard.gameObject.SetActive(false);
        keyboard_up = false;
    }

    public void showKeyboard(int i)
    {
        keyboard_up = true;
        Keyboard.gameObject.SetActive(true);
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
        if (keyboard_up)
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
            keyboardTextObject.text = "NAME:" + pName.ToUpper();
        }

        if (menu_up)
        {
            if (activeTab == 0)
            {

            }

            else if(activeTab == 1)
            {
                audioPercentTexts[0].text = (int)(menuScrollbars[1].value * 100) + "%";
                audioPercentTexts[1].text = (int)(menuScrollbars[2].value * 100) + "%";
                audioPercentTexts[2].text = (int)(menuScrollbars[3].value * 100) + "%";
            }
        }
    }

    public void setDifficulty(int i)
    {
        switch (i)
        {
            case 0:
                radialButtonObjects[0].GetComponent<Image>().sprite = sprites[1];
                radialButtonObjects[1].GetComponent<Image>().sprite = sprites[0];
                radialButtonObjects[2].GetComponent<Image>().sprite = sprites[0];
                break;
            case 1:
                radialButtonObjects[1].GetComponent<Image>().sprite = sprites[1];
                radialButtonObjects[0].GetComponent<Image>().sprite = sprites[0];
                radialButtonObjects[2].GetComponent<Image>().sprite = sprites[0];
                break;
            case 2:
                radialButtonObjects[2].GetComponent<Image>().sprite = sprites[1];
                radialButtonObjects[1].GetComponent<Image>().sprite = sprites[0];
                radialButtonObjects[0].GetComponent<Image>().sprite = sprites[0];
                break;
        }
        diff = i;
    }

    public void createSave()
    {
        if (pName != "")
        {
            hideKeyboard();
            GameManager.instance.save[callingPlanet].difficulty = diff;
            GameManager.instance.save[callingPlanet].planetName = pName;
            GameManager.instance.save[callingPlanet].percent = 0;
            GameManager.instance.copySaveToMenuPrefs(callingPlanet);
            GameManager.instance.StartGame(callingPlanet);
        }
    }

    //Settings

    public void hideMenu()
    {
        Menu.gameObject.SetActive(false);
        menu_up = false;
    }

    public void showMenu()
    {
        menu_up = true;
        Menu.gameObject.SetActive(true);
        switchMenuTab(0);
    }

    public void applySettings()
    {

    }

    public void switchMenuTab(int i)
    {
        switch (i)
        {
            case 0:
                tabObjects[0].GetComponent<Image>().sprite = sprites[3];
                tabObjects[1].GetComponent<Image>().sprite = sprites[4];
                tabObjects[2].GetComponent<Image>().sprite = sprites[6];
                menuObjectHolders[0].gameObject.SetActive(true);
                menuObjectHolders[1].gameObject.SetActive(false);
                menuObjectHolders[2].gameObject.SetActive(false);
                menuScrollbars[0].value = 0;
                break;
            case 1:
                tabObjects[0].GetComponent<Image>().sprite = sprites[2];
                tabObjects[1].GetComponent<Image>().sprite = sprites[5];
                tabObjects[2].GetComponent<Image>().sprite = sprites[6];
                menuObjectHolders[0].gameObject.SetActive(false);
                menuObjectHolders[1].gameObject.SetActive(true);
                menuObjectHolders[2].gameObject.SetActive(false);
                break;
            case 2:
                tabObjects[0].GetComponent<Image>().sprite = sprites[2];
                tabObjects[1].GetComponent<Image>().sprite = sprites[4];
                tabObjects[2].GetComponent<Image>().sprite = sprites[7];
                menuObjectHolders[0].gameObject.SetActive(false);
                menuObjectHolders[1].gameObject.SetActive(false);
                menuObjectHolders[2].gameObject.SetActive(true);
                break;
        }
        activeTab = i;
    }
}