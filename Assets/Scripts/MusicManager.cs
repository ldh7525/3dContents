using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip clip1; // ù ��° ����
    public AudioClip clip2; // �� ��° ����
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        // ù ��° ���� ��� (Loop ��Ȱ��ȭ)
        audioSource.loop = false;
        audioSource.clip = clip1;
        audioSource.volume = 0f; // ���� ���� 0���� ����
        audioSource.Play();

        // ù ��° ���� ���̵� �� (���� ���� 0.2f)
        yield return StartCoroutine(FadeIn(2.0f, 0.2f)); // 2�� ���� ���̵� ��

        // ù ��° ���� ������ 4�� ���� ���
        yield return new WaitForSeconds(clip1.length - 4);

        // ù ��° ���� ���̵� �ƿ�
        yield return StartCoroutine(FadeOut(2.0f)); // 2�� ���� ���̵� �ƿ�

        // �� ��° ���� ��� (Loop Ȱ��ȭ)
        audioSource.clip = clip2;
        audioSource.loop = true;
        audioSource.Play();

        // �� ��° ���� ���̵� �� (���� ���� 0.2f)
        yield return StartCoroutine(FadeIn(2.0f, 0.2f)); // 2�� ���� ���̵� ��
    }

    private IEnumerator FadeOut(float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.volume = 0; // ������ ����
    }

    private IEnumerator FadeIn(float duration, float targetVolume)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume; // ���� ���� ����
    }

}