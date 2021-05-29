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
        // StartCoroutine(PlayerDeath());
    }

    public void PlayerAlive()
    {
        freezeCamera = false;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (!freezeCamera)
        {
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y,
                transform.position.z);

            if (player.transform.localScale.x > 0f)
            {
                playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
            }
            else
            {
                playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
            }

            transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
        }
    }

    public void PlayerDying()
    {
        freezeCamera = true;
       // StartCoroutine(PlayerDeath());
    }

   /* private IEnumerator PlayerDeath()
    {
        freezeCamera = true;
        /*playerPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
        
        if (player.transform.localScale.x > 0f)
        {
            while (player.transform.localScale.x >= 0f)
            {
                playerPosition = new Vector3(playerPosition.x - offset, transform.position.y, playerPosition.z);

            }
            
        }
        else
        {
            while (player.transform.localScale.x <= 0f)
            {
                playerPosition = new Vector3(playerPosition.x + offset, transform.position.y, playerPosition.z);
            }
        }
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
        yield return null;
    }*/
    
}
