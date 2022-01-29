using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapWorldChanger : MonoBehaviour
{
    private CameraMapManager scriptParent;
    public void Start()
    {
        scriptParent = gameObject.GetComponentInParent<CameraMapManager>();
    }

    public void AllowCameraMove()
    {
        scriptParent.AllowCameraMove();
        gameObject.GetComponent<Animator>().Play("world changereturn");
    }

    public void UnactiveWorldChanger()
    {
        scriptParent.UnactiveWorldChanger();
        gameObject.SetActive(false);
        
    }
}
