using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckFPS : MonoBehaviour
{
    public List<float> fps_list = new List<float>();
    public GameObject fps_info;
    
    void Start()
    {
        StartCoroutine(Check_FPS());
        
    }


    IEnumerator Check_FPS()
    {
        yield return new WaitForSeconds(4);

        for (int i = 0; i < 12; i++)
        {
            while (Time.timeScale == 0)
            {
                yield return new WaitForSeconds(3);
            }
            
            fps_list.Add(1 / Time.deltaTime);
            yield return new WaitForSeconds(1f);
        }
        
        End();
        
    }
    

    void End()
    {
        float fps_all=0;
        foreach (var VARIABLE in fps_list)
        {
            fps_all += VARIABLE;
        }

        fps_all = fps_all / fps_list.Count;

        if (QualityScript.quality == 2)
        {
            if (fps_all < 26)
            {
                QualityScript.quality = 1;
                QualityScript.Check();
                StartCoroutine(ShowInfo());
            }
            
        }
    }

    IEnumerator ShowInfo()
    {

        string[] trans = new String[] {"the quality settings have been changed to improve the game performance", "Ustawienia jakości zostały zmienione, aby poprawić wydajność gry", "Die Qualitätseinstellungen wurden geändert, um die Spielleistung zu verbessern"};

        fps_info.GetComponentInChildren<TextMeshProUGUI>().text = trans[(int) LevelManager.lang];
        
        Vector3 info_local = fps_info.GetComponent<Transform>().localPosition;

        fps_info.GetComponent<Transform>().localPosition= new Vector3(info_local.x,info_local.y+250, info_local.z);
        fps_info.SetActive(true);
        while (fps_info.GetComponent<Transform>().localPosition.y>=info_local.y)
        {
            fps_info.GetComponent<Transform>().localPosition= new Vector3(info_local.x,fps_info.GetComponent<Transform>().localPosition.y-Time.deltaTime*120, info_local.z);
            yield return new WaitForSeconds(Time.deltaTime/3);
        }
        yield return new WaitForSeconds(3);
        
        while (fps_info.GetComponent<Transform>().localPosition.y<=info_local.y+259)
        {
            fps_info.GetComponent<Transform>().localPosition= new Vector3(info_local.x,fps_info.GetComponent<Transform>().localPosition.y+Time.deltaTime*90, info_local.z);
            yield return new WaitForSeconds(Time.deltaTime/3);
        }
        Destroy(fps_info);
        Destroy(this);
    }

}
