using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    public TMP_Text ScoreTXT;

    public void INIT(int score)
    {
        ScoreTXT.text = $"{score}";

        if (score > ValuteController.Instance.BestScore)
            ValuteController.Instance.BestScore = score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
