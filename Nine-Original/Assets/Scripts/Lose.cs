using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public TMP_Text score1;

    public void SetScore(int score)
    {
        score1.text = score.ToString();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
