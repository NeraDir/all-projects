using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_MenuPage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bestScoreText;

    [SerializeField]
    private UI_TutorialPage uI_TutorialPage;
    

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("EnterToGane"))
        {
            PlayerPrefs.SetInt("EnterToGane", 7);
            ShowTutorialPage();
        }

        if (ScoreData.bestScore == 0)
            bestScoreText.text = "BEST SCORE: 0";
        else
            bestScoreText.text = "BEST SCORE: " + ScoreData.bestScore;
        
    }

    public void TapPlayButton()
    {
        LoadGameScene();
    }
    public void TapExitButton()
    {
        CloseGame();
    }
    public void TapQuestionButton()
    {
        GetComponent<Animator>().SetInteger("stateID", 1);
    }

    public void ShowTutorialPage()
    {
        uI_TutorialPage.gameObject.SetActive(true);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
