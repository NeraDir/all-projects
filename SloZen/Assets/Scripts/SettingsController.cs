using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat("SloZenMusicVolume", 1f);
        set => PlayerPrefs.SetFloat("SloZenMusicVolume", value);
    }

    public static float EffectsVolume
    {
        get => PlayerPrefs.GetFloat("SloZenEffectsVolume", 1f);
        set => PlayerPrefs.SetFloat("SloZenEffectsVolume", value);
    }

    public static Action<AudioClip> onPlayEffect;
    public static SettingsController instance;

    private AudioSource _musicSource;
    private AudioSource _effectsSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
            _effectsSource = transform.GetChild(1).GetComponent<AudioSource>();
            _musicSource.volume = MusicVolume;
            _effectsSource.volume = EffectsVolume;
            onPlayEffect += OnPlayEffect;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        onPlayEffect -= OnPlayEffect;
    }

    private void OnPlayEffect(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    private void LateUpdate()
    {
        if (_musicSource != null)
            _musicSource.volume = MusicVolume;
        if (_effectsSource != null)
            _effectsSource.volume = EffectsVolume;
    }
}
