using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject InstructionToPlayPage;

    public GameObject menu;

    public TMP_Text BestScoreDisplay;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("DontSeenstruction"))
        {
            InstructionToPlayPage.SetActive(true);
            menu.SetActive(false);
            PlayerPrefs.SetInt("DontSeenstruction", 1);
        }

        BestScoreDisplay.text = GameManager.avikBestScoreValue.ToString();
    }

    public void Play() 
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
