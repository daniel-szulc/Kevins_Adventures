using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialSettings : MonoBehaviour
{
    public TextMeshProUGUI[] textSign;
    public TextMeshProUGUI[] textTutor;
    public GameObject turtle;

    void Start()
    {
CheckLang();
    }


    public void CheckLang()
    {
        String[,] SignTransl =
        {
            {"THIS WAY", "TĘDY", "DIESEN WEG"},
            {"ANGRY TURTLE", "WŚCIEKŁY ŻÓŁW", "Wütende Schildkröte"},
            {"BE CAREFUL", "BĄDŹ OSTROŻNY", "ACHTUNG"}
        };
        for (int i = 0; i < 3; i++)
        {
            textSign[i].text = SignTransl[i,(int) LevelManager.lang];
        }
        
        String[,] TutorTransl =
        {
            {"USE\nOR\nTO MOVE", "UŻYJ \nLUB\nDO PORUSZANIA", "BENUTZEN\nODER\nUM ZU GEHEN"},
            {"USE\n\nTO JUMP", "UŻYJ\n\nABY SKOCZYĆ", "BENUTZEN\n\n<size=85%>UM ZU SPRINGEN"},
            {"USE\nTWICE\nTO JUMP HIGHER", "UŻYJ \n\tDWUKROTNIE\nABY SKOCZYĆ WYŻEJ", "BENUTZEN\n\tZWEIMAL\n<size=85%>UM höher ZU SPRINGEN"},
            {"USE \n\nTO SHOOT", "UŻYJ \n\nABY STRZELIĆ", "BENUTZEN\n\n<size=85%>UM ZU schießen"}
        };
        for (int i = 0; i < 4; i++)
        {
            textTutor[i].text = TutorTransl[i,(int) LevelManager.lang];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            turtle.SetActive(true);
    }
}
