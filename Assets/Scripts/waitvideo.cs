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
        AsyncOperation async = SceneManager.LoadSceneAsync("MainMenu");
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(7);
        async.allowSceneActivation = true;
    }
}
