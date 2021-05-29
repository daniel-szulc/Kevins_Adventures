using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;


public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    public string gameID = "3463087";
    public bool testMode = false;
    
    private void Start()
    {

        Advertisement.Initialize (gameID, testMode);
        Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);

        Debug.Log("Unity Ads initialized: " + Advertisement.isInitialized);
        Debug.Log("Unity Ads is supported: " + Advertisement.isSupported);
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {

            if (this != instance)
                Destroy(this.gameObject);
        }
        
    }
    
    
    
    public void AdVideo()
    {
        if(!_Level.noAds)
        StartCoroutine(Showvideo());
    }

    private IEnumerator Showvideo()
    {
        while (!Advertisement.IsReady("video"))
        {
            yield return null;
        }
        Advertisement.Show("video");
    }

    public void AdBanner()
    {
       if(!_Level.noAds)
        StartCoroutine (ShowBannerWhenReady ());
    }

    private IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady ("banner")) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.Show ("banner");
    }
    
    public void BannerHide()
    {
        if(Advertisement.Banner.isLoaded)
        Advertisement.Banner.Hide();   
    }

}
