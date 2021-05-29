using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_04Controller : EnemyWalking
{
    public Animator _animator;
    private bool PlayerDetected;
    public Coroutine playergone;
    public GameObject enemycollison;
    public Transform hedgehogobject;
    
    
    void Start()
    {
     //   _animator = gameObject.GetComponentInChildren<Animator>();
        speed = 1;
        distance = 0.5f;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FallDetector"))
        {
            Destroy(gameObject);
        }
    }
    
    private void FixedUpdate()
    {
        if (stopped)
        {
            speed = 0;
        }
        if (ignoreWall)
        {
            enemycollison.SetActive(false);
        }
        else
        {
            enemycollison.SetActive(true);
        }

        if (!stopped)
        {
            speed = 1;
        }
    }

    public void _Turn()
    {
        stopped = true;
        StartCoroutine(WaitForTurn());
    }

    public void PlayerDetect()
    {

        PlayerDetected = true;
       // stopped = true;
        _animator.SetBool("playerdetect", true);
    }
    
    public IEnumerator PlayerGone()
    {
        
        PlayerDetected = false;
        yield return new WaitForSeconds(2);
        if (PlayerDetected == false)
        {
            _animator.SetBool("playerdetect", false);
          // stopped = false;
        }
    }

    public override void Hurt()
    {
        if (!isBeingStomped)
        {
            base.Hurt();
            isBeingStomped = true;
            stopped = true;
            ScoreManager.instance.EnemyCounter();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            _animator.SetBool("dead", true);
            gameObject.GetComponentInChildren<enemy04detector>().gameObject.SetActive(false);
            gameObject.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
            StartCoroutine(Dying());
        }
        
    }
    
    
    public IEnumerator Dying()
    {
       // Transform parent = GetComponentInParent<Transform>();
        if (killedbybox)
        {
            yield return new WaitForSeconds(0.05f);
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Foreground";
            Vector3 FirstPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            hedgehogobject.localScale = new Vector3(1,-1,1);
            while (transform.localPosition.y >= FirstPosition.y - 20f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 5 * Time.deltaTime);
                yield return new WaitForSeconds(0.5f * Time.deltaTime);
            }
        }
        else
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            float progress = 0f;
            Vector3 scale = hedgehogobject.localScale;
            while (progress <= 1f)
            {
                yield return new WaitForSeconds(0.02f);
                hedgehogobject.localScale = Vector3.Lerp(hedgehogobject.localScale, scale * 0f, progress);
                progress += Time.deltaTime*1f;
            }
        }
        Destroy(gameObject);
    }
}
