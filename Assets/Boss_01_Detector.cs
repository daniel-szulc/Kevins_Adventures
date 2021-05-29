using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01_Detector : MonoBehaviour
{
   private bool onGround = true;
   private Enemy_Boss_01_Controller _enemyBoss01Controller;
   

   private void Start()
   {
      _enemyBoss01Controller = GetComponentInParent<Enemy_Boss_01_Controller>();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Ground"))
      {
         if (!onGround)
         {
            onGround = true;
            ChangeValue();
            _enemyBoss01Controller.Landing();
         }
      }
   }

   private void OnTriggerStay2D(Collider2D other)
   {
      if (other.CompareTag("Ground"))
      {
         if (!onGround)
         {
            onGround = true;
            ChangeValue();
         }
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.CompareTag("Ground"))
      {
         if (onGround)
         {
            onGround = false;
            ChangeValue();
         }
      }
   }

   void ChangeValue()
   {
      _enemyBoss01Controller.GroundCheck = onGround;
   }
}
