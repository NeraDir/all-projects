using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameObject _infoScreen;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    [SerializeField] private Text _musicTxt;
    [SerializeField] private Text _soundTxt;

    [SerializeField] private Text _maxReachTxt;

    public static int MaxreachedLevel
    {
        get => PlayerPrefs.GetInt("ChicoMaxreachedLevel", 0);
        set => PlayerPrefs.SetInt("ChicoMaxreachedLevel", value);
    }

    public static int CurrentSkinIndex
    {
        get => PlayerPrefs.GetInt("ChicoCurrentSkinIndex", 0);
        set => PlayerPrefs.SetInt("ChicoCurrentSkinIndex", value);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("ChicoFirstEntry"))
        {
            _menuScreen.SetActive(false);
            _infoScreen.SetActive(true);
            PlayerPrefs.SetInt("ChicoFirstEntry", 1);
        }
        _maxReachTxt.text = "LEVEL " + (MaxreachedLevel + 1).ToString();
        _musicSlider.value = SettingsManager.MusicVolume;
        _soundSlider.value = SettingsManager.SoundVolume;
        _musicTxt.text = (SettingsManager.MusicVolume * 100).ToString("0") + "%";
        _soundTxt.text = (SettingsManager.SoundVolume * 100).ToString("0") + "%";
        _musicSlider.onValueChanged.AddListener(OnMusicChange);
        _soundSlider.onValueChanged.AddListener(OnSoundChange);
    }

    private void OnDestroy()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicChange);
        _soundSlider.onValueChanged.RemoveListener(OnSoundChange);
    }

    private void OnMusicChange(float value)
    {
        SettingsManager.MusicVolume = value;
        _musicTxt.text = (SettingsManager.MusicVolume * 100).ToString("0") + "%";
        
    }

    private void OnSoundChange(float value)
    {
        SettingsManager.SoundVolume = value;

        _soundTxt.text = (SettingsManager.SoundVolume * 100).ToString("0") + "%";
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
