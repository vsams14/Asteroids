using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class MenuPrefs
{
    public int color = 0;
    public bool[] save = { false, false, false, false, false };
    public string[] planetName = { "Blue", "Brown", "Red", "Green", "Purple" };
    public string[] diff = { "Easy", "Normal", "Difficult" };
    public int[] planetDiff = { 0, 0, 0, 0, 0 };
    public int[] percent = { 0, 0, 0, 0, 0 };
}