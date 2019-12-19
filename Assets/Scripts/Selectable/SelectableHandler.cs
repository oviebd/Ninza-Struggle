using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableHandler : MonoBehaviour {

    public static SelectableHandler instance;
    public List<GameObject> allPlayerPrefabs;

    SettingData savedSettData;

	void Awake () {

        if (instance == null)
            instance = this;
	}
	
	public SettingData GetSettingData()
    {
       return DataHandler.instance.LoadSettingData();
    }

    // public  GameObject GetCurrentPlayerPrefabs(VariableUtilities.PlayerNamesEnum playerType)
    public GameObject GetCurrentPlayerPrefab()
    {
        GameObject playerPrefab = allPlayerPrefabs[0];
        VariableUtilities.PlayerNamesEnum playerType ;

        playerType = DataHandler.instance.LoadSettingData().playerNameEnum;

        if (playerType == null)
        {
            return playerPrefab;
        }

        for (int i = 0; i < allPlayerPrefabs.Count; i++)
        {
            GameObject player_Obj = allPlayerPrefabs[i];
            if (player_Obj.GetComponent<PlayerMovement>().playerType == playerType)
            {
                playerPrefab = allPlayerPrefabs[i];
                break;
            }
        }

        return playerPrefab;
    }

}
