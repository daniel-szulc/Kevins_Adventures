using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cloudScript : MonoBehaviour
{
    public GameObject[] clouds = new GameObject[5];

    public float speed=3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
       clouds[0].transform.localPosition =  new Vector3(clouds[0].transform.localPosition.x+0.18f, clouds[0].transform.localPosition.y);
      clouds[1].transform.localPosition =  new Vector3(clouds[1].transform.localPosition.x+0.21f, clouds[1].transform.localPosition.y);
           clouds[2].transform.localPosition =  new Vector3(clouds[2].transform.localPosition.x+0.20f, clouds[2].transform.localPosition.y+0.01f);
           clouds[3].transform.localPosition =  new Vector3(clouds[3].transform.localPosition.x+0.12f, clouds[3].transform.localPosition.y);
           clouds[4].transform.localPosition =  new Vector3(clouds[4].transform.localPosition.x+0.11f, clouds[4].transform.localPosition.y);



        //  cloud1.GetComponent<Transform>().localPosition = new Vector3(cloud1.transform.localPosition.x, cloud1.transform.localPosition.y);
    }

}
