using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    VariableUtilities.GameMode current_GameMode;
    StageItem currentStageItem;
    private void Awake()
    {
         if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        SoundManager.instance.SetSoundImage();
        SoundManager.instance.PlayBackgroundSound();
        InitializeGameStage.instance.InstantiatePlayer();  //for menu 
        SoundManager.instance.SetBackGroundAudioClip(VariableUtilities.SoundType.menuSound);

        Invoke("RquestForBanner",1.0f);
      //  StartGame();
    }

    void RquestForBanner()
    {
        AdManagerNew.instance.RequestBanner();
    }

    public void StartGame(VariableUtilities.GameMode gameMode, StageItem stageItem = null)
    {
        AdManagerNew.instance.DestroyBanner();

        Time.timeScale = 1;
        VariableUtilities.gameMode = gameMode;
        this.current_GameMode = gameMode;
        UiManager.instance.ManageStartGameUI();
        switch (gameMode)
        {
            case VariableUtilities.GameMode.GameMode_New:
             //   Debug.Log("New Mode");
                LevelManager.instance.LoadLevel(1);
                break;
            case VariableUtilities.GameMode.GameModeContinue:
                SettingData settingData=DataHandler.instance.LoadSettingData();
                LevelManager.instance.LoadLevel(settingData.previousStageForCampaignMode);
                //   Debug.Log("Continue  Mode");
                break;
            case VariableUtilities.GameMode.GameModeInfinity:
                if (stageItem == null)
                    stageItem = currentStageItem;
                else
                    currentStageItem = stageItem;
                LevelManager.instance.LoadInfinityLevel(stageItem);
                break;
        }

        VariableUtilities.gameState = VariableUtilities.GameState.Running;
    }


    public void ReStartFromPrevPoint()
    {
        AdManagerNew.instance.DestroyBanner();

        InitializeGameStage.instance.InstantiatePlayer();
        Time.timeScale = 1;
        SoundManager.instance.PlayBackgroundSound();
        UiManager.instance.ManageStartGameUI();
    }

    public void EndGame()
    {
        SoundManager.instance.PauseBackgroundSound();
        Time.timeScale = 0;
        if (current_GameMode != null)
        {
            if(current_GameMode != VariableUtilities.GameMode.GameModeInfinity)
            {
                SettingData settingData = DataHandler.instance.LoadSettingData();
                settingData.previousStageForCampaignMode = LevelManager.instance.current_level;
                int current_level_no = InitializeGameStage.instance._current_StageItem.stageNumber;

                if ( current_level_no > settingData.maximumStageCleared)
                {
                    settingData.maximumStageCleared = current_level_no;
                }
                DataHandler.instance.SaveSettingData(settingData);
            }

            UiManager.instance.ManageGameOverUi();
        }

        MenuController.instance.ShowGameOverMenu();
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ReTryGame()
    {
        AdManagerNew.instance.DestroyBanner();
        InitializeGameStage.instance.ClearEveryThing();
        //StartGame(VariableUtilities.gameMode);
        if(VariableUtilities.gameMode == VariableUtilities.GameMode.GameModeInfinity)
        {
            StartGame(VariableUtilities.gameMode);
        }
        else
            StartGame(VariableUtilities.GameMode.GameModeContinue);
    }

    public void PauseGame()
    {
        SoundManager.instance.PauseBackgroundSound();
        Time.timeScale = 0;
        UiManager.instance.ManagePauseUI();
        VariableUtilities.gameState = VariableUtilities.GameState.Paused;
    }

    public void ResumeGame()
    {
        AdManagerNew.instance.DestroyBanner();
        SoundManager.instance.PlayBackgroundSound();
        UiManager.instance.ManageResumeUi();
        VariableUtilities.gameState = VariableUtilities.GameState.Running;
        Time.timeScale = 1;
    }

    void MakeTimeScaleOne()
    {
     //   Debug.Log("Game Manager : " + "TimeScale");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
