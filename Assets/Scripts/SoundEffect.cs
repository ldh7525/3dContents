using UnityEngine;
using UnityEngine.UI;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource 컴포넌트
    public AudioClip clickSound;    // 버튼 클릭 효과음
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
