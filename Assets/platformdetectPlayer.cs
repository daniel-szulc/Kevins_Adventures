using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformdetectPlayer : MonoBehaviour
{
    private GameObject parent;
    void Start()
    {
        parent = gameObject.GetComponentInParent<PlayerController>().gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            parent.transform.parent = other.transform;
        }

    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            parent.transform.parent = other.transform;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            parent.transform.parent = null;
        }
    }
}
