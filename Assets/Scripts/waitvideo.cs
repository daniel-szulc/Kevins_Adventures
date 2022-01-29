using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waitvideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        //Script has been removed due security reasons.
        yield return null;
    }
}
