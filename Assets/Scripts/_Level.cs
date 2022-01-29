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
