using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{
    public float speed;
    public Collider2D[] colliders;
    private Rigidbody2D rb;
    
    public  enum direction {
        horizontal, vertical
    };
    public direction Direction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (Direction == direction.horizontal)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
            gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
            gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezePositionX;
        }
    }


    void Update()
    {
        if (Direction == direction.horizontal)
        {
           transform.Translate(Vector2.left * speed * Time.deltaTime);
       
        }
        else
        {
           transform.Translate(Vector2.up * speed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (other.Equals(colliders[i]))
            {
                speed *= -1;
            }
        }

        if (other.CompareTag("StopPlatform"))
            speed = 0;
    }


}
