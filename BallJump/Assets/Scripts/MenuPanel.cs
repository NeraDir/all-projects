using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text recordTxt;

    [SerializeField]
    private GameObject tutorPage;


    private void OnEnable()
    {
        string recordString = "";

        if (BallConfigsController.mainRecordMetters == 0)
        {
            recordString = "0m";
        }
        else
        {
            recordString = BallConfigsController.mainRecordMetters.ToString("#m");
        }

        recordTxt.text = "RECORD: " + recordString;



        if (!PlayerPrefs.HasKey("FirstEnter"))
        {
            PlayerPrefs.SetInt("FirstEnter", 1);
            OpenHowToPlay();
        }


    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void OpenHowToPlay()
    {
        tutorPage.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }

}
