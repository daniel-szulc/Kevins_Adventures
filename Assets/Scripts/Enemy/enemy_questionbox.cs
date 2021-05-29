using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_questionbox : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("questBox"))
        {
            if (other.gameObject.GetComponent<QuestionBox>().bounced == true)
                {
                    gameObject.GetComponentInParent<EnemyWalking>().killedbybox = true;
                    gameObject.GetComponentInParent<EnemyWalking>().Hurt();
                }
            
        }
        if(other.gameObject.CompareTag("DirtBox"))
        {
            if ( other.gameObject.GetComponentInParent<DestroyBlock>().destroyed == true )
            {
                gameObject.GetComponentInParent<EnemyWalking>().killedbybox = true;
                gameObject.GetComponentInParent<EnemyWalking>().Hurt();
            }
            
        }
    }
}
