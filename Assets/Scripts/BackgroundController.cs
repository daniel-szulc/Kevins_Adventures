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

    }
    void FixedUpdate()
    {

        float dist = (maincamera.transform.position.x * parallaxEffect);
        transform.position=new Vector3(startpos+dist, transform.position.y , transform.position.z);

    }
}
