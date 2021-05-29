using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsScript : MonoBehaviour
{
   public void Patreon()
   {
      Application.OpenURL("https://www.patreon.com/DanielSzulc/");
   }

   public void Facebook()
   {
      _Level.instance.Facebook();
   }
}
