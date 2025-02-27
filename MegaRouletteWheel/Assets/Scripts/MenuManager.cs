using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private LevelItem _itemPref;
    [SerializeField] private Transform[] _itemSpawnPosition;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    [SerializeField] private TMP_Text _musicTxt;
    [SerializeField] private TMP_Text _soundTxt;

    private void Start()
    {
        for (int i = 0; i < 18; i++)
        {
            LevelItem newItem = Instantiate(_itemPref, new Vector3(Random.Range(_itemSpawnPosition[0].position.x, _itemSpawnPosition[1].position.x), _itemSpawnPosition[0].position.y, _itemSpawnPosition[0].position.z),Quaternion.Euler(0,0,Random.Range(-360,360)), _itemSpawnPosition[0].parent);
            newItem.Init(i);
        }

        _musicSlider.value = SettingsManager.MusicVolume;
        _musicTxt.text = (_musicSlider.value * 100).ToString("00") + "%";

        _soundSlider.value = SettingsManager.MusicVolume;
        _soundTxt.text = (_soundSlider.value * 100).ToString("00") + "%";

        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
    }

    private void OnMusicVolumeChanged(float value)
    {
        SettingsManager.MusicVolume = value;
        _musicTxt.text = (value * 100).ToString("00") + "%";
    }

    private void OnSoundVolumeChanged(float value)
    {
        SettingsManager.SoundVolume = value;
        _soundTxt.text = (value * 100).ToString("00") + "%";
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
