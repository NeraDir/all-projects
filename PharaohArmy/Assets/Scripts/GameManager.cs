using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private mainAnimationController mainAnimationController;

    [SerializeField]
    private Transform indicator;

    [SerializeField]
    private Animator mainAnimator;

    [SerializeField]
    private TMP_Text showPlayerScore;

    [SerializeField]
    private TMP_Text showEnemieScore;

    public int enemieScore;

    public int playerScore;

    public static bool cqanClick;

    [SerializeField]
    private GameObject pausePanel;

    public static int playerRecordCount 
    {
        get 
        {
            if (PlayerPrefs.HasKey("PlayerCricketRecordSavekey"))
            {
                return PlayerPrefs.GetInt("PlayerCricketRecordSavekey");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("PlayerCricketRecordSavekey", value);
        }
    }

    public static int enemieRecordCount
    {
        get
        {
            if (PlayerPrefs.HasKey("enemieRecordCountCricketdSavekey"))
            {
                return PlayerPrefs.GetInt("enemieRecordCountCricketdSavekey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("enemieRecordCountCricketdSavekey", value);
        }
    }

    private void Start()
    {
        cqanClick = false;
        Time.timeScale = 1;
    }

    private void LateUpdate()
    {
        showEnemieScore.text = enemieScore.ToString("0");
        showPlayerScore.text = playerScore.ToString("0");
        enemieRecordCount = enemieScore;
        playerRecordCount = playerScore;
    }

    public void OnClickSetAnima() 
    {
        if (cqanClick)
            return;
        cqanClick = true;
        if (indicator.localPosition.y > 350)
        {
            mainAnimator.SetInteger("mainAnimator", 3);
            Invoke(nameof(AddScoreToPlayer), 4);
        }
        else
        {
            mainAnimator.SetInteger("mainAnimator", 2);
            Invoke(nameof(AddScoreToEnemie), 4);
        }
    }

    public void AddScoreToPlayer()
    {
        playerScore++;
    }

    public void AddScoreToEnemie()
    {
        enemieScore++;
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void Menu() 
    {
        SceneManager.LoadScene(5);
    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume() 
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause() 
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
