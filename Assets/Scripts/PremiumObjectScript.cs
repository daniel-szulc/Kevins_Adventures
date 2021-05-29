using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PremiumObjectScript : MonoBehaviour
{
    public TextMeshProUGUI[] objects;
    void Start()
    {
        CheckBag();
    }

    void CheckBag()
    {
        objects[0].text =_Level.heartsPotion.ToString();
        objects[1].text = _Level.slimePack.ToString();
        objects[2].text = _Level.shield.ToString();
    }

    public void Health()
    {
        if (_Level.heartsPotion>0)
        {
            if ((HeartScript.instance.live < 3 && !_Level.fourthHeart) || (HeartScript.instance.live < 4 && _Level.fourthHeart))
            {
                HeartScript.instance.FullLive();
                _Level.heartsPotion--;
                CheckBag();
            }
        }
        
    }

    public void Slime()
    {
        if (_Level.slimePack > 0)
        {
            ScoreManager.instance.changeSlime(5);
            _Level.slimePack--;
            CheckBag();
        }
    }

    public void Shield()
    {
        if (PlayerController.instance.gameObject.GetComponentInChildren<playerProtectedScript>() == null && _Level.shield>0)
        {
            PlayerController.instance.GetComponentInChildren<playerProtectedScript>(includeInactive: true).ProtectionStart();
            _Level.shield--;
            CheckBag();
       }
      
    }
    
}
