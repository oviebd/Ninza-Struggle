using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [HideInInspector] public static ScoreManager instance;
    [HideInInspector] public int currentScore = 0;

    public Text scoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        ResetScore(); 
    }

    public void AddScore(int score)
    {
        currentScore = currentScore + score;
        scoreText.text = currentScore + "";
    }

    public void ResetScore()
    {
        currentScore = 0;
        AddScore(0);
    }
	

}
