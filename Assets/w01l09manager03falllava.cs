using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w01l09manager03falllava : MonoBehaviour
{
    public GameObject reviveOn;
    public GameObject reviveOff;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            reviveOn.SetActive(false);
            reviveOff.SetActive(true);
        }
    }
}
