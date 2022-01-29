using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{
   
    public Sprite box;
    public Sprite inactivebox;
    public bool isActive = true;
    private float bounceSpeed = 7f;
    private float bounceHight = 0.5f;
    public GameObject coinPrefab;
    public GameObject heartPrefab;
    public GameObject[] premiumObjPrefab;
    private GameObject coin;
    private GameObject heart;
    private GameObject premium;
    public int coinsAmount;
    public enum Collectobject{ Heart, Coin, Weight, Running, Shield, Magnet};
    private Vector2 originalPosition;
    private static int counter;
    public bool bounced=false;
    private IEnumerator bounceRoutine;
    [Range(-1, 1)] public int moveDirection = 1;
    private AudioSource _audioSource;
    public AudioClip audioCrash;
    

    void Start()
    {
        bounceRoutine = Bounce();
        _audioSource = gameObject.GetComponent<AudioSource>();
        originalPosition = transform.localPosition;
        if(isActive)
            GetComponent<SpriteRenderer>().sprite = box;
        else
            GetComponent<SpriteRenderer>().sprite = inactivebox;

        if (choose == Collectobject.Coin)
        {
            if (coinsAmount == 0)
                coinsAmount = 1;
            Coin();
        }
        else if(choose == Collectobject.Heart)
        {
            Heart();
            coinsAmount = 0;
        }
        else 
        {
            PremiumObj((int)choose);
            coinsAmount = 0;
        }
        if (coinsAmount>0)
            counter += coinsAmount-1;
    }

    public static int Counter => counter;
    
    void Update()
    {
    }

    private void Coin()
    {
        coin = Instantiate(coinPrefab, transform.localPosition, Quaternion.identity) as GameObject;
            coin.transform.parent = transform;
            coin.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            coin.GetComponentInChildren<ParticleSystem>().Stop();
            coin.transform.localPosition= new Vector3(0,0,0);
            coin.GetComponent<CircleCollider2D>().enabled = false;
            coinsAmount--;
        }

    private void Heart()
    {
        heart = Instantiate(heartPrefab, transform.localPosition, Quaternion.identity) as GameObject;
        heart.transform.parent = transform;
        heart.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        heart.GetComponentInChildren<ParticleSystem>().Stop();
        heart.GetComponent<CapsuleCollider2D>().enabled = false;
        heart.transform.localPosition= new Vector3(0,0,0);
    }

    void PremiumObj(int choosedObject)
    {
        premium = Instantiate(premiumObjPrefab[choosedObject-2], transform.localPosition, Quaternion.identity) as GameObject;
        premium.transform.parent = transform;
        premium.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        premium.GetComponent<CircleCollider2D>().enabled = false;
        premium.transform.localPosition= new Vector3(0,0,0);
    }
    
    private IEnumerator CoinCollect()
    {
        while (coin.transform.localPosition.y <= 1.2f)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + 7f * Time.deltaTime);
            yield return new WaitForSeconds(0.5f * Time.deltaTime);
        }
        coin.GetComponent<Coin>().Collect();
        if (coinsAmount > 0)
            Coin();
    }

    private IEnumerator HeartCollect()
    {
        while (heart.transform.localPosition.y <= 1.6f)
        {
            heart.transform.localPosition = new Vector2(heart.transform.localPosition.x, heart.transform.localPosition.y + 6f * Time.deltaTime);
            yield return new WaitForSeconds(0.5f * Time.deltaTime);
        }

        heart.GetComponent<HeartCollect>().movementdirection = moveDirection;

        heart.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        heart.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        heart.GetComponentInChildren<ParticleSystem>().Play();
        heart.GetComponent<CapsuleCollider2D>().enabled = true;

    }

    private IEnumerator PremiumCollect()
    {
        while (premium.transform.localPosition.y <= 1.6f)
        {
            premium.transform.localPosition = new Vector2(premium.transform.localPosition.x, premium.transform.localPosition.y + 6f * Time.deltaTime);
            yield return new WaitForSeconds(0.5f * Time.deltaTime);
        }

        premium.GetComponent<premiumCollect>().movementdirection = moveDirection;

        premium.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        premium.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        premium.GetComponent<CircleCollider2D>().enabled = true;
    }
    
    public Collectobject choose;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isActive && bounced==false)
            {

                StartCoroutine(Bounce());
                if(coinsAmount<=0)
                     isActive = false;
            }
        }
    }

    private IEnumerator Bounce()
    {
        bounced = true;
        _audioSource.PlayOneShot(audioCrash,1);
        while (transform.localPosition.y <= originalPosition.y + bounceHight)
        {
                 transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);
                 yield return new WaitForSeconds(0.5f * Time.deltaTime);
        }
        if(coinsAmount<=0)
            GetComponent<SpriteRenderer>().sprite = inactivebox;
        if (choose == Collectobject.Coin)
            StartCoroutine(CoinCollect());
        else if (choose == Collectobject.Heart)
            StartCoroutine(HeartCollect());
        else
        {
            StartCoroutine(PremiumCollect());
        }
        while (transform.localPosition.y > originalPosition.y)
        {
                  transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime);
                  yield return new WaitForSeconds(0.5f * Time.deltaTime); 
        }

        transform.localPosition = originalPosition;
        bounced = false;
        yield return null;
    }
    

}
