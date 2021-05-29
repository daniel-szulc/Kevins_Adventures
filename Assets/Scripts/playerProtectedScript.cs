using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class playerProtectedScript : MonoBehaviour
{
    private IEnumerator timer;
    private bool higher=false;
    private bool higher2 = false;
    public GameObject stompbox;
    public void ProtectionStart()
    {
        gameObject.SetActive(true);
        if (PlayerController.instance.GetComponentInChildren<PlayerMagnetScript>() != null)
        {
            PlayerController.instance.GetComponentInChildren<PlayerMagnetScript>().moveUp();
        }
        if (PlayerController.instance.GetComponentInChildren<PlayerRunningScript>() != null)
        {
            PlayerController.instance.GetComponentInChildren<PlayerRunningScript>().moveUp();
        }

        PlayerController.instance.HurtBlink = true;
        stompbox.SetActive(false);
        if(timer!=null)
            StopCoroutine(timer);
        timer = Timer();
        StartCoroutine(timer);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        endProtection();
    
    }
        
    public void moveUp()
    {
  
        if (higher)
        {
            this.GetComponent<Animator>().Play("protectionhigher2");
            higher2 = true;
        }
        else
        {
            this.GetComponent<Animator>().Play("protectionhigher");
            higher = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(other.gameObject.GetComponent<Enemy>()!=null)
                other.gameObject.GetComponent<Enemy>().Hurt();
            else
                other.gameObject.GetComponentInParent<Enemy>().Hurt();
        }
    }


    
    public void endProtection()
    {
        if(higher2)
            gameObject.GetComponent<Animator>().Play("protection end higher2");
        else if(higher)
            gameObject.GetComponent<Animator>().Play("protection end higher");
        else
            gameObject.GetComponent<Animator>().Play("protection end");

        StartCoroutine(disabledObject());
    }

    private IEnumerator disabledObject()
    {
        yield return new WaitForSeconds(3.1f);
        higher = false;
        higher2 = false;
        stompbox.SetActive(true);
        PlayerController.instance.HurtBlink = false;
        gameObject.SetActive(false);
    }
}
