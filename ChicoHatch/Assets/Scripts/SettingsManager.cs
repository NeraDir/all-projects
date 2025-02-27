using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat("ChicoMusicVolume", 1f);
        set => PlayerPrefs.SetFloat("ChicoMusicVolume", value);
    }

    public static float SoundVolume
    {
        get => PlayerPrefs.GetFloat("ChicoSoundVolume", 1f);
        set => PlayerPrefs.SetFloat("ChicoSoundVolume", value);
    }

    public Action<AudioClip> onPlaySound;
    public static SettingsManager instance;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
            _soundSource = transform.GetChild(1).GetComponent<AudioSource>();
            _musicSource.volume = MusicVolume;
            _soundSource.volume = SoundVolume;
            onPlaySound += OnPlaySound;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        onPlaySound -= OnPlaySound;
    }

    private void OnPlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    private void LateUpdate()
    {
        if (_musicSource != null)
            _musicSource.volume = MusicVolume;
        if (_soundSource != null)
            _soundSource.volume = SoundVolume;
    }
}
