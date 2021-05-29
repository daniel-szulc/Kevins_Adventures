using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fire_Controller  : EnemyWalking
{
    public BoxCollider2D downCol;
    public BoxCollider2D upCol;
    public float Setspeed = 5;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        stopped = false;
        gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
        gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezePositionX;
        speed = Setspeed;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ignoreGroundDetection = true;
    }

    public override void Update()
    {
        if (!stopped)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    IEnumerator Fire(bool direction)
    {
      
        yield return new WaitForSeconds(Time.deltaTime*1.8f);

        if (direction)
        {
            speed = Setspeed*(-1.15f);
            _spriteRenderer.flipY = false;
        }
        else
        {
            yield return new WaitForSeconds(Time.deltaTime*50);
            speed = Setspeed;
            _spriteRenderer.flipY = true;
        }
        stopped = false;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.Equals(downCol))
        {
      
            stopped = true;
            StartCoroutine(Fire(false));
        }
        else if (other.Equals(upCol))
        {
    
            stopped = true;
            StartCoroutine(Fire(true));
        }
    }


    IEnumerator Dying()
    {
        yield return new WaitForSeconds(Time.deltaTime*1);
        stopped = false;
        speed = Setspeed*(-2.6f);
        _spriteRenderer.flipY = false;
    }
    
    
    public override void Hurt()
    {
        if (!isBeingStomped)
        {
            base.Hurt();
            isBeingStomped = true;
            ScoreManager.instance.EnemyCounter();
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(Dying());
        }
    }
}
