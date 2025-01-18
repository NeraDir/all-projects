using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static Action<AudioClip> playSound;
    public static Action<AudioClip> changeMusic;
    public static Action<float> changeMusicVolume;
    public static Action<float> changeSoundVolume;
    
    private AudioSource _musicSource;
    private AudioSource _effectsSource;

    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat("TigerMusicVolume", 1f);
        set => PlayerPrefs.SetFloat("TigerMusicVolume", value);
    }
    
    public static float SoundVolume
    {
        get => PlayerPrefs.GetFloat("TigerSoundVolume", 1f);
        set => PlayerPrefs.SetFloat("TigerSoundVolume", value);
    }
    
    private void Awake()
    {
        if (FindObjectsOfType<SettingsManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        _effectsSource = transform.GetChild(1).GetComponent<AudioSource>();
        
        _musicSource.volume = MusicVolume;
        _effectsSource.volume = SoundVolume;
        
        changeMusicVolume += OnChangeMusicVolume;
        changeSoundVolume += OnChangeSoundVolume;
        changeMusic += OnChangeMusic;
        playSound += OnPlaySound;
    }

    private void OnChangeMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }
    
    private void OnChangeMusicVolume(float value)
    {
        MusicVolume = value / 100f;
        _musicSource.volume = MusicVolume;
    }
    
    private void OnChangeSoundVolume(float value)
    {
        SoundVolume = value / 100f;
        _effectsSource.volume = SoundVolume;
    }

    private void OnDestroy()
    {
        playSound -= OnPlaySound;
        changeMusicVolume -= OnChangeMusicVolume;
        changeSoundVolume -= OnChangeSoundVolume;
        changeMusic -= OnChangeMusic;
    }

    private void OnPlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }
}
