using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class CompleteMenu : MonoBehaviour
{
    public static CompleteMenu instance;
    public TextMeshProUGUI[] text = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] score = new TextMeshProUGUI[3];
    public GameObject[] stars = new GameObject[3];
    public GameObject menu;
    public GameObject canvas;
    NumberFormatInfo f = new NumberFormatInfo {NumberGroupSeparator = " "};
    private AsyncOperation async;
    public GameObject loading;
    private bool nextClick = false;
    public GameObject rateObject;
    void Start()
    {
        nextClick = false;
        if (instance == null)
            instance = this;
    }

    public void Menu(int level)
    {
        for(int i =0; i<3; i++)
            stars[i].SetActive(false);
    
        Time.timeScale = 0;
        menu.SetActive(true);
        if (level % 3 == 0 && !_Level.rated && level!=0)
        {
            rateObject.SetActive(true);
        }
        
        _Level.Lvl[level].Finished = true;
        _Level.Lvl[level+1].Unlocked = true;
        score[0].text = (ScoreManager.instance.Coins).ToString("#,0",f);
        score[1].text = (ScoreManager.instance.Score).ToString("#,0",f);
        score[2].text = (ScoreManager.instance.EnemyCount).ToString();
        if (_Level.Lvl[level].Coins < ScoreManager.instance.Coins)
            _Level.Lvl[level].Coins = ScoreManager.instance.Coins;
        if (_Level.Lvl[level].Score < ScoreManager.instance.Score)
            _Level.Lvl[level].Score = ScoreManager.instance.Score;
        if (_Level.Lvl[level].Enemies < ScoreManager.instance.EnemyCount)
            _Level.Lvl[level].Enemies = ScoreManager.instance.EnemyCount;
                
                
        _Level.fullCoins += ScoreManager.instance.Coins;
        canvas.SetActive(false);
                                      String[,] translation = new String[3, 4]
                                      {
                                          {"COMPLETE", "COINS EARNED", "ENEMIES KILLED", "SCORE"},
                                          {"UKOŃCZONO", "ZDOBYTE MONETY", "POKONANI WROGOWIE", "WYNIK" },
                                          {"KOMPLETT", "MÜNZEN VERDIENT", "FEINDE BESIEGT", "ERGEBNIS"}
                                      };

        for(int i = 0; i<text.Length; i++)
            text[i].text = translation[(int) LevelManager.lang, i];
        Debug.Log(ScoreManager.instance.Score + " / " + ScoreManager.instance.FullScore + " = " + (float)ScoreManager.instance.Score / ScoreManager.instance.FullScore);

                if ((float)ScoreManager.instance.Score / ScoreManager.instance.FullScore >= 0.30)
                {
                    stars[0].SetActive(true);
                    if(_Level.Lvl[level].Stars<1)
                    _Level.Lvl[level].Stars = 1;
                }
                if ((float)ScoreManager.instance.Score / ScoreManager.instance.FullScore >= 0.50)
                {
                    stars[1].SetActive(true);
                    if(_Level.Lvl[level].Stars<2)
                    _Level.Lvl[level].Stars = 2;
                }
                if ((float)ScoreManager.instance.Score / ScoreManager.instance.FullScore >= 0.85)
                {
                    stars[2].SetActive(true);
                    _Level.Lvl[level].Stars = 3;
                }
                    
           DataSave.SaveData();
        
    }
    
    public void Restart()
    {
        AdManager.instance.BannerHide();

        PlayerController.instance.finishLevel = false;
        Time.timeScale = 1;
        loading.SetActive(true);
        async =SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        async.allowSceneActivation = false;
        StartCoroutine(waitforLvl());
    }

    public void Exit()
    {

        PlayerController.instance.finishLevel = false;
        
        AdManager.instance.BannerHide();

        Time.timeScale = 1f;
        SceneManager.LoadScene("map");
    }

    public void Next()
    {
    
if(!nextClick)
{
    nextClick = true;
    PlayerController.instance.finishLevel = false;
      
        Time.timeScale = 1;
     
        _Level.actualLevel++;
        loading.SetActive(true);
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        async.allowSceneActivation = false;
        StartCoroutine(waitforLvl());
        AdManager.instance.BannerHide();
    }}
    
    public IEnumerator waitforLvl()
    {
        yield return new WaitForSeconds(1);
        async.allowSceneActivation = true;
    }
    
}
