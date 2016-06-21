using UnityEngine;
using System.Collections;

public class CameraSingleton : MonoBehaviour
{

    public static CameraSingleton instance = null;

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
    }
}