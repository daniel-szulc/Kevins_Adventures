using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_03Controller : EnemyWalking
{
    public Animator _animator;
    private bool PlayerDetected;
    public Coroutine playergone;
    public bool hided=false;
    public GameObject head;
    private Coroutine wait;
    public GameObject enemycollison;
    private bool angryMode = false;
    public AudioClip audioHurtPunch;

    void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        speed = 0.2f;
        distance = 0.5f;
  
    }

    private void FixedUpdate()
    {
        if (stopped)
        {
            speed = 0;
        }
        else
        {
            if (angryMode)
            {
                speed = 1.6f;
            }
            else
            {
                speed = 0.2f;
            }
        }
        if (ignoreWall)
        {
            enemycollison.SetActive(false);
        }
        else
        {
            enemycollison.SetActive(true);

        }
        if (stopAnim)
        {
            //  gameObject.GetComponent<Animator>().Play("walk", 0,0);
            _animator.enabled = false;

            stopAnim = false;
        }
        else if(startAnim)
        {
           // speed = 0.2f;
            // gameObject.GetComponent<Animator>().Play("walk", 0,1);
           _animator.enabled = true;
           startAnim = false;
        }
        
    }

    public void PlayerDetect(bool rightside)
    {

        PlayerDetected = true;
        angryMode = true;
        ignoreWall = true;
        ignoreGroundDetection = true;
        _animator.SetBool("playerdetected", true);
        speed = 1.6f;
        if (rightside == MoveRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            MoveRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            MoveRight = true;
        }
    }

    public IEnumerator PlayerGone()
    {
        PlayerDetected = false;
        
        yield return new WaitForSeconds(1);
        ignoreWall = false;
        ignoreGroundDetection = false;
        yield return new WaitForSeconds(4);
        if (PlayerDetected == false)
        {
          
            _animator.SetBool("playerdetected", false);
            yield return new WaitForSeconds(1.2f);
            speed = 0.2f;
            angryMode = false;
        }
    }

    public override void Hurt()
    { 
        if (!isBeingStomped)
        {base.Hurt();
            speed = 0;
            _animator.SetBool("hide", true);
            isBeingStomped = true;
            head.SetActive(false);
            stopped = true;
        if (!hided)
        {base.Hurt();
            hided = true;
            wait=StartCoroutine(Wait());
        }
        else
        {  
            if(wait!=null)
            StopCoroutine(wait);
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioHurtPunch,1 );
                ScoreManager.instance.EnemyCounter();
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                StartCoroutine(Dying());
        }

        if (killedbybox)
        {
            base.Hurt();
            hided = true;
            if(wait!=null)
            StopCoroutine(wait);
            ScoreManager.instance.EnemyCounter();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            StartCoroutine(Dying());
        }
        
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.39f);
        isBeingStomped = false;
        head.SetActive(true);
        yield return  new WaitForSeconds(5);
        _animator.SetBool("hide", false);
        yield return new WaitForSeconds(2f);
        hided = false;
            speed = 0.2f;
        stopped = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FallDetector"))
        {
            Destroy(gameObject);
        }
    }
    
    public IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.05f);
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Foreground";
            Vector3 FirstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            while (transform.localPosition.y >= FirstPosition.y - 20f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 5 * Time.deltaTime);
                yield return new WaitForSeconds(0.5f * Time.deltaTime);
            }

            Destroy(gameObject);
    }
    
}
