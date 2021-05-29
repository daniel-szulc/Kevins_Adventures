using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QualityScript : MonoBehaviour
{
    public Image _switch;
    public Sprite[] switchQual = new Sprite[3];
    public static int quality=2;
    public static Resolution _resolution;

    void Start()
    {
        if (quality == 2)
        {
            HighQ();
        }
    }

    private void Awake()
    {
        if (quality <= 0)
        {
            _switch.sprite = switchQual[0];
        }
        else if (quality <= 1)
        {
            _switch.sprite = switchQual[1];
        }
        else if (quality <= 2)
        {
            _switch.sprite = switchQual[2];
        }
    }

    public static void Check()
        {
            if (quality <= 0)
            {
                LowQ();
            }
            else if (quality <= 1)
            {
                MediumQ();
            }
            else if (quality <= 2)
            {
                HighQ();
            }
        }

    static void LowQ()
    {
        quality = 0;
        Application.targetFrameRate = 30;
        QualitySettings.SetQualityLevel (0);
        Screen.SetResolution(_resolution.width/2,_resolution.height/2,true);
    }

    static void MediumQ()
    {
        quality = 1;
        Application.targetFrameRate = 50;
        QualitySettings.SetQualityLevel (1);
        Screen.SetResolution((int) (_resolution.width/1.5f),(int) (_resolution.height/1.5f),true);
    }


    static void HighQ()
    {
        quality = 2;
        Application.targetFrameRate = 60;
        QualitySettings.SetQualityLevel (2);
        Screen.SetResolution(_resolution.width,_resolution.height,true);
    }

    

    public void Change()
    {
        if (quality <= 0)
        {
            MediumQ();
        }
        else if (quality <= 1)
        {
            HighQ();
        }
        else if (quality <= 2)
        {
            LowQ();
        }
        Awake();
    }
}
