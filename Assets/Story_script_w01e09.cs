using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_script_w01e09 : MonoBehaviour
{
    public static bool StoryDone = false;
    private AsyncOperation asyncOperation;
    public GameObject[] pictures = new GameObject[3];
    private Vector2 firstposition, endposition;
    Vector2[] endpos = new Vector2[] {new Vector2(-6.7f, 4.57f), new Vector2(6.7f, 4.57f), new Vector2(-6.7f, -4.57f), new Vector2(6.7f, -4.57f)};
    float progress = 0f;
    public GameObject nextButton;
    public GameObject loading;
    private IEnumerator coroutine;
    private bool buttonIsActive = false;
    public TextMeshProUGUI[] castle;
    void Start()
    {
        coroutine = Story();
        StartCoroutine(coroutine);
        
        String[] castleTrans = {"CASTLE", "ZAMEK", "BURG"};
        foreach (var VARIABLE in castle)
        {
            VARIABLE.text = castleTrans[(int) LevelManager.lang];
        }

        if (!StoryDone)
        {
            asyncOperation = SceneManager.LoadSceneAsync("W01L10");
            _Level.actualLevel = 9;
        }
        else
        {
            asyncOperation = SceneManager.LoadSceneAsync("map");
        }
        asyncOperation.allowSceneActivation = false;
    }

    private IEnumerator Story()
    {
        for (int i = 0; i < 4; i++)
        {
            progress = 0f;
            firstposition = pictures[i].transform.localPosition;
            while (progress <= 1f)
            {
                pictures[i].transform.localPosition = Vector2.Lerp(firstposition, endpos[i], progress);
                progress += Time.deltaTime;
                yield return new WaitForSeconds(0.005f);
            }

            pictures[i].transform.localPosition = endpos[i];
            yield return new WaitForSeconds(0.8f);
        }

        nextButton.SetActive(true);
        buttonIsActive = true;
    }

    public void Next()
    {
        StartCoroutine(LoadNewScene());
    }

    IEnumerator LoadNewScene()
    {
        loading.SetActive(true);
        StoryDone = true;
        _Level.Lvl[9].Unlocked = true;
        yield return new WaitForSeconds(1);
        asyncOperation.allowSceneActivation = true;
    }


    public void Click()
    {
        if (!buttonIsActive)
        {
    StopCoroutine(coroutine);
        for (int i = 0; i < 4; i++)
        {
    pictures[i].transform.localPosition = endpos[i];
        }
        nextButton.SetActive(true);
        buttonIsActive = true;
        }
        else
        {
            StartCoroutine(LoadNewScene());
        }
    }
}
