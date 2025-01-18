using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    public static float MusicVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("CurrentMusicVolumeLlinoRimsSaveKey"))
                return PlayerPrefs.GetFloat("CurrentMusicVolumeLlinoRimsSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetFloat("CurrentMusicVolumeLlinoRimsSaveKey", value);
        }
    }

    public static float SoundVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("CurrentSoundVolumeLlinoRimsSaveKey"))
                return PlayerPrefs.GetFloat("CurrentSoundVolumeLlinoRimsSaveKey");
            return 1;
        }
        set
        {
            PlayerPrefs.SetFloat("CurrentSoundVolumeLlinoRimsSaveKey", value);
        }
    }
    
    [SerializeField]
    private Slider _musicVolumeSlider;
    
    [SerializeField]
    private Slider _soundVolumeSlider;

    public static Slider MusicVolumeSlider;
    public static Slider SoundVolumeSlider;
    
   public void OnClickChooseBg(int value)
   {
       GameController.CurrentBgIndex = value;
   }

   private void Awake()
   {
       MusicVolumeSlider = _musicVolumeSlider;
       SoundVolumeSlider = _soundVolumeSlider;
       MusicVolumeSlider.value = MusicVolume;
       SoundVolumeSlider.value = SoundVolume;
       MusicVolumeSlider.maxValue = 1;
       MusicVolumeSlider.minValue = 0;
       SoundVolumeSlider.maxValue = 1;
       SoundVolumeSlider.minValue = 0;
       MusicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
       SoundVolumeSlider.onValueChanged.AddListener(ChangeSoundVolume);
   }

   private void ChangeMusicVolume(float value)
   {
       MusicVolume = value;
   }

   private void ChangeSoundVolume(float value)
   {
       SoundVolume = value;
   }
}
