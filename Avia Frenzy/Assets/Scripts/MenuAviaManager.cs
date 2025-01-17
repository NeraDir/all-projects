using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAviaManager : MonoBehaviour
{
    public GameObject howToPlayWindow;

    public TMP_Text maxreachedScoreDisplayer; 

    private void Start()
    {
        if (!PlayerPrefs.HasKey("howtoplayingwindowopensavekey"))
        {
            howToPlayWindow.SetActive(true);
            PlayerPrefs.SetString("howtoplayingwindowopensavekey", "true");
        }
        maxreachedScoreDisplayer.text = GameAviaManager.MaxReachedScore.ToString("0");
    }

    public void LaunchGame() 
    {
        SceneManager.LoadScene("AviaGameScene");
    }

    public void CloseGame() 
    {
        Application.Quit();
    }
}
