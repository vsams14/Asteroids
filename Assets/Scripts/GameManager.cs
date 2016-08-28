using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public BoardManager boardScript;
    public int level = 0;
    public List<int> prev = new List<int>();
    public static GameManager instance = null;
    public MenuPrefs data = new MenuPrefs();
    public SaveFile[] save = new SaveFile[5];
    public int saveBeingPlayed = -1;

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
        boardScript = GetComponent<BoardManager>();
        boardScript.SetupScene(instance.level);
        loadData();
    }

    void OnApplicationQuit()
    {
        saveData();
    }

    public void saveData()
    {
        if (!Directory.Exists("Saves"))
        {
            Directory.CreateDirectory("Saves");
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/prefs.bin");
        formatter.Serialize(saveFile, data);
        saveFile.Close();
        for(int i = 0; i<5; i++)
        {
            if (data.save[i])
            {
                saveFile = File.Create("Saves/save_" + i + ".bin");
                formatter.Serialize(saveFile, save[i]);
                saveFile.Close();
            }            
        }
    }

    public void loadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //load menu prefs
        if (File.Exists("Saves/prefs.bin"))
        {
            FileStream saveFile = File.Open("Saves/prefs.bin", FileMode.Open);
            data = (MenuPrefs)formatter.Deserialize(saveFile);
            saveFile.Close();
        }
        else
        {
            data = new MenuPrefs();
            saveData();
        }

        for (int i=0; i<5; i++)
        {
            //load save_i
            if (File.Exists("Saves/save_"+i+".bin"))
            {
                FileStream saveFile = File.Open("Saves/save_"+i+".bin", FileMode.Open);
                save[i] = (SaveFile)formatter.Deserialize(saveFile);
                saveFile.Close();
                data.save[i] = true;
                data.planetName[i] = save[i].planetName;
                data.planetDiff[i] = save[i].difficulty;
                data.percent[i] = save[i].percent;
            }
            else
            {
                data.save[i] = false;
            }
        }        
    }

    public void copySaveToMenuPrefs(int saveNum)
    {
        if(save[saveNum].planetName != "")
        {
            data.save[saveNum] = true;
            data.planetName[saveNum] = save[saveNum].planetName;
            data.planetDiff[saveNum] = save[saveNum].difficulty;
            data.percent[saveNum] = save[saveNum].percent;
        }        
    }

    public void StartGame(int save)
    {
        saveData();
        prev.Add(level);
        level = 3;
        saveBeingPlayed = save;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
