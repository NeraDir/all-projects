using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SETTINGSMANAGER : MonoBehaviour
{
    public static Action<AudioClip> playSound;

    private AudioSource _musicSource;
    public AudioSource _soundSource;

    public static SETTINGSMANAGER instance;

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
        _musicSource.volume = PLAYERDATA.MUSICVOLUME;
        _soundSource.volume = PLAYERDATA.SOUNDVOLUME;
    }
}
