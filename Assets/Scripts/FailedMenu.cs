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
     
        //canvas.SetActive(false);
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
        Advertisement.AddListener (this);
        ShowRewardedAd();
        reviveMenu.SetActive(false);
    }

public void payRevive()
{
    if (_Level.diamonds >= 2)
    {
        _Level.diamonds -= 2;
        reviveMenu.SetActive(false);
        Reward();
    }
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
        failedMenu.SetActive(false);
        PlayerController.instance.Checkpoint();
        CameraController.instance.PlayerAlive();
        Debug.Log("Alive");
        PlayerController.instance.playerAnimation.SetBool("isDying", false);
        PlayerController.instance.playerAnimation.Play("_idle");
        PlayerController.instance.rigidBody.constraints = RigidbodyConstraints2D.None;
        PlayerController.instance.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        PlayerController.instance.rigidBody.gravityScale = 5;
        PlayerController.instance.collider2d.enabled = true;
        HeartScript.instance.Alive();
        pauseMenu.SetActive(true);
        canvas.SetActive(true);
        pauseMenu.GetComponent<PauseMenu>().Awake();
    }
    
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == myPlacementId)
        {
            if (showResult == ShowResult.Finished)
                         {
                // Reward the user for watching the ad to completion.
       Reward();
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                infoBox.SetActive(true);
                String[] message = new string[]
                    {"video has been skipped", "Wideo zostało pominięte", "Video wurde übersprungen"};
                infoBox.GetComponentInChildren<TextMeshProUGUI>().text = message[(int) LevelManager.lang];
                infoBox.GetComponent<Animator>().Play("paymentinfo");
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
                infoBox.SetActive(true);
                String[] _message = new string[]
                {
                    "Video cannot be displayed. Try again.", "Nie można wyświetlić wideo. spróbuj ponownie",
                    "Video kann nicht angezeigt werden. versuchen Sie erneut."
                };
                infoBox.GetComponentInChildren<TextMeshProUGUI>().text = _message[(int) LevelManager.lang];
                infoBox.GetComponent<Animator>().Play("paymentinfo");
            }
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }
    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    }


}
