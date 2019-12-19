using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    private InitializeGameStage _initGameStage;
    private Animator _level_end_anim;

    // public GameObject[] stages_Prefab;
    public StageItem infinityStageItem;

    public StageItem[] stageItems;
    [HideInInspector]  public int max_level;
    [HideInInspector]  public int current_level ;

    private bool _canLevel_Load;
    private bool isItInfinityMode=false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        _initGameStage = FindObjectOfType<InitializeGameStage>();
        _level_end_anim= GameObject.FindGameObjectWithTag("level_end_animation").GetComponent<Animator>();

        current_level = 1;
        max_level = stageItems.Length;
        _canLevel_Load = true;

       // LoadLevel();
    }

    public void LoadInfinityLevel(StageItem stageItem)
    {
        isItInfinityMode = true;
        infinityStageItem.stage_prefab = stageItem.stage_prefab;
        InstantiateStage(infinityStageItem);
       
    }

    public void LoadLevel(int level_no)
    {
        isItInfinityMode = false;
      //  Debug.Log("Current Stage : " + level_no );
        current_level = level_no;
        if ( current_level <= stageItems.Length)
        {
            //If it is first stage then just load the level 
            //if not, then first play the level end animation
             InstantiateStage();

        }
        else
        {
            UiManager.instance.ShowCongratesPanel();
        }
    }

    public void LoadLevel()
    {
        isItInfinityMode = false;
       // Debug.Log("Current Stage : " + current_level);
        UiManager.instance.ManageStartGameUI();
        if (current_level  <= stageItems.Length)
        {
            //If it is first stage then just load the level 
            //if not, then first play the level end animation
            _level_end_anim.SetTrigger("is_load_game");
            Invoke("InstantiateStage", 1.0f);
        }
        else
        {
            UiManager.instance.ShowCongratesPanel();
        }
        _canLevel_Load = true;
    }

    public StageItem GetCurrentStage()
    {
        return stageItems[current_level - 1];
    }

     void InstantiateStage(StageItem stageItem)
    {
        _initGameStage.CreateEnvironment(stageItem);
    }
    void InstantiateStage()
    {
        _initGameStage.CreateEnvironment(stageItems[current_level - 1]);
    }

    public void CheckForStageCleared()
    {
        Invoke("OnStageClearedChecked", 0.5f);
    }

    void OnStageClearedChecked()
    {
        if (IsPlayerKilledAllEnemy())
        {
          bool isBossInstantiated = _initGameStage.InstantiateBoss();
            
            if(GetCurrentStage().boss == null && _canLevel_Load == true && isItInfinityMode == false)
            {
                //Debug.Log();
                _canLevel_Load = false;
                Invoke("GoNextLevel", 0.2f);
            }
         // if (isBossInstantiated == false)
               
        }
    }

    public void GoNextLevel()
    {
        
        FindObjectOfType<PlayerMovement>().OnPlayerWin();
        current_level++;
        Invoke("LoadLevel", 1.0f);
    }

    bool IsPlayerKilledAllEnemy()
    {
        if (FindObjectOfType<Enemy_Behaviour>() != null)
        {
            //As ball component exist , so stage is not cleared yet
            return false;
        }
        else
            return true;
    }


}
