using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 인스턴스를 위한 정적 프로퍼티
    public static ScoreManager Instance { get; private set; }    // 싱글톤 인스턴스를 위한 정적 프로퍼티

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 인스턴스를 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재할 경우 새 오브젝트 삭제
        }
    }

}
