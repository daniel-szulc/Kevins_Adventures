using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars_overhead_boss : MonoBehaviour
{
    public float rotationSpeed = 40.0f;
    private Vector3 scale;
    private void Start()
    {
        scale = transform.localScale;
    }

    void Update() {
     //   transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
     transform.Rotate(0, 1, 0, Space.Self);
    }

    IEnumerator StarsStart()
    {
        
        transform.localScale=new Vector3(0, 0);
        while (transform.localScale.x<scale.x)
        {
            transform.localScale += new Vector3(scale.x / 90, scale.y / 90, scale.z/90);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.localScale = scale;
    }
    
    IEnumerator StarsStop()
    {
        while (transform.localScale.x>0)
        {
            transform.localScale -= new Vector3(scale.x / 90, scale.y / 90, scale.z/90);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.localScale = new Vector3(0,0,0);
    }
}
