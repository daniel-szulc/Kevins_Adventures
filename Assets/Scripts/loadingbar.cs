using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class loadingbar : MonoBehaviour
{
    public TextMeshProUGUI loadingtxt;
    public GameObject kevin;
    void Start()
    {
        String[,] loadingTxtTrans = new String[,]
        {
            {"     LOADING", "     LOADING.", "     LOADING..", "     LOADING..."},
            {"   ŁADOWANIE", "   ŁADOWANIE.", "   ŁADOWANIE..", "   ŁADOWANIE..."},
            {"WIRD GELADEN", "WIRD GELADEN.", "WIRD GELADEN..", "WIRD GELADEN..."}
        };
        StartCoroutine(Loading(loadingTxtTrans));

        if (kevin != null)
        {
            
            kevin.GetComponent<Animator>().SetFloat("Speed", 1);
            kevin.GetComponent<Animator>().SetBool("OnGround", true);
            kevin.GetComponent<SpriteResolver>().SetCategoryAndLabel(LevelManager.characterName[LevelManager.choosedCharacter],"walk");
        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Loading(String[,] loadingTxtTrans)
    {

        loadingtxt.text = loadingTxtTrans[ (int) LevelManager.lang,0];
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            for (int i = 1; i < 4; i++)
            {
                loadingtxt.text = loadingTxtTrans[(int) LevelManager.lang,i];
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
