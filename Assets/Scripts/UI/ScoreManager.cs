using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ��� ���� ���� ������Ƽ
    public static ScoreManager Instance { get; private set; }    // �̱��� �ν��Ͻ��� ���� ���� ������Ƽ
    public int score;
    public TMP_Text scoreText;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ��� �� ������Ʈ ����
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
