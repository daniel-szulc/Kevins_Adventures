using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdBannerScript : MonoBehaviour
{
   public string gameID = "3463087";
   public string placementId = "banner";
   public bool testMode = false;

   void Start () {
      Advertisement.Initialize (gameID, testMode);
      Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);

   }

   public void BannerAd()
   {
      StartCoroutine (ShowBannerWhenReady ());

   }

   public void BannerHide()
   {
   Advertisement.Banner.Hide();   
   }
   
   IEnumerator ShowBannerWhenReady () {
      while (!Advertisement.IsReady (placementId)) {
         yield return new WaitForSeconds (0.5f);
      }
      Advertisement.Banner.Show (placementId);
   }
   
}
