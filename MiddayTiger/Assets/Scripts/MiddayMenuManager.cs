using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiddayMenuManager : MonoBehaviour
{
    public GameObject MiddaYHowPlayScreen;

    public TMP_Text MiddayMaxReachedLevel;
    public TMP_Text MiddayMaxScore;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MiddayPlayerFirstEnterValueSaveKey"))
        {
            MiddaYHowPlayScreen.SetActive(true);
            PlayerPrefs.SetString("MiddayPlayerFirstEnterValueSaveKey", "true");
        }
        MiddayMaxReachedLevel.text = "LVL " + MiddayGameManager.middayBestLevel.ToString("0");
        MiddayMaxScore.text = MiddayGameManager.middayBestScore.ToString("0");
    }

    public void OnPlayButtonPressed() 
    {
        MiddayGameManager.middayScore = 0;
        MiddayGameManager.middayCurrentLevel = 0;
        SceneManager.LoadScene("MiddayGameScene");
    }

    public void OnExitButtonPressed() 
    {
        Application.Quit();
    }
}
