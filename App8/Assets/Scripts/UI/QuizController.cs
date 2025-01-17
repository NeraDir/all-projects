using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    public WinPanelController winPanel;
    public TMP_Text QuestionTXT;
    public TMP_Text CorrectTXT;

    public List<QuestionStruct> questions = new();

    public int EndCountCorrect = 5;
    public int CurrentCorrect = 0;

    QuestionStruct CurrentQuestion;

    private void Start()
    {
        CorrectTXT.text = $"{CurrentCorrect} / {EndCountCorrect}";
        GenerateNewQuestion();
    }

    public void GenerateNewQuestion()
    {
        CurrentQuestion = questions[Random.Range(0, questions.Count)];
        QuestionTXT.text = CurrentQuestion.QuestionString;
    }

    public void ClickYes()
    {
        if(CurrentQuestion.correct)
        {
            CurrentCorrect++;
            CorrectTXT.text = $"{CurrentCorrect} / {EndCountCorrect}";
            GenerateNewQuestion();

            if (CurrentCorrect >= EndCountCorrect)
            {
                winPanel.BonusBtn.SetActive(false);
                gameObject.SetActive(false);
                GameManager.CountHelpPazzle += 3;
            }
        }
        else
        {
            GoMenu();
        }
    }

    public void ClickNo()
    {
        if (!CurrentQuestion.correct)
        {
            CurrentCorrect++;
            CorrectTXT.text = $"{CurrentCorrect} / {EndCountCorrect}";
            GenerateNewQuestion();

            if (CurrentCorrect >= EndCountCorrect)
            {
                winPanel.BonusBtn.SetActive(false);
                gameObject.SetActive(false);
                GameManager.CountHelpPazzle += 3;
            }
        }
        else
        {
            GoMenu();
        }
    }

    public void GoMenu()
    {
        GameManager.LevelCount = 0;
        GameManager.Instance.UnPauseGame();
        SceneManager.LoadScene("MenuScene");
    }
}

[System.Serializable]
public struct QuestionStruct
{
    public string QuestionString;
    public bool correct;
}