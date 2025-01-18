using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LuckyGameMenuComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject luckyGameHowToPlay;

    [SerializeField]
    private Text luckyPlayerBestScoreDisplay;

    private void Start()
    {
        if (LuckyGameControllerComponent.LuckyPlayerSeesGameHowToPlay != 1)
        {
            luckyGameHowToPlay.SetActive(true);
            LuckyGameControllerComponent.LuckyPlayerSeesGameHowToPlay = 1;
        }
        luckyPlayerBestScoreDisplay.text = LuckyGameControllerComponent.LuckyPlayerBestScore.ToString("0") + " C";
    }

    public void OnClickLuckyStartGame() 
    {
        SceneManager.LoadScene("LuckyGameScene");
    }

    public void OnClickLuckyCloseGame() 
    {
        Application.Quit();
    }
}
