using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;





public class HeartCollect : MonoBehaviour
{

    public Collider2D colliderChild;
    public ParticleSystem particle;
    public ParticleSystem particlestop;
    public bool gravity = true;
    private bool collect = false;
    private Vector3 startscale;
    public float movementdirection=0f;
    private IEnumerator grow;
    private Rigidbody2D rb;
    private Vector2 movement;
    float moveSpeed=5;
    private bool follow=false;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        if (gravity == false)
        {
            colliderChild.enabled = false;
            rb.gravityScale = 0.0f;
        }
        startscale = transform.localScale;
        grow = Grow();
    }

    
    private void FixedUpdate()
    {
        if (follow)
        {
            moveHeart(movement);
        }
    }

    void moveHeart(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position+(direction*moveSpeed*Time.deltaTime));
        moveSpeed += 0.05f;
    }
    void Update()
    {
        if (follow)
        {
            
            Vector3 direction = PlayerController.instance.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x * Mathf.Rad2Deg);
            direction.Normalize();
            movement = direction;
        }
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementdirection*1.2f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !collect)
        {
            collect = true;
            HeartScript.instance.AddLive();
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            colliderChild.enabled = false;
            particle.Stop();
           
            StartCoroutine(grow);
            StartCoroutine(Small());

        }
        if (other.CompareTag("Magnet"))
        {
            if (gameObject.GetComponentInParent<QuestionBox>() == null)
            {
                colliderChild.enabled = false;
                rb.gravityScale = 0.0f;
                follow = true;
            }
        }
        
        if (other.gameObject.CompareTag("weight"))
        {
            if (gameObject.GetComponentInParent<QuestionBox>() == null)
            {
                gravity = true;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                colliderChild.enabled = true;
            }
        }
    }

    private IEnumerator Grow()
    {
        float progress = 0f;
        Vector3 scale = transform.localScale;
        while (progress <= 1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scale * 1.2f, progress);
            progress += Time.deltaTime*9f;
            yield return new WaitForSeconds(0.008f);
        }
    }
    private IEnumerator Small()
    {
        yield return new WaitForSeconds(0.12f);
        StopCoroutine(grow);
        Vector3 scale = transform.localScale;
        float progress = 0f;
        particlestop.Play();
        while (progress <= 1f)
        {
            yield return new WaitForSeconds(0.05f);
            transform.localScale = Vector3.Lerp(transform.localScale, scale * 0f, progress);
            progress += Time.deltaTime*6f;
            
        }
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        transform.localScale = startscale;
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}