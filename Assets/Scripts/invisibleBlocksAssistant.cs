using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisibleBlocksAssistant : MonoBehaviour
{
    private invisibleBlocks parent;
    void Start()
    {
        parent = GetComponentInParent<invisibleBlocks>();
    }


    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        parent.stay = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        parent.stay = false;
    }
}
