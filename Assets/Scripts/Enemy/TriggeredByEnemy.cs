using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TriggeredByEnemy : MonoBehaviour
{
    // Start is called before the first frame update
   // public static Collider2D _collider;

   private bool isTouchingEnemy = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (!PlayerController.instance.HurtBlink && isTouchingEnemy)
        {
            PlayerController.instance.Hurt();
        }*/
    }


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
