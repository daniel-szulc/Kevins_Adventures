using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class snowball_Script : MonoBehaviour
{
    public ParticleSystem particledone;
    public GameObject particle;
    protected bool detected=false;
    protected AudioSource _audioSource;
    public AudioClip clip;
    public AudioClip snowballStart;
    public Vector2 direction;
    public float moveSpeed;
    public Rigidbody2D rb;
    private GameObject snowball;
    public Vector2 target;
    private bool ended = false;

    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        StartCoroutine(Lifetime());
        _audioSource = GetComponent<AudioSource>();
        snowball = GetComponentInChildren<SpriteRenderer>().gameObject;
        _audioSource.PlayOneShot(snowballStart);
    }



    void Update()
    {
    //    rb.MovePosition((Vector2)transform.position+(direction*moveSpeed*Time.deltaTime));
    if(!detected)
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

/*private void OnCollisionEnter2D(Collision2D other)
    {
        if (!detected)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                end();
                if (other.gameObject.GetComponent<DestroyBlock>() != null)
                {
              
                    other.gameObject.GetComponent<DestroyBlock>().Destroy();
                }
           
                snowball.SetActive(false);
            }
            else if (other.gameObject.tag.Equals("Player") &&  !other.gameObject.GetComponent<PlayerController>().HurtBlink)
            {  
          
                end();
                snowball.SetActive(false);
                other.gameObject.GetComponent<PlayerController>().Hurt();
            
            }

        }
    }*/
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!detected)
        {
            if (other.gameObject.CompareTag("DirtBox"))
            {
                if (!other.gameObject.GetComponentInParent<DestroyBlock>().destroyed)
                {
                    end();
                    other.gameObject.GetComponentInParent<DestroyBlock>().Destroy();
                    snowball.SetActive(false);
                }
            }
            else if (other.gameObject.CompareTag("Ground"))
            {
                end();
                if (other.gameObject.GetComponent<DestroyBlock>() != null)
                {
              
                    other.gameObject.GetComponent<DestroyBlock>().Destroy();
                }
           
                snowball.SetActive(false);
            }
           else if (other.gameObject.tag.Equals("Player")&&  !other.gameObject.GetComponent<PlayerController>().HurtBlink)
            {
     
                end();
                snowball.SetActive(false);
 
                other.gameObject.GetComponent<PlayerController>().Hurt();
            }
            else if (other.gameObject.CompareTag("Slime"))
            {
                end();
                snowball.SetActive(false);
            }
            else if (other.gameObject.CompareTag("protectionShield"))
            {
                end();
                snowball.SetActive(false);
            }

        }
        
    }
    
    public void end()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<CircleCollider2D>().enabled = false;
            ended = true;
            snowball.SetActive(false);
            detected = true;
            particle.SetActive(false);
            particledone.Play();
            _audioSource.spatialBlend = 0.9f;
            _audioSource.PlayOneShot(clip);
        }
    
    
    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);   
    }
}
