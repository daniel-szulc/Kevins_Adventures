using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class invisibleBlocks : MonoBehaviour
{
    private Tilemap tilemap;
    private Color colorNow;
    private IEnumerator show;
    private IEnumerator hide;
    private bool stop = false;
    private Color invisibleColor = new Color(1, 1, 1, 0);
    private Color visibleColor = new Color(1, 1, 1, 1);
    private Color setColor;
    public float time = 0;
    public bool stay = false;
    void Start()
    {
        tilemap = this.GetComponent<Tilemap>();
        colorNow = tilemap.color;
        setColor = tilemap.color;
        invisibleColor = new Color(tilemap.color.r,tilemap.color.g,tilemap.color.b, 0);
        visibleColor = new Color(tilemap.color.r,tilemap.color.g,tilemap.color.b, 1);
    }

    private void Update()
    {
        tilemap.color = Color.Lerp(colorNow, setColor, time);
        if (time <= 1) 
        time += Time.deltaTime*4;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*StopCoroutine(show);
        show = ShowBlock();
        stop = true;
        StartCoroutine(show);*/
        if (other.CompareTag("Player"))
        {
            time = 0;
            colorNow = tilemap.color;
            setColor = invisibleColor;
        }
    }

    /*
    private IEnumerator ShowBlock()
    {
        stop = false;
        while (progress < 1)
        {
            tilemap.color = Color.Lerp(colorNow, invisibleColor)
            yield return new WaitForSeconds(Time.deltaTime*3);
                if(stop)
                    break;
        }

        stop = false;


        currentColor = Color.Lerp(Color.red, Color.blue, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !stay)
        {
            time = 0;
            colorNow = tilemap.color;
            setColor = visibleColor;
        }
    }

    /*private IEnumerator HideBlock()
    {
        
    }*/
}
