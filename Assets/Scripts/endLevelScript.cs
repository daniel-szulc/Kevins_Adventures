using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class endLevelScript : MonoBehaviour
{
    public GameObject touchlock;
    private bool end=false;
    [Range(-1,1)]
    public int direction=1;

    public float waitTime = 2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           StartEnd();
        }
    }

    public void StartEnd()
    {
        touchlock.SetActive(true);
        StartCoroutine(endLevel());
        end = true;
        AdManager.instance.AdVideo();
        AdManager.instance.AdBanner();
    }

    void Update()
    {
        if (end)
        {
            PlayerController.instance.movement = direction;
            PlayerController.instance._movement = direction;
        }
    }
    IEnumerator endLevel()
    {
        yield return new WaitForSeconds(waitTime);
        PlayerController.instance.finishLevel = true;
        _Level.instance.Complete();
        end = false;
        touchlock.SetActive(false);
    }
}
