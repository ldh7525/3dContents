using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    // 싱글턴 인스턴스를 위한 정적 프로퍼티
    public static ScoreManager Instance { get; private set; }    // 싱글턴 인스턴스를 위한 정적 프로퍼티
    public int score;
    public TMP_Text scoreText;

    private void Awake()
    {
        // 싱글턴 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재할 경우 새 오브젝트 삭제
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
