using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashSceneController : MonoBehaviour {

    public Slider slider;
  //  public Text progressText;

	void Start () {
        //   LoadLevel();
        slider.gameObject.SetActive(false);
     //   progressText.gameObject.SetActive(false);
    }

    public void LoadLevel()
    {
        slider.gameObject.SetActive(true);
     //   progressText.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncLevel());
    }

    IEnumerator LoadAsyncLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            int processInt = ((int)progress * 100);

            slider.value = progress;

            if (processInt < 1)
                processInt = 1;

         //   progressText.text = (processInt) + " %";
           // progressText.text =( progress * 100) + " 100%";
          //  Debug.Log(progress);
            yield return null;
        }
    }
}
