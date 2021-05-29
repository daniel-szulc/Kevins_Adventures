using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GiantSpikesMove : MonoBehaviour
{
    public float speed = 6.8f;
    private Rigidbody2D rigidBody;
    private bool stop = false;
    public Rigidbody2D[] gears;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        
                StartCoroutine(ChangeSlimeSpeed());
    }

    IEnumerator ChangeSlimeSpeed()
    {
        yield return new WaitForSeconds(Time.deltaTime*3);
        if (_Level.fastershooting<2)
        {
            PlayerController.instance.Slimespeed = 9;
            PlayerController.instance.timeBetweenSlimes = 10;
        }
        if (_Level.fastershooting<1)
        {
            PlayerController.instance.Slimespeed = 9;
        PlayerController.instance.timeBetweenSlimes = 20;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
      //  transform.TransformVector(speed+transform.position.x, transform.position.y, transform.position.z);
      //  transform.position = new Vector2( speed, transform.position.y);
      if(!stop)
      {rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            PlayerController.instance.Death();
        if (other.CompareTag("Finish"))
        {
            stop = true;
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;

            foreach (var VARIABLE in gears)
            {
                VARIABLE.bodyType = RigidbodyType2D.Dynamic;
                if (VARIABLE.GetComponent<Animator>() != null)
                {
                    VARIABLE.GetComponent<Animator>().enabled = false;
                }

            }
Random rand=new Random();
            for (int i = 0; i < gears.Length; i++)
            {
                gears[i].mass =(float)(rand.Next(5, 500))/10 ;
                
            }
        }
    }
}
