using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour {

    float pref_spawn_time;
    private LevelManager levelManager;

    void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        pref_spawn_time = Time.time;
      //  Invoke("Spawn_enemy",2.0f);
	}

    private void Update()
    {
       if(Time.time - pref_spawn_time >= 2.0f)
        {
            Spawn_enemy();
            pref_spawn_time = Time.time;
        }
    }

    void Spawn_enemy()
    {
        FindObjectOfType<InitializeGameStage>().InstantiateEnemy();
    }
}
