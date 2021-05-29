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

    // Update is called once per frame
    void Update()
    {
        if (Direction == direction.horizontal)
        {
           transform.Translate(Vector2.left * speed * Time.deltaTime);
          //  rb.MovePosition(transform.position + transform.right*speed*Time.deltaTime);
        }
        else
        {
           transform.Translate(Vector2.up * speed * Time.deltaTime);
          //  rb.MovePosition(transform.position + transform.up*speed*Time.deltaTime);
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




    /*
    void ConnectTo(Rigidbody2D character)
    {  
        SliderJoint2D joint = GetComponent<SliderJoint2D>();
        joint.connectedBody = character;
    }
    /*void OnCollisionEnter2D(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Halloo");
            ConnectTo(collision.collider.GetComponent<Rigidbody2D>());
        }
    }#1#

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ConnectTo(collision.collider.GetComponent<Rigidbody2D>());
        }
    }*/
}
