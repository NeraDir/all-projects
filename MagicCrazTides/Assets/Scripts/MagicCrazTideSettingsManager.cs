using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCrazTideSettingsManager : MonoBehaviour
{
    public static float MagicCrazTideMusicVolume
    {
        get => PlayerPrefs.GetFloat("MagicCrazTideMusicVolume", 1);
        set => PlayerPrefs.SetFloat("MagicCrazTideMusicVolume", value);
    }

    public static float MagicCrazTideSoundVolume
    {
        get => PlayerPrefs.GetFloat("MagicCrazTideSoundVolume", 1);
        set => PlayerPrefs.SetFloat("MagicCrazTideSoundVolume", value);
    }

    public static Action<AudioClip> playSound;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    private static MagicCrazTideSettingsManager instance;

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
        _musicSource.volume = MagicCrazTideMusicVolume;
        _soundSource.volume = MagicCrazTideSoundVolume;
    }
}
