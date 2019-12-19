using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour {

    public static AppController instance;

    public GameObject rootTutorial;
    public GameObject rootMainGame;
    public GameObject splashVideoHolder;
    public static bool isItFirstLoad =  false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start () {

        /* if (Utility.IsGameFirstLaunced())
         {
             Utility.MakeSoundOn();
             ShowTutorial();
         }
         else
         {
             ShowMainGame();
         }*/
        Debug.Log("isFirsttLoaa  :" +  isItFirstLoad);
         if(isItFirstLoad == false)
        { 
            Utility.MakeSoundOn();
           isItFirstLoad = true;
            ShowSplashVideo();
        }
        
         
	}
	
    public void ShowMainGame()
    {
        rootMainGame.SetActive(true);
        rootTutorial.SetActive(false);
        splashVideoHolder.SetActive(false);
    }

    public void ShowTutorial()
    {
        rootTutorial.SetActive(true);
        rootMainGame.SetActive(false);
        splashVideoHolder.SetActive(false);

    }

    public void ShowSplashVideo()
    {
        splashVideoHolder.SetActive(true);
        rootTutorial.SetActive(false);
        rootMainGame.SetActive(false);

        Invoke("ShowMainGame",6.0f);
    }
}
