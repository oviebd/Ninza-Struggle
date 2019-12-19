using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour {

    public static DataHandler instance;

    public CollectableSavedData defaultCollectableData;
    public SettingData defaultSettingData;
  

    private string identifier_Collectable = "colectableData";
    private string identifier_Setting = "settingData";

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start () {

        Debug.Log("Is It First Time:  " + PlayerPrefabsUtility.GetIsItFirstTime());
        Debug.Log("Path : " + Application.persistentDataPath);
        if (PlayerPrefabsUtility.GetIsItFirstTime()==true)
        {
          //  Debug.Log("Path : " + Application.persistentDataPath);
            PlayerPrefabsUtility.ChangeIsItFirstTimeFalse();
            //Create default Setting and collectable Data
            SaveCollectableData(defaultCollectableData);
            SaveSettingData(defaultSettingData);

        }
       
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Delete all data");
            PlayerPrefs.DeleteAll();

          //  Debug.Log("Is It First Time:  " + PlayerPrefabsUtility.GetIsItFirstTime());
        }
    }

    public void SaveCollectableData(CollectableSavedData collectableData)
    {
        SaveGame.Save<CollectableSavedData>(identifier_Collectable, collectableData, SerializerDropdown.Singleton.ActiveSerializer);
    }

    public CollectableSavedData LoadCollectableData()
    {
        CollectableSavedData data = SaveGame.Load<CollectableSavedData>(
            identifier_Collectable,
            new CollectableSavedData(),
            SerializerDropdown.Singleton.ActiveSerializer);
        return data;
    }

    public int GetSpecificCollectableStoredNo(VariableUtilities.collectableType type)
    {
        CollectableSavedData data = LoadCollectableData();
        int number=  Utility.GetStoredZemNoByType(data,type);
        return number;
    }

    public void SaveSettingData(SettingData settingData)
    {
      //  new SaveGameJsonSerializer()
              SaveGame.Save<SettingData>(identifier_Setting, settingData, SerializerDropdown.Singleton.ActiveSerializer);
      //  SaveGame.Save<SettingData>(identifier_Setting, settingData, SaveGameJsonSerializer());
    }

    public SettingData LoadSettingData()
    {
        SettingData data = SaveGame.Load<SettingData>(
            identifier_Setting,
            new SettingData(),
            SerializerDropdown.Singleton.ActiveSerializer);
        return data;
    }
}
