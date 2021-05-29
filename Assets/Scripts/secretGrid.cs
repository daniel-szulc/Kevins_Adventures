using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Tilemaps;

public class secretGrid : MonoBehaviour
{
    private Tilemap _tilemap;
    private IEnumerator show, hide;
    private bool CRstarted = false;

    void Start()
    {
        _tilemap = gameObject.GetComponent<Tilemap>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _tilemap.color=new Color(255,255,255,0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _tilemap.color=new Color(255,255,255,0);
        }
    }

    void Update()
    {

    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _tilemap.color=new Color(255,255,255,255);
        }
 
    }
    

    /*IEnumerator Show()
    {
        StopCoroutine(hide);
        CRstarted = true;
        Color tileColor=new Color(255,255,255,0);

        while (_tilemap.color.a>tileColor.a)
        {
            _tilemap.color = Color.Lerp(_tilemap.color, tileColor, 6*Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
        }

        CRstarted = false;
    }
    IEnumerator Hide()
    {
       // CRstarted = true;
        StopCoroutine(show);
        Color tileColor=new Color(255,255,255,255);

        while (_tilemap.color.a<tileColor.a)
        {
            _tilemap.color = Color.Lerp(_tilemap.color, tileColor, 6*Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
        }

      //  CRstarted = false;*/
    }


