using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject InfoPanel;
    public string NextSceneName;

    private int FirstTime
    {
        get
        {
            if (!PlayerPrefs.HasKey("FirstTimeEnter"))
                return 0;

            return PlayerPrefs.GetInt("FirstTimeEnter");
        }
        set
        {
            PlayerPrefs.SetInt("FirstTimeEnter", value);
        }
    }

    private void Start()
    {
        if(InfoPanel != null)
        {
            if(FirstTime == 0)
            {
                InfoPanel.SetActive(true);
                FirstTime = 1;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void NextScene()
    {
        SceneManager.LoadScene(NextSceneName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
