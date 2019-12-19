using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    public static InventoryController instance;

    public List<InventoryItems> inventoryItems;
    public List<GameObject> collectableGobjsList;
    GameObject[] inventoryPanels;
//    DataHandler dataHandler;

    private void Awake()
    {
        if (instance == null)
             instance = this;
    }

    void Start () {

        //dataHandler = FindObjectOfType<DataHandler>();
       
      //  PrepareInventory();
	}

   public void PrepareInventory()
    {
        inventoryPanels = GameObject.FindGameObjectsWithTag(TagUtility.Tag_Inventory_Panel_UI);

        CollectableSavedData collectableSavedData = DataHandler.instance.LoadCollectableData();

        for (int i = 0; i < inventoryPanels.Length; i++)
        {
            InventoryItemContoller itemController = inventoryPanels[i].GetComponent<InventoryItemContoller>();
            InventoryItems item;
            item = inventoryItems[i];
            //  Debug.Log("current panel player name : " + item.playerName);
            /*   if (inventoryItems.Count <= i)
                   item = inventoryItems[i];
               else
                   item = inventoryItems[inventoryItems.Count-1];*/

            int storedZem = Utility.GetStoredZemNoByType(collectableSavedData, item.zemType);

            itemController.SetUpInventory(item,storedZem);
        }
    }

    public void OnInventoryItemClicked(InventoryItems item)
    {
        SettingData settingData= DataHandler.instance.LoadSettingData();
        settingData.playerNameEnum = item.playerNameEnum;
        DataHandler.instance.SaveSettingData(settingData);

        MenuController.instance.HidehooseHeroPanel();

        InitializeGameStage.instance.InstantiatePlayer();
    }


    public GameObject GetNextCollectsbleForAds()
    {
      //  bool isPrepared = false;
        GameObject collectableObj = null;

       for(int j=0;j<collectableGobjsList.Count;j++)
        {
          //  int index = Random.Range(0, collectableGobjsList.Count);
            collectableObj = collectableGobjsList[j];
            VariableUtilities.collectableType type = collectableObj.GetComponentInChildren<CollectableBehaviour>().type;
        //    Debug.Log("in J " + j + "  coll obj neme " +type);
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                InventoryItems inventoryItem = inventoryItems[i];
             //   Debug.Log("in J " + j + "  inventory obj Zem  neme " + inventoryItem.zemType);

                if (type != inventoryItem.zemType)
                  break;
            
                int collectableNo = DataHandler.instance.GetSpecificCollectableStoredNo(type);
                int diff = inventoryItem.requiredAmountToUnlock - collectableNo;

             //   Debug.Log("Req for unlock : " + inventoryItem.requiredAmountToUnlock + "   colll No : " + collectableNo);

            //    Debug.Log("Diff in I " + i + "  "+diff );
             if (diff > 0)
              {
                return collectableObj;
              }
            
            }
        }
        return collectableObj;
    }


}
