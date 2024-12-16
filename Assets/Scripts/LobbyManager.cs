using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    // ī�� ǥ��� ����� ����
    public Button gameStartButton;
    public Button gameQuitButton;
    public FadeController fadeController;
    private bool isStarting = false;

    private void Awake()
    {


        // ��ư �̺�Ʈ ���
        gameStartButton.onClick.AddListener(StartGame);
        gameQuitButton.onClick.AddListener(QuitGame);
    }

    // ���� ����
    public void StartGame()
    {
        if (isStarting == false) 
        {
            isStarting = true;
            fadeController.ChangeScene("Game");
        }
    }


    // ���� ȭ��
    private void OpenSettings()
    {
        Debug.Log("���� ȭ�� ����!");
    }

    // ���� ����
    public void QuitGame()
    {
        if (isStarting == false)
        {
            Debug.Log(1);
            Application.Quit();
        }
    }
}
