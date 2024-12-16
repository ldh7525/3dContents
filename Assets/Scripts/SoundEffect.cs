using UnityEngine;
using UnityEngine.UI;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource ������Ʈ
    public AudioClip clickSound;    // ��ư Ŭ�� ȿ����
    public AudioClip qSound;
    public AudioClip shootSound;
    public AudioClip combineSound;
    
    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, 6.0f);
        }
    }

    public void PlayQSound()
    {
        if (audioSource != null && qSound != null)
        {
            audioSource.PlayOneShot(qSound, 3.0f);
        }
    }

    public void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound, 7.0f);
        }
    }

    public void PlayCombineSound()
    {
        if (audioSource != null && combineSound != null)
        {
            audioSource.PlayOneShot(combineSound, 7.0f);
        }
    }
}
