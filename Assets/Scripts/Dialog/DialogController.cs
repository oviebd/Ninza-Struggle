using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour {

    public static DialogController instance;
    public GameObject PopUpPanel, alertPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start () {
        if(PopUpPanel!=null)
            PopUpPanel.SetActive(false);
        if(alertPanel!=null)
            alertPanel.SetActive(false);


    }

	public void ShowPopUp(string title)
    {
        PopUpPanel.SetActive(true);
        PopUpPanel.GetComponent<DialogUtility>().ShowPopUpPanel(title);
    }

    public void ShowAlert(string title)
    {
        alertPanel.SetActive(true);
        alertPanel.GetComponent<DialogUtility>().ShowAlertPanel(title);
    }
}
