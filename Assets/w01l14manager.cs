using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w01l14manager : MonoBehaviour
{
    public GameObject heart;
    public GameObject touchlock;
    private bool end;
    public GameObject collectObj;
    private static bool collected = false;
    public endLevelScript _endLevelScript;
    void Start()
    {
        heart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endLevel()
    {
        end = true;
        PlayerController.instance.movement = 0;
        touchlock.SetActive(true);
        StartCoroutine(waitForEnd());
    }

    IEnumerator waitForEnd()
    {
        yield return new WaitForSeconds(3);
        LevelManager.characterUnlocked[1] = true;
        if(!_Level.Lvl[13].Finished && !collected)
        {
        collectObj.SetActive(true);
        _Level.diamonds++;
        collected = true;
        }
        else
        {
            _endLevelScript.StartEnd();
        }
    }

    public void CollectClick()
    {
        collectObj.GetComponent<Animator>().Play("boxAnimOUT");
        _endLevelScript.StartEnd();
    }

    public void ReleaseHeart()
    {
        heart.SetActive(true);
        StartCoroutine(DestroyHeart());
    }

    IEnumerator DestroyHeart()
    {
        yield return new WaitForSeconds(10);
        if (heart != null)
        {
            Destroy(heart);
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (var VARIABLE in FindObjectsOfType<Enemy_01Controller>())
        {
            if (!VARIABLE.isBeingStomped)
            {
                VARIABLE.isBeingStomped = true;
                VARIABLE.stopped = true;
                VARIABLE.head.SetActive(false);
                VARIABLE.gameObject.GetComponent<Animator>().Play("dead");
                VARIABLE.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                VARIABLE.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine(VARIABLE.Dying());
            }
        }
        foreach (var VARIABLE in FindObjectsOfType<Enemy_03Controller>())
        {
            if (!VARIABLE.isBeingStomped)
            {
                VARIABLE.isBeingStomped = true;
                VARIABLE.stopped = true;
                VARIABLE.head.SetActive(false);
                VARIABLE.hided = true;
                VARIABLE._animator.SetBool("hide", true);
                VARIABLE.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                StartCoroutine(VARIABLE.Dying());
            }
        }
        foreach (var VARIABLE in FindObjectsOfType<Enemy_04Controller>())
        {
            if (!VARIABLE.isBeingStomped)
            {
                VARIABLE.isBeingStomped = true;
                VARIABLE.stopped = true;
                VARIABLE.gameObject.GetComponent<Animator>().Play("dead");
                VARIABLE.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                VARIABLE.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine(VARIABLE.Dying());
            }
        }
    }
}
