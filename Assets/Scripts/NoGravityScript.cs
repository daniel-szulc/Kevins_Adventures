using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravityScript : MonoBehaviour
{
    public bool gravityOn;
 void OnTriggerStay2D(Collider2D other)
 {
     if (other.CompareTag("Player"))
     {
         if(!gravityOn)
         other.GetComponent<Rigidbody2D>().gravityScale = -0.5f;
         else
         {
             other.GetComponent<Rigidbody2D>().gravityScale = 5;
         }
     }
  }
 void OnTriggerEnter2D(Collider2D other)
 {
     if (other.CompareTag("Player"))
     {
         if(!gravityOn)
             other.GetComponent<Rigidbody2D>().gravityScale = -1f;
         else
         {
             other.GetComponent<Rigidbody2D>().gravityScale = 5;
         }
     }
 }

 
}
