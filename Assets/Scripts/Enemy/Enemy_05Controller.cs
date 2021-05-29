using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy_05Controller : EnemyWalking
{
    public Collider2D leftcol;
    public Collider2D rightcol;
    public Collider2D upcol;
    public Collider2D downcol;
    public GameObject head;
    public GameObject enemy;
    public float Setspeed = 1;

    public  enum direction {
        horizontal, vertical
    };
    public direction Direction;
    
    void Start()
    {
        ignoreGroundDetection = true;
        speed = Setspeed;

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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.Equals(leftcol) || other.Equals(rightcol))
        {
            stopped = true;
            StartCoroutine(WaitForTurn());

        }
        else if (other.Equals(downcol))
        {
            stopped = true;
            StartCoroutine(WaitForVerticalTurn(false));
        }
        else if(other.Equals(upcol))
        {
            stopped = true;
            StartCoroutine(WaitForVerticalTurn(true));
        }
        
        if (other.gameObject.CompareTag("FallDetector"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitForVerticalTurn(bool upcollider)
    {
        stopAnim = true;
        yield return new WaitForSeconds(Time.deltaTime*2);
        stopped = false;
        stopAnim = false;
        startAnim = true;

        if (upcollider)
        {
            speed = Setspeed*(-1);
        }
        else
        {
            speed = Setspeed;
        }
    }
    
    
    public override void Update()
    {
        if (Direction == direction.horizontal)
        {
            base.Update();
        }
        else
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
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
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            enemy.GetComponent<Animator>().Play("dying");
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
{yield return new WaitForSeconds(0.05f);
            enemy.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
            Vector3 FirstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
//enemy.transform.localScale=new Vector3(1,-1,1);
            while (transform.localPosition.y >= FirstPosition.y - 20f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x,
                    transform.localPosition.y - 5 * Time.deltaTime);
                yield return new WaitForSeconds(0.5f * Time.deltaTime);
            }
}
Destroy(gameObject);
    }
    
}
