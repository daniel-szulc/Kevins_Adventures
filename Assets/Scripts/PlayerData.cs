using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

using UnityEngine;
[System.Serializable]
public class PlayerData
{
    
    public int coins;
    public int diamonds;
    public LevelsInfo[] Lvl = new LevelsInfo[15];
    public int lang;
    public int[] _resolution = new int[2];
    public int quality;
    public bool sound;
    public bool music;
    public bool[] characterUnlocked = new bool[7];
    public int choosedCharacter;
    public bool noAds;
    public DateTime? timeOfLastCollect;
    public int GiftCollected;
    public int heartsPotion;
    public int slimePack;
    public int shield;
    public int slimesOnStart;
    public bool fourthHeart;
    public bool tripleJump;
    public int fastershooting;
    public bool rated;


    public PlayerData()
    {
        coins = _Level.fullCoins;
        diamonds = _Level.diamonds;
        for (int i = 0; i < 15; i++)
        {
            Lvl[i]=new LevelsInfo();
        }
        Lvl = _Level.Lvl;
        lang = (int)LevelManager.lang;
        _resolution[0] = QualityScript._resolution.height;
        _resolution[1] = QualityScript._resolution.width;
        quality = QualityScript.quality;
        sound = settings.sound;
        music = settings.music;
        characterUnlocked = LevelManager.characterUnlocked;
        choosedCharacter = LevelManager.choosedCharacter;
        noAds = _Level.noAds;
        timeOfLastCollect = DailyReward.timeOfLastCollect;
        GiftCollected = DailyReward.GiftCollected;
        heartsPotion = _Level.heartsPotion;
        slimePack = _Level.slimePack;
        shield = _Level.shield;
        slimesOnStart = _Level.slimesOnStart;
        fourthHeart = _Level.fourthHeart;
        tripleJump = _Level.tripleJump;
        fastershooting = _Level.fastershooting;
    }
    
    
}
