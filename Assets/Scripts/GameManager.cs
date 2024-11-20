using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //�̱���
    public TMP_Text gameOverText;
    public Button retryButton;
    public TMP_Text resultScore;
    public bool isGameOver;

    private void Awake()
    {
        if (Instance == null) //�̱���
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

    
    public void OnRetry() // Retry ��ư�� ������ ���� ó��
    {
        // ���� �� �̸�(SceneManager.GetActiveScene().name)�� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
