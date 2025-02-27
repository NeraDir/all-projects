using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCrazTideMenuManager : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    [SerializeField] private Text _musicText;
    [SerializeField] private Text _soundText;

    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameObject _aboutScreen;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MagicCrazTideFirstEntry"))
        {
            _menuScreen.SetActive(false);
            _aboutScreen.SetActive(true);
            PlayerPrefs.SetInt("MagicCrazTideFirstEntry", 1);
        }

        _musicSlider.value = MagicCrazTideSettingsManager.MagicCrazTideMusicVolume;
        _soundSlider.value = MagicCrazTideSettingsManager.MagicCrazTideSoundVolume;

        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        _soundSlider.onValueChanged.AddListener(OnSoundVolumeChange);

        _musicText.text = (MagicCrazTideSettingsManager.MagicCrazTideMusicVolume * 100).ToString("0.0") + "%";
        _soundText.text = (MagicCrazTideSettingsManager.MagicCrazTideSoundVolume * 100).ToString("0.0") + "%";
    }

    private void OnMusicVolumeChange(float value)
    {
        MagicCrazTideSettingsManager.MagicCrazTideMusicVolume = value;
        _musicText.text = (MagicCrazTideSettingsManager.MagicCrazTideMusicVolume * 100).ToString("0.0") + "%";
    }

    private void OnSoundVolumeChange(float value)
    {
        MagicCrazTideSettingsManager.MagicCrazTideSoundVolume = value;
        _soundText.text = (MagicCrazTideSettingsManager.MagicCrazTideSoundVolume * 100).ToString("0.0") + "%";
    }
}
