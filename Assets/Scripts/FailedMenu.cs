using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class FailedMenu : MonoBehaviour, IUnityAdsListener
{

    public static FailedMenu instance;
    public GameObject infoBox;
    public GameObject failedMenu;
    public GameObject canvas;
    public GameObject pauseMenu;
    public TextMeshProUGUI[] text = new TextMeshProUGUI[7];
    public TextMeshProUGUI[] score = new TextMeshProUGUI[3];
    NumberFormatInfo f = new NumberFormatInfo {NumberGroupSeparator = " "};
    private string myPlacementId = "rewardedVideo";
    public GameObject reviveMenu;
    public GameObject payReviveButtonOn;
    public GameObject payReviveButtonOff;
    public TextMeshProUGUI[] revive;
    
    
    void Start()
    {
        instance = this;
        Time.timeScale = 0f;
    }

    public void Awake()
    {
       
        score[0].text = ScoreManager.instance.Coins.ToString("#,0",f);
        score[1].text = ScoreManager.instance.Score.ToString("#,0",f);
        score[2].text = ScoreManager.instance.EnemyCount.ToString();

        String[,] translation = new String[3, 7]
        {
            {"FAILED","COINS", "SCORE", "ENEMY", "RESTART", "REVIVE", "EXIT"},
            {"PRZEGRAŁEŚ","MONETY", "WYNIK", "WROGOWIE", "POWTÓRZ", "ODŻYJ", "WYJDŹ" },
            {"VERLOREN","MÜNZEN","ERGEBNIS", "FEINDE", "NEU STARTEN", "WIEDERBELEBEN", "BEENDEN"}
        };

        for(int i = 0; i<text.Length; i++)
            text[i].text = translation[(int) LevelManager.lang, i];

        String[,] reviveTrans = new String[,]
        {
            {"REVIVE", "PAY", "WATCH \nAD VIDEO"},
            {"ODŻYJ", "ZAPŁAĆ", "OBEJRZYJ REKLAMĘ WIDEO"},
            {"WIEDERBELEBEN", "Bezahlung", "WERBUNG VIDEO ANSEHEN"}
        };

        for (int i = 0; i < revive.Length; i++)
        {
            revive[i].text = reviveTrans[(int) LevelManager.lang, i];
        }
        AdManager.instance.AdVideo();
        AdManager.instance.AdBanner();
        
    }
    
    
    public void Restart()
    {
        Time.timeScale = 1;
        AdManager.instance.BannerHide();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {

        AdManager.instance.BannerHide();

        Time.timeScale = 1f;
        SceneManager.LoadScene("map");
        
    }
    
    
    public void Revive()
    {
        reviveMenu.SetActive(true);
        if (_Level.diamonds < 2)
        {
            payReviveButtonOff.SetActive(true);
            payReviveButtonOn.SetActive(false);
        }
        else
        {
            payReviveButtonOff.SetActive(false);
            payReviveButtonOn.SetActive(true);
        }
    }
    
public void videoRevive()
    {
        //Script has been removed due security reasons.
    }

public void payRevive()
{
    //Script has been removed due security reasons.
}



    public void StopTime()
    {
        Time.timeScale = 0;
    } 
    
    
    public void ShowRewardedAd()
    {
     
        Advertisement.Show(myPlacementId);
    }
    
    public void OnUnityAdsReady (string placementId) {

        }

    private void Reward()
    {
        //Script has been removed due security reasons.
    }
    
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //Script has been removed due security reasons.
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }
    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    }


}
