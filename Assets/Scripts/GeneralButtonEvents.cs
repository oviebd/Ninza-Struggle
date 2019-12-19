using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralButtonEvents : MonoBehaviour {


    public void onShootButtonClicked()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.Shoot();
        }
    }
}
