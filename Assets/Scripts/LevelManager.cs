using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject canvas;
    public enum Language
    {
        en,
        pl,
        de
    };

    public static bool choosed =false;
    public static Language lang;
    public static String[] characterName = new string[]{"kevin","kevinsnow", "kevinminer", "lara", "ninja", "robot", "panda", "diana"};
    public static int choosedCharacter=0;
    public static bool[] characterUnlocked = new bool[characterName.Length];

    void Start()
    {
        canvas.SetActive(true);
    }

}
