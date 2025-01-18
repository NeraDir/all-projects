using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loosepanel : MonoBehaviour
{
    public static bool gameOver;

    public void LoadMiniGame() 
    {
        if (gameOver)
        {
            MiniGameController.GameWinningValue = GameManager.currentWinValue;
            SceneManager.LoadScene("MiniGameScene");
        }
    }
}
