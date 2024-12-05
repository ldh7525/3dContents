using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //싱글턴
    public TMP_Text gameOverText;
    public Button retryButton;
    public TMP_Text resultScore;
    public bool isGameOver;

    private void Awake()
    {
        if (Instance == null) //싱글턴
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        resultScore.gameObject.SetActive(true);

        resultScore.text = "Score:\n" + ScoreManager.Instance.score;
        isGameOver = true;
    }

    
    public void OnRetry() // Retry 버튼을 눌렀을 때의 처리
    {
        // 현재 씬 이름(SceneManager.GetActiveScene().name)을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
