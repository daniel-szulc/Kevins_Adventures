using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(ParticleSystem))]
public class ParticleMask : MonoBehaviour
{
    public GameObject[] Trigger = new GameObject[2];


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("turn"))
        gameObject.GetComponent<ParticleSystem>().Stop();
}

    private void OnTriggerEnter2D(Collider2D other)
    {    if (other.CompareTag("turn"))
        Debug.Log("Trigger");
    }


    private void OnTriggerExit2D(Collider2D other)
{ 
 
    if (other.CompareTag("turn"))
    gameObject.GetComponent<ParticleSystem>().Play();
}
}
