using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip bgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬을 이동해도 AudioManager가 파괴되지않음
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        BGM_On(bgm);
    }

    public void BGM_On(AudioClip bgm) //
    {
        audioSource.clip = bgm;
        audioSource.Play();
    }

    public void SwitchBGM(AudioClip newBGM)
    {
        audioSource.Stop();
        BGM_On(newBGM);
    }

    public void StopBGM() // 배경음악이 계속 흘러나오는걸 방지하기 위한 코드
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    public void PlayOneShot(AudioClip clip, float volume = 2.0f)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
    public void SetVolume(float volume) // 볼륨 조절
    {
        audioSource.volume = volume;
    }
}
