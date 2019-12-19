using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public static TutorialManager instance;
    public GameObject joySticktutorialPoint;
    public GameObject shootPoint;
    public GameObject tutorialOverPanel;

    string instruction_joystick = "swap left right to move player";

    public enum TutorialState {JoyStickTutorial, ShootTutorial,TutorialOver };
    public static TutorialState tutorialState;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
     //   Debug.Log("Start Called");
        ShowJoyStickTutorial();
    }

    void ShowJoyStickTutorial()
    {
        tutorialState = TutorialState.JoyStickTutorial;
        joySticktutorialPoint.SetActive(true);
    }

    public void RepeatTraining()
    {
        tutorialOverPanel.SetActive(false);
        ShowJoyStickTutorial();
    }



    public void GotItBtnClicked()
    {
        switch (tutorialState)
        {
            case TutorialState.JoyStickTutorial:
                joySticktutorialPoint.SetActive(false);
                Invoke("ShowShootTutorial", 2.0f);
                break;

            case TutorialState.ShootTutorial:
                shootPoint.SetActive(false);
              //  OverTutorialSesson();
                Invoke("OverTutorialSesson", 2.0f);
                break;

            case TutorialState.TutorialOver:
                tutorialOverPanel.SetActive(false);
                AppController.instance.ShowMainGame();
              //  SceneManager.LoadScene("MainGame");
                break;
        }
    }


    public void ShowShootTutorial()
    {
        tutorialState = TutorialState.ShootTutorial;
        shootPoint.SetActive(true);
    }

    public void OverTutorialSesson()
    {
        tutorialState = TutorialState.TutorialOver;
        tutorialOverPanel.SetActive(true);
    }

}
