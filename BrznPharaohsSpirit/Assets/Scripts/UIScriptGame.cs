using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScriptGame : MonoBehaviour
{
    public GameObject PanelInfo;
    public bool gameScene = false;

    public int FirstStart
    {
        get
        {
            if (!PlayerPrefs.HasKey("FirstStartSave"))
                return 0;

            return PlayerPrefs.GetInt("FirstStartSave");
        }
        set
        {
            PlayerPrefs.SetInt("FirstStartSave", value);
        }
    }

    private void Start()
    {
        if(FirstStart == 0 && gameScene)
        {
            Time.timeScale = 0;
            PanelInfo.SetActive(true);
            FirstStart = 1;
        }
        else if(FirstStart == 0)
        {
            PanelInfo.SetActive(true);
        }
    }

    public void DisactivateInfo()
    {
        PanelInfo.SetActive(false);
        Time.timeScale = 1;
    }

    public void GomeNU()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
