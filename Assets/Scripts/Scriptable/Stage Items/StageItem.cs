using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageItem : ScriptableObject
{
    public bool isItInfinityMode = false;

    public string stage_title;
    public VariableUtilities.StageName stageName;
    public int stageNumber;
    public Sprite stage_sprite;

    public int enemy_no;
    public float spawnRate;
    public int max_enemy_num;
    public GameObject stage_prefab;
    public GameObject boss;
    public List<GameObject> enemy_prefabs;


    #region collectable
    public int max_colectable_num;
    public float timeForSpawnFirstCollectable;
    public float collectable_Spawn_Rate;
    public List<GameObject> collectablePrefabs;
    #endregion

}

