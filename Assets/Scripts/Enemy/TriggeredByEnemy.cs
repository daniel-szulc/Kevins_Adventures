using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TriggeredByEnemy : MonoBehaviour
{

   private bool isTouchingEnemy = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {

            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (!PlayerController.instance.HurtBlink && !enemy.isBeingStomped)
                {
                    PlayerController.instance.Hurt();
                    isTouchingEnemy = true;
                }
            }
            else if (other.GetComponentInParent<Enemy_03Controller>() != null)
            {
                if (other.GetComponentInParent<Enemy_03Controller>().hided == false &&
                    !PlayerController.instance.HurtBlink &&
                    !other.GetComponentInParent<Enemy_03Controller>().isBeingStomped)
                {
                    PlayerController.instance.Hurt();
                    isTouchingEnemy = true;
                }
            }
            else if(enemy==null)
            {
                enemy = other.gameObject.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    if (!PlayerController.instance.HurtBlink && !enemy.isBeingStomped)
                    {
                        PlayerController.instance.Hurt();
                        isTouchingEnemy = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            isTouchingEnemy = false;
        }
    }
}
