using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUtility : MonoBehaviour {

  //  private Animator _anim;

    public enum DialogType { QuitGameType, GemAdsype, RestartGameType ,ReTryGame,MainMenu,OnlyDialog};
    public static DialogType dialogType;

    public static string dialoq_SureTitle = "Are You Sure";
    public static string dialoq_GoMainMenu = "Are you sure.";
    public static string dialoq_NetworkError = "You are not connected with internet. Please check your internet connection";
    public static string dialoq_ErrorAdsLoading = "Add Loading Error.";
    public static string dialoq_LockPlayerMessage = "Collect Gem to Unlock This Player";
    public static string dialoq_LockLevelMessage = "Play campaign mode to unlock this stag.";

    private void Start()
    {
    //    _anim = GetComponent<Animator>();
      //  ShowAlertPanel("Test");
    }
   

    public void ShowPopUpPanel(string title)
    {
        Text text = gameObject.GetComponentInChildren<Text>() as Text;
        text.text = title;
    }

    public void ShowAlertPanel(string title)
    {
        Text text = gameObject.GetComponentInChildren<Text>() as Text;
        text.text = title;
    }

    public void PositiveButonClicked()
    {
       // _anim.SetBool(anim_Disappear, true);
        gameObject.SetActive(false);
      //  Invoke("OnDisappeared", animationTime);
        PerformPositiveBtnClicked();
    }

    public void NegativeButonClicked()
    {
     //   _anim.SetBool(anim_Disappear, true);
        gameObject.SetActive(false);
       // Invoke("OnDisappeared", animationTime);
    }

    public void OnDisappeared()
    {
      //  Debug.Log("dissappear");
        gameObject.SetActive(false);
      
    }

    public void OkButonClicked()
    {
        gameObject.SetActive(false);
        PerformPositiveBtnClicked();
    }

    void PerformPositiveBtnClicked()
    {
        switch (dialogType)
        {
            case DialogType.QuitGameType:
                Application.Quit();
                Debug.Log("Quit Game");
                break;

            case DialogType.ReTryGame:
                GameManager.instance.ReTryGame();
                Debug.Log("Retry Game");
                break;

            case DialogType.MainMenu:
                GameManager.instance.RestartGame();
                Debug.Log("Restart Game");
                break;
        }
    }
}
