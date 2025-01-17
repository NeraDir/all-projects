using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        GameManager.LevelCount = 0;
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene("MenuScene");
    }
}
