using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [SerializeField] private List<SoundAudioClip> musicAudioClips;
    [SerializeField] private List<SoundAudioClip> sfxAudioClips;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(Sound.BackgroundMusic);
    }

    public void PlayMusic(Sound sound)
    {
        SoundAudioClip soundAudioClip = musicAudioClips.Find(s => s.sound == sound);
        if(soundAudioClip == null) Debug.LogWarning("Sound not found: " + sound);
        else
        {
            musicSource.clip = soundAudioClip.audioClip;
            musicSource.Play();
        }
    }
    
    public void PlaySfx(Sound sound)
    {
        SoundAudioClip soundAudioClip = sfxAudioClips.Find(s => s.sound == sound);
        if(soundAudioClip == null) Debug.LogWarning("Sound not found: " + sound);
        else
        {
            sfxSource.PlayOneShot(soundAudioClip.audioClip);
        }
    }

    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }
    public void ChangeSfxVolume(float value)
    {
        sfxSource.volume = value;
    }
}

public enum Sound
{
    BackgroundMusic,
    ButtonSfx,
    LevelUp,
    Order,
    BillPaid,
}

[Serializable]
public class SoundAudioClip
{
    public Sound sound;
    public AudioClip audioClip;
}
