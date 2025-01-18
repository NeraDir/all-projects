using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static Action<AudioClip> playSound;

    private AudioSource _muiscPlayer;
    private AudioSource _soundPlayer;

    private void Awake()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        AudioSource[] sources = GetComponentsInChildren<AudioSource>();
        _muiscPlayer = sources[0];
        _soundPlayer = sources[1];
        playSound += OnSoundPlay;
    }

    private void OnDestroy()
    {
        playSound -= OnSoundPlay;
    }

    private void OnSoundPlay(AudioClip clip)
    {
        _soundPlayer.PlayOneShot(clip);
    }
}
