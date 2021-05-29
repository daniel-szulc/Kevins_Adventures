using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.UI;

public class Enemy_Boss_01_Controller : EnemyWalking
{
    public int live = 10;

    public int Live => live;
    private Animator animator;
    public GameObject head;
    public GameObject stars;
    private Rigidbody2D _rigidbody2D;
    private bool onturn = false;
    private bool modeinProgress = false;
    private int mode = 0;
    public bool GroundCheck=true;
    public ParticleSystem particleSystem;
    public Slider hpSlider;
    public Image bossFace;
    public Sprite[] bossFaces=new Sprite[4];
    public w01l14manager _manager;
    public GameObject bludgeon;
    private bool dead = false;
    void Start()
    {
        speed = 3;
        distance = 0.5f;
        animator = GetComponentInChildren<Animator>();
        animator.enabled = true;
        stars.SetActive(false);
        ignoreGroundDetection = true;
        stopped = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        hpSlider.value=live;
        bossFace.sprite = bossFaces[0];
      
    }


    private void FixedUpdate()
    {
        if (mode == 1)
        {
            if (GroundCheck)
            {
                stopped = true;
            }
            else
            {
                stopped = false;
            }
        }
        
        

        if (live >= 7 && !modeinProgress  && !isBeingStomped)
        {
            StartCoroutine(walk(0));
        }
        else if (live >= 4 && !modeinProgress && !isBeingStomped)
        {
            StartCoroutine(angry01());
        }
        else if (live < 4 && !modeinProgress && !isBeingStomped)
        {
            StartCoroutine(angry02());
        }
    }

    void HPChange()
    {
        if (live>7)
        {
            bossFace.sprite = bossFaces[0];
            
        }
        else if (live>5)
        {
            bossFace.sprite = bossFaces[1];
        }
        else if (live>2)
        {
            bossFace.sprite = bossFaces[2];
        }
        else if (live>1)
        {
            bossFace.sprite = bossFaces[3];
        }

        if (live == 5)
        {
            _manager.ReleaseHeart();
        }
        if (live <= 0)
        {
            _manager.DestroyAllEnemies();
            _manager.endLevel();
        }
        if (live == 7)
        {
            GetComponent<SpriteResolver>().SetCategoryAndLabel("tired","cyclop");
        }
        else if (live == 4)
        {
            GetComponent<SpriteResolver>().SetCategoryAndLabel("weak","cyclop");
        }

        speed += 0.5f;

    }
    
    public override void Hurt()
    {
        if (!isBeingStomped)
        {
            stars.SetActive(true);
            base.Hurt();
            isBeingStomped = true;
            stopped = true;
            head.SetActive(false);
            ScoreManager.instance.EnemyCounter();
            animator.SetBool("hurt", true);
            if(live<=1)
            {
                animator.SetBool("dead", true);
                StartCoroutine(Dying());
            }
            // gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(WaitHurt());
            live--;
            hpSlider.value=live;
            HPChange();
        }
    }

    IEnumerator WaitHurt()
    {
        stopped = true;
        yield return new WaitForSeconds(2);
        stopped = true;
        yield return new WaitForSeconds(6.5f);
        if (!dead)
        {
            isBeingStomped = false;
            stopped = false;
            animator.SetBool("hurt", false);
            head.SetActive(true);
            stars.SetActive(false);
            head.GetComponent<EnemyHead>().isBeingStomped = false;
        }
    }

    void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10);
    }

    IEnumerator walk(int addTime)
    {
        mode = 0;
        modeinProgress = true;
        animator.SetBool("angry01", false);
        animator.SetBool("angry02", false);
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(addTime);
        modeinProgress = false;
    }

    IEnumerator Dying()
    {
        dead = true;
        isBeingStomped = true;
        stopped = true;
        animator.SetBool("angry01", false);
        animator.SetBool("angry02", false);
        yield return new WaitForSeconds(0.08f);
        bludgeon.SetActive(true);
        bludgeon.transform.parent = GetComponentInParent<Transform>();
        isBeingStomped = true;
        stopped = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.3f);
        
        animator.enabled = false;
    }
    
    IEnumerator angry01()
    {
        mode = 1;
        int count = 0;
        while (mode == 1 && count < 3)
        {
            modeinProgress = true;
            animator.SetBool("angry01", true);
            animator.Play("angry_1");
            yield return new WaitForSeconds(0.3f);
            if (isBeingStomped)
            {
                while (isBeingStomped)
                {
                   yield return new WaitForSeconds(0.3f); 
                }
            }
            Jump();
            yield return new WaitForSeconds(2);
            if (isBeingStomped)
            {
                while (isBeingStomped)
                {
                    yield return new WaitForSeconds(0.3f); 
                }
            }
            animator.SetBool("angry01", false);
            yield return new WaitForSeconds(2);
            count++;
        }

        mode = 0;
        stopped = false;
        StartCoroutine(walk(2));
    }

    IEnumerator angry02()
    {
        mode = 2;
        modeinProgress = true;
        animator.SetBool("angry01", false);
        animator.SetBool("angry02", true);
        animator.Play("angry_2");
        yield return new WaitForSeconds(2);
        modeinProgress = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("turn"))
        {
            if (!onturn)
            {
                StartCoroutine(CheckTurn());
            }

            onturn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("turn"))
        {
            onturn = false;
        }
    }

    IEnumerator CheckTurn()
    {
        yield return new WaitForSeconds(1);
        if (onturn)
        {
            Turn();
        }
    }

    public void Landing()
    {
        particleSystem.Play();
    }
   
}
