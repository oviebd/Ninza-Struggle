using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SwipeMenu
{
	/// <summary>
	/// Attach to any menu item. 
	/// </summary>
	public class MenuItem : MonoBehaviour
	{
        public StageItem stageItem;
        public GameObject holderPanel;
        public GameObject lockPanel;

        public TextMeshProUGUI text;
		/// <summary>
		/// The behaviour to be invoked when the menu item is selected.
		/// </summary>
		public Button.ButtonClickedEvent OnClick;

		/// <summary>
		/// The behaviour to be invoked when another menu item is selected.
		/// </summary>
		public Button.ButtonClickedEvent OnOtherMenuClick;

        bool isItLocked = false;

        private void Start()
        {
          //  Prepare();
        }

        private void OnEnable()
        {
            Prepare();
        }
        public void Prepare()
        {
            //Debug.Log("Prepare Called");
            text.text = stageItem.stage_title;

            SettingData settingData = DataHandler.instance.LoadSettingData();
            int setting_StageNum = settingData.maximumStageCleared;
            holderPanel.GetComponent<Image>().sprite = stageItem.stage_sprite;

            if (stageItem.stageNumber > setting_StageNum)
                isItLocked = true;
            else
                isItLocked = false;

            lockPanel.SetActive(isItLocked);
        }


        public void OnItemClicked()
        {
            //MenuController.instance.ChooseStageItemButtonClicked(stageItem);
            if (isItLocked == false)
            {
                MenuController.instance.ChooseStageItemButtonClicked(stageItem);
            }
            else
            {
                DialogUtility.dialogType = DialogUtility.DialogType.OnlyDialog;
                DialogController.instance.ShowPopUp(DialogUtility.dialoq_LockLevelMessage);
            }
                 
        }
	}
}