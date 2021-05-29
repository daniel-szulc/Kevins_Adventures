using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_bomb_assistant_script : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Hurt();
        }
        
        if (other.gameObject.CompareTag("DirtBox"))
        {
            if (!other.gameObject.GetComponentInParent<DestroyBlock>().destroyed)
            {
                other.gameObject.GetComponentInParent<DestroyBlock>().Destroy();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (other.gameObject.tag.Equals("Enemy"))
        {
            
            if(other.gameObject.GetComponent<Enemy>()!=null)
                other.gameObject.GetComponent<Enemy>().Hurt();
            else
                other.gameObject.GetComponentInParent<Enemy>().Hurt();
        }

        if (other.gameObject.CompareTag("LivedEnemy"))
        {
            if(other.gameObject.GetComponent<Enemy>()!=null)
                other.gameObject.GetComponent<Enemy>().Hurt();
        }
        if (other.gameObject.tag.Equals("EnemyHead"))
        {
            if( other.gameObject.GetComponentInParent<Enemy>()!=null)
                other.gameObject.GetComponentInParent<Enemy>().Hurt();
            else
                other.gameObject.GetComponentInParent<GameObject>().GetComponentInParent<Enemy>().Hurt();
        }

        if (other.gameObject.CompareTag("Slime"))
        {
            other.gameObject.GetComponent<slimeScript>().end();
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<snowball_Script>().end();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("DirtBox"))
        {
            if (!other.gameObject.GetComponentInParent<DestroyBlock>().destroyed)
            {
                other.gameObject.GetComponentInParent<DestroyBlock>().Destroy();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        if (other.gameObject.tag.Equals("Enemy"))
        {
            
            if(other.gameObject.GetComponent<Enemy>()!=null)
                other.gameObject.GetComponent<Enemy>().Hurt();
            else
                other.gameObject.GetComponentInParent<Enemy>().Hurt();
        }

        if (other.gameObject.tag.Equals("EnemyHead"))
        {
            if( other.gameObject.GetComponentInParent<Enemy>()!=null)
                other.gameObject.GetComponentInParent<Enemy>().Hurt();
            else
                other.gameObject.GetComponentInParent<GameObject>().GetComponentInParent<Enemy>().Hurt();
        }

        if (other.gameObject.CompareTag("Slime"))
        {
            other.gameObject.GetComponent<slimeScript>().end();
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<snowball_Script>().end();
        }

        if (other.CompareTag("SlimeCollect"))
        {
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("CoinCollect"))
        {
            Destroy(other.gameObject);
        }
   
    }
}
