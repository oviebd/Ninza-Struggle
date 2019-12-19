using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public GameObject mainCamera;

    public static UiManager instance;

    public GameObject joyStickObject;
    public GameObject menuHolderPanel;
    public GameObject mainMenuOptionPanel;
    public GameObject playOptionMenuPanel;
    public GameObject campaignModeOptionPanel;
    public GameObject pausePanel;
    public GameObject collectablePanel;
    public GameObject stageSelectablePanel;
 //   public GameObject backBtn;
    public GameObject menuBackGroundObject;
    public GameObject gameOverPanel;
    public GameObject pauseBtnObj;
    public GameObject scorePanelInfinityObject;

    public GameObject bossHealthBarPanel;
    public GameObject congratulationPanel;

    public GameObject timerPanel;
   
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start () {
        ShowMainMenu();
    }
	
    public void ShowPlayOptionMenu()
    {
        mainMenuOptionPanel.SetActive(false);
        campaignModeOptionPanel.SetActive(false);
        playOptionMenuPanel.SetActive(true);
    }

    public void ShowMainMenuOptionPanel()
    {
        mainMenuOptionPanel.SetActive(true);
        playOptionMenuPanel.SetActive(false);
    }

    public void ShoweCampaignModeOptionPanel()
    {
        playOptionMenuPanel.SetActive(false);
        campaignModeOptionPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        HideAllPanel();
        mainCamera.SetActive(true);
        menuHolderPanel.SetActive(true);
        mainMenuOptionPanel.SetActive(true);
        collectablePanel.SetActive(true);
        menuBackGroundObject.SetActive(true);
       // backBtn.SetActive(true);
    }

    public void ShowStageSelectablePanel()
    {
        ShowPanel(stageSelectablePanel);
        mainCamera.SetActive(false);
    }

    public void ManageStartGameUI()
    {
        ShowPanel(joyStickObject);
        mainCamera.SetActive(true);
        pauseBtnObj.SetActive(true);
       // Debug.Log("Game Mod e " + VariableUtilities.gameMode);
        if (VariableUtilities.gameMode == VariableUtilities.GameMode.GameModeInfinity)
        {
            //Debug.Log("Game Mod e  is fine" );
            scorePanelInfinityObject.SetActive(true);
        }
    }

    public void ManagePauseUI()
    {
        ShowPanel(pausePanel);
        menuHolderPanel.SetActive(true);

    }

    public void ManageResumeUi()
    {
        menuHolderPanel.SetActive(false);
        joyStickObject.SetActive(true);
        pauseBtnObj.SetActive(true);
    }

   
    public void ShowTimerPanel()
    {
        gameOverPanel.SetActive(false);
        timerPanel.SetActive(true);
    }

    public void ShowBossHealthPanel()
    {
        ShowPanel(bossHealthBarPanel);

        joyStickObject.SetActive(true);
        pauseBtnObj.SetActive(true);
    }

    public void ManageGameOverUi()
    {
        ShowPanel(gameOverPanel);
    }

    public void ShowCongratesPanel()
    {
        ShowPanel(congratulationPanel);
    }

    public void ShowPanel(GameObject showedPanel)
    {
        HideAllPanel();
        showedPanel.SetActive(true);
    }

    public void HideAllPanel()
    {
        joyStickObject.SetActive(false);
        mainMenuOptionPanel.SetActive(false);
        playOptionMenuPanel.SetActive(false);
        campaignModeOptionPanel.SetActive(false);
        collectablePanel.SetActive(false);
        stageSelectablePanel.SetActive(false);
        bossHealthBarPanel.SetActive(false);
        menuBackGroundObject.SetActive(false);
       // backBtn.SetActive(false);
        gameOverPanel.SetActive(false);
        pauseBtnObj.SetActive(false);

        congratulationPanel.SetActive(false);
        menuHolderPanel.SetActive(false);
        pausePanel.SetActive(false);
        scorePanelInfinityObject.SetActive(false);
        timerPanel.SetActive(false);
    }
}
