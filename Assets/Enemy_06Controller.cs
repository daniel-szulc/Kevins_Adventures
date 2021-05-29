using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_06Controller : EnemyWalking
{
    public Collider2D upcol;
    public Collider2D downcol;
    public Collider2D slowcol;
    public Collider2D slowcol2;
    public GameObject head;
    public Transform net;
    public GameObject spider;
    private bool downdirect=true;
    public float lateStart = 0;

    void Start()
    {
        if (lateStart > 0)
        {
            speed = 0;
            StartCoroutine(LateStart(lateStart));
        }
        else
        {
            speed = -2.85f;
        }
    }

    public override void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private IEnumerator LateStart(float lateTime)
    {
        yield return new WaitForSeconds(lateTime);
        speed = -2.85f;
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.Equals(slowcol) && downdirect)
        {
            speed = -2;
        }
        if (other.Equals(slowcol2) && downdirect)
        {
            speed = -1;
            downdirect = false;
        }
        if (other.Equals(downcol))
        {
            stopped = true;
            StartCoroutine(WaitForVerticalTurn(false));
        }
        else if(other.Equals(upcol))
        {
            stopped = true;
            StartCoroutine(WaitForVerticalTurn(true));
            downdirect = true;
        }
        
        if (other.gameObject.CompareTag("FallDetector"))
        {
            Destroy(gameObject);
        }
    }
    
    
    IEnumerator WaitForVerticalTurn(bool upcollider)
    {
        stopAnim = true;
        speed = 0;
        yield return new WaitForSeconds(2);
        stopped = false;
        stopAnim = false;
        startAnim = true;

        if (upcollider)
        {
            speed = -2.85f;
        }
        else
        {
            speed = 1.2f;
        }
    }
    
    public override void Hurt()
    {
        if (!isBeingStomped)
        {
            base.Hurt();
            isBeingStomped = true;
            stopped = true;
            speed = 0;
            head.SetActive(false);
            ScoreManager.instance.EnemyCounter();
            net.localScale=new Vector3(0,0,0);
            GetComponent<Animator>().SetBool("dead", true);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
        {yield return new WaitForSeconds(0.05f);
            GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
            Vector3 FirstPosition = new Vector3(spider.transform.position.x, spider.transform.position.y, spider.transform.position.z);
            while (spider.transform.localPosition.y >= FirstPosition.y - 20f)
            {
                spider.transform.localPosition = new Vector2(spider.transform.localPosition.x,
                    spider.transform.localPosition.y - 7 * Time.deltaTime);
                yield return new WaitForSeconds(0.4f * Time.deltaTime);
            }
        }
        Destroy(gameObject);
    }
}
