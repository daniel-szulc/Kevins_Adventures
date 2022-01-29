using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    public bool dontDestroy = false;
    
    private static T m_Instance;


    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType<T>();

                if (m_Instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_Instance = singleton.AddComponent<T>();
                }
            }

            return m_Instance;


        }
    }


    public void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
            if (dontDestroy)
            {
                transform.parent = null;
                DontDestroyOnLoad((this.gameObject));
            }
        }
        else 
            Destroy(gameObject);
    }
}