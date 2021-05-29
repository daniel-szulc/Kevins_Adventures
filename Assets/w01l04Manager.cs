using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w01l04Manager : MonoBehaviour
{
    public platformScript platform;
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
       {
           platform.enabled = true;
       }   
   }
}
