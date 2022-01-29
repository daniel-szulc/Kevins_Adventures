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




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _tilemap.color=new Color(255,255,255,255);
        }
 
    }
    

    }


