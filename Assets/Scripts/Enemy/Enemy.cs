using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isBeingStomped = false;

    public AudioClip audioHurt;



    
    public virtual void Hurt()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioHurt,1 );
    }

    public virtual void Turn()
    {
        
    }
}
