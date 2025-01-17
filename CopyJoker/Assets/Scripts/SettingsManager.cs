using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static float MusicVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey("MusicVolume"))
                return 0.05f;

            return PlayerPrefs.GetFloat("MusicVolume");
        }
        set
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }
    public static float SoundVolume
    {
        get
        {
            if (!PlayerPrefs.HasKey("SoundVolume"))
                return 0.05f;

            return PlayerPrefs.GetFloat("SoundVolume");
        }
        set
        {
            PlayerPrefs.SetFloat("SoundVolume", value);
        }
    }

    public Slider MusicSlider;
    public Slider SoundSlider;

    private void Start()
    {
        MusicSlider.maxValue = 0.05f;
        SoundSlider.maxValue = 0.05f;

        MusicSlider.value = MusicVolume;
        SoundSlider.value = SoundVolume;
    }

    public void ChangeMusicVolume()
    {
        MusicVolume = MusicSlider.value;
    }

    public void ChangeSoundVolume()
    {
        SoundVolume = SoundSlider.value;
    }
}
