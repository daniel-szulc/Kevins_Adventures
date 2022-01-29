using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyWalking : Enemy
{
    public float speed;
    protected float distance;
    protected bool MoveRight = false;
    public Transform groundDetection;
    public bool ignoreGroundDetection=false;
    public bool stopped = false;
    public bool ignoreWall = false;
    public bool killedbybox=false;
    public bool stopAnim = false;
    public bool startAnim = false;
    
    

    

    public virtual void Update()
    {
        if (!stopped)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (!ignoreGroundDetection)
            {
                int layer_mask = LayerMask.GetMask("Ground");
                RaycastHit2D groundInfo =
                    Physics2D.Raycast(groundDetection.position, Vector2.down, distance, layer_mask);
                if (groundInfo.collider == false)
                {
                    stopped = true;
                    StartCoroutine(WaitForTurn());
                }
            }
        }
        

  
    }


    

    public override void Turn()
    {
        if (!ignoreGroundDetection || !stopped || !ignoreWall)
        {
            stopped = true;
            StartCoroutine(WaitForTurn());
        }
    }

    public IEnumerator WaitForTurn()
    {
        stopAnim = true;
        yield return new WaitForSeconds(Time.deltaTime*2);
        stopped = false;
        stopAnim = false;
        startAnim = true;
        if (MoveRight == false)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            MoveRight = true; 
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            MoveRight = false;
        }
    }
}
