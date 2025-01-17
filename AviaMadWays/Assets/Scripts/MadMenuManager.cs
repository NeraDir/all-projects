using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MadMenuManager : MonoBehaviour
{
    public GameObject HowToPlayPanel;

    public TMP_Text maxStarsCollectedDisplay;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MadOpenedHowTOPlay"))
        {
            HowToPlayPanel.SetActive(true);
            PlayerPrefs.SetInt("MadOpenedHowTOPlay", 1);
        }
        maxStarsCollectedDisplay.text = MadGameManager.madBestCountOfCollectedStars.ToString();
    }

    public void ClickOpenGame() 
    {
        SceneManager.LoadScene("MadGameScene");
    }

    public void ClickCloseGame() 
    {
        Application.Quit();
    }
}
