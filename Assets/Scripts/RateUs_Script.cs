using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RateUs_Script : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    public GameObject[] logoimg;
    
    void Start()
    {
        CheckLanguage();
        AdManager.instance.BannerHide();
    }

    void CheckLanguage()
    {
        String[,] translation = new String[3, 5]
        {
            {"RATE US!","Do you like Kevin's Adventures? \nTell us what you think!", "Rate now!", "Maybe Later", "Like us also\non Facebook!"},
            {"Oceń nas!","Lubisz przygody Kevina? \nPowiedz nam co myślisz!", "Oceń teraz!", "Może później", "Polub nas również \nna Facebooku!" },
            {"Bewerten Sie uns!","Magst du Kevins Abenteuer? Sagen Sie uns was Sie denken!", "Jetzt bewerten!", "vielleicht später", "Like uns auch \nauf Facebook!"}
        };
        for(int i = 0; i<text.Length; i++)
            text[i].text = translation[(int) LevelManager.lang, i];
        logoimg[0].SetActive(false);
        logoimg[1].SetActive(false);
        logoimg[2].SetActive(false);
        logoimg[(int) LevelManager.lang].SetActive(true);
    }

    public void RateNow()
    {
        _Level.rated = true;
        Application.OpenURL ("market://details?id=com.DanielSzulc.KevinsAdventures");
        X();
    }


    public void X()
    {
        gameObject.GetComponent<Animator>().Play("boxAnimOUT");
    }
    public void Facebook()
    {
        _Level.instance.Facebook();
    }
}
