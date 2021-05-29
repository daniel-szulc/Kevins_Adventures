using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class premiumCollect : MonoBehaviour
{
    public Collider2D colliderChild;
    private bool collect = false;
    private Vector3 startscale;
    public float movementdirection=0f;
    private IEnumerator grow;
    private Rigidbody2D rb;
    private Vector2 movement;
    float moveSpeed=5;
    private bool follow=false;
    public enum Typeobject{ Weight, Running, Shield, Magnet};

    public GameObject weightObject;

    public Typeobject choose;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        startscale = transform.localScale;
        grow = Grow();
        
    }

    void collectObj()
    {
        switch ((int) choose)
        {
            case 0:
                weightObject.SetActive(true);
                break; 
            case 1:
              PlayerController.instance.gameObject.GetComponentInChildren<PlayerRunningScript>(includeInactive: true).RunningStart();

             
                break;
            case 2:
                PlayerController.instance.gameObject.GetComponentInChildren<playerProtectedScript>(includeInactive: true).ProtectionStart();
                break;
            case 3:
                PlayerController.instance.gameObject.GetComponentInChildren<PlayerMagnetScript>(includeInactive: true).MagnetStart();
                break;

        }
    }
    
    private void FixedUpdate()
    {
        if (follow)
        {
            moveObject(movement);
        }
    }
    void moveObject(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position+(direction*moveSpeed*Time.deltaTime));
    }
    void Update()
    {
        if (follow)
        {
            
            Vector3 direction = PlayerController.instance.transform.localPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x * Mathf.Rad2Deg);
            direction.Normalize();
            movement = direction;
        }
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementdirection*1.2f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !collect)
        {
            collect = true;
            ScoreManager.instance.changeScores(150);
            collectObj();
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            colliderChild.enabled = false;
            StartCoroutine(grow);
            StartCoroutine(Small());

        }
        if (other.CompareTag("Magnet"))
        {
            if (gameObject.GetComponentInParent<QuestionBox>() == null)
            {
                colliderChild.enabled = false;
                rb.gravityScale = 0.0f;
                follow = true;
            }
            else 
            {
                if (gameObject.GetComponentInParent<QuestionBox>().isActive == false)
                {
                    follow = true;
                    colliderChild.enabled = false;
                    rb.gravityScale = 0.0f;
                }
            }
        }
    }
    private IEnumerator Grow()
    {
        float progress = 0f;
        Vector3 scale = transform.localScale;
        while (progress <= 1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scale * 1.2f, progress);
            progress += Time.deltaTime*9f;
            yield return new WaitForSeconds(0.008f);
        }
    }
    private IEnumerator Small()
    {
        yield return new WaitForSeconds(0.12f);
        StopCoroutine(grow);
        Vector3 scale = transform.localScale;
        float progress = 0f;
        while (progress <= 1f)
        {
            yield return new WaitForSeconds(0.05f);
            transform.localScale = Vector3.Lerp(transform.localScale, scale * 0f, progress);
            progress += Time.deltaTime*6f;
            
        }
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        transform.localScale = startscale;
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
