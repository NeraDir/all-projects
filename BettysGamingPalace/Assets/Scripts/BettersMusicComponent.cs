using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettersMusicComponent : MonoBehaviour
{
    public Action<AudioClip> playSound;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    public static BettersMusicComponent instance;

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
        _musicSource.volume = ProfileData.BettersMusicVolume;
        _soundSource.volume = ProfileData.BettersSoundVolume;
    }
}
