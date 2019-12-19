using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour {

    public static BossHealthBarController instance;
    public Image healthImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetBossHealthBar(int currentHealth, int maximumHealth)
    {
        healthImage.fillAmount = currentHealth * 1.00f /( maximumHealth*1.00f);
    }

}
