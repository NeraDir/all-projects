using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrostingMenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject howToPlayPage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("FrostingHowToPlayPage"))
        {
            howToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("FrostingHowToPlayPage", 1);
        }
    }

    public void OnClickLaunchLevel(string input) 
    {
        FrostingGameManager.frostingDefaultLevelKey = input;
        SceneManager.LoadScene("FrostingGame");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
