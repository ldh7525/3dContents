using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    // 카멜 표기법 적용된 변수
    public Button gameStartButton;
    public Button scoreRecordButton;
    public Button settingsButton;
    public Button gameQuitButton;
    public FadeController fadeController;

    private void Awake()
    {


        // 버튼 이벤트 등록
        gameStartButton.onClick.AddListener(StartGame);
        scoreRecordButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        gameQuitButton.onClick.AddListener(QuitGame);
    }

    // 게임 시작
    private void StartGame()
    {

        fadeController.ChangeScene("Game");
    }


    // 설정 화면
    private void OpenSettings()
    {
        Debug.Log("설정 화면 열기!");
    }

    // 게임 종료
    private void QuitGame()
    {
        Debug.Log(1);
        Application.Quit();
    }
}
