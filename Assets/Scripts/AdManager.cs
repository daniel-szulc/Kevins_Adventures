using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;


public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    public string gameID = "removed due security reasons";
    
    
    private void Start()
    {
        //Script has been removed due security reasons.
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
        yield return null;
        //Script has been removed due security reasons.
    }

    public void AdBanner()
    {
       if(!_Level.noAds)
        StartCoroutine (ShowBannerWhenReady ());
    }

    private IEnumerator ShowBannerWhenReady()
    {
        yield return null;
        //Script has been removed due security reasons.
    }
    
    public void BannerHide()
    {
        //Script has been removed due security reasons.
    }

}
