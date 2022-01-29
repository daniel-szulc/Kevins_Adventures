using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class story_script : MonoBehaviour
{
    public static bool StoryDone = false;
    private AsyncOperation asyncOperation;
    public GameObject s1, s2;
    public GameObject[] pictures = new GameObject[9];
    private Vector2 firstposition, endposition;
    private bool firstscene = true, buttonIsActive = false;
    public Animator[] animators = new Animator[3];
    Vector2[] endpos = new Vector2[] {new Vector2(-7,4.57f), new Vector2(7,4.57f), 
        new Vector2(-7, -0.291f), new Vector2(7, -0.291f), 
        new Vector2(0, -0.291f)};
    float progress=0f;
    public GameObject nextButton;
    // Start is called before the first frame update
    private IEnumerator co1, co2;
    public GameObject loading;
    void Start()
    {
        co1 = FirstStory();
        co2 = SecondStory();
        StartCoroutine(co1);
        if(!StoryDone)
            asyncOperation = SceneManager.LoadSceneAsync("W01L01");
        else
        {
            asyncOperation = SceneManager.LoadSceneAsync("map");
        }
        asyncOperation.allowSceneActivation = false;
    }


    private IEnumerator FirstStory()
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
        
        for (int i = 0; i < 3; i++)
        {
            progress = 0f;
            firstposition = pictures[i+4].transform.localPosition;
            while (progress <= 1f)
            {
                pictures[i+4].transform.localPosition = Vector2.Lerp(firstposition, endpos[i], progress);
                progress += Time.deltaTime;
                yield return new WaitForSeconds(0.005f);
            }
            pictures[i+4].transform.localPosition = endpos[i];
            yield return new WaitForSeconds(0.8f);
        }
        progress = 0f;
        firstposition = pictures[7].transform.localPosition;
        while (progress <= 1f)
        {
            pictures[7].transform.localPosition = Vector2.Lerp(firstposition, endpos[4], progress);
            progress += Time.deltaTime;
            yield return new WaitForSeconds(0.005f);
        }
        pictures[7].transform.localPosition = endpos[4];
      
        yield return new WaitForSeconds(0.8f);
        
        progress = 0f;
        firstposition = pictures[8].transform.localPosition;
        while (progress <= 1f)
        {
            pictures[8].transform.localPosition = Vector2.Lerp(firstposition, endpos[3], progress);
            progress += Time.deltaTime;
            yield return new WaitForSeconds(0.005f);
        }
        pictures[8].transform.localPosition = endpos[3];
        yield return new WaitForSeconds(0.8f);
        
        
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
            for (int i = 0; i < 4; i++)
            {
                pictures[i].transform.localPosition = endpos[i];
                
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
            pictures[4].transform.localPosition = endpos[0];
            pictures[5].transform.localPosition = endpos[1];
            pictures[6].transform.localPosition = endpos[2];
            pictures[7].transform.localPosition = endpos[4];
            pictures[8].transform.localPosition = endpos[3];
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
        StoryDone = true;
        _Level.Lvl[0].Unlocked = true;
        yield return new WaitForSeconds(1);
        asyncOperation.allowSceneActivation = true;
    }

}
