using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireFollowPlayer : MonoBehaviour
{
    public GameObject followPoint;
    private Rigidbody2D rb;
    public float moveSpeed=1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = followPoint.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x * Mathf.Rad2Deg);
      //  direction.Normalize();
        rb.MovePosition((Vector2)transform.position + (direction*moveSpeed*0.0001f));
    }
    

}
