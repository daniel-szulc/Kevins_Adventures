using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
    using System.Runtime;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour, IUnityAdsListener
{
    private DateTimeOffset? time;
    public GameObject infoBox;
    public GameObject menu;
    public GameObject noNetMessage;
    public static int GiftCollected=0;
    public static DateTime? timeOfLastCollect=null;
    private DateTime timenow;
    private DateTime waitingTime;
    public Sprite collectedGift;
    public TextMeshProUGUI[] daysText;
    public Image[] giftsImage;
    public GameObject[] shine;
    public TextMeshProUGUI dailygiftTxt;
    public TextMeshProUGUI collectTxt;
    public TextMeshProUGUI watchAdTxt;
    public TextMeshProUGUI counter;
    public TextMeshProUGUI[] noNetMessageTxt;
    private float totalsec;
    public GameObject[] collectBtn;
    public GameObject[] collectx2Btn;
    private bool timer=false;
    public GameObject mapManager;
    private bool collecting=false;
    private int multiplyreward=1;
    private string myPlacementId = "rewardedVideo";
    public GameObject loadingMenu;
    public TextMeshProUGUI checkingText;

    void Update()
    {
        if (timer)
        {
            if (totalsec <= 0)
            {
                Menu();
                timer = false;
            }
            totalsec -= Time.deltaTime*1;
            String hours = ((int) totalsec / 3600).ToString("00");
            String minutes = ((int) ((totalsec % 3600)/60)).ToString("00");
            String seconds = (totalsec%60).ToString("00");
            counter.text = hours + "<voffset=0.1em>:<voffset=0em>" + minutes + "<voffset=0.1em>:<voffset=0em>" + seconds;
        }
    }

    void Checktime()
    {
        loadingMenu.SetActive(true);
        StartCoroutine(waitForMessage());
    }

    private IEnumerator waitForMessage()
    {
        yield return new WaitForSeconds(0.5f);
        time = GetCurrentTime();
        
        if (time != null)
        {
            menu.SetActive(true);
            checkGifts();
        }
        else
        {
            noNetMessage.SetActive(true);
        }
        // loadingMenu.GetComponent<Animator>().Play("boxAnimOUT");
        loadingMenu.SetActive(false);
      //  LangCheck();
    }

    public void Menu()
    {
        LangCheck();
        Checktime();
        
    }

    public void Refresh()
    {
        noNetMessage.GetComponent<Animator>().Play("boxAnimOUT");
        noNetMessage.SetActive(false);
        Menu();
    }

 
    
    void checkGifts()
    {
        var offset = DateTimeOffset.Parse(time.ToString());
        timenow=offset.DateTime;
        if (GiftCollected > 0 && GiftCollected < 5)
        {
            for (int i = 0; i < GiftCollected; i++)
            {
                giftsImage[i].sprite = collectedGift;
            }
            DateTime lastCollect = (DateTime) timeOfLastCollect;
            
            
             if (timenow.Ticks - lastCollect.AddHours(24).Ticks < 0)
            {
                collectBtn[1].SetActive(true);
                collectBtn[0].SetActive(false);
                collectx2Btn[1].SetActive(true);
                collectx2Btn[0].SetActive(false);
                
               TimeSpan totalsecSpan = lastCollect.AddHours(24).Subtract(timenow);

               totalsec = Convert.ToInt32(totalsecSpan.TotalSeconds);
                timer = true;
            }
            else
            {
                collectBtn[0].SetActive(true);
                collectBtn[1].SetActive(false);
                collectx2Btn[0].SetActive(true);
                collectx2Btn[1].SetActive(false);

                if (GiftCollected == 4)
                {
                    collectx2Btn[0].SetActive(false);
                    collectx2Btn[1].SetActive(false);
                }
            }
        }
        else if (GiftCollected >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                giftsImage[i].sprite = collectedGift;
            }
            collectBtn[1].SetActive(true);
            collectBtn[0].SetActive(false);
            collectx2Btn[1].SetActive(false);
            collectx2Btn[0].SetActive(false);
            
            
         LangCheck();
        }
            else
        {
            collectBtn[0].SetActive(true);
            collectBtn[1].SetActive(false);
            collectx2Btn[0].SetActive(true);
            collectx2Btn[1].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            if (GiftCollected == i)
            {
                shine[i].SetActive(true);
            }
            else
            {
                shine[i].SetActive(false);
            }
        }
        collecting = false;
    }

    public void Collect()
    {
        Checktime();
        if (time != null && collecting==false)
        {
            collecting = true;
            multiplyreward = 1;
            var offset = DateTimeOffset.Parse(time.ToString());
            timeOfLastCollect=offset.DateTime;
            GiftCollected++;
            collectGift();
            checkGifts();
        }
        else
        {
                noNetMessage.GetComponent<Animator>().Play("boxAnimOUT");
                noNetMessage.SetActive(true);
        }
    }

    public void CollectWatchAd()
    {
        Checktime();
        if (time != null)
        {
          ShowRewardedAd();
        }
        else
        {
            noNetMessage.GetComponent<Animator>().Play("boxAnimOUT");
            noNetMessage.SetActive(true);
        }
     
    }

    void collectGift()
    {
        switch (GiftCollected)
        {
            case 1:
                _Level.diamonds += 1*multiplyreward;
                _Level.fullCoins += 100*multiplyreward;
                break;
            case 2:
                _Level.diamonds += 2*multiplyreward;
                _Level.fullCoins += 200*multiplyreward;
                break;
            case 3:
                _Level.diamonds += 3*multiplyreward;
                _Level.fullCoins += 300*multiplyreward;
                break;
            case 4:
                _Level.diamonds += 4*multiplyreward;
                _Level.fullCoins += 400*multiplyreward;
                break;
            case 5:
                LevelManager.characterUnlocked[4]= true;
                break;
        }
        multiplyreward = 1;
        DataSave.SaveData();
        mapManager.GetComponent<map_manager>().CheckFunds();
    }
    

    private void LangCheck()
    {
        String[] dailyGiftTrans = {"DAILY GIFT", "CODZIENNY PREZENT", "TÄGLICHES GESCHENK"};
        dailygiftTxt.text = dailyGiftTrans[(int) LevelManager.lang];
        
        String[] dayTrans = {"DAY ", "DZIEŃ ", "TAG "};
        for (int i = 0; i < 5; i++)
        {
            daysText[i].text = dayTrans[(int) LevelManager.lang] + (i + 1).ToString();
        }

        String[] collectTrans = {"COLLECT", "ODBIERZ", "SAMMELN"};
        collectTxt.text = collectTrans[(int) LevelManager.lang];

        String[,] noNetTrans =
        {
            {"NO INTERNET CONNECTION", "BRAK POŁĄCZENIA INTERNETOWEGO", "keine Internetverbindung"},
            {
                "Connect to the internet to receive your free gift.",
                "Połącz się z Internetem, aby otrzymać darmowy prezent.",
                "Stellen Sie eine Verbindung zum Internet her, um Ihr kostenloses Geschenk zu erhalten."
            }
        };
        noNetMessageTxt[0].text = noNetTrans[0,(int) LevelManager.lang];
        noNetMessageTxt[1].text = noNetTrans[1,(int) LevelManager.lang];

        String[] checkingNetTrans =
        {
            "checking internet connection", "sprawdzanie połączenia internetowego", "Überprüfen die Internetverbindung"
        };
        checkingText.text=checkingNetTrans[(int) LevelManager.lang];

        if (GiftCollected >= 5)
        {
            String[] allColectedTrans = {"ALL COLLECTED", "WSZYSTKIE ODEBRANO", "ALLES GESAMMELT"};
            counter.text = allColectedTrans[(int) LevelManager.lang];
        }
    }
    
    
    public static DateTimeOffset? GetCurrentTime()
    {
        using (var client = new HttpClient())
        {
            try
            {
                var result = client.GetAsync("https://google.com", 
                    HttpCompletionOption.ResponseHeadersRead).Result;
                return result.Headers.Date;
            }
            catch
            {
                return null;
            }
        }
    }

    
    public void ShowRewardedAd()
    {
        Advertisement.AddListener (this);

        Advertisement.Show(myPlacementId);
    }
    
    public void OnUnityAdsReady (string placementId) {

        }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == myPlacementId)
        {
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                var offset = DateTimeOffset.Parse(time.ToString());
                timeOfLastCollect = offset.DateTime;
                GiftCollected++;
                multiplyreward = 2;
                collectGift();
                checkGifts();
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
                    "Video cannot be displayed. \nTry again.", "Nie można wyświetlić wideo. \nSpróbuj ponownie",
                    "Video kann nicht angezeigt werden. \nVersuchen Sie erneut."
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
