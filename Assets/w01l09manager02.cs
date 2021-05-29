using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class w01l09manager02 : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    public CinemachineVirtualCamera VirtualCamera;
    private bool done = false;
    public AudioClip rocksfall;
    private AudioSource _audioSource;
    
    void Start()
    {
        _particleSystem = GetComponentInParent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !done)
        {
            done = true;
            _particleSystem.Play();
            StartCoroutine(CameraMove());
            _audioSource.PlayOneShot(rocksfall);
        }
    }
    
    IEnumerator CameraMove()
    {
        var cameranoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        cameranoise.m_AmplitudeGain = 2;
        yield return new WaitForSeconds(0.6f);
        cameranoise.m_AmplitudeGain = 1;
        yield return new WaitForSeconds(1f);
        cameranoise.m_AmplitudeGain = 0;
    }

    public void otherStart()
    {
        done = true;
        _particleSystem.Play();
        StartCoroutine(CameraMove());
        _audioSource.PlayOneShot(rocksfall);
    }
    
}
