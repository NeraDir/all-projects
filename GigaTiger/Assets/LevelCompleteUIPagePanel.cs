using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteUIPagePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text starsDisplayText;




    private void OnEnable()
    {
        Time.timeScale = 0;
        if (starsDisplayText != null)
        {
            starsDisplayText.text = GamePlayController.starsCount.ToString();
        }

    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void LoadNextLevel()
    {
        GamePlayController.currentLevelNumber++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMenu()
    {
        GamePlayController.lastLevel = 1;
        SceneManager.LoadScene("MENUSCENE");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
