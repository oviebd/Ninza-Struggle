using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    public static AdManager instance;
    string gameID = "2981251";
    
    string normalVideoString = "video";
    string rewardVideoString = "rewardedVideo";

    void Awake()
    {
        if (instance == null)
            instance = this;
       // Advertisement.Initialize(gameID, false);
       // Advertisement.Initialize(gameID, false);
    }

    public bool isNetworkAvilable()
    {
        bool isNetworkAvilable= false;
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

    public void ShowRewardVideo()
    {

       /* if (isNetworkAvilable() == false)
        {
            DialogUtility.dialogType = DialogUtility.DialogType.GemAdsype;
            DialogController.instance.ShowPopUp(DialogUtility.dialoq_NetworkError);
        }


#if UNITY_EDITOR
        StartCoroutine(WaitForAd());
#endif
        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(rewardVideoString))
            Advertisement.Show(rewardVideoString, options);*/
    }

    public void ShowNormalVideo(string zone = "")
    {
       /*
#if UNITY_EDITOR
        StartCoroutine(WaitForAd());
#endif

        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(normalVideoString))
            Advertisement.Show(normalVideoString, options);*/
    }

    public void ShowAd(string zone = "")
    {
        /*
#if UNITY_EDITOR
        StartCoroutine(WaitForAd());
#endif

        if (string.Equals(zone, ""))
            zone = null;

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        if (Advertisement.IsReady(zone))
            Advertisement.Show(zone, options);*/
    }

    /*void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                OnAddSuccessFullyLoaded();
                Debug.Log("Ad Finished. Rewarding player...");
                break;
            case ShowResult.Skipped:
                OnAddLoadFailed();
                Debug.Log("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                OnAddLoadFailed();
                Debug.Log("I swear this has never happened to me before");
                break;
        }
    }

    void OnAddSuccessFullyLoaded()
    {
        switch (VariableUtilities.adsEventType)
        {
            case VariableUtilities.AdsEventType.GemCollectAds:
              //  Debug.Log("Give me some gem");
                int zemNo = Random.Range(1, 5);
                Utility.IncreaseSpecificZemNo(VariableUtilities.zemTypeForAds,zemNo+"");

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

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }*/
}
