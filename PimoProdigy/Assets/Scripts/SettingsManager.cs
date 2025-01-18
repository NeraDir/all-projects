using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private Image _bgImage;

    [SerializeField]
    private Sprite[] _bgSprites;

    private AudioSource _musicSource;
    private AudioSource _soundSource;

    private Slider _musicSlider;
    private Slider _soundSlider;

    public static int bgIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("PimoProdigyBgIndexKey"))
                return PlayerPrefs.GetInt("PimoProdigyBgIndexKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PimoProdigyBgIndexKey", value);
        }
    }

    public static Action<int> changeBg;


    private void Start()
    {
        foreach (var item in FindObjectsOfType<SettingsManager>())
        {
            if (item != this)
            {
                Destroy(item.gameObject);
            }
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        _bgImage = GameObject.Find("bg").GetComponent<Image>();
        _bgImage.sprite = _bgSprites[bgIndex];
        changeBg += OnChangeBg;
        _musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        _soundSource = transform.GetChild(1).GetComponent<AudioSource>();
        _soundSlider = MenuManager.soundSlider;
        _soundSlider.onValueChanged.AddListener(OnChangeSoundVolume);
        _musicSlider = MenuManager.musicSlider;
        _musicSlider.onValueChanged.AddListener(OnChangeMusicVolume);
        _soundSlider.value = _soundSource.volume;
        _musicSlider.value = _musicSource.volume;
    }

    private void LateUpdate()
    {
        if (_bgImage == null)
        {
            _bgImage = GameObject.Find("bg").GetComponent<Image>();
        }
        if (_soundSlider == null)
        {
            _soundSlider = MenuManager.soundSlider;
            _soundSlider.onValueChanged.AddListener(OnChangeSoundVolume);
        }
        if (_musicSlider == null)
        {
            _musicSlider = MenuManager.musicSlider;
            _musicSlider.onValueChanged.AddListener(OnChangeSoundVolume);
        }
    }

    private void OnChangeMusicVolume(float value)
    {
        _musicSource.volume = value;
    }

    private void OnChangeSoundVolume(float value)
    {
        _soundSource.volume = value;
    }

    private void OnDestroy()
    {
        changeBg -= OnChangeBg;
    }

    private void OnChangeBg(int value)
    {
        bgIndex = value;
        _bgImage.sprite = _bgSprites[bgIndex];
    }
}
