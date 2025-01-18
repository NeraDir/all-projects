using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text LevelTXT;
    [SerializeField] private TMP_Text ScoreTXT;
    [SerializeField] private GameObject RestartBtn;
    [SerializeField] private GameObject NextLvlBtn;
    [SerializeField] private GameObject MainMenu;

    public GameController gameController;

    public void Init(float score)
    {
        if (score >= 80)
        {
            LevelTXT.text = "Level Completed";

            if (gameController.Level.NextLevelName != "")
                NextLvlBtn.SetActive(true);

            gameController.Level.Completed = 1;

            MainMenu.SetActive(true);
        }
        else if (score < 80)
        {
            LevelTXT.text = "Level not Completed";

            RestartBtn.SetActive(true);
            MainMenu.SetActive(true);
        }

        ScoreTXT.text = score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextLvl()
    {
        SceneManager.LoadScene(gameController.Level.NextLevelName);
    }
}
