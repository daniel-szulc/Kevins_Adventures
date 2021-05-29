using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class millRotateScript : MonoBehaviour
{
    public float rotationSpeed = 40.0f; 
 
    void Update() {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
