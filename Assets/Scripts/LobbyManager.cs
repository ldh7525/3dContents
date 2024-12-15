using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    // ī�� ǥ��� ����� ����
    public Button gameStartButton;
    public Button scoreRecordButton;
    public Button settingsButton;
    public Button gameQuitButton;
    public FadeController fadeController;

    private void Awake()
    {


        // ��ư �̺�Ʈ ���
        gameStartButton.onClick.AddListener(StartGame);
        scoreRecordButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        gameQuitButton.onClick.AddListener(QuitGame);
    }

    // ���� ����
    private void StartGame()
    {

        fadeController.ChangeScene("Game");
    }


    // ���� ȭ��
    private void OpenSettings()
    {
        Debug.Log("���� ȭ�� ����!");
    }

    // ���� ����
    private void QuitGame()
    {
        Debug.Log(1);
        Application.Quit();
    }
}
