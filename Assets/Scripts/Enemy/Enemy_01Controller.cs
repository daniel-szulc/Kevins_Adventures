using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01Controller : EnemyWalking
{
   
    /*public Transform groundDetection;
    public bool ignoreGroundDetection=false;*/
    public GameObject head;

    
    /*public bool onGround;*/


    void Start()
    {
      
        speed = 1f;
        distance = 0.5f;
        gameObject.GetComponent<Animator>().enabled = true;

    }


    void FixedUpdate()
    {
        if (stopAnim)
        {
          //  gameObject.GetComponent<Animator>().Play("walk", 0,0);
          gameObject.GetComponent<Animator>().enabled = false;
            stopAnim = false;
        }
        else if(startAnim)
        {
           // gameObject.GetComponent<Animator>().Play("walk", 0,1);
           gameObject.GetComponent<Animator>().enabled = true;

            startAnim = false;
        }
    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
   
        /*if (other.gameObject.CompareTag("questBox"))
        {
            Debug.Log("collision enemy with quest");
            if( other.gameObject.GetComponent<QuestionBox>().bounced == true)
                Hurt();
        }*/

    }

    private void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.CompareTag("FallDetector"))
      {
          Destroy(gameObject);
      }
  }



    public override void Hurt()
    {
        
        if (!isBeingStomped)
        {
           
            base.Hurt();
            isBeingStomped = true;
            stopped = true;
            head.SetActive(false);
            ScoreManager.instance.EnemyCounter();
            gameObject.GetComponent<Animator>().Play("dead");
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(Dying());
        }
    }

    public IEnumerator Dying()
  {
      if (killedbybox)
      {yield return new WaitForSeconds(0.05f);
          gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
                        Vector3 FirstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
          while (transform.localPosition.y >= FirstPosition.y - 20f)
          {
              transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 5 * Time.deltaTime);
              yield return new WaitForSeconds(0.5f * Time.deltaTime);
          }
      }
      else
      {
          yield return new WaitForSeconds(0.5f);
          gameObject.GetComponentInChildren<ParticleSystem>().Play();
          float progress = 0f;
          Vector3 scale = transform.localScale;
          while (progress <= 1f)
          {
              yield return new WaitForSeconds(0.02f);
              transform.localScale = Vector3.Lerp(transform.localScale, scale * 0f, progress);
              progress += Time.deltaTime*1f;
          }
      }
      
      Destroy(gameObject);
  }
}

