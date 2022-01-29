using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settings : MonoBehaviour
{
    public GameObject settMenu;
    public GameObject langMenu;
    public GameObject Xbutton;
    public TextMeshProUGUI[] text = new TextMeshProUGUI[3];
    public TextMeshProUGUI scoreTxt;
    public GameObject pausemenu;
    public static bool sound=true;
    public static bool music=true;
    public TextMeshProUGUI resetTxt;
    public Sprite onmusic, offmusic;
    public Image musicImage, soundImage;
    public TextMeshProUGUI version;
    public Image languageImage;
    public Sprite[] langSprite = new Sprite[3];
    public TextMeshProUGUI[] qualityTxt = new TextMeshProUGUI[3];
    public AudioSource audioMusic;
    
    public GameObject acceptBox;
    private bool question = false; //0-exit; 1-reset;
    public GameObject warning;
    public TextMeshProUGUI[] comingsoon;
    
    public bool Question => question;
    
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(acceptBox!=null)
            Exit();
            else
            {
                pausemenu.SetActive(true);
                pausemenu.GetComponent<PauseMenu>().Awake();
            }
    }
    }

    public void Awake()
    {
        version.text = "v." + Application.version.ToString();
        checkLanguage();
    }

    public void Start()
    {
        CheckMusic();
    }

    void CheckMusic()
    {
        if (music)
        {
            audioMusic.mute = false;
            musicImage.sprite = onmusic;
            AudioListener.volume = 1;
            sound = true;
        }
        else
        {
            audioMusic.mute = true;
            musicImage.sprite = offmusic;
        }
        if (sound)
        {
            soundImage.sprite = onmusic;
            AudioListener.volume = 1;
        }
        else
        {
            soundImage.sprite = offmusic;
            AudioListener.volume = 0;
            audioMusic.mute = true;
            musicImage.sprite = offmusic;
        }
    }
    private void checkLanguage()
    {
        String[,] translation = new String[,]
        {
            {"SETTINGS", "CREDITS", "Privacy Policy"},
            {"USTAWIENIA", "AUTORZY", "POLITYKA PRYWATNOŚCI" },
            {"EINSTELLUNGEN", "AUTOREN", "Datenschutzerklärung"}
        };
       
        for(int i = 0; i<text.Length; i++)
            text[i].text = translation[(int) LevelManager.lang, i];

        languageImage.sprite = langSprite[(int) LevelManager.lang];
        
        String[] translationScore = new String[3]{"SCORE", "PUNKTY", "PUNKTE"};
        scoreTxt.text = translationScore[(int) LevelManager.lang];
        if(resetTxt!=null)
        {
        String[] transReset = new string[]{ "RESET GAME", "RESETUJ GRĘ", "SPIEL ZURÜCKSETZEN"};
        resetTxt.text = transReset[(int) LevelManager.lang];
        }
        if(qualityTxt!=null)
        {
            String[,] transQual = new string[3,3]
            {
                {"LOW", "MEDIUM", "HIGH"},
                {"NISKA", "ŚREDNIA", "WYSOKA" },
                {"NIEDRIG", "MITTEL", "HOCH"}
            };
            for(int i = 0; i<qualityTxt.Length; i++) 
                qualityTxt[i].text = transQual[(int) LevelManager.lang, i];
        }

        String[] comingsoonTrans = new string[]
        {
            "COMING SOON", "WKRÓTCE DOSTĘPNE", "demnächst verfügbar"
        };
        if (comingsoon != null)
        {
            foreach (var VARIABLE in comingsoon)
            {
                VARIABLE.text = comingsoonTrans[(int) LevelManager.lang];
            }
        }

    }

    public void Language()
    {
        langMenu.SetActive(true);
    }

    public void PLlang()
         {
             LevelManager.lang = LevelManager.Language.pl;
             langMenu.GetComponent<Animator>().Play("boxAnimOUT");
             checkLanguage();
         }
    public void ENlang()
    {
        LevelManager.lang = LevelManager.Language.en;
        langMenu.GetComponent<Animator>().Play("boxAnimOUT");
        checkLanguage();
    }
    public void DElang()
    {
        LevelManager.lang = LevelManager.Language.de;
        langMenu.GetComponent<Animator>().Play("boxAnimOUT");
        checkLanguage();
    }

    public void Sound()
         {
             if (sound)
             {
                 sound = false;
                 soundImage.sprite = offmusic;
                 music = false;
             }
             else
             {
                 sound = true;
                 soundImage.sprite = onmusic;
                 
             }
             CheckMusic();
         }
    public void Music()
    {
        if (music)
        {
            music = false;
            musicImage.sprite = offmusic;
        }
        else
        {
            music = true;
            musicImage.sprite = onmusic;
            sound = true;
        }
        CheckMusic();
    }

    public void Mail()
    {
        if (LevelManager.lang == LevelManager.Language.en)
        {
            Application.OpenURL("mailto:dszulc.dev@gmail.com?subject=Kevin's Adventures");
        }
        else if (LevelManager.lang == LevelManager.Language.de)
        {
            Application.OpenURL("mailto:dszulc.dev@gmail.com?subject=Kevins Abenteuer");
        }
        else if (LevelManager.lang == LevelManager.Language.pl)
        {
            Application.OpenURL("mailto:dszulc.dev@gmail.com?subject=Przygody Kevina");
        }
        }
    
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://sites.google.com/view/kevins-adventures/privacy-policy");
    }

    public void Close()
    {
        
}

    public void X()
    {
        gameObject.GetComponentInChildren<Animator>().Play("boxAnimOUT");
        if(AdManager.instance!=null)
        AdManager.instance.BannerHide();
    }

    public void Exit()
    {
        String[] textMessage = new String[]{"Are you sure you want to leave the game?", "Jesteś pewny/a, że chcesz opuścić grę?", "Bist du sicher, dass du das Spiel beenden willst?" };
        question = false;
        acceptBox.SetActive(true);
        warning.SetActive(false);
        acceptBox.GetComponentInParent<AcceptMessage>().Accept(textMessage);
    }

    public void Reset()
    {
        String[] textMessage = new String[]{"You will lose all saved game progress, purchased items and entered settings. This process is irreversible.", "Utracisz wszystkie zapisane postępy w grze, zakupione przedmioty i wprowadzone ustawienia. Ten proces jest nieodwracalny.", "Sie verlieren alle gespeicherten Spielfortschritte, gekauften Gegenstände und eingegebenen Einstellungen. Dieser Prozess ist irreversibel." };
        question = true;
        acceptBox.SetActive(true);
        warning.SetActive(true);
        acceptBox.GetComponentInParent<AcceptMessage>().Accept(textMessage);
      
    }

    public void Accept()
    {
        if (question)
        {
            Debug.Log("Reset");
            Application.Quit();
        }
        else
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
    

    public void toMenu()
    {
        
        SceneManager.LoadScene(0);
    }
    
}
