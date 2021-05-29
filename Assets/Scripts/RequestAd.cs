using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestAd : MonoBehaviour
{

    public void VideoAd()
    {
        AdManager.instance.AdVideo();
    }

    public void BannerAd()
    {
        AdManager.instance.AdBanner();
    }

    public void DestroyBannerAd()
    {
        AdManager.instance.BannerHide();
    }
}
