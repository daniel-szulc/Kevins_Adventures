using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w01l05Manager : MonoBehaviour
{
    public GameObject[] enemies;

    private void Start()
    {
        foreach (var VARIABLE in enemies)
        {
            VARIABLE.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            foreach (var VARIABLE in enemies)
            {
                VARIABLE.SetActive(true);
            }
        }
    }
}
