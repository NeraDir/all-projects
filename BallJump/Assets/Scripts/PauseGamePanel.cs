using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGamePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject gamePanel;

    private void OnEnable()
    {
        gamePanel.SetActive(false);
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        gamePanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void GoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Continue()
    {
        gameObject.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
