﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance = null;
    public Text planetText; //The text which shows planet name/progress
    public Text textField; //NAME: *******
    public GameObject Keyboard; //keyboard object reference
    public GameObject Menu; //menu object reference
    public Sprite radial, radial_selected, keys, keys_selected, audios, audios_selected, video, video_selected;
    public Button[] radialB; //difficulty select buttons
    public Button[] tabs; //setting tabs
    public bool keyboard_up = false;
    public bool menu_up = false;   
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
        planetText.GetComponent<Text>().enabled = false;
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
            textField.text = "NAME:" + pName.ToUpper();
        }

        if (menu_up && activeTab == 0)
        {

        }
    }

    public void setDifficulty(int i)
    {
        switch (i)
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
    }

    public void switchMenuTab(int i)
    {
        switch (i)
        {
            case 0:
                tabs[0].GetComponent<Image>().sprite = keys_selected;
                tabs[1].GetComponent<Image>().sprite = audios;
                tabs[2].GetComponent<Image>().sprite = video;
                break;
            case 1:
                tabs[0].GetComponent<Image>().sprite = keys;
                tabs[1].GetComponent<Image>().sprite = audios_selected;
                tabs[2].GetComponent<Image>().sprite = video;
                break;
            case 2:
                tabs[0].GetComponent<Image>().sprite = keys;
                tabs[1].GetComponent<Image>().sprite = audios;
                tabs[2].GetComponent<Image>().sprite = video_selected;
                break;
        }
        activeTab = i;
    }
}