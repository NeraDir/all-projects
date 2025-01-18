using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelCompletedPage : MonoBehaviour
{
    [SerializeField]
    private int levelNumber;
   

    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject menuButton;

    [SerializeField]
    private TMP_Text levelStateText;

    private void OnEnable()
    {

        if (levelNumber == 3)
        {
            levelNumber = 0;

            nextButton.SetActive(false);
            menuButton.SetActive(true);

            levelStateText.text = "All levels are completed!";
        }
        else
        {
            nextButton.SetActive(true);
            menuButton.SetActive(false);

            levelStateText.text = "Level completed!";
        }
    }


    public void ClickNextScenePage()
    {
        string nextScene = "";

        if (levelNumber == 1)
        {
            nextScene = "level_2";
        }
        else if (levelNumber == 2)
        {
            nextScene = "level_3";
        }
        else if (levelNumber == 3)
        {
            nextScene = "Menu";
        }

        SceneManager.LoadScene(nextScene);
    }

    public void ClickMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }


}
