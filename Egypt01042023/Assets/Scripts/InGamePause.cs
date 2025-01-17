using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePause : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject ShopPanel;
    public GameObject PausePanel;

    public void Pause()
    {
        Time.timeScale = 0f;

        PausePanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    public void Shop()
    {
        Time.timeScale = 0f;

        ShopPanel.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;

        PausePanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    public void fromShop()
    {
        Time.timeScale = 1f;

        ShopPanel.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menuScene");
    }
}
