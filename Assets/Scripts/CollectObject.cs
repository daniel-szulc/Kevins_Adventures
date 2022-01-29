using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    
    public Collider2D colliderChild;
    public ParticleSystem particle;
    public ParticleSystem particlestop;
    public bool gravity = true;
    
    void Start()
    {
        if (gravity == false)
        {
            colliderChild = GetComponentInChildren<Collider2D>();
            colliderChild.enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        }
    }
    
    



    public IEnumerator Break()
    {
        yield return new WaitForSeconds(2*Time.deltaTime);
        particlestop.Stop();
        particle.Play();
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
