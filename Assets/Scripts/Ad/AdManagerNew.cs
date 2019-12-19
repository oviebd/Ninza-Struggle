using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManagerNew : MonoBehaviour {

    private BannerView bannerView;
    private RewardBasedVideoAd rewardBasedVideo;


    public static AdManagerNew instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    public void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-7034086702288798~1062205000";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

     //   this.RequestBanner();
        
      // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        
       // Called when an ad request has successfully loaded.
       rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
       // Called when an ad request failed to load.
       rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
       // Called when an ad is shown.
       rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
       // Called when the ad starts to play.
       rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
       // Called when the user should be rewarded for watching a video.
       rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
       // Called when the ad is closed.
       rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
       // Called when the ad click caused the user to leave the application.
       rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;


        this.RequestRewardBasedVideo();
    }

    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7034086702288798/7436041669";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
          AdRequest request = new AdRequest.Builder().Build();
        /*AdRequest request = new AdRequest.Builder()
   .AddTestDevice("C6D8BF15F03AE9B8")
   .Build();*/
        // Load the banner with the request.
        bannerView.LoadAd(request);

    }

    public void DestroyBanner()
    {
        bannerView.Destroy();
    }

    public void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7034086702288798/2545132478";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.

       /* AdRequest request = new AdRequest.Builder()
  .AddTestDevice("C6D8BF15F03AE9B8")
  .Build();*/
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }


    public void ShowRewardVideo()
    {
        if (isNetworkAvilable() == false && rewardBasedVideo.IsLoaded()==false)
        {
            DialogUtility.dialogType = DialogUtility.DialogType.GemAdsype;
            DialogController.instance.ShowPopUp(DialogUtility.dialoq_NetworkError);
        }
        else
        {
            if (rewardBasedVideo.IsLoaded())
            {
                rewardBasedVideo.Show();
            }
        }
    }


    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
       Debug.Log("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        OnAddLoadFailed();
        Debug.Log(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoStarted event received");
    }

 

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        OnAddSuccessFullyLoaded();
        Debug.Log(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoLeftApplication event received");
    }


    public bool isNetworkAvilable()
    {
        bool isNetworkAvilable = false;
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            isNetworkAvilable = false;
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            isNetworkAvilable = true;
            Debug.Log("Internet ase!");
        }

        return isNetworkAvilable;
    }


    void OnAddSuccessFullyLoaded()
    {
        switch (VariableUtilities.adsEventType)
        {
            case VariableUtilities.AdsEventType.GemCollectAds:
                //  Debug.Log("Give me some gem");
                int zemNo = UnityEngine.Random.Range(1, 5);
                Utility.IncreaseSpecificZemNo(VariableUtilities.zemTypeForAds, zemNo + "");

                DialogUtility.dialogType = DialogUtility.DialogType.GemAdsype;
                //Debug.Log("ads manager : " + "You Get " + zemNo + " " + VariableUtilities.zemTypeForAds);
                DialogController.instance.ShowPopUp("You Get " + zemNo + " " + VariableUtilities.zemTypeForAds);
                break;

            case VariableUtilities.AdsEventType.PlayerReviveAds:
                //  Debug.Log("Revive my player");
                UiManager.instance.ShowTimerPanel();
                break;
        }
    }

    void OnAddLoadFailed()
    {
        switch (VariableUtilities.adsEventType)
        {
            case VariableUtilities.AdsEventType.GemCollectAds:
                DialogUtility.dialogType = DialogUtility.DialogType.GemAdsype;
                DialogController.instance.ShowPopUp(DialogUtility.dialoq_ErrorAdsLoading);
                break;

            case VariableUtilities.AdsEventType.PlayerReviveAds:
                DialogUtility.dialogType = DialogUtility.DialogType.GemAdsype;
                DialogController.instance.ShowPopUp(DialogUtility.dialoq_ErrorAdsLoading);
                break;
        }
    }


}
