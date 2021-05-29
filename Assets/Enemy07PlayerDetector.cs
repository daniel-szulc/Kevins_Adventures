using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy07PlayerDetector : MonoBehaviour
{
    private bool waitForAttack = false;
    private GameObject snow;
    public float snowspeed = 12f;
    public GameObject snowPrefab;
    public GameObject snowParent;
    private bool detected = false;
    private float timer;
    private double randomTimer;
    public Transform targetObj;
    public bool stopped = false;
    static Random random = new Random();

    double Random()
    {
        return random.NextDouble() * (7) + 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !detected)
        {
            detected = true;
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        while (true)
            {
                if (!stopped)
                {
                 
                    
                    Vector3 direction = PlayerController.instance.gameObject.transform.position - transform.position;
                   
              
                    float angle = GetComponentInParent<Enemy_07Controller>().RbRotateBody.rotation;

                    snow = Instantiate(snowPrefab,
                        new Vector3(snowParent.transform.position.x, snowParent.transform.position.y - 0.1f),
                        Quaternion.identity) as GameObject;
                    snowball_Script thisSnow = snow.GetComponent<snowball_Script>();
                    thisSnow.direction = direction;
                    thisSnow.moveSpeed = snowspeed;
                    thisSnow.rb.rotation = angle;
                    thisSnow.target = new Vector2(targetObj.position.x, targetObj.position.y);
                    yield return new WaitForSeconds((float) Random());
                }

                if (stopped)
                    break;
            }
    }
}

