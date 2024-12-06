using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }    // Singleton
    public int score;
    public TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null) //Singleton
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AddScore(0);
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = "Score: " + score;
    }

}
