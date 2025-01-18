using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgSetter : MonoBehaviour
{
    private Image _bgImage;
    [SerializeField]
    private Sprite[] _bgSprites;
    
    private AudioSource _musicSource;
    private AudioSource _soundSource;

    public static Action<AudioClip> playSound;
    
    private void Awake()
    {
        transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
        _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        _soundSource = transform.GetChild(1).GetComponent<AudioSource>();
        playSound += PlaySound;
    }

    private void PlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }
    
    private void LateUpdate()
    {
        if (_musicSource != null)
        {
            _musicSource.volume = SettingsWindow.MusicVolume;
        }

        if (_soundSource != null)
        {
            _soundSource.volume = SettingsWindow.SoundVolume;   
        }
        if (_bgImage == null)
        {
            _bgImage = GameObject.Find("bg").GetComponent<Image>();
            
        }
        if(_bgImage != null)
            _bgImage.sprite = _bgSprites[GameController.CurrentBgIndex];
    }
}
