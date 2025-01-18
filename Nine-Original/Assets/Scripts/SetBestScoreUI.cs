using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetBestScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScore;

    private void Start()
    {
        bestScore.text = SaveClass.MaxScore.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
