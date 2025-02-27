using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat("MusicVolumeSaveKey", 1);
        set => PlayerPrefs.SetFloat("MusicVolumeSaveKey", value);
    }

    public static float SoundVolume
    {
        get => PlayerPrefs.GetFloat("SoundVolumeSaveKey", 1);
        set => PlayerPrefs.SetFloat("SoundVolumeSaveKey", value);
    }

    public static Action<AudioClip> playSound;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    private static SettingsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            AudioSource[] sources = GetComponentsInChildren<AudioSource>();
            _musicSource = sources[0];
            _soundSource = sources[1];
            playSound += OnPlaySound;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        playSound -= OnPlaySound;
    }

    private void OnPlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    private void LateUpdate()
    {
        _musicSource.volume = MusicVolume;
        _soundSource.volume = SoundVolume;
    }
}
