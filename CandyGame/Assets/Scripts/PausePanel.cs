using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour, IService
{
    [SerializeField] private GameObject panel;

    public void Open()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1;

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;

    }
}
