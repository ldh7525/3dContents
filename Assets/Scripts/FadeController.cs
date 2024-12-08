using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // 검은 Image
    public float fadeDuration; // 페이드 시간

    private void Start()
    {
        // 초기화: 페이드인 효과
        StartCoroutine(FadeIn());
    }

    public void ChangeScene(string sceneName)
    {
        // Scene 전환
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        color.a = 1f;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeDuration; // 일정량 감소
            fadeImage.color = color;
            yield return null; // 다음 프레임으로 대기
        }

        color.a = 0f; // 완전 투명으로 설정
        fadeImage.color = color;
    }

    private IEnumerator FadeOut(string sceneName)
    {
        Color color = fadeImage.color;
        color.a = 0f;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime / fadeDuration; // 일정량 증가
            fadeImage.color = color;
            yield return null; // 다음 프레임으로 대기
        }

        color.a = 1f; // 완전 불투명으로 설정
        fadeImage.color = color;

        SceneManager.LoadScene(sceneName);
    }

}
