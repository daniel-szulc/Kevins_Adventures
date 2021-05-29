using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenuplayer : MonoBehaviour
{
    public static mainmenuplayer instance;
    public float speed = 0;
    public float movement = 0;
    public Rigidbody2D rigidBody;
  private Animator playerAnimation;
    public bool isenemy;
    public GameObject thisobject;
    private bool MoveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        movement = 1f;
        speed = 600;
            if (instance == null)
                instance = this;

            rigidBody = GetComponent<Rigidbody2D>();
          playerAnimation = GetComponent<Animator>();
      
            //  MoveRight = true;
    }

    public void StartMove()
    {
        
        movement = 1f;
        speed = 480f;
        thisobject.SetActive(true);
    }

    public void StopMove()
    {
       // movement = 1f;
        speed = 480f;
        thisobject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (movement > 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
            //   transform.localScale = new Vector3(1,1,1);
        }
        else if (movement < 0f)
        {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.eulerAngles = new Vector3(0, -180, 0);
            // transform.localScale = new Vector3(-1,1,1);
        }*/

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        if (!isenemy)
        {
            playerAnimation.SetFloat("Speed", 1);
            playerAnimation.SetBool("OnGround", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
  
        if (other.gameObject.CompareTag("turn"))
        {
            /*Debug.Log("Triggered");
            if (movement > 0f)
                movement = -1f;
            else if (movement < 0f)
                movement = 1f;*/
            
            if (MoveRight == false)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                MoveRight = true; 
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                MoveRight = false;
            }
        }
        /*else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Triggered");
            if (movement > 0f)
                movement = -1f;
            else if (movement < 0f)
                movement = 1f;
        }*/
        
    }
    
}
