using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningScript : MonoBehaviour
{
    private IEnumerator timer;
    private bool higher=false;
    private bool higher2 = false;
    public void RunningStart()
    {
        
        gameObject.SetActive(true);
        
        if (PlayerController.instance.GetComponentInChildren<PlayerMagnetScript>() != null)
        {
            PlayerController.instance.GetComponentInChildren<PlayerMagnetScript>().moveUp();
        }
        if (PlayerController.instance.GetComponentInChildren<playerProtectedScript>() != null)
        {
            PlayerController.instance.GetComponentInChildren<playerProtectedScript>().moveUp();
        }
        
        PlayerController.instance.speed = 12f;
        if(timer!=null)
            StopCoroutine(timer);
        timer = Timer();
        StartCoroutine(timer);
    }

    private void Update()
    {
        throw new NotImplementedException();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        endRunning();
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
        PlayerController.instance.speed = 6.8f;
        gameObject.SetActive(false);
    }
}
