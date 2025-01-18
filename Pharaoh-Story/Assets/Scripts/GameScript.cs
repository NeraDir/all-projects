using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject PausePanel;
    public GameObject GameResultPanel;

    public static int currenCoins;
    public static int currentScore;

    public int MenuSceneIndex;

    [SerializeField]
    private TMP_Text[] showScore;

    [SerializeField]
    private TMP_Text[] showCoins;

    public Image TimerSlider;
    public TMP_Text TimerTXT;

    public Image FindPhonto;

    private void Awake()
    {
        currentScore = 0;
        currenCoins = 0;
    }

    public void OnClickPause() 
    {
        Time.timeScale = 0;
        GamePanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    private void LateUpdate()
    {
        foreach (var item in showScore)
        {
            item.text = currentScore.ToString();
        }

        foreach (var item in showCoins)
        {
            item.text = currenCoins.ToString();
        }
    }

    public void OnClickResume() 
    {
        Time.timeScale = 1;
        GamePanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void UpdateTime(float time)
    {
        StopAllCoroutines();
        TimerTXT.text = $"{time} s";
        currentScore += 1;
        currenCoins += 2;
        TimerSlider.fillAmount = time / time;
        StartCoroutine(Timer(time));
    }

    IEnumerator Timer(float time)
    {
        float timer = 0;

        while(timer < time)
        {
            yield return new WaitForSeconds(1);
            timer++;
            TimerTXT.text = $"{time - timer} s";
            TimerSlider.fillAmount = (time - timer) / time;
            yield return null;
        }

        if(timer >= time)
        {
            GameResultPanel.SetActive(true);
            Time.timeScale = 0;
        }
        //Logic Lose
    }

    public void OnClickOpenMenu() 
    {
        UserData.userMoney += currenCoins;
        if (currentScore > UserData.userBestRecord)
        {
            UserData.userBestRecord = currentScore;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(MenuSceneIndex);
    }

    public void OnClickOpenPauseMenu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(MenuSceneIndex);
    }

    public void OnClickRestart() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickPlayAgain() 
    {
        UserData.userMoney += currenCoins;
        if (currentScore > UserData.userBestRecord)
        {
            UserData.userBestRecord = currentScore;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
