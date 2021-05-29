using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class w01l02Manager : MonoBehaviour
{
    public TextMeshProUGUI textSign;
    public GameObject[] _obj;

    public void Start()
    {
        foreach (var VARIABLE in _obj)
        {
            VARIABLE.SetActive(false);
        }
        CheckLang();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var VARIABLE in _obj)
            {
                VARIABLE.SetActive(true);
            }
        }
    }

    public void CheckLang()
    {
        String[] SignTransl =
            {"BE CAREFUL", "BĄDŹ OSTROŻNY", "ACHTUNG"};
        textSign.text = SignTransl[(int) LevelManager.lang];
    }
}
