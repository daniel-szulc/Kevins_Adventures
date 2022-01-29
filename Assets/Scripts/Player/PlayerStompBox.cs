using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStompBox : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("EnemyHead"))
        {
         
            Enemy enemy = other.gameObject.GetComponent<EnemyHead>().enemy;
            if (enemy != null)
            {
               PlayerController.instance.rigidBody.velocity = new Vector2(PlayerController.instance.rigidBody.velocity.x, 12f);
          
                                             enemy.Hurt();
            }
        }
    }
    

    

    
}
