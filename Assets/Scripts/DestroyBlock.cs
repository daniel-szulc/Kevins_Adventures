using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public bool destroyed =false;

    private AudioSource _audioSource;

    public AudioClip audioCrash;
    // Update is called once per frame
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      
            if (other.tag == "Player")
            {
                Destroy();
            }
        
    }

    public void Destroy()
    {
        if (!destroyed)
        {
            _audioSource.PlayOneShot(audioCrash,0.95f);
            destroyed = true;
            if (gameObject.GetComponent<Animator>() != null)
            {
                gameObject.GetComponent<Animator>().enabled = false;
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            ScoreManager.instance.changeScores(1);
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Time.deltaTime);
       foreach (BoxCollider2D collider in gameObject.GetComponents<BoxCollider2D>())
        {collider.enabled = false;}
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    
    public IEnumerator DestroybyUnstableBox()
    {
        foreach (BoxCollider2D collider in gameObject.GetComponents<BoxCollider2D>())
        {collider.enabled = false;}
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    
}
