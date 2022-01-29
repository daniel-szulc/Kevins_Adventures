using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class buttonAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clickaudio;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
    }

    public void SoundPlay()
    {
        audioSource.PlayOneShot(clickaudio[(int)Random.Range(0, 4)]);
    }
}
