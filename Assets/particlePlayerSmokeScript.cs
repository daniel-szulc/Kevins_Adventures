using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlePlayerSmokeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(counter());
    }

    IEnumerator counter()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
