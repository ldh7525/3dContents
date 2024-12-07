using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Singleton
    public Image gameoverBoard;
    public TMP_Text resultScore;
    public bool isGameOver;

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

    public void OnGameOver()
    {
        if (isGameOver == false) 
        {
            gameoverBoard.gameObject.SetActive(true);

            resultScore.text = "Score:\n" + ScoreManager.Instance.score;
            isGameOver = true;
            Debug.Log("Game Over");
        }
    }
}
