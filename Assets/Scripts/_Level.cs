using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class _Level : MonoBehaviour
{
    public static _Level instance;
    public static LevelsInfo[] Lvl = new LevelsInfo[25];
    public static int actualLevel=0;
    public static int fullScores=0;
    public static int fullCoins=0;
    public static int diamonds=0;
    public static bool noAds = false;
    public static int heartsPotion = 2;
    public static int slimePack = 2;
    public static int shield = 2;
    public GameObject langMenu;
    public static int slimesOnStart=3;
    public static bool fourthHeart=false;
    public static bool tripleJump=false;
    public static int fastershooting=0;
    public static bool rated = false;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        for (int i = 0; i < Lvl.Length; i++)
        {
            Lvl[i] = new LevelsInfo();
        }
        PlayerData data = DataSave.LoadData();
        if (data != null)
        {
            fullCoins = data.coins;
            diamonds = data.diamonds;
            Lvl = data.Lvl;
            LevelManager.lang = (LevelManager.Language) data.lang;
            LevelManager.choosed = true;
            LevelManager.characterUnlocked = data.characterUnlocked;
            LevelManager.choosedCharacter = data.choosedCharacter;
            story_script.StoryDone = true;
            QualityScript._resolution.height = data._resolution[0];
         QualityScript._resolution.width = data._resolution[1];
         QualityScript.quality = data.quality;
         QualityScript.Check();
         settings.sound = data.sound;
            settings.music = data.music;
            noAds = data.noAds;
            DailyReward.timeOfLastCollect = data.timeOfLastCollect;
            DailyReward.GiftCollected = data.GiftCollected;
            heartsPotion=data.heartsPotion;
            slimePack=data.slimePack ;
            shield=data.shield;
            slimesOnStart = data.slimesOnStart;
            fourthHeart = data.fourthHeart;
            tripleJump = data.tripleJump;
            fastershooting = data.fastershooting;
            rated = data.rated;
            if (Lvl[6].Finished == true)
                story_script_w01e07.StoryDone = true;
            if (Lvl[8].Finished == true)
                Story_script_w01e09.StoryDone = true;
        }
        else
        {
            Debug.Log("Error");
        }
        langMenu.SetActive(true);
    }

    public int FullScores()
    {
        fullScores = 0;
        for (int i = 0; i < Lvl.Length; i++)
        {
            fullScores += Lvl[i].Score;
        }

        return fullScores;
    }

    void Awake()
    {
       // Application.targetFrameRate = 30;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {

            if (this != instance)
                Destroy(this.gameObject);
        }
        
    }

    public void Facebook()
    {
        StartCoroutine(OpenFacebookPage());
    }
    
    IEnumerator OpenFacebookPage()
    {
        float startTime;
        startTime = Time.realtimeSinceStartup;
        Application.OpenURL("fb://page/101091661928932/");
        yield return new WaitForSecondsRealtime(1);
        if (Time.realtimeSinceStartup - startTime <2)
        {
            Application.OpenURL("https://www.facebook.com/KevinsAdv");
        }
    }

    public void Complete()
    {
        CompleteMenu.instance.Menu(actualLevel);
    }
}
