using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerMagnetScript : MonoBehaviour
{
    
    private IEnumerator timer;
    private bool higher=false;
    private bool higher2 = false;
    public void MagnetStart()
    {
        gameObject.SetActive(true);
        
        if (PlayerController.instance.GetComponentInChildren<PlayerRunningScript>() != null)
        {
            PlayerController.instance.GetComponentInChildren<PlayerRunningScript>().moveUp();
        }
        if (PlayerController.instance.GetComponentInChildren<playerProtectedScript>() != null)
        {
            PlayerController.instance.GetComponentInChildren<playerProtectedScript>().moveUp();
        }
        
        if(timer!=null)
            StopCoroutine(timer);
        timer = Timer();
        StartCoroutine(timer);
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



    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        endRunning();
    }
    
    
    public void endRunning()
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
        gameObject.SetActive(false);
    }
}
