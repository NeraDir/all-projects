using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonsEndGame : MonoBehaviour
{   

    public void RestartGame()
    {
        EndGame.hasTriggered = false;
        CollisionController.currentCoin = 0;
        EndGame.endGame = false;
        SceneManager.LoadScene("Game");
    }

    public void LeaveMenu()
    {
        EndGame.hasTriggered = false;
        CollisionController.currentCoin = 0;
        SceneManager.LoadScene("MenuScene");
    }
}
