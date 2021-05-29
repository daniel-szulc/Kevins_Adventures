using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_play_menu : MonoBehaviour
{
    public TextMeshProUGUI[] text = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] score = new TextMeshProUGUI[3];
    public GameObject[] stars = new GameObject[3];
    public static level_play_menu instance;
    public GameObject menu;
    private int level;
    public GameObject loading;
    private AsyncOperation async;
    void Start()
    {
        instance = this;
       
    }

    public void Menu(int lvl)
    {
        level = lvl;
        score[0].text = _Level.Lvl[lvl].Coins.ToString();
        score[1].text = _Level.Lvl[lvl].Score.ToString();
        score[2].text = _Level.Lvl[lvl].Enemies.ToString();

 for (int i = 0; i < 3; i++)
        {
				   stars[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
			if(_Level.Lvl[level].Stars > i)
				   stars[i].SetActive(true);

        }

        String[,] translation = new String[3, 4]
        {
            {"LEVEL 01-"+(level+1).ToString("00"), "COINS EARNED", "ENEMIES KILLED", "BEST SCORE"},
            {"POZIOM 01-"+(level+1).ToString("00"),"ZDOBYTE MONETY", "POKONANI WROGOWIE", "NAJLEPSZY WYNIK"},
            {"LEVEL 01-"+(level+1).ToString("00"), "MÜNZEN VERDIENT", "FEINDE BESIEGT", "BESTES ERGEBNIS"}
        };

        for(int i = 0; i<text.Length; i++)
            text[i].text = translation[(int) LevelManager.lang, i];
    }


    public void Play()
    {
        _Level.actualLevel = level;
        loading.SetActive(true);
        String levelname = "W01L" + (level + 1).ToString("00");
        async = SceneManager.LoadSceneAsync(levelname);
         async.allowSceneActivation = false;
         StartCoroutine(waitforLvl());
    }

    public IEnumerator waitforLvl()
    {
        yield return new WaitForSeconds(1);
        async.allowSceneActivation = true;
    }

}
