using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class slimeScript : MonoBehaviour
{
  public float _speed=3.5f;
  public ParticleSystem particledone;
  public GameObject particle;
  protected bool detected=false;
  protected AudioSource _audioSource;
  public AudioClip clip;
  private bool ended = false;
  private void Start()
  {
      StartCoroutine(Lifetime());
      if (_speed >= 0)
      {
          particledone.gameObject.GetComponent<Transform>().localScale=new Vector3(1,1,1);
      }
      else
      {
          particledone.gameObject.GetComponent<Transform>().localScale=new Vector3(-1,1,1);
      }

      _audioSource = GetComponent<AudioSource>();
  }
  
    private void Update()
    {
        if(!detected)
       gameObject.transform.localPosition =  new Vector3(gameObject.transform.localPosition.x+_speed*Time.deltaTime, gameObject.transform.localPosition.y);
    }


    private void OnCollisionEnter2D(Collision2D other)
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
           
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (other.gameObject.tag.Equals("Enemy"))
            {
                end();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                other.gameObject.GetComponent<Enemy>().Hurt();
                
            }
        }
    }
    
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
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
           else if (other.gameObject.tag.Equals("Enemy"))
            {
                end();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                if(other.gameObject.GetComponent<Enemy>()!=null)
                    other.gameObject.GetComponent<Enemy>().Hurt();
                else
                    other.gameObject.GetComponentInParent<Enemy>().Hurt();
            }

            else if (other.gameObject.tag.Equals("EnemyHead"))
            {
               end();
               gameObject.GetComponent<SpriteRenderer>().enabled = false;
               if( other.gameObject.GetComponentInParent<Enemy>()!=null)
                   other.gameObject.GetComponentInParent<Enemy>().Hurt();
               else
                   other.gameObject.GetComponentInParent<GameObject>().GetComponentInParent<Enemy>().Hurt();
                  }
            else if (other.gameObject.CompareTag("Bullet"))
            {
                end();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                ScoreManager.instance.changeScores(10);
            }
            

        }
    }

  public void end()
  {
      if (!ended)
      {
          foreach (var VARIABLE in GetComponents<CircleCollider2D>())
          {
              VARIABLE.enabled = false;
          }
          
          ended = true;
          detected = true;
          particle.SetActive(false);
          particledone.Play();
          _audioSource.spatialBlend = 0.9f;
          _audioSource.PlayOneShot(clip);
          gameObject.GetComponent<SpriteRenderer>().enabled = false;
      }
  }

  /*
  private void OnTriggerStay2D(Collider2D other)
  {
      
      if (other.gameObject.tag.Equals("Enemy"))
      {
          detected = true;
          particle.SetActive(false);
          particledone.Play();
          gameObject.GetComponent<SpriteRenderer>().enabled = false;
          other.gameObject.GetComponent<Enemy_01Controller>().Hurt();
      }
  }
  */
    


    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(10);
     Destroy(gameObject);   
    }
}
