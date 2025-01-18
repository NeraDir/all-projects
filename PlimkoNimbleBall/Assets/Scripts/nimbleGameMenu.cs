using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nimbleGameMenu : MonoBehaviour
{
    [SerializeField]
    private Text _showMaxLevel;

    [SerializeField]
    private GameObject _howToPlay;

    public static int nimbleMaxLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("nimbleMaxLevelSaveKey"))
            {
                return PlayerPrefs.GetInt("nimbleMaxLevelSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("nimbleMaxLevelSaveKey", value);
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("nimbleHowToPlaySaveKey"))
        {
            _howToPlay.SetActive(true);
            PlayerPrefs.SetInt("nimbleHowToPlaySaveKey", 1);
        }
        _showMaxLevel.text = nimbleMaxLevel.ToString("0");
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("nimbleGameScene");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
