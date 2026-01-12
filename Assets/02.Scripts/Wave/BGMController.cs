using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;

    public void PlayBGM(AudioClip clip)
    {
        if (bgmAudioSource.clip == clip) return;

        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
}
