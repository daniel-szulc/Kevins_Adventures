using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public GameObject player;
    public float offset;
    private Vector3 playerPosition;
    public float offsetSmoothing;
    public bool freezeCamera = false;
    public Collider2D bounds;

    void Start()
    {
        if (instance == null)
            instance = this;
        freezeCamera = false;
    }
    public void PlayerDying()
    {
        freezeCamera = true;

    }

    public void PlayerAlive()
    {
        freezeCamera = false;
    }

    
    
}
