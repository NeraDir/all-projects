using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text bestScoreText;

    public GameObject howtoplayWindow;

    private void OnEnable()
    {
        bestScoreText.text = "BEST SCORE\n" + Game.recordcountscore;

        if (!PlayerPrefs.HasKey("howtoplayWindow"))
        {
            HowToPlay();
            PlayerPrefs.SetFloat("howtoplayWindow", 0.1f);
        }

    }

    public void Play()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void HowToPlay()
    {
        howtoplayWindow.gameObject.SetActive(true);
    }

    public void Close()
    {
        howtoplayWindow.gameObject.SetActive(false);
    }
}
