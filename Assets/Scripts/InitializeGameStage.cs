using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGameStage : MonoBehaviour {

    public static InitializeGameStage instance;

    public Vector3 instantiateEnemyPosition = new Vector3(14, 9, -68);
   [HideInInspector]  public StageItem _current_StageItem;
    private List<GameObject> _enemy_Prefabs=new List<GameObject>();

    float pref_spawn_time;
    float spawnTime;
    private GameObject _current_GameStageObj;
    int enemy_count;

    bool isGameStarted = false;
    bool canEnemySpawn = false;
    bool canBossSpawn = false;


    //Collectable
    int collectable_count;
    float lastCollectableSpawnTime;
    float collectableSpawnRate;
    //  bool canCllectablSpawn;

    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
       // ResetData();
    }


    private void Update()
    {
        if (Time.time - pref_spawn_time >= 2.0f && canEnemySpawn == true)
        {
            InstantiateEnemy();
            pref_spawn_time = Time.time;
        }

        if (Time.time - lastCollectableSpawnTime >= collectableSpawnRate && isGameStarted==true)
        {
            InstantiateCollectable();
            collectableSpawnRate = _current_StageItem.collectable_Spawn_Rate;
            lastCollectableSpawnTime = Time.time;
        }
    }

    public void CreateEnvironment(StageItem current_stageItem)
    {
        ResetData();
        isGameStarted = true;
        this._current_StageItem = current_stageItem;
        spawnTime = _current_StageItem.spawnRate;
        collectableSpawnRate = _current_StageItem.timeForSpawnFirstCollectable;

        if (current_stageItem.stageName == VariableUtilities.StageName.Stage10)
            SoundManager.instance.SetBackGroundAudioClip(VariableUtilities.SoundType.bossSound);
        else
            SoundManager.instance.SetBackGroundAudioClip(VariableUtilities.SoundType.stageSound);

        if (_current_GameStageObj != null)
            Destroy(_current_GameStageObj);
  
        GameObject environment_Prefab = current_stageItem.stage_prefab;

        _current_GameStageObj = Instantiate(environment_Prefab, environment_Prefab.transform.position, Quaternion.identity);

         Invoke("CreateEnemy",1.0f);
        Invoke("InstantiatePlayer", 0.5f);
      //  InstantiatePlayer();
    }

   public void InstantiatePlayer()
    {
        //   Debug.Log("Instantiate player");

        ClearAllPlayer();
       
        playerPrefab = SelectableHandler.instance.GetCurrentPlayerPrefab();
       
        GameObject playerSpawnParent = GameObject.FindGameObjectWithTag(TagUtility.Tag_Player_Spawn_Position);
      
        if (playerSpawnParent == null)
        {
           // Debug.Log("Instantiate player parent Null ");
            return;
        }
      //  Debug.Log("Instantiate player parent Not Null ");
        GameObject playerObj = Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
        playerObj.transform.SetParent(playerSpawnParent.transform, false);

        //enemyObj.transform.parent = null;
       // enemyObj.transform.localPosition = new Vector3(instantiatePosition.x, instantiatePosition.y, enemyObj.transform.localPosition.z);
    }

    void ResetData()
    {
        canEnemySpawn = false;
        canBossSpawn = true;
        pref_spawn_time = Time.time;
        enemy_count = 0;
        isGameStarted = false;
     //   canCllectablSpawn = false;
        collectable_count = 0;
        lastCollectableSpawnTime = Time.time;

        ClearEveryThing();
      //  ClearAllEnemy();
    }

    void ClearAllPlayer()
    {
        PlayerMovement prevPlayerMovement = FindObjectOfType<PlayerMovement>();
        if (prevPlayerMovement != null)
            Destroy(prevPlayerMovement.gameObject);
    }

   public void ClearAllEnemy()
    {
        if (FindObjectOfType<Enemy_Behaviour>() != null)
        {
            Enemy_Behaviour[] enemy_Behaviours = FindObjectsOfType<Enemy_Behaviour>();
            for (int i = 0; i < enemy_Behaviours.Length; i++)
            {
                GameObject gobj = enemy_Behaviours[i].gameObject;
                Destroy(gobj);
            }
        }
    }

    void ClearAllCollectables()
    {
        GameObject[]  collectableItems = GameObject.FindGameObjectsWithTag(TagUtility.Tag_Collectable);

        if (collectableItems !=null && collectableItems.Length > 0){

            for (int i = 0; i < collectableItems.Length; i++)
            {
                GameObject gobj = collectableItems[i].gameObject;
                Destroy(gobj);
            }
        }
    }

    public void ClearEveryThing()
    {
        GameObject stageItem = GameObject.FindGameObjectWithTag(TagUtility.Tag_StageItem);
        if (stageItem != null)
            Destroy(stageItem);

        ClearAllEnemy();
        ClearAllCollectables();
    }

    void CreateEnemy()
    {
        canEnemySpawn = true;
        _enemy_Prefabs = _current_StageItem.enemy_prefabs;
        for (int i = 0; i <_current_StageItem.enemy_no; i++)
        {
            //We use this Invoke, Because if we instantiate all in smae time, It behaves odd..
            //Do not know why this happen..
            Invoke("InstantiateEnemy", .5f);
        }
    }

    public void InstantiateEnemy(Vector3 instantiatePosition,GameObject enemyPrefab,Vector2 force)
    {
        GameObject enemySpawnParent = GameObject.FindGameObjectWithTag("EnemySpawnPoint");
        if (enemySpawnParent == null)
            return;
        GameObject enemyObj = Instantiate(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity);

        enemyObj.transform.SetParent(enemySpawnParent.transform, false);

        enemyObj.transform.parent = null;
        enemyObj.transform.localPosition = new Vector3(instantiatePosition.x, instantiatePosition.y, enemyObj.transform.localPosition.z);

        enemyObj.GetComponent<Enemy_Behaviour>().SetForce(force);
     
        //  Debug.Log("Enemy point : " + enemyObj.transform.position);
    }

    public void InstantiateEnemy()
    {
        //Debug.Log("Enemy Instantiate");

        if (enemy_count >= _current_StageItem.max_enemy_num)
            return;

        GameObject enemySpawnParent = GameObject.FindGameObjectWithTag("EnemySpawnPoint");
        if (enemySpawnParent == null)
            return;

        int prefab_index = Random.Range(0, _enemy_Prefabs.Count);
        float location_x_pos = Random.Range((instantiateEnemyPosition.x) * -1, instantiateEnemyPosition.x);
       // Debug.Log("Random X pos : " + location_x_pos) ;

        GameObject enemy_prefab = _enemy_Prefabs[prefab_index];
        GameObject enemyObj = Instantiate(enemy_prefab, enemy_prefab.transform.position, Quaternion.identity);
     
        enemyObj.transform.SetParent(enemySpawnParent.transform, false);
     
        enemyObj.transform.parent = null;
        enemyObj.transform.localPosition = new Vector3(location_x_pos, enemyObj.transform.localPosition.y, enemyObj.transform.localPosition.z);

        int x_Force;
        if ((enemy_count % 2) == 0)
            x_Force = 3 ;
        else
            x_Force = -3 ;

        enemyObj.GetComponent<Enemy_Behaviour>().SetForce (new Vector2(x_Force, 0f));
        enemyObj.GetComponent<Enemy_Behaviour>().canItSpilit = true;
        if (_current_StageItem.isItInfinityMode==false)
            enemy_count++;


        //  Debug.Log("Enemy point : " + enemyObj.transform.position);
    }

   public bool InstantiateBoss()
    {
        if (canBossSpawn == false)
            return false ;

        GameObject boss_prefab = _current_StageItem.boss;
        GameObject bossSpawnPoint = GameObject.FindGameObjectWithTag("boss_spawn_point");

        if (boss_prefab != null && bossSpawnPoint != null)
        {
            GameObject enemyObj = Instantiate(boss_prefab, boss_prefab.transform.position, Quaternion.identity);

            enemyObj.transform.SetParent(bossSpawnPoint.transform, false);
            canBossSpawn = false;

            return true;
        }
        else
            return false;
    }


    public void InstantiateBossCelaBela(GameObject prefab)
    {
        GameObject enemySpawnParent = GameObject.FindGameObjectWithTag("EnemySpawnPoint");

        if (enemySpawnParent == null)
            return;

        float location_x_pos = Random.Range((instantiateEnemyPosition.x) * -1, instantiateEnemyPosition.x);
      
        GameObject enemyObj = Instantiate(prefab, prefab.transform.position, Quaternion.identity);

        enemyObj.transform.SetParent(enemySpawnParent.transform, false);

        enemyObj.transform.parent = null;
        enemyObj.transform.localPosition = new Vector3(location_x_pos, enemyObj.transform.localPosition.y, enemyObj.transform.localPosition.z);

        int x_Force;
        if ((enemy_count % 2) == 0)
            x_Force = 3;
        else
            x_Force = -3;

        Enemy_Behaviour enemy_behaviour = enemyObj.GetComponent<Enemy_Behaviour>();

        if (enemy_behaviour != null)
        {
            enemy_behaviour.isItBossCela = true;
            enemy_behaviour.SetForce(new Vector2(x_Force, 0f));
            enemy_behaviour.canItSpilit = true;
            enemy_count++;
        }

    }

    public void InstantiateCollectable()
    {
       // Debug.Log("Instantiate collectable Prev");
        if (collectable_count >= _current_StageItem.max_colectable_num)
             return;

       // Debug.Log("Instantiate collectable after");
        GameObject collectableSpawnParent = GameObject.FindGameObjectWithTag(TagUtility.Tag_Collectable_Spawner);

        if (collectableSpawnParent == null)
            return;
     
        int prefab_index = Random.Range(0, _current_StageItem.collectablePrefabs.Count);
        int xPosRandm = Random.Range(0, 2);

        float location_x_pos = Random.Range((instantiateEnemyPosition.x) * -1, instantiateEnemyPosition.x);

        GameObject collectable_prefab = _current_StageItem.collectablePrefabs[prefab_index];
        GameObject colectable_Obj = Instantiate(collectable_prefab, collectable_prefab.transform.position, Quaternion.identity);

        colectable_Obj.transform.SetParent(collectableSpawnParent.transform, false);

        colectable_Obj.transform.parent = null;
        colectable_Obj.transform.localPosition = new Vector3(location_x_pos, colectable_Obj.transform.localPosition.y, colectable_Obj.transform.localPosition.z);

        if (_current_StageItem.isItInfinityMode == false)
            collectable_count++;
    }

}
