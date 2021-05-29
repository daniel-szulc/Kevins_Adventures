using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UNLOCK_ALL_LEVELS : MonoBehaviour
{
    public map_manager MapManager;

    void Start()
    {
       
    }

    public void UnlockALL()
    {
        for (int i = 0; i < 15; i++)
        {
            if (!_Level.Lvl[i].Unlocked)
            {
                _Level.Lvl[i].Unlocked = true;
            }
        }

        MapManager.CheckLevels();
    }
}