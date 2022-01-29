using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unstableBox : MonoBehaviour
{
    private bool started = false;
    private AudioSource _audioSource;
    void Start()
    {
        gameObject.GetComponentInParent<Animator>().enabled = false;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !started)
        {
            started = true;
            if (gameObject.GetComponentInParent<DestroyBlock>().destroyed == false)
            {
                gameObject.GetComponentInParent<Animator>().enabled = true;
                gameObject.GetComponentInParent<Animator>().Play("destroyed");
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
                _audioSource.Play();
            }
        }
    }

    

}
