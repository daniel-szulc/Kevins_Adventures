using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_collision : MonoBehaviour
{


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy") ||  other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("turn"))
    //     enemy.Turn();
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("turn"))
        {
             gameObject.GetComponentInParent<Enemy>().Turn();
            
        }
}

}
