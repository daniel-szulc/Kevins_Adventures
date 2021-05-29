using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class mine_bomb_script : MonoBehaviour
{
    private bool explode = false;
    public GameObject sign;
    public Sprite signDanger;
    private AudioSource _audioSource;
    public AudioClip alarmSound, ExplosiveSound;
    public GameObject redLight;
    public GameObject explosion;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            if (explode == false)
            {
                explode = true;
                StartCoroutine(Explode());
            }
        }
    }

    public IEnumerator Explode()
    {
        sign.GetComponent<SpriteRenderer>().sprite = signDanger;
        sign.GetComponent<millRotateScript>().rotationSpeed = 500;
        _audioSource.PlayOneShot(alarmSound);
        redLight.SetActive(true);
        yield return new WaitForSeconds(1);
        explosion.SetActive(true);
        _audioSource.Stop();
        _audioSource.PlayOneShot(ExplosiveSound);
        yield return new WaitForSeconds(Time.deltaTime);
        sign.SetActive(false);
        redLight.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(GetComponentInParent<GameObject>());
    }
}
