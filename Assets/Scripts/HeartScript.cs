using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public static HeartScript instance;
    public Animator[] heart;
    public int live=3;

    public bool fourthHeart = false;
   // public PlayerController player;
    void Start()
    {
     
        
        if (instance == null)
            instance = this;
        fourthHeart = _Level.fourthHeart;
        
        heart[0].Play("heart_get");
        if(live>1)
            heart[1].Play("heart_get");
        if (live > 2)
        {
            heart[2].Play("heart_get");
        }
        if (fourthHeart)
        {
            heart[3].gameObject.SetActive(true);
            heart[live].Play("heart_get");
            live++;
            if (live > 4)
            {
                live = 4;
            }
        }
        else
        {
            heart[3].gameObject.SetActive(false);
            if (live >= 4)
            {
                live = 3;
            }
        }


    }


    
    public void AddLive()
    {
        ScoreManager.instance.changeScores(300);
        switch (live)
        { case 0:
                live++;
                heart[0].Play("heart_get");
                break; 
        case 1:
             live++;
             heart[1].Play("heart_get");
            break;
        case 2:
            live++;
            heart[2].Play("heart_get");
            break;
        case 3:
            if (!fourthHeart)
            {
                ScoreManager.instance.changeScores(100);
            }
            else
            {
                live++;
                heart[3].Play("heart_get");
            }
            break;
        case 4:
        {
            if (fourthHeart)
            {
                ScoreManager.instance.changeScores(100);
            }
            break;
        }
        }
    }

    public void FullLive()
    {
        if(live<1)
            heart[0].Play("heart_get");
        if(live<2)
            heart[1].Play("heart_get");
        if(live<3)
            heart[2].Play("heart_get");
        if (live < 4 && fourthHeart)
        {
            heart[3].Play("heart_get");
        }
        if(!fourthHeart)
        live = 3;
        else
        {
            live = 4;
        }
    }
    

    public void Death()
    {
        heart[0].Play("heart_empty");
        heart[1].Play("heart_empty");
        heart[2].Play("heart_empty");
        if(fourthHeart)
            heart[3].Play("heart_empty");
        live = 0;
    }

    public void Alive()
    {
        heart[0].Play("heart full");
        heart[1].Play("heart_empty");
        heart[2].Play("heart_empty");
        if(fourthHeart)
            heart[3].Play("heart_empty");
        live = 1;
    }

    public void SubLive()
    {
        switch (live)
        {
            case 1:
                live=0;
                heart[0].Play("heart_lose");
                if(PlayerController.instance)
                PlayerController.instance.Death();
                break;
            case 2:
                live--;
                heart[1].Play("heart_lose");
                break;
            case 3:
                live--;
                heart[2].Play("heart_lose");
                break;
            case 4:
                live--;
                heart[3].Play("heart_lose");
                break;
        }
    }
}
