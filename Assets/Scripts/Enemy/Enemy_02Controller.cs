using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02Controller : Enemy
{
    void Start()
    {
    }

    public void ShowEnemy()
    {
        if(!isBeingStomped)
        StartCoroutine(Wait());
    }
    
    public IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(Random.Range(2.5f,5f));
        gameObject.GetComponent<Animator>().Play("plant_anim",-1,0f);
    }

    public override void Hurt()
    {
        if (!isBeingStomped)
        {
            base.Hurt();
            isBeingStomped = true;
            ScoreManager.instance.EnemyCounter();
            gameObject.GetComponent<Animator>().SetBool("Death", true);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

}
