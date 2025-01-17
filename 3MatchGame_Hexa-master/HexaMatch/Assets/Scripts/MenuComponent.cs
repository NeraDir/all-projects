using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _aboutScreen;

    [SerializeField]
    private Text _showMaxLevel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CapCakeessAboutShowKey"))
        {
            _aboutScreen.SetActive(true);
            PlayerPrefs.SetInt("CapCakeessAboutShowKey", 1);
        }
        _showMaxLevel.text = EffectManager.MaxLeve.ToString();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("HexaMatch");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
