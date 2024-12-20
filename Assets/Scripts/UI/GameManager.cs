using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundEffect soundEffect;
    public ParticleSystem delRotEffect;
    public MusicManager musicManager;
    public FadeController fadeController;
    public static GameManager Instance; //Singleton
    public GameObject inGameUI;
    public GameObject gaugeBar;
    public Image gameoverBoard;
    public TMP_Text resultScore;
    public ImgsFillDynamic imgsFillDynamic;
    public GameObject pauseMenu; // 일시정지 메뉴 UI
    public Button resumeButton; // Resume 버튼
    public Button restartButton; // Restart 버튼
    public Button homeButton; // Home 버튼
    public bool isGameOver;
    public bool isPaused = false; // 일시정지 상태 관리
    public int skillGauge;
    public int skillGaugeMax;

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

        // 버튼 클릭 이벤트 등록
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        homeButton.onClick.AddListener(GoToHome);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Q to use a skill
        if (Input.GetKeyDown(KeyCode.Q) && skillGauge == skillGaugeMax)
        {
            if (soundEffect != null)
            {
                soundEffect.PlayQSound();
            }

            Debug.Log("skillGauge : 0");
            skillGauge = 0;
            var allObjects = FindObjectsOfType<VeggiesCombine>();
            var filteredObjects = allObjects.Where(obj => obj.isElapsed).ToList();
            foreach (var obj in filteredObjects)
            {
                Instantiate(delRotEffect, obj.transform.position, Quaternion.identity).Play();
                obj.Combine();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // 일시정지 상태 토글
        pauseMenu.SetActive(isPaused); // UI 활성화/비활성화

        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 멈춤
        }
        else
        {
            Time.timeScale = 1f; // 게임 재개
        }
    }

    public void OnGameOver()
    {
        if (isGameOver == false)
        {
            gameoverBoard.gameObject.SetActive(true);
            imgsFillDynamic.gameObject.SetActive(false);
            inGameUI.gameObject.SetActive(false);
            gaugeBar.gameObject.SetActive(false);
            resultScore.text = "Score:\n" + ScoreManager.Instance.score;
            isGameOver = true;
            Time.timeScale = 0f; // 게임 멈춤
            Debug.Log("Game Over");
        }
    }

    public void shinyCombine(int point)
    {
        skillGauge = Mathf.Min(skillGaugeMax, skillGauge + point);
        Debug.Log($"skillGauge : {skillGauge}");
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        // 현재 씬 다시 로드
        Time.timeScale = 1f; // 시간 재개
        fadeController.ChangeScene(SceneManager.GetActiveScene().name);
    }

    public void GoToHome()
    {
        // 홈 씬으로 이동 (메인 메뉴)
        Time.timeScale = 1f; // 시간 재개
        fadeController.ChangeScene("Lobby"); // "HomeScene"을 홈 씬 이름으로 대체
    }
}
