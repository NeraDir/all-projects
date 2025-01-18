using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _aboutScreen;
    [SerializeField] private GameObject _menuScreen;

    [SerializeField] private AudioClip _menuClip;
    
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("TigerPlayerEnetryGame"))
        {
            _aboutScreen.SetActive(true);
            _menuScreen.SetActive(false);
            PlayerPrefs.SetInt("TigerPlayerEnetryGame", 1);
        }
        SettingsManager.changeMusic?.Invoke(_menuClip);
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("ForTenGameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
