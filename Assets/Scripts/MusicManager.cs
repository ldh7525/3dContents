using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip clip1; // 첫 번째 음악
    public AudioClip clip2; // 두 번째 음악
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        // 첫 번째 음악 재생 (Loop 비활성화)
        audioSource.loop = false;
        audioSource.clip = clip1;
        audioSource.volume = 0f; // 시작 볼륨 0으로 설정
        audioSource.Play();

        // 첫 번째 음악 페이드 인 (최종 볼륨 0.2f)
        yield return StartCoroutine(FadeIn(2.0f, 0.2f)); // 2초 동안 페이드 인

        // 첫 번째 음악 끝나기 4초 전에 대기
        yield return new WaitForSeconds(clip1.length - 4);

        // 첫 번째 음악 페이드 아웃
        yield return StartCoroutine(FadeOut(2.0f)); // 2초 동안 페이드 아웃

        // 두 번째 음악 재생 (Loop 활성화)
        audioSource.clip = clip2;
        audioSource.loop = true;
        audioSource.Play();

        // 두 번째 음악 페이드 인 (최종 볼륨 0.2f)
        yield return StartCoroutine(FadeIn(2.0f, 0.2f)); // 2초 동안 페이드 인
    }

    private IEnumerator FadeOut(float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.volume = 0; // 완전히 꺼짐
    }

    private IEnumerator FadeIn(float duration, float targetVolume)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume; // 최종 볼륨 설정
    }

}