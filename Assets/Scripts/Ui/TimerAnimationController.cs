using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAnimationController : MonoBehaviour {

    public Text text;
    public int timer_sec;
    int current_time;


    private void Start()
    {
        ResetData();
    }

    void ResetData()
    {
        current_time = timer_sec;
        text.text = current_time+"";
    }
    private void OnEnable()
    {
        ResetData();
    }

    public void DecreseTimer()
    {
        current_time = current_time - 1;
        text.text = current_time + "";
        if (current_time <= 0)
        {
            TimesUp();
        }
    }

     void TimesUp()
    {
        GameManager.instance.ReStartFromPrevPoint();
        gameObject.SetActive(false);
    }
}
