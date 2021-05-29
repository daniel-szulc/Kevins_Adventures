using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class w01l12Manager : MonoBehaviour
{

    public TextMeshProUGUI minefieldText;
    public GameObject enemy;

    private void Start()
    {
        enemy.SetActive(false);
        langCheck();
    }

    public void langCheck()
    {
        String[] trans = new[] {"MINEFIELD", "POLE MINOWE", "Minenfeld"};
        minefieldText.text = trans[(int) LevelManager.lang];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.SetActive(true);
        }
    }
}
