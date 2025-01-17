using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener((float value) => ChangeVolume(value));
        slider.value = Music.Instance.Volume;
    }
    public void ChangeVolume(float value)
    {
        Music.Instance.SetVolume(value);
    }
}
