using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    [HideInInspector]
    public BoardManager boardScript;
    public int level = 0;
    public static GameManager instance = null;

    void Awake() {
        if (instance == null) {
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        boardScript = GetComponent<BoardManager>();
        boardScript.SetupScene(instance.level);
    }

    public void NextGen() {
        instance.boardScript.planetInstance.Next();
    }

    public void PrevGen() {
        instance.boardScript.planetInstance.Prev();
    }

    public void UpdateSidebar() {
        instance.boardScript.UpdateSidebar();
    }

    public void ClickControls() {
        instance.boardScript.ShowControls();
        instance.boardScript.HideAudio();
        instance.boardScript.HideVideo();
    }

    public void ClickAudio() {
        instance.boardScript.HideControls();
        instance.boardScript.ShowAudio();
        instance.boardScript.HideVideo();
    }

    public void ClickVideo() {
        instance.boardScript.HideControls();
        instance.boardScript.HideAudio();
        instance.boardScript.ShowVideo();
    }

    public void ClickPlay() {
        instance.boardScript.DestroyBoard();
        instance.boardScript.SetupScene(1);
    }

    public void quit() {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    void OnApplicationQuit() {
    }
}
