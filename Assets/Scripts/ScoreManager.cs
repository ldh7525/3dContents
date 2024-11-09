using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ��� ���� ���� ������Ƽ
    public static ScoreManager Instance { get; private set; }    // �̱��� �ν��Ͻ��� ���� ���� ������Ƽ

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ν��Ͻ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ��� �� ������Ʈ ����
        }
    }

}
