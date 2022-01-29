using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class AcceptMessage : MonoBehaviour
{
    public GameObject messageBox;
    public TextMeshProUGUI[] text = new TextMeshProUGUI[4];
    String[,] texts = new string[,]{{"Are you sure?", "Czy na pewno?", "Bist du sicher?"},{"YES", "TAK", "JA"}, {"NO", "NIE", "NEIN"} };


    public void Accept(String[] _text)
    {
        Debug.Log("Message");
        messageBox.SetActive(true);
        text[0].text = texts[0, (int) LevelManager.lang];
        text[1].text = _text[(int) LevelManager.lang];
        text[2].text = texts[1, (int) LevelManager.lang];
        text[3].text = texts[2, (int) LevelManager.lang];

        if (gameObject.GetComponent<settings>())
        {
            if (gameObject.GetComponent<settings>().Question)
            {
               text[0].color=new Color(1,0,0,1);
               text[1].color=new Color(1,0,0,1);
            }
            else
            {
                text[0].color=new Color(1,1,1,1);
                text[1].color=new Color(1,1,1,1);
            }
        }
    }

    public void no()
    {
      
        messageBox.GetComponent<Animator>().Play("boxAnimOUT");
        
    }
    
    
    
}
