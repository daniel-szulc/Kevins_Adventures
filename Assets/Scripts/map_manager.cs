using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class map_manager : MonoBehaviour
{


    public GameObject[] levels = new GameObject[15];
    public GameObject[] stories;

    public Sprite lvlBlock;
    public Sprite[] levelSprite = new Sprite[4];
    
    public GameObject textFinishLvlPrefab;
    public GameObject textParent;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI score;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI diamonds;
    GameObject textObject;
    NumberFormatInfo f = new NumberFormatInfo {NumberGroupSeparator = " "};
    private AsyncOperation async;
    public GameObject loading;


    void Start()
    {
        if (LevelManager.lang == LevelManager.Language.pl)
            scoretext.text = "PUNKTY";
        else if (LevelManager.lang == LevelManager.Language.en)
            scoretext.text = "SCORE";
        else if (LevelManager.lang == LevelManager.Language.de)
            scoretext.text = "PUNKTE";

        CheckLevels();

    UpdateLeaderboardScore();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CheckFunds()
    {
        coins.text = _Level.fullCoins.ToString("#,0", f);
        diamonds.text = _Level.diamonds.ToString("#,0", f);
    }

    public void CheckLevels()
    {
        score.text = _Level.instance.FullScores().ToString("#,0", f);
        CheckFunds();

        for (int i = 0; i < 14; i++)
        {
            if (_Level.Lvl[i].Unlocked)
            {
                levels[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
                levels[i].GetComponent<Image>().sprite = levelSprite[_Level.Lvl[i].Stars];
            }
            else
            {
                levels[i].GetComponent<Image>().sprite = lvlBlock;
                levels[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        if (_Level.Lvl[7].Unlocked || _Level.Lvl[8].Unlocked)
        {
            stories[0].SetActive(true);
        }
        else
        {
            stories[0].SetActive(false);
        }
        
        if (_Level.Lvl[9].Unlocked || _Level.Lvl[10].Unlocked)
        {
            stories[1].SetActive(true);
        }
        else
        {
            stories[1].SetActive(false);
        }
        
        if (_Level.Lvl[13].Finished)
        {
            stories[2].SetActive(true);
        }
        else
        {
            stories[2].SetActive(false);
        }
        
    }


    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Story()
    {
        loading.SetActive(true);
        async = SceneManager.LoadSceneAsync("movie_start");
        async.allowSceneActivation = false;
        StartCoroutine(waitforLvl());
    }

    public void OtherStory(int storynumber)
    {
        loading.SetActive(true);
        async = SceneManager.LoadSceneAsync("movie_" + storynumber);
        async.allowSceneActivation = false;
        StartCoroutine(waitforLvl());
    }

    public IEnumerator waitforLvl()
{
yield return new WaitForSeconds(1);
async.allowSceneActivation = true;
}
    
    public void Level(int lvl)
    {
        if (_Level.Lvl[lvl].Unlocked)
        {
            level_play_menu.instance.menu.SetActive(true);
           level_play_menu.instance.Menu(lvl);
        }
        else
        {
            String[] translation = new string[]{"Finish the previous level to unlock this one!", 
                "Ukończ poprzedni poziom, aby odblokować!", "Schließe das vorige Level ab, um dieses freizuschalten!"};
            textObject = Instantiate(textFinishLvlPrefab, new Vector3(0,0,-5), Quaternion.identity) as GameObject;
            textObject.GetComponent<TextMeshProUGUI>().text = translation[(int) LevelManager.lang];
            textObject.transform.parent = textParent.transform;
            Destroy(textObject,2);
        }
        
        
        
        
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

    public void OpenLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void UpdateLeaderboardScore()
    {
        Social.ReportScore(_Level.instance.FullScores(), GPGSIds.leaderboard_high_score, null);
    }
    
}
