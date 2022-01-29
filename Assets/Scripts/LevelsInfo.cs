using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class LevelsInfo
{
    
    private static int counter=0;
    private int lvlNumber;
    private bool unlocked = false;
    private bool finished = false;
    private int score = 0;
    private int coins = 0;
    private int enemies = 0;
    private int stars = 0;
    private int fullscore = 0;

    public int Fullscore
    {
        get => fullscore;
        set => fullscore = value;
    }
    public bool Finished
    {
        get => finished;
        set => finished = value;
    }
    public int Score
    {
        get => score;
       set => score = value;
    }

    public int Coins
    {
        get => coins;
        set => coins = value;
    }

    public int Enemies
    {
        get => enemies;
        set => enemies = value;
    }

    public int Stars
    {
        get => stars;
        set => stars = value;
    }
    
    public LevelsInfo()
    {
        lvlNumber = counter;
        counter++;
    }
    
    public bool Unlocked
    {
        get => unlocked;
        set => unlocked = value;
    }
    
    public int LvlNumber => lvlNumber;
    
    
}
