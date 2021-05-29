using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textSlime;
    private int score=0;
   private int fullScore=0;
   public int setMaxScore=0;
    private int coins = 0;
    private int enemyCount = 0;
    private int scoreValueNow=0;
    public TextMeshProUGUI scoretext;
    private int slime;
    NumberFormatInfo f = new NumberFormatInfo {NumberGroupSeparator = " "};
    void Start()
    {
       
        if (instance == null)
            instance = this;
        fullScore = 0;
        
        Debug.Log(fullScore);
        fullScore += (GameObject.FindGameObjectsWithTag("LivedEnemy").Length) * 100;
        Debug.Log(fullScore);
        fullScore += ((GameObject.FindGameObjectsWithTag("Coins").Length + QuestionBox.Counter) * 10);
        Debug.Log(fullScore);
        fullScore += (GameObject.FindGameObjectsWithTag("Heart").Length) * 400;
        Debug.Log(fullScore);
        
        
        score = 0;
        coins = 0;
        enemyCount = 0;
        scoreValueNow = 0;
        if (LevelManager.lang == LevelManager.Language.pl)
            scoretext.text = "PUNKTY";
        else if (LevelManager.lang == LevelManager.Language.en)
            scoretext.text = "SCORE";
        else if (LevelManager.lang == LevelManager.Language.de)
            scoretext.text = "PUNKTE";
        
        Debug.Log(fullScore);
        if (setMaxScore > 10)
        {
            _Level.Lvl[_Level.actualLevel].Fullscore = setMaxScore;
            fullScore = setMaxScore;
        }
        else
        {
            if (_Level.Lvl[_Level.actualLevel].Fullscore == 0)
            {
                _Level.Lvl[_Level.actualLevel].Fullscore = fullScore;
            }
            fullScore = _Level.Lvl[_Level.actualLevel].Fullscore;
        }
        changeSlime(_Level.slimesOnStart);
        Debug.Log(fullScore);
    }

    public int Slime => slime;
    public int Score => score;

    public int Coins => coins;

    public int EnemyCount => enemyCount;

    public int FullScore => fullScore;

    public void EnemyCounter()
    {
        enemyCount++;
        changeScores(100);
    }
    
    public void changeCoins(int coinValue)
    {
        coins = coins + coinValue;
        textCoin.text = coins.ToString("#,0",f);
    }
    public void changeScores(int scoreValue)
    {
        score = score + scoreValue;
        StartCoroutine(TimeScore(score));
        // textScore.text = score.ToString();
    }

    public void changeSlime(int slimeValue)
    {
        slime += slimeValue;
        textSlime.text = slime.ToString("#,0", f);
    }

    private IEnumerator TimeScore(int score)
    {
        while (scoreValueNow<score)
        {
            scoreValueNow++;
            textScore.text = scoreValueNow.ToString("#,0",f);
            yield return new WaitForSeconds(0.001f);
        }
    }
}
