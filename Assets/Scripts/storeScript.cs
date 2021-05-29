using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class storeScript : MonoBehaviour, IUnityAdsListener
{
    public GameObject infoBox; 
    public GameObject mapManager;
    public GameObject[] category;
    [Space(10)] [Header("Premium")] 
    public GameObject buttonAds;
    public GameObject buttonNoAds;
    public TextMeshProUGUI storeTxt;
    public TextMeshProUGUI[] item = new TextMeshProUGUI[9];
    public TextMeshProUGUI viewtext;
    public GameObject plusNoAdstxt;

    [Space(20)] 
    [Header("Boosts")]
    public TextMeshProUGUI[] boostsItem; 
    public TextMeshProUGUI[] boostsItemDescription;
    public TextMeshProUGUI[] inBag;
    [Space(20)] 
    [Header("Upgrades")]
    public TextMeshProUGUI[] upgradesItem;
    public TextMeshProUGUI[] upgradesItemDescription;
    public TextMeshProUGUI[] upgradeLevel;
    public GameObject[] upgradesPurchased;
    public GameObject[] upgradesButtons;
    private string myPlacementId = "rewardedVideo";
    private bool allowReward=false;
    public TextMeshProUGUI[] priceText;
    
    public void Start()
    {
        if (_Level.noAds)
        {
            buttonAds.SetActive(false);
            buttonNoAds.SetActive(true);
        }
        category[0].SetActive(true);
        category[1].SetActive(false);
        category[2].SetActive(false);
        langCheck();
    }

    public void Awake()
    {
        
    }


    public void BannerHide()
    {
        AdManager.instance.BannerHide();
    }
    
    public void langCheck()
    {
        StartCoroutine(LoadPriceRoutine());
        
        String[] textnoAds =
        {
            "<voffset=0.1em>+   <voffset=0.4em><size=130%> <sprite=\"noADS\" index=1></voffset><space=-0.5em><size=100%> no ads",
            "<voffset=0.1em>+   <voffset=0.4em><size=130%> <sprite=\"noADS\" index=1></voffset><space=-0.5em><size=100%> brak reklam",
            "<voffset=0.1em>+   <voffset=0.4em><size=130%> <sprite=\"noADS\" index=1></voffset><space=-0.5em><size=100%> Anzeigen ausblenden"
        };
            
        foreach (TextMeshProUGUI textads in plusNoAdstxt.GetComponentsInChildren<TextMeshProUGUI>())
        {
            textads.text = textnoAds[(int)LevelManager.lang];
        }


        IEnumerator LoadPriceRoutine()
        {
            while (!IAPManager.Instance.IsInitialized())
            {
                yield return null;
            }
            
            

            priceText[0].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.no_ads);
            priceText[1].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.pack_starter);
            priceText[2].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.pack_starter_extra);
            priceText[3].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.pack_standard);
            priceText[4].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.pack_standard_extra);
            priceText[5].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.pack_best);
            priceText[6].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.pack_premium);
            priceText[7].text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.pack_immortality);
        }
        
        
        String[] storeTrans = {"STORE", "SKLEP", "SHOP"};

        storeTxt.text = storeTrans[(int) LevelManager.lang];
        
        //PREMIUM/////////////////////////////
        //////////////////////////////////////
       
        String[,] itemTrans =
        {
            {
                "NO ADS", "VIEW AD VIDEO", "STARTER PACK", "STARTER PACK EXTRA", "STANDARD PACK", "STANDARD PACK EXTRA",
                "BEST PACK", "PRESTIGIOUS PACK", "Immortality Pack"
            },
            {
                "BRAK REKLAM", "OBEJRZYJ REKLAMĘ WIDEO", "PAKIET STARTOWY", "EXTRA PAKIET STARTOWY", "PAKIET STANDARD",
                "EXTRA PAKIET STANDARD", "NAJLEPSZY PAKIET", "PRESTIŻOWY PAKIET", "PAKIET NIEŚMIERTELNOŚCI"
            },
            {
                "Anzeigen ausblenden", "WERBUNG VIDEO ANSEHEN", "ANFÄNGER PACK", "ANFÄNGER PACK EXTRA", "STANDARD PACK",
                "STANDARD PACK EXTRA", "BESTE PACK", "prestigeträchtige PACK", "Unsterblichkeit PACK"
            }
        };
        for (int i = 0; i < item.Length; i++)
        {
            item[i].text = itemTrans[(int) LevelManager.lang, i];
        }

        String[] view = {"view AD", "OBEJRZYJ REKLAMĘ", "AD ANSEHEN"};
        viewtext.text = view[(int) LevelManager.lang];

        String[] purchased ={"purchased", "ZAKUPIONO", "gekauft"};
        if (_Level.noAds)
        {
            buttonNoAds.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = purchased[(int) LevelManager.lang];
        }

        //BOOSTS/////////////////////////////
        //////////////////////////////////////

        String[,] boostsItemTrans =
        {
            {"HEARTS POTION", "SLIME PACK", "SHIELD"},
            {"ELIKSIR SERC", "PAKIET SLIME", "TARCZA"},
            {"HERZTRANK", "SLIME PACK", "SCHILD"}
        };
        for (int i = 0; i < boostsItem.Length; i++)
        {
           boostsItem[i].text = boostsItemTrans[(int) LevelManager.lang, i];
        }

        String[,] boostsItemDescriptionTrans =
        {
            {"restores full health", "adds 5 slimes", "protects you for 10 seconds"},
            {"przywraca pełne zdrowie", "dodaje 5 slime", "chroni cię przez 10 sekund"},
            {"stellt die volle Gesundheit wieder her", "fügt 5 Slime", "schützt dich für 10 Sekunden"}
        };
        for (int i = 0; i < boostsItemDescription.Length; i++)
        {
            boostsItemDescription[i].text = boostsItemDescriptionTrans[(int) LevelManager.lang, i];
        }
        
checkBag();


        //UPGRADES/////////////////////////////
        //////////////////////////////////////
        
        String[,] upgradesItemTrans =
        {
            {"EXTRA CHANCE", "TRIPLE JUMP", "FASTER SHOOTING", "MORE SLIME"},
            {"DODATKOWA SZANSA", "POTRÓJNY SKOK", "SZYBSZE STRZAŁY", "WIĘCEJ SLIME"},
            {"zusätzliche Chance", "DREIFACHSPRUNG","schneller SCHIESSEN", "MEHR SLIME"}
        };
        for (int i = 0; i < upgradesItem.Length; i++)
        {
            upgradesItem[i].text = upgradesItemTrans[(int) LevelManager.lang, i];
        }

        String[,] upgradesItemDescriptionTrans =
        {
            {"adds a fourth heart", "allows 3 jump", "speeds up slime shots", "adds 2 more slime at start"},
            {"dodaje czwarte serce", "umożliwa 3 skoki", "zwiększa prędkość slime", "dodaje 2 slime więcej na starcie"},
            {"fügt ein viertes Herz hinzu.", "erlaubt 3 Sprünge", "erhöht die Geschwindigkeit von Slime", "fügt zu Beginn 2 weitere Slime hinzu"}
        };
        for (int i = 0; i < upgradesItemDescription.Length; i++)
        {
            upgradesItemDescription[i].text = upgradesItemDescriptionTrans[(int) LevelManager.lang, i];
        }
        
        
       foreach (var VARIABLE in upgradesPurchased)
       {
           VARIABLE.GetComponentInChildren<TextMeshProUGUI>(includeInactive: true).text=purchased[(int) LevelManager.lang];
       }

       checkUpgrade();
        
    }

   void checkBag()
   {
       String[] bagTrans =
           {"in bag: ", "w torbie: ", "in der Tasche: "};
       inBag[0].text = bagTrans[(int) LevelManager.lang] + _Level.heartsPotion.ToString();
       inBag[1].text = bagTrans[(int) LevelManager.lang] + _Level.slimePack.ToString();
       inBag[2].text = bagTrans[(int) LevelManager.lang] + _Level.shield.ToString();
   }

   void checkUpgrade()
   {
       String[] LevelTrans =
           {"upgrade level: ", "ulepszono: ", "Upgrade Ebene: "};
       upgradeLevel[0].text = LevelTrans[(int) LevelManager.lang] + _Level.fastershooting.ToString() + " / 3";
       upgradeLevel[1].text = LevelTrans[(int) LevelManager.lang] + ((_Level.slimesOnStart - 3) / 2).ToString() + " / 3";

       if (_Level.fourthHeart)
       {
           upgradesButtons[0].SetActive(false);
           upgradesPurchased[0].SetActive(true);
       }

       if (_Level.tripleJump)
       {
           upgradesButtons[1].SetActive(false);
           upgradesPurchased[1].SetActive(true);
       }

       if (_Level.fastershooting >= 3)
       {
           upgradesButtons[2].SetActive(false);
           upgradesPurchased[2].SetActive(true);
       }

       if (_Level.slimesOnStart >= 9)
       {
           upgradesButtons[3].SetActive(false);
           upgradesPurchased[3].SetActive(true);
       }
       
       
   }
   
   public void paymentBoosts(int type)  //currency: 0-diamond, 1-coin
   {
       bool currency=false;
       int value = 0;
       int product=3;
       switch (type)
       {
           case 0:
               product = 0;
               value = 1;
               currency = false;
               break;
           case 1:
               product = 0;
               value = 200;
               currency = true;
               break;
           case 2:
               product = 1;
               value = 1;
               currency = false;
               break;
           case 3:
               product = 1;
               value =150;
               currency = true;
               break;
           case 4:
               product = 2;
               value = 1;
               currency = false;
               break;
           case 5:
               product = 2;
               value =100;
               currency = true;
               break;
       }
       
       
       bool accept=false;
       if (currency)
       {
           if (_Level.fullCoins >= value)
           {
               accept = true;
               _Level.fullCoins -= value;
           }
       }
       else
       {
           if (_Level.diamonds >= value)
           {
               accept = true;
               _Level.diamonds -= value;
           }
       }
       if (accept)
       {
           switch (product)
           {
               case 0:
                   _Level.heartsPotion++;
                   break;
               case 1:
                   _Level.slimePack++;
                   break;
               case 2:
                   _Level.shield++;
                   break;
           }
           accept = false;
           checkBag();
           mapManager.GetComponent<map_manager>().CheckFunds();
           DataSave.SaveData();
       }
       else
       {
           GameObject.FindObjectOfType<character_change>().NoFunds();
           category[0].SetActive(true);
           category[1].SetActive(false);
           category[2].SetActive(false);
       }
   }


   public void paymentUpgrades(int item)
   {
       int value=0;
       bool accept=false;
       switch (item)
       {
           case 0:
               value = 20;
               if (!_Level.fourthHeart)
                   accept = true;
               break;
           case 1:
               value = 20;
               if (!_Level.tripleJump)
                   accept = true;
               break;
           case 2:
               value = 5;
               if (_Level.fastershooting<3)
                   accept = true;
               break;
           case 3:
               value = 5;
               if (_Level.slimesOnStart<9)
                   accept = true;
               break;
       }
       
       
       if (_Level.diamonds >= value && accept)
       {
           accept = true;
           _Level.diamonds -= value;
       }
       else
       {
           accept = false;
       }
       
       if (accept)
       {
           switch (item)
           {
               case 0:
                   _Level.fourthHeart = true;
                   break;
               case 1:
                   _Level.tripleJump = true;
                   break;
               case 2:
                   _Level.fastershooting++;
                   break;
               case 3:
                   _Level.slimesOnStart += 2;
                   break;
           }
           accept = false;
           checkUpgrade();
           mapManager.GetComponent<map_manager>().CheckFunds();
           DataSave.SaveData();
       }
       else
       {
           GameObject.FindObjectOfType<character_change>().NoFunds();
           category[0].SetActive(true);
           category[1].SetActive(false);
           category[2].SetActive(false);
       }
       
   }
   
   
    public void payment(int item)
    {
        switch (item)
        {
            case 0:
                
                IAPManager.Instance.BuyNoAds();
                break;
            case 1:
                allowReward = true;
                ShowRewardedAd();
                break;
            case 2:
             IAPManager.Instance.BuyStarter();
                break;
            case 3:
                IAPManager.Instance.BuyStarterExtra();
                break;
            case 4:
                IAPManager.Instance.BuyStandard();
                break;
            case 5:
                IAPManager.Instance.BuyStandardExtra();
                break;
            case 6:
                IAPManager.Instance.BuyBest();
                break;
            case 7:
                IAPManager.Instance.BuyPremium();
                break;
            case 8:
                IAPManager.Instance.BuyImmortality();
                break;

        }
 
    }
    
    public void noAdsFunction()
    {
        _Level.noAds = true;
        buttonAds.SetActive(false);
        buttonNoAds.SetActive(true);
        BannerHide();
    }


    public void MessagePaymentSuccessful()
    {
        DataSave.SaveData();
        infoBox.SetActive(true);
        String[] message = new string[]
            {"payment successful", "płatność zakończona powodzeniem", "Bezahlung erfolgreich"};
        infoBox.GetComponentInChildren<TextMeshProUGUI>().text = message[(int)LevelManager.lang];
        infoBox.GetComponent<Animator>().Play("paymentinfo");
        mapManager.GetComponent<map_manager>().CheckFunds();
    }

    public void MessagePaymentFailed()
    {
        infoBox.SetActive(true);
        String[] message = new string[]
            {"payment failed", "płatność zakończona niepowodzeniem", "Bezahlung fehlgeschlagen"};
        infoBox.GetComponentInChildren<TextMeshProUGUI>().text = message[(int)LevelManager.lang];
        infoBox.GetComponent<Animator>().Play("paymentinfo");
    }



    public void ShowRewardedAd()
    {
        Advertisement.AddListener (this);
        Advertisement.Show (myPlacementId);
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
                if (allowReward)
                {
                    _Level.diamonds += 1;
                    _Level.fullCoins += 50;
                    DataSave.SaveData();
                    mapManager.GetComponent<map_manager>().CheckFunds();
                    allowReward = false;
                }
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


    public void X()
    {
        DataSave.SaveData();
        gameObject.GetComponentInChildren<Animator>().Play("boxAnimOUT");
        AdManager.instance.BannerHide();
        mapManager.GetComponent<map_manager>().CheckFunds();
    }
    

}
