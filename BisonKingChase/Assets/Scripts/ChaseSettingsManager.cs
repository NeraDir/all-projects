using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseSettingsManager : MonoBehaviour
{
    [SerializeField]
    private Slider _musicSlider;

    [SerializeField]
    private Slider _soundSlider;

    [SerializeField] private Button _vibrationButton;

    [SerializeField] private Sprite _vibrateSprite;
    [SerializeField] private Sprite _unVibrateSprite;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    [SerializeField] private AudioClip _soundClip;

    public static Action playSound;

    private void Awake()
    {
        Image buttonImage = _vibrationButton.transform.GetChild(0).GetComponent<Image>();
        _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        _soundSource = transform.GetChild(1).GetComponent<AudioSource>();
        buttonImage.sprite = ChasePlayerDataComponent.ChaseVibrationState ? _vibrateSprite : _unVibrateSprite;
        _vibrationButton.onClick.AddListener(() =>
        {
            ChasePlayerDataComponent.ChaseVibrationState = !ChasePlayerDataComponent.ChaseVibrationState;
            buttonImage.sprite = ChasePlayerDataComponent.ChaseVibrationState ? _vibrateSprite : _unVibrateSprite;
        });

        _musicSlider.value = ChasePlayerDataComponent.ChaseMuiscVolume;
        _soundSlider.value = ChasePlayerDataComponent.ChaseSoundsVolume;
        
        _musicSlider.onValueChanged.AddListener(value =>
        {
            ChasePlayerDataComponent.ChaseMuiscVolume = value;
            _musicSource.volume = ChasePlayerDataComponent.ChaseMuiscVolume;
        });
        _soundSlider.onValueChanged.AddListener(value =>
        {
            ChasePlayerDataComponent.ChaseSoundsVolume = value;
            _soundSource.volume = ChasePlayerDataComponent.ChaseSoundsVolume;
        });
        _musicSource.volume = ChasePlayerDataComponent.ChaseMuiscVolume;
        _soundSource.volume = ChasePlayerDataComponent.ChaseSoundsVolume;
        playSound += OnPlaySound;
    }

    private void OnDestroy()
    {
        playSound -= OnPlaySound;
    }

    private void OnPlaySound()
    {
        _soundSource.PlayOneShot(_soundClip);
    }
}
