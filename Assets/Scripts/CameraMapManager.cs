using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMapManager : MonoBehaviour
{
    public GameObject camera;
    public Transform[] map;
    public int actualWorld = 1;
    private bool isMoving = false;
    public GameObject changeWorld;
    private bool stopZoomCamera = false;
    private bool size = false;
    

    
    public void Right()
    {
        if (!isMoving)
        {
            isMoving = true;
            if (actualWorld == 1)
            {
                StartCoroutine(moveCamera(1, true));
                actualWorld = 2;
            }
            else if(actualWorld==2)
            {
                changeWorld.SetActive(true);
                actualWorld = 3;
                StartCoroutine(zoomCamera());
            }
            else if(actualWorld==3)
            {
                changeWorld.SetActive(true);
                actualWorld = 1;
                
            }
        }
    }

    public void Left()
    {
        if (!isMoving)
        {
            isMoving = true;
            if (actualWorld == 1)
            {
                actualWorld = 3;
             
               StartCoroutine(zoomCamera());
                changeWorld.SetActive(true);
            }
            else if(actualWorld == 2)
            {
                StartCoroutine(moveCamera(0, false));
                actualWorld = 1;
            }
            else if(actualWorld == 3)
            {
                actualWorld = 2;

                changeWorld.SetActive(true);
            }
        }
    }


    
    public void AllowCameraMove()
    {
       // camera.transform.localPosition = map[actualWorld - 1].transform.localPosition;
       if (size==true)
           camera.GetComponent<Camera>().orthographicSize = 15.70586f;
       size = false;
       stopZoomCamera = true;
        camera.transform.localPosition= new Vector2(map[actualWorld - 1].transform.localPosition.x, map[actualWorld - 1].transform.localPosition.y);
    }

    public void UnactiveWorldChanger()
    {
        changeWorld.SetActive(false);
        stopZoomCamera = false;
        isMoving = false;
    }

    public IEnumerator zoomCamera()
    {
        
        var camerasize = camera.GetComponent<Camera>();
        while (!stopZoomCamera && camerasize.orthographicSize>0)
        {
            camerasize.orthographicSize -= 10*Time.deltaTime;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
        }
        stopZoomCamera = false;
        camerasize.orthographicSize = 5;
    }
    

    public IEnumerator moveCamera(int mapNumber, bool right)
    {
      
        if (right)
        {
            while (camera.transform.localPosition.x <= map[mapNumber].localPosition.x)
            {
                camera.transform.localPosition = new Vector2(camera.transform.localPosition.x + 10 * Time.deltaTime,
                    camera.transform.localPosition.y);
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
        }
        else
        {
            while (camera.transform.localPosition.x >= map[mapNumber].localPosition.x)
            {
                camera.transform.localPosition = new Vector2(camera.transform.localPosition.x - 10 * Time.deltaTime,
                    camera.transform.localPosition.y);
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
        }
        isMoving = false;
    }
    
    public IEnumerator moveCameraWorld4(int mapNumber, bool right)
    {
        //Vector3 FirstPosition = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        var camerasize = camera.GetComponent<Camera>();
        if (right)
        {
            while (camera.transform.localPosition.x <= map[mapNumber].localPosition.x)
            {
                camera.transform.localPosition = new Vector2(camera.transform.localPosition.x + 18 * Time.deltaTime,
                    camera.transform.localPosition.y);
                if(camerasize.orthographicSize>5)
                {camerasize.orthographicSize -= 4*Time.deltaTime;}
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
        }
        else
        {
            while (camera.transform.localPosition.x >= map[mapNumber].localPosition.x)
            {
                camera.transform.localPosition = new Vector2(camera.transform.localPosition.x - 18 * Time.deltaTime,
                    camera.transform.localPosition.y);
                if(camerasize.orthographicSize<17)
                {camerasize.orthographicSize += 4*Time.deltaTime;}
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
        }
        isMoving = false;
    }
    
}
