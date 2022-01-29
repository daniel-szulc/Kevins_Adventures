using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy03detector : MonoBehaviour
{
    public bool rightcollider;

    private void OnTriggerEnter2D(Collider2D other)
         {
             if (other.CompareTag("Player") && PlayerController.instance.HurtBlink!=true)
             {
                 gameObject.GetComponentInParent<Enemy_03Controller>().PlayerDetect(rightcollider);
             }
         }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.instance.HurtBlink!=true)
        {
            gameObject.GetComponentInParent<Enemy_03Controller>().PlayerDetect(rightcollider);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameObject.GetComponentInParent<Enemy_03Controller>().playergone!=null)
                if(gameObject.GetComponentInParent<Enemy_03Controller>().playergone!=null)
                    StopCoroutine(gameObject.GetComponentInParent<Enemy_03Controller>().playergone);
            gameObject.GetComponentInParent<Enemy_03Controller>().playergone= StartCoroutine(gameObject.GetComponentInParent<Enemy_03Controller>().PlayerGone());
        }
    }
}
