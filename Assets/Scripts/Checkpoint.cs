using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint instance;
    public Vector3 cpPosition;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            cpPosition = other.gameObject.transform.position;
            
        }
    }
}
