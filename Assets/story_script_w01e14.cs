using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class story_script_w01e14 : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public GameObject[] pictures = new GameObject[10];
    private Vector2 firstposition, endposition;
    //Vector2[] endpos = new Vector2[] {new Vector2(-6.7f, 4.57f), new Vector2(6.7f, 4.57f), new Vector2(-6.7f, -4.57f), new Vector2(6.7f, -4.57f)};
    float progress = 0f;
    public GameObject nextButton;
    private bool firstscene = true;
    public GameObject loading;
    private IEnumerator coroutine;
    private bool buttonIsActive = false;
    public GameObject s1, s2;
    private IEnumerator co1, co2;
    void Start()
    {
        co1 = FirstStory();
        co2 = SecondStory();
        StartCoroutine(co1);
        asyncOperation = SceneManager.LoadSceneAsync("map");
        asyncOperation.allowSceneActivation = false;
    }

    private IEnumerator FirstStory()
    {
        for (int i = 0; i < 5; i++)
        {
            progress = 0f;
            firstposition = pictures[i].transform.localPosition;
            
            float x=0;
            float y=0;
            if (pictures[i].transform.localPosition.x > 0)
            {
                x = 6.7f;
            }
            else if (pictures[i].transform.localPosition.x < 0)
            {
                x = -6.7f;
            }
            else
            {
                x = 0;
            }
            if (pictures[i].transform.localPosition.y > 0)
            {
                y = 4.57f;
            }
            else if (pictures[i].transform.localPosition.y < 0)
            {
               y = -4.57f;
            }
            Vector2 endpos = new Vector2(x,y);
            
            while (progress <= 1f)
            {
                pictures[i].transform.localPosition = Vector2.Lerp(firstposition, endpos, progress);
                progress += Time.deltaTime;
                yield return new WaitForSeconds(0.005f);
            }
            pictures[i].transform.localPosition = endpos;
            yield return new WaitForSeconds(0.8f);
        }
               
        nextButton.SetActive(true);
        buttonIsActive = true;
        while (buttonIsActive)
        {
            yield return null;
        }
        nextButton.SetActive(false);
        s1.SetActive(false);
        s2.SetActive(true);
        firstscene = false;
        StartCoroutine(co2);
    }
    
    private IEnumerator SecondStory()
    {
        
        for (int i = 5; i < 10; i++)
        {
            progress = 0f;
            firstposition = pictures[i].transform.localPosition;
             
            float x=0;
            float y=0;
            if (pictures[i].transform.localPosition.x > 0)
            {
                x = 6.7f;
            }
            else if (pictures[i].transform.localPosition.x < 0)
            {
                x = -6.7f;
            }
            else
            {
                x = 0;
            }
            if (pictures[i].transform.localPosition.y > 0)
            {
                y = 4.57f;
            }
            else if (pictures[i].transform.localPosition.y < 0)
            {
                y = -4.57f;
            }
            Vector2 endpos = new Vector2(x,y);
            while (progress <= 1f)
            {
                pictures[i].transform.localPosition = Vector2.Lerp(firstposition, endpos, progress);
                progress += Time.deltaTime;
                yield return new WaitForSeconds(0.005f);
            }
            pictures[i].transform.localPosition = endpos;
            yield return new WaitForSeconds(0.8f);
        }

        nextButton.SetActive(true);
        buttonIsActive = true;
    }
    
    public void Next()
    {
        if (firstscene)
        {
            buttonIsActive = false;
        }
        else
        {
            StartCoroutine(LoadNewScene());
        }
    }

    public void Click()
    {
        if(firstscene && !buttonIsActive)
        {
            StopCoroutine(co1);
            for (int i = 0; i < 5; i++)
            {
                float x=0;
                float y=0;
                if (pictures[i].transform.localPosition.x > 0)
                {
                    x = 6.7f;
                }
                else if (pictures[i].transform.localPosition.x < 0)
                {
                    x = -6.7f;
                }
                else
                {
                    x = 0;
                }
                if (pictures[i].transform.localPosition.y > 0)
                {
                    y = 4.57f;
                }
                else if (pictures[i].transform.localPosition.y < 0)
                {
                   y = -4.57f;
                }
                Vector2 endpos = new Vector2(x,y);
                pictures[i].transform.localPosition = endpos;
            }
            nextButton.SetActive(true);
            buttonIsActive = true;
        }
        else if(firstscene && buttonIsActive)
        {
            StopCoroutine(co1);
            buttonIsActive = false;
            nextButton.SetActive(false);
            s1.SetActive(false);
            s2.SetActive(true);
            firstscene = false;
            StartCoroutine(co2);
        }
        else if(!firstscene && !buttonIsActive)
        {      
            StopCoroutine(co2);
            for (int i = 5; i < 10; i++)
            {
                float x;
                float y=0;
                if (pictures[i].transform.localPosition.x > 0)
                {
                    x = 6.7f;
                }
                else if (pictures[i].transform.localPosition.x < 0)
                {
                    x = -6.7f;
                }
                else
                {
                    x = 0;
                }
                if (pictures[i].transform.localPosition.y > 0)
                {
                    y = 4.57f;
                }
                else if (pictures[i].transform.localPosition.y < 0)
                {
                    y = -4.57f;
                }
                Vector2 endpos = new Vector2(x,y);
                pictures[i].transform.localPosition = endpos;
            }
            nextButton.SetActive(true);
            buttonIsActive = true;
        }
        else if (!firstscene && buttonIsActive)
        {
            StopCoroutine(co2);
            StartCoroutine(LoadNewScene());
        }
    }

    IEnumerator LoadNewScene()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(1);
        asyncOperation.allowSceneActivation = true;
    }
}
