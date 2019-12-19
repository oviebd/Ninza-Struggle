using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemContoller : MonoBehaviour {

    InventoryItems currentInventoryItem;

    private bool _isItLocked;
    private int storedZem = 0;

    public Color lockedColor, unLockedColor;

    public Image playerImage;
    public Text playerText;
    public Image zemImage;
    public Text zemText;
    public Image zemAmountPanelImage;
    public GameObject lockPanel;

   public void SetUpInventory(InventoryItems inventoryItem,int storedZem )
     {
        currentInventoryItem = inventoryItem;
        playerImage.sprite = inventoryItem.playerSprite;
        //    playerText.text = inventoryItem.playerNameEnum.ToString();
        playerText.text = inventoryItem.playerPrefab.GetComponent<PlayerMovement>().playerName;
        zemImage.sprite = inventoryItem.zemSprite;
     

        this.storedZem = storedZem;
        SetLockPanel();
    }


    void SetLockPanel()
    {
        if (storedZem < currentInventoryItem.requiredAmountToUnlock)
        {
            //Lock the Item
            _isItLocked = true;
            zemAmountPanelImage.color = lockedColor;
        }
        else
        {
            //Unlock It
            _isItLocked = false;
            zemAmountPanelImage.color = unLockedColor;
            storedZem = currentInventoryItem.requiredAmountToUnlock;
        }
        zemText.text = storedZem + "/" +  currentInventoryItem.requiredAmountToUnlock;
        lockPanel.SetActive(_isItLocked);
    }

    public void OnClickedInventoryItem()
    {
        if (_isItLocked == false)
        {
            InventoryController.instance.OnInventoryItemClicked(currentInventoryItem);
        }
        else
        {
            DialogUtility.dialogType = DialogUtility.DialogType.OnlyDialog;
            DialogController.instance.ShowPopUp(DialogUtility.dialoq_LockPlayerMessage);
        }

       // InventoryController.instance.OnInventoryItemClicked(currentInventoryItem);

    }
}
