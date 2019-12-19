using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using admob;


public class AdmobAdManager : MonoBehaviour {

    public static AdmobAdManager instance;
    // Use this for initialization
    
  //  string appId = "ca-app-pub-7034086702288798~1062205000";

    string appId = "ca-app-pub-3940256099942544~3347511713";//test

    //   string bannerId = "ca-app-pub-7034086702288798/7436041669";
    string bannerId = "ca-app-pub-7034086702288798/7436041669"; //test
    string rewardId = "ca-app-pub-7034086702288798/2545132478";
    string interstellerId = "ca-app-pub-7034086702288798/8254013900";
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start () {
        //    DontDestroyOnLoad(gameObject);


        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        ShowBannerAds();

    }

    public void ShowBannerAds()
    {
        AdRequest request = new AdRequest.Builder().Build();

        BannerView bannerAd = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Top);
        bannerAd.LoadAd(request);
        // Admob.Instance().showBannerAbsolute()
        /* Admob.Instance().showBannerRelative(bannerId, AdSize.BANNER, AdPosition.BOTTOM_CENTER,0);*/
    }

    public void ShowRewardAds()
    {
       /* if (Admob.Instance().isRewardedVideoReady())
        {
            Admob.Instance().showRewardedVideo();
        }*/
    }

    public void ShowInterstallerAds()
    {
       
    }
	

}
