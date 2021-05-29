using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class AdMob : MonoBehaviour
{
    public static AdMob instance;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    public RewardedAd rewardedAd;
     private String appID = "ca-app-pub-8784140287447461~7703931058";
     private String bannerID = "ca-app-pub-8784140287447461/4503052645";

  //   private String bannerID = "ca-app-pub-3940256099942544/6300978111";
     private String VideoAdID = "ca-app-pub-8784140287447461/2866730860";
    private String RewardedAdID = "ca-app-pub-8784140287447461/9967681052";

    public string RewardedAdId => RewardedAdID;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        
     MobileAds.Initialize(appID);
     //MobileAds.Initialize(initStatus => { });
     Debug.Log(SystemInfo.deviceUniqueIdentifier);
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

    public void ShowVideoAd()
    {
        if(!_Level.noAds)
       this.RequestVideoAd();
    }

    private void RequestVideoAd()
    {
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }
        this.interstitial = new InterstitialAd(VideoAdID);
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        
        this.interstitial.LoadAd(this.CreateAdRequest());

    }
    public void RequestBanner()
    {
        if (!_Level.noAds)
        {
            if (this.bannerView != null)
            {
                this.bannerView.Destroy();
            }

            this.bannerView = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Bottom);
            this.bannerView.LoadAd(this.CreateAdRequest());
        }
    }

    public void DestroyBanner()
    {
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }
    }

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    
    
    public AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
         //  .AddTestDevice("56A6460332BA9F16DFA5EEBA6C74F108")  //Phone
       //    .AddTestDevice("79D200BB38F836601DD44EB805C5157B")  //Nox
         .AddTestDevice("36af61c9cb727376685444280b1660ea5de5bfd4") //Unity
            .Build();
    }
}
