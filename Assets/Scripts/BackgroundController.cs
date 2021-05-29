using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float length, startpos;
    public GameObject maincamera;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
       // length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
      //  float temp = (maincamera.transform.position.x * (1 - parallaxEffect));
        float dist = (maincamera.transform.position.x * parallaxEffect);
        transform.position=new Vector3(startpos+dist, transform.position.y , transform.position.z);

        /*if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;*/
    }
}
