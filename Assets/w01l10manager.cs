using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w01l10manager : MonoBehaviour
{
    public GameObject[] bird;
    void Start()
    {
        foreach (var VARIABLE in bird)
        {
            VARIABLE.SetActive(false);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var VARIABLE in bird)
            {
                VARIABLE.SetActive(true);
            }
        }
    }
}
