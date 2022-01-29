using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Coin : MonoBehaviour
{
    public int coinValue = 1;
  
   public Collider2D colliderChild;
    public ParticleSystem particle;
    public ParticleSystem particlestop;
    public bool gravity = true;
    public bool collect = false;
    private Vector3 startscale;
    private IEnumerator grow;
    private Vector2 movement;
    private Rigidbody2D rb;
    float moveSpeed=5;
    private bool follow=false;
    private AudioSource audiosource;
    void Start()
    {
        if (gravity == false)
        {
          
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            colliderChild.enabled = false;
        }

        startscale = transform.localScale;
        grow = Grow();

        rb = this.GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
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
    }
    
    
    

    private void FixedUpdate()
    {
        if (follow)
        {
            moveCoin(movement);
        }
    }

    void moveCoin(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position+(direction*moveSpeed*Time.deltaTime));
                                   moveSpeed += 0.05f;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !collect)
        {
            Collect();
        }
        if (other.gameObject.CompareTag("FallDetector"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Magnet"))
            {
                if (gameObject.GetComponentInParent<QuestionBox>() == null)
                {
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

    public void Collect()
    {
        collect = true;
        audiosource.Play();
        ScoreManager.instance.changeCoins(coinValue);
        ScoreManager.instance.changeScores(coinValue*10);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        colliderChild.enabled = false;
        particle.Stop();
        StartCoroutine(grow);
        StartCoroutine(Small());
    }

    private IEnumerator Grow()
    {
        float progress = 0f;
        Vector3 scale = transform.localScale;
        while (progress <= 0.95f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scale * 1.2f, progress);
            progress += Time.deltaTime*6f;
            yield return new WaitForSeconds(0.006f);
        }
    }
    private IEnumerator Small()
    {
        particlestop.Play();
        yield return new WaitForSeconds(0.12f);
        StopCoroutine(grow);
        
       StopCoroutine(Grow());
        Vector3 scale = transform.localScale;
        float progress = 0f;
        
        while (progress <= 1f)
        {
            yield return new WaitForSeconds(0.03f);
            transform.localScale = Vector3.Lerp(transform.localScale, scale * 0f, progress);
            progress += Time.deltaTime*8f;
        }
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        transform.localScale = startscale;
        
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

}
