using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy04detector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<Enemy_04Controller>().PlayerDetect();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<Enemy_04Controller>().PlayerDetect();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameObject.GetComponentInParent<Enemy_04Controller>().playergone!=null)
                if(gameObject.GetComponentInParent<Enemy_04Controller>().playergone!=null)
                    StopCoroutine(gameObject.GetComponentInParent<Enemy_04Controller>().playergone);
            gameObject.GetComponentInParent<Enemy_04Controller>().playergone= StartCoroutine(gameObject.GetComponentInParent<Enemy_04Controller>().PlayerGone());
        }
    }
}
