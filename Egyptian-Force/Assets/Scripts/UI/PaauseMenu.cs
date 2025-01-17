using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void SetPause()
    {
        pausePanel.SetActive(true);
    }

    public void UnsetPause()
    {
        pausePanel.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
