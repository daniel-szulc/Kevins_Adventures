using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class languageMENU : MonoBehaviour
{
    public enum Language
    {
        en,
        pl,
        de
    };

    public TextMeshProUGUI text;
    public TextMeshProUGUI eng;
    public TextMeshProUGUI pol;
    public TextMeshProUGUI ger;
    public TextMeshProUGUI tapToPlay;
    public TextMeshProUGUI welcome;
    public GameObject menu;
    public GameObject mainmenu;
    public GameObject settingsButton;
    public GameObject logo;
    public Sprite logoPL;
    public Sprite logoEN;
    public Sprite logoDE;

    public UnityEngine.UI.Button btnEN;
    public UnityEngine.UI.Button btnPL;
    public UnityEngine.UI.Button btnDE;
    public static Language lang;
    public GameObject player, enemy;
    
    public void Start()
    {
        settingsButton.SetActive(false);
        if (!LevelManager.choosed)
        {
            QualityScript._resolution = Screen.currentResolution;
            menu.SetActive(true);
           // logo.SetActive(false);
            if (Application.systemLanguage == SystemLanguage.German)
            {
                German();
                btnDE.Select();
            }
            else if (Application.systemLanguage == SystemLanguage.Polish)
            {
                Polish();
                btnPL.Select();
            }
            else
            {
                English();
                btnEN.Select();
            }
            
        }
        else
        {
            menu.SetActive(false);
            if (LevelManager.lang == LevelManager.Language.pl)
            {
                Polish();
                btnPL.Select();
                OK();
            }
            else if(LevelManager.lang==LevelManager.Language.de)
            {
                German();
                btnDE.Select();
                OK();
            }
            else
            {
                English();
                btnEN.Select();
                OK();
            }
            player.GetComponent<mainmenuplayer>().StartMove();
            enemy.GetComponent<mainmenuplayer>().StartMove();
        }
    }
    
    void OnEnable()
    {
      //  logo.SetActive(false);
        
        if (LevelManager.lang == LevelManager.Language.pl)
        {
            btnPL.Select();
        }
        else if(LevelManager.lang==LevelManager.Language.de)
        {
            btnDE.Select();
        }
        else
        {
            btnEN.Select();
        }
    }

    void Update()
    {
        
    }

    public void OK()
    {
        LevelManager.lang = (LevelManager.Language) lang;
        LevelManager.choosed = true;
        if(logo!=null)
        logo.SetActive(true);
        if (menu != null)
        {
            mainmenu.SetActive(true);
            settingsButton.SetActive(true);
            menu.GetComponent<Animator>().Play("boxAnimOUT");
        }
    }

    public void Polish()
    {
        lang = Language.pl;
        welcome.text = "WITAJ";
        text.text = "WYBIERZ SWÓJ JĘZYK";
        pol.text = "POLSKI";
        eng.text = "ANGIELSKI";
        ger.text = "NIEMIECKI";
        tapToPlay.text = "KLIKNIJ, ABY ZAGRAĆ";
        logo.GetComponent<SpriteRenderer>().sprite = logoPL;
        btnPL.enabled = false;
        btnDE.enabled = true;
        btnEN.enabled = true;
    }

    public void German()
    {
        lang = Language.de;
        welcome.text = "WILLKOMMEN";
        text.text = "WÄHLEN DEINE SPRACHE";
        pol.text = "POLNISCH";
        eng.text = "ENGLISCH";
        ger.text = "DEUTSCH";
        tapToPlay.text = "KLICKEN SIE HIER, UM DAS SPIEL ZU";
        logo.GetComponent<SpriteRenderer>().sprite = logoDE;
        btnPL.enabled = true;
        btnDE.enabled = false;
        btnEN.enabled = true;
    }

    public void English()
    {
        lang = Language.en;
        welcome.text = "WELCOME";
        text.text = "CHOOSE YOUR LANGUAGE";
        pol.text = "POLISH";
        eng.text = "ENGLISH";
        ger.text = "GERMAN";
        tapToPlay.text = "TAP TO PLAY";
        logo.GetComponent<SpriteRenderer>().sprite = logoEN;
        btnPL.enabled = true;
        btnDE.enabled = true;
        btnEN.enabled = false;
    }
    
    
}
