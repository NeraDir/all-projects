using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundSliderComponent : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TMP_Text _percentText;
    
    private void Awake()
    {
       
        slider = GetComponent<Slider>();
        slider.value = SettingsManager.SoundVolume * 100f;
        _percentText.text = slider.value.ToString("0") + "%";
        slider.onValueChanged.AddListener(x =>
        {
            SettingsManager.changeSoundVolume?.Invoke(x);
            _percentText.text = slider.value.ToString("0") + "%";
        });
    }
}
