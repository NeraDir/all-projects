using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldMenu : MonoBehaviour
{
    public TMP_Text bestScoreTXT;

    public GameObject goldHowToPlayScreen;

    private void Start()
    {
        bestScoreTXT.text = GoldLoader.goldBestScoreValue.ToString("0") + " B";
        if (!PlayerPrefs.HasKey("goldHowToPlayOpenedSave"))
        {
            goldHowToPlayScreen.SetActive(true);
            PlayerPrefs.SetInt("goldHowToPlayOpenedSave", 1);
        }
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("GoldGame");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
