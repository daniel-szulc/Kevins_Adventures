using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    
    public GameObject acceptBox;
    private bool question = false; //0-exit; 1-restart;
    
    public TextMeshProUGUI[] text = new TextMeshProUGUI[5];
    void Start()
    {
        instance = this;
       
    }

   public void Awake()
    {
        Time.timeScale = 0f;
        langCheck();
        if(AdManager.instance!=null)
        AdManager.instance.AdBanner();
    }

   public void langCheck()
   {
       String[,] translation = new String[3, 5]
       {
           {"PAUSE","RESUME", "RESTART", "OPTIONS", "EXIT"},
           {"PAUZA","WZNÓW", "POWTÓRZ", "USTAWIENIA", "WYJDŹ" },
           {"PAUSE","FORTSETZEN", "NEU STARTEN", "OPTIONEN", "BEENDEN"}
       };
       for(int i = 0; i<text.Length; i++)
           text[i].text = translation[(int) LevelManager.lang, i];
   }

    public void Resume()
    {
       

        Time.timeScale = 1;
        pauseMenu.GetComponent<Animator>().Play("boxAnimOUT");
        if(AdManager.instance!=null)
        AdManager.instance.BannerHide();
    }
    
    public void Restart()
    {
        
        String[] textMessage = new String[]{"Do you want to start again?", "Czy chcesz zacząć od nowa?", "Möchten Sie erneut beginnen?" };
        question = true;
        acceptBox.SetActive(true);
        AdManager.instance.BannerHide();
        acceptBox.GetComponent<AcceptMessage>().Accept(textMessage);
    }

    public void Exit()
    {
        
      
        String[] textMessage = new String[]{"Do you want to return to the map?", "Czy chcesz wrócić do mapy?", "Möchten Sie zur Karte zurückkehren?" };
        question = false;
        DataSave.SaveData();
        AdManager.instance.BannerHide();
        acceptBox.GetComponent<AcceptMessage>().Accept(textMessage);
    }
    
    public void Accept()
    { 
        Time.timeScale = 1f;
        if (question == true)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene("map");
    }

    public void Options()
    {
        settingsMenu.SetActive(true);
    }

    public void Website()
    {
        if (LevelManager.lang == LevelManager.Language.en)
        {
            Application.OpenURL("https://www.danielszulc.pl/kevinsadventures/");
        }
        else if (LevelManager.lang == LevelManager.Language.pl)
        {
            Application.OpenURL("https://www.danielszulc.pl/pl/kevinsadventures/");
        }
        else if (LevelManager.lang == LevelManager.Language.de)
        {
            Application.OpenURL("https://www.danielszulc.pl/de/kevinsadventures/");
        }
    }

    public void Facebook()
    {
        _Level.instance.Facebook();
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    } 
}
