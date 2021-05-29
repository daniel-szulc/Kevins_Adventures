using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class w01l09manager : MonoBehaviour
{
    private IEnumerator _coroutine;
    private IEnumerator coroutinelava;
    public GameObject tile;
    public GameObject lava;
    public GameObject camera;
    public CinemachineVirtualCamera VirtualCamera;
    public AudioClip doorclosing, doorclosed, dungeonfast;
    public AudioSource _audioSource;
    public AudioSource _audioSource2;
    public AudioSource mainSound;
    public w01l09manager02 _object;
    void Start()
    {
        _coroutine = DownWall();
        coroutinelava = LavaUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(_coroutine);
            StartCoroutine(coroutinelava);
        }
    }

    IEnumerator DownWall()
    {
        _audioSource.PlayOneShot(doorclosing);
        Vector3 start = new Vector3(tile.transform.position.x, tile.transform.position.y);
        Vector3 end = new Vector3(tile.transform.position.x, -3);
        while (tile.transform.position.y > end.y)
        {
            tile.transform.position =
                new Vector3(tile.transform.position.x, tile.transform.position.y - Time.deltaTime * 4f);
            yield return new WaitForSeconds(0.0008f);
        }

        StartCoroutine(CameraMove());
        _audioSource.Stop();
        _audioSource.PlayOneShot(doorclosed);
        tile.transform.position = end;
        mainSound.clip = dungeonfast;
        mainSound.Play();
        _object.otherStart();
    }

    IEnumerator LavaUp()
    {
        
        _audioSource2.Play();
        Vector3 start = new Vector3(lava.transform.position.x, lava.transform.position.y);
        Vector3 end = new Vector3(lava.transform.position.x, 253);
        while (lava.transform.position.y < end.y)
        {
            lava.transform.position =
                new Vector3(lava.transform.position.x, lava.transform.position.y + Time.deltaTime * 0.95f);
            yield return new WaitForSeconds(Time.deltaTime*1);
        }
    }

    IEnumerator CameraMove()
    {
        var cameranoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        cameranoise.m_AmplitudeGain = 1;
        yield return new WaitForSeconds(0.3f);
        cameranoise.m_AmplitudeGain = 0;
    }
}

