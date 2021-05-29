using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class reward_w01e14 : MonoBehaviour
{
    public TextMeshProUGUI[] _text;
    void Start()
    {
        String[,] trans =
        {
            {"congratulations", "Well done. You beat the Cyclop.\nhere are your rewards:", "collect", "character", "KEVIN IN\nA SNOW CAP", "diamond"},
            {"gratulacje", "dobra robota. Pokonałeś Cyklopa.\nOto Twoje nagrody:", "odbierz", "postać", "Kevin w zimowej czapce", "diament"},
            {"Glückwünsche", "Gut gemacht. Du hast die Zyklopen geschlagen. Hier sind deine Belohnungen:", "sammeln", "Charakter", "KEVIN IN EINER SCHNEEKAPPE", "Diamant"}
        };
        for (int i = 0; i < _text.Length; i++)
        {
            _text[i].text = trans[(int) LevelManager.lang,i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
