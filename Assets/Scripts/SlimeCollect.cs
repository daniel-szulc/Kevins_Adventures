using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCollect : MonoBehaviour
{
    private int slimeValue = 1;
    public ParticleSystem particle;
    public ParticleSystem particlestop;
    public bool collect = false;
    private Vector3 startscale;
    private Rigidbody2D rb;
    private Vector2 movement;
private float moveSpeed=5;
    private bool follow=false;
    private AudioSource audiosource;
    void Start()
    {
        startscale = transform.localScale;
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
            moveSlime(movement);
        }
    }

    void moveSlime(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position+(direction*moveSpeed*Time.deltaTime));
        moveSpeed += 0.3f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !collect)
        {
            Collect();
        }
        if (other.CompareTag("Magnet"))
        {
            if (gameObject.GetComponentInParent<QuestionBox>() == null)
            {
                follow = true;
            }
        }
        if (other.gameObject.CompareTag("FallDetector"))
        {
            Destroy(gameObject);
        }
    }

    
    
    public void Collect()
    {
        collect = true;
        audiosource.Play();
        ScoreManager.instance.changeSlime(slimeValue);
        ScoreManager.instance.changeScores(slimeValue*10);
        particle.Stop();
        particlestop.Play();
        StartCoroutine(Small());
    }
    
    private IEnumerator Small()
    {
        Vector3 scale = transform.localScale;
        float progress = 0f;
        
        while (progress <= 1f)
        {
            yield return new WaitForSeconds(0.03f);
            transform.localScale = Vector3.Lerp(transform.localScale, scale * 0f, progress);
            progress += Time.deltaTime*8f;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.localScale = startscale;
        
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
