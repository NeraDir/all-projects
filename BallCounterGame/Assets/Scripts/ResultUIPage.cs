using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultUIPage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelCompleteText;
    [SerializeField]
    private TMP_Text nextButtonText;

    [SerializeField]
    private GameObject gamePlayUIPage;

    private void OnEnable()
    {
        gamePlayUIPage.SetActive(false);

        if (GamePlayController.hasRigthAnswerByPlayer)
        {
            nextButtonText.text = "NEXT";
            levelCompleteText.text = GamePlayController.levelNumber + " LEVEL\nCOMPLETE";
        }
        else
        {
            levelCompleteText.text = "YOU\nLOSE";
            nextButtonText.text = "RESTART";
        }

        if (GamePlayController.levelNumber > GamePlayController.maxLevel)
        {
            GamePlayController.maxLevel = GamePlayController.levelNumber;
        }
    }
    private void OnDisable()
    {
        
    }

    public void TapNextLevelButton()
    {

        if (GamePlayController.hasRigthAnswerByPlayer)
        {
            GamePlayController.levelNumber++;
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
    public void TapMenuButton()
    {
        SceneManager.LoadScene("SCENE_MENU");
    }


}
