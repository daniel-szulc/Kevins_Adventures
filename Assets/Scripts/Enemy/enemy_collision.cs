using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_collision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("turn"))
        {
             gameObject.GetComponentInParent<Enemy>().Turn();
            
        }
}

}
