using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.UI;

public class character_change : MonoBehaviour
{
   private Character[] _character = new Character[8];
   public TextMeshProUGUI charText;
   public TextMeshProUGUI[] names = new TextMeshProUGUI[8];
  // public TextMeshProUGUI[] buttonTxt = new TextMeshProUGUI[7];
  public TextMeshProUGUI dailyGiftBtn;
  public GameObject[] message = new GameObject[3];
   private String[,] txtAbled = new String[,] {{"DISABLED", "ENABLED"}, {"DOSTĘPNY", "WYBRANO"},{"VERFÜGBAR", "AUSGEWÄHLT"} };
   GameObject textObject;
   public GameObject textFinishLvlPrefab;
   public GameObject textParent;
   public Sprite choosed, notchoosed;
   public GameObject[] buttonBuyed = new GameObject[8];
   public GameObject[] buttontoBuy = new GameObject[8];
   //  public Image[] buttons = new Image[7];
   private String[] messageNofunds= new String[]{"Not sufficient funds!", "Brak wystarczających funduszy!", "Nicht genügend Guthaben"};
   
   public GameObject store, characterstore;
   public GameObject mapManager;


   void Start()
     {
         _character[0] = new Character(0, 0, new String[]{"KEVIN","KEVIN","KEVIN"});
       _character[0].unlocked = true;
       _character[1] = new Character(5, 2, new String[]{"KEVIN IN A SNOW CAP", "KEVIN W ZIMOWEJ CZAPCE", "KEVIN IN EINER SCHNEEKAPPE"});
       _character[2] = new Character(10, 2, new String[]{"KEVIN MINER", "GÓRNIK KEVIN", "BERGMANN KEVIN"});
       _character[3] = new Character(15, 2, new String[]{"LARA", "LARA", "LARA"});
       _character[4] = new Character(5000, 1, new String[]{"NINJA","NINJA","NINJA"});
       _character[5] = new Character(8000, 1, new String[]{"ROBOT","ROBOT","ROBOT"});
       _character[6] = new Character(30, 2, new String[]{"PANDA","PANDA","PANDA"});
       _character[7] = new Character(6000, 1, new String[]{"SUPER DIANA","SUPER DIANA","SUPER DIANA"});
       CheckCharacters();
       LangCheck();
     }
   public void BannerAd()
   {
       AdManager.instance.AdBanner();
   }

     public void X()
     {
         DataSave.SaveData();
         gameObject.GetComponentInChildren<Animator>().Play("boxAnimOUT");
         AdManager.instance.BannerHide();
     }
     
     public void LangCheck()
     {
         for (int i = 0; i < 3; i++)
         {
             if (message[i]!=null)
             {
                 String[] messageTransl = new String[]
                 {
                     "End the world " + (i + 1) + " to unlock for free!",
                     "Ukończ świat " + (i + 1) + " aby odblokować za darmo!",
                     "Beende die Welt " + (i + 1) + ", um sie kostenlos freizuschalten!"
                 };
                 message[i].GetComponentInChildren<TextMeshProUGUI>().text = messageTransl[(int) LevelManager.lang];
             }
             
         }

         String[] translation = new string[]{"CHARACTERS", "POSTACIE", "CHARAKTERE"};
         charText.text = translation[(int) LevelManager.lang];
         for (int i = 0; i < _character.Length; i++)
         {
             names[i].text = _character[i].Name[(int) LevelManager.lang];
             if (_character[i].unlocked==true)
             {
                 buttonBuyed[i].GetComponentInChildren<TextMeshProUGUI>().text=txtAbled[(int)LevelManager.lang, 0];
             }
         }
         buttonBuyed[LevelManager.choosedCharacter].GetComponentInChildren<TextMeshProUGUI>().text = txtAbled[(int)LevelManager.lang, 1];
         
         String[] dailyTrans = new string[]{"DAILY GIFT", "CODZIENNY PREZENT", "TÄGLICHES GESCHENK"};
         dailyGiftBtn.text= dailyTrans[(int)LevelManager.lang];
     }

     public void CheckCharacters()
     {
         if (_character[1].unlocked == true)
         {
             message[0].SetActive(false);
         }
         /*for (int i = 1; i < 4; i++)
         {
             if (_character[i].unlocked==true)
             {
                 message[i-1].SetActive(false);
             }
         }*/
         for (int i = 0; i < _character.Length; i++)
         {
             if (LevelManager.characterUnlocked[i]==true)
             {
                 _character[i].unlocked = true;
                 buttontoBuy[i].SetActive(false);
                 buttonBuyed[i].SetActive(true);
                 buttonBuyed[i].GetComponentInChildren<TextMeshProUGUI>().text=txtAbled[(int)LevelManager.lang, 0];
             }
         }
         buttonBuyed[LevelManager.choosedCharacter].GetComponentInChildren<TextMeshProUGUI>().text = txtAbled[(int)LevelManager.lang, 1];
         buttonBuyed[LevelManager.choosedCharacter].GetComponent<Image>().sprite = choosed;
     }

     public void choose(int charNumber)
     {
         if (_character[charNumber].unlocked == true)
         {
             buttonBuyed[LevelManager.choosedCharacter].GetComponentInChildren<TextMeshProUGUI>().text =txtAbled[(int) LevelManager.lang, 0];
             buttonBuyed[LevelManager.choosedCharacter].GetComponent<Image>().sprite = notchoosed;
             LevelManager.choosedCharacter = charNumber;
             buttonBuyed[charNumber].GetComponentInChildren<TextMeshProUGUI>().text = txtAbled[(int) LevelManager.lang, 1];
             buttonBuyed[charNumber].GetComponent<Image>().sprite = choosed;
         }
         else
         {
             if ((_character[charNumber].Currency == 1 && _character[charNumber].Price <= _Level.fullCoins) ||
                 (_character[charNumber].Currency == 2 && _character[charNumber].Price <= _Level.diamonds))
             {
                 if (_character[charNumber].Currency == 1)
                 {
                     _Level.fullCoins -= _character[charNumber].Price;
                 }
                 else
                 {
                     _Level.diamonds -= _character[charNumber].Price;
                 }
                 LevelManager.characterUnlocked[charNumber]= true;
                 _character[charNumber].unlocked = true;
                 DataSave.SaveData();
                 buttonBuyed[LevelManager.choosedCharacter].GetComponentInChildren<TextMeshProUGUI>().text =txtAbled[(int) LevelManager.lang, 0];
                 buttonBuyed[LevelManager.choosedCharacter].GetComponent<Image>().sprite = notchoosed;
                 LevelManager.choosedCharacter = charNumber;
                 buttonBuyed[charNumber].GetComponentInChildren<TextMeshProUGUI>().text = txtAbled[(int) LevelManager.lang, 1];
                 buttonBuyed[charNumber].GetComponent<Image>().sprite = choosed;
                 mapManager.GetComponent<map_manager>().CheckFunds();
                 CheckCharacters();
             }
             else
             {
                 NoFunds();
                 store.SetActive(true);
                 characterstore.SetActive(false);
                store.GetComponentInParent<storeScript>().Start();
                
             }
         }
     }


public void NoFunds()
{
    textObject =
        Instantiate(textFinishLvlPrefab, new Vector3(0, 0, -5), Quaternion.identity) as GameObject;
    textObject.GetComponent<TextMeshProUGUI>().text = messageNofunds[(int) LevelManager.lang];
    textObject.transform.parent = textParent.transform;
    Destroy(textObject, 2);
}
}
public class Character
{
    public bool unlocked = false;
    private int price=0;
    private int currency; // 0-none; 1-coin; 2-diamond;
    private String[] name = new String[3];
   

    public int Price => price;

    public int Currency => currency;

    public string[] Name => name;
    
    public Character(int price, int currency, String[] name)
    {
        this.currency = currency;
        if (currency == 0 || currency == 3)
            price = 0;
        else
            this.price = price;
        
        this.name = name;
    }

}
