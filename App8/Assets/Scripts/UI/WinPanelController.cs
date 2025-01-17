using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelController : MonoBehaviour
{
    public TMP_Text NewRecordTXT;
    public GameObject BonusBtn;

    public void Init()
    {
        if (GameManager.Instance.GameTimer < GameManager.BestTimeSeconds)
        {
            GameManager.BestTimeSeconds = GameManager.Instance.GameTimer;
            NewRecordTXT.text = $"NEW TIME RECORD !\r\n{GameManager.BestTimeSeconds} Seconds";
            NewRecordTXT.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        GameManager.LevelCount++;
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
