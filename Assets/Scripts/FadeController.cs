using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // ���� Image
    public float fadeDuration; // ���̵� �ð�

    private void Start()
    {
        // �ʱ�ȭ: ���̵��� ȿ��
        StartCoroutine(FadeIn());
    }

    public void ChangeScene(string sceneName)
    {
        // Scene ��ȯ
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        color.a = 1f;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeDuration; // ������ ����
            fadeImage.color = color;
            yield return null; // ���� ���������� ���
        }

        color.a = 0f; // ���� �������� ����
        fadeImage.color = color;
    }

    private IEnumerator FadeOut(string sceneName)
    {
        Color color = fadeImage.color;
        color.a = 0f;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime / fadeDuration; // ������ ����
            fadeImage.color = color;
            yield return null; // ���� ���������� ���
        }

        color.a = 1f; // ���� ���������� ����
        fadeImage.color = color;

        SceneManager.LoadScene(sceneName);
    }

}
