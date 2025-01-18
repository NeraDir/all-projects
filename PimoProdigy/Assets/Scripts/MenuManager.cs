using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static Slider musicSlider;
    public static Slider soundSlider;

    [SerializeField]
    private Slider _musicSlider;

    [SerializeField]
    private Slider _soundSlider;

    [SerializeField]
    private GameObject _menu;

    [SerializeField]
    private GameObject _info;

    private void Awake()
    {
        musicSlider = _musicSlider;
        soundSlider = _soundSlider;

        if (!PlayerPrefs.HasKey("PimoProdigyInfoShowKey"))
        {
            _info.SetActive(true);
            _menu.SetActive(false);
            PlayerPrefs.SetInt("PimoProdigyInfoShowKey", 1);
        }
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
