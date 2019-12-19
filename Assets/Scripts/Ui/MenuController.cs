using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    public Animator uiCanvasanimator;
    public Animator pausePanelAnimator;
    public GameObject gameOverPanel;

    private string _anim_PlayOptionShowed = "isPlayOptionShowed";
    private string _anim_ChooseHeroShowed = "isChooseHeroAppeared";
    private string _anim_CampaignOptionShowed = "isCampaignOptionShowed";
    private string _anim_MainMenuShowed = "isMainMenuShowed";
    //private string _anim_IsIdle = "isIdle";
    private string _anim_PauseMenuDisAppear = "isPauseMenuDisAppear";

    private enum menuState {MainMenu,PlayOptionMenu,CampaignOptionMenu,CollectableMenu,PauseState ,StageSelectionState};
    private menuState currentMenuState;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        ShowMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            ShowNormalAdsVideo();
        if (Input.GetKeyDown(KeyCode.L))
            ShowRewardVideo();

    }
    void ShowMainMenu()
    {
        Time.timeScale = 1;
        UiManager.instance.ShowMainMenu();
        currentMenuState = menuState.MainMenu;
    }

    void ShowPlayOptionMenu()
    {
        UiManager.instance.ShowPlayOptionMenu();
        currentMenuState = menuState.PlayOptionMenu;
    }

    void ShowCampaignOptionMenu()
    {
        UiManager.instance.ShoweCampaignModeOptionPanel();
        currentMenuState = menuState.CampaignOptionMenu;
    }

    public void PlayButtonClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        ShowPlayOptionMenu();
    }

    public void TutorialButtonClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        AppController.instance.ShowTutorial();
        // SceneManager.LoadScene("TutorialScene");
    }

    public void ChooseHeroBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        InventoryController.instance.PrepareInventory();
        uiCanvasanimator.SetBool(_anim_ChooseHeroShowed, true);
        currentMenuState = menuState.CollectableMenu;
    }

    public void HidehooseHeroPanel()
    {
        uiCanvasanimator.SetBool(_anim_ChooseHeroShowed, false);
        currentMenuState = menuState.MainMenu;
    }

    public void NewGameBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        GameManager.instance.StartGame(VariableUtilities.GameMode.GameMode_New);
    }

    public void ContinueGameBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        GameManager.instance.StartGame(VariableUtilities.GameMode.GameModeContinue);
    }

    public void InfinityGameBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        currentMenuState = menuState.StageSelectionState;
        UiManager.instance.ShowStageSelectablePanel();
        //GameManager.instance.StartGame(VariableUtilities.GameMode.GameModeInfinity);
    }

  

    public void CampaignBtnBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        ShowCampaignOptionMenu();
    }

    public void PauseBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        GameManager.instance.PauseGame();
    }

    public void ResumeBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        GameManager.instance.ResumeGame();
    }

    public void ReTryBtnClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        if(VariableUtilities.gameState == VariableUtilities.GameState.Paused)
        {
            DialogUtility.dialogType = DialogUtility.DialogType.ReTryGame;
            DialogController.instance.ShowAlert(DialogUtility.dialoq_SureTitle);
         
        }
        else
        {
            GameManager.instance.ReTryGame();
        }
       
    }

    public void MainMenuBtnClicked()
    {
        Debug.Log("mainMenu clicked");
        SoundManager.instance.PlayUiButtonSound();
        DialogController.instance.ShowAlert(DialogUtility.dialoq_GoMainMenu);
        DialogUtility.dialogType = DialogUtility.DialogType.MainMenu;

        AdManagerNew.instance.DestroyBanner();
    }
    public void ShowGameOverMenu()
    {
       // gameOverPanelAnimator.gameObject.SetActive(false);
       // gameOverPanelAnimator.gameObject.SetActive(true);
    }

    public void GameOverMainMenuClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        GameManager.instance.RestartGame();
        //gameOverPanelAnimator.gameObject.SetActive(true);
        //gameOverPanelAnimator.SetBool(_anim_PauseMenuDisAppear, true);
    }

    public void GameOverRetryFromPrevPositionBtnClicked()
    {

        VariableUtilities.adsEventType = VariableUtilities.AdsEventType.PlayerReviveAds;
        Debug.Log("Ads type : " + VariableUtilities.adsEventType);
        AdManagerNew.instance.ShowRewardVideo();
       //iManager.instance.ShowTimerPanel();
        // AdManager.instance.ShowRewardVideo();
        //   UiManager.instance.ShowTimerPanel();
        //  Debug.Log("Stage Clicked : " + stageItem.stageName);
    }

    public void ChooseStageItemButtonClicked(StageItem stageItem)
    {
        SoundManager.instance.PlayUiButtonSound();
        GameManager.instance.StartGame(VariableUtilities.GameMode.GameModeInfinity, stageItem);
       
        //  Debug.Log("Stage Clicked : " + stageItem.stageName);
    }

    public void ShowRewardVideo()
    {
        SoundManager.instance.PlayUiButtonSound();
        //AdManager.instance.ShowRewardVideo();
        // AdmobAdManager.instance.ShowRewardAds();
        AdManagerNew.instance.RequestRewardBasedVideo();
    }

    public void ShowNormalAdsVideo()
    {
        SoundManager.instance.PlayUiButtonSound();
        AdManager.instance.ShowNormalVideo();
    }

    public void ShowAdsForCollectable()
    {
        
       SoundManager.instance.PlayUiButtonSound();
        GameObject collectableObjs =   InventoryController.instance.GetNextCollectsbleForAds();

        if (collectableObjs != null)
        {
            VariableUtilities.adsEventType = VariableUtilities.AdsEventType.GemCollectAds;
            VariableUtilities.zemTypeForAds = collectableObjs.GetComponentInChildren<CollectableBehaviour>().type;
            AdManagerNew.instance.ShowRewardVideo();
            //   AdManager.instance.ShowRewardVideo();
            //    AdmobAdManager.instance.ShowRewardAds();
            //    GoogleMobileAdsDemoScript.instance.ViewIntertialAd_PublicFunc();
            //    Utility.IncreaseSpecificZemNo(collectableObjs.GetComponentInChildren<CollectableBehaviour>().type);
            //   Debug.Log("next collectable : " + collectableObjs.GetComponentInChildren<CollectableBehaviour>().type);

            //  AdManagerNew.instance.RequestRewardBasedVideo();
        }
       
    }

    public void QuitBtnclicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        DialogUtility.dialogType = DialogUtility.DialogType.QuitGameType;
        DialogController.instance.ShowAlert(DialogUtility.dialoq_SureTitle);
    }

    public void SoundButtonClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        SoundManager.instance.SwapSound();
    }

    public void BackButtonClicked()
    {
        SoundManager.instance.PlayUiButtonSound();
        Debug.Log("Current STate : " + currentMenuState);
        if(VariableUtilities.gameState == VariableUtilities.GameState.Paused)
        {
            Debug.Log("Do nothing");
         //   return;
        }
        switch (currentMenuState)
        {
            case menuState.MainMenu:
                Debug.Log("Do nothing");
                break;
            case menuState.PlayOptionMenu:
                ShowMainMenu();  
                break;
            case menuState.CampaignOptionMenu:
                ShowPlayOptionMenu();
                break;
            case menuState.CollectableMenu:
                uiCanvasanimator.SetBool(_anim_ChooseHeroShowed, false);
                currentMenuState = menuState.MainMenu;
                break;
            case menuState.StageSelectionState:
                ShowMainMenu();
                break;
        }
    }


}
