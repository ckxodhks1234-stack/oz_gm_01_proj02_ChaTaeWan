using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;

    public static BGMController Instance {  get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //중복 제거
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        //같은 브금이면 리턴
        if (bgmAudioSource.clip == clip && bgmAudioSource.isPlaying) return;

        bgmAudioSource.Stop();
        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }
}
