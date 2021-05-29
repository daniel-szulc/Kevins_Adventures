using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Enemy_07Controller : EnemyWalking
{
    public GameObject rotateBody;
    private Rigidbody2D rb_rotateBody;
    private GameObject parentObj;
    public float angle;
    public Transform body_legs;
    public GameObject head;
    private int live = 3;
    public Slider hpSlider;
    
    
    public Rigidbody2D RbRotateBody => rb_rotateBody;
    void Start()
    {
        rb_rotateBody = rotateBody.GetComponent<Rigidbody2D>();
        parentObj = GetComponentInParent<Transform>().gameObject;
        hpSlider.value=live;
    }

    // Update is called once per frame
    public override void Update()
    {
        if(!stopped){
        Vector3 direction = PlayerController.instance.gameObject.transform.position - transform.position;
         angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       rb_rotateBody.rotation = angle;

       if ((angle >= 90 && angle<=180)|| (angle < -90 && angle>=-180))
       {
           //transform.localScale = new Vector2(1,1);

          rotateBody.transform.localScale=new Vector3(1, 1,1);
          body_legs.localScale = new Vector2(1, 1);

         
            if ((angle >= (110) && angle <= 180) || (angle >= -110 && angle <= -180))
            {
                rb_rotateBody.rotation = angle;
            }
        }
        else  if ((angle <= 90 && angle>=0 )||( angle > -90 && angle<=0) )
        {
            //transform.localScale = new Vector2(1,-1);
           // rotateBody.transform.localRotation=new Quaternion(0,180, rb_rotateBody.rotation,0);
            body_legs.localScale = new Vector2(1, -1);
            rotateBody.transform.localScale=new Vector3(1, -1,1);

            if ((angle >= (-70) && angle <= 0) || (angle >= 0 && angle <= 70))
            {
                angle = (Mathf.Atan2(direction.y, direction.x)) * Mathf.Rad2Deg;
                rb_rotateBody.rotation = angle;

            }
        }}
        
   
    }



    public override void Hurt()
    {
        if (live >= 3)
        {
            hpSlider.gameObject.GetComponentInParent<Animator>().SetBool("hp_start", true);
           // hpSlider.gameObject.GetComponent<Animator>().Play("hp_bar_active");
        }
        if (live > 0)
        {
            base.Hurt();
            ScoreManager.instance.changeScores(10);
            HpHurt();
        }
    }



    void HpHurt()
    {
        live--;
        hpSlider.value=live;
        if (live <= 0 && !isBeingStomped)
        {
            isBeingStomped = true;
            ScoreManager.instance.EnemyCounter();
            Dead();
        }
    }

    void Dead()
    {
        GetComponent<Animator>().SetBool("dead",true);
        hpSlider.gameObject.GetComponentInParent<Animator>().SetBool("hp_start", false);
        hpSlider.gameObject.GetComponentInParent<Animator>().SetBool("dead", true);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        StartCoroutine(Dying());
        rotateBody.gameObject.SetActive(false);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FallDetector"))
        {
            Destroy();
        }
    }
    
    IEnumerator Dying()
    {
        stopped = true;
        if (GetComponentInChildren<Enemy07PlayerDetector>() != null)
        {
            GetComponentInChildren<Enemy07PlayerDetector>().stopped = true;
        }

        /*if (killedbybox)
        {
            transform.localScale=new Vector3(transform.localScale.x, transform.localScale.y*(-1), transform.localScale.z);
            yield return new WaitForSeconds(0.05f);
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
            Vector3 FirstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            while (transform.position.y >= FirstPosition.y - 20f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x,
                    transform.localPosition.y - 5 * Time.deltaTime);
                yield return new WaitForSeconds(0.3f * Time.deltaTime);
            }
        }
        else
        {*/
        yield return new WaitForSeconds(1.5f);
            
        Destroy();
    }

    void particlePlay()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    void Destroy()
    {
        Destroy(GetComponentInParent<Transform>().gameObject);
    }
}
