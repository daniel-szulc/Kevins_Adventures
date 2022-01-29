using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public TextMeshProUGUI text;
    void Start()
    {
        Time.timeScale = 0;
        String[] translation = new String[3]
        {
            "LEVEL 01-"+(_Level.actualLevel+1).ToString("00"),
            "POZIOM 01-"+(_Level.actualLevel+1).ToString("00"),
            "LEVEL 01-"+(_Level.actualLevel+1).ToString("00")
        };
        text.text = translation[(int) LevelManager.lang];
    }
    

    public void StartGame()
    {
        Time.timeScale = 1;
    }
   
}
