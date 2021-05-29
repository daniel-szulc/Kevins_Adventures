using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behind_face_tree : MonoBehaviour
{
    public GameObject face;
    private Animator anim;
    public int lateTimeStart;
    void Start()
    {
        anim = face.GetComponent<Animator>();
        if (lateTimeStart > 0)
        {
            face.SetActive(false);
            StartCoroutine(lateStart());
        }
    }

    IEnumerator lateStart()
    {
        yield return new WaitForSeconds(Time.deltaTime*lateTimeStart);
        face.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("playerDetect", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("playerDetect", false);
        }
    }
}
