using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : Window
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectSlider;

    [SerializeField] private Text _musicText;
    [SerializeField] private Text _effectText;

    public override void Init()
    {
        _musicText.text = (SettingsController.MusicVolume * 100).ToString("0") + "%";
        _effectText.text = (SettingsController.EffectsVolume * 100).ToString("0") + "%";
        _musicSlider.value = SettingsController.MusicVolume;
        _effectSlider.value = SettingsController.EffectsVolume;

        _musicSlider.onValueChanged.AddListener(OnMusicSliderChange);
        _effectSlider.onValueChanged.AddListener(OnEffectSliderChange);
        base.Init();
    }

    private void OnDestroy()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicSliderChange);
        _effectSlider.onValueChanged.RemoveListener(OnEffectSliderChange);
    }

    private void OnMusicSliderChange(float value)
    {
        SettingsController.MusicVolume = value;
        _musicText.text = (SettingsController.MusicVolume * 100).ToString("0") + "%";
    }

    private void OnEffectSliderChange(float value)
    {
       SettingsController.EffectsVolume = value;
        _effectText.text = (SettingsController.EffectsVolume * 100).ToString("0") + "%";
    }
}
