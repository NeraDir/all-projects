using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayController : MonoBehaviour
{
    public static int levelNumber;
    public static int maxLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey("MAXLEVELSAVE"))
            {
                PlayerPrefs.SetInt("MAXLEVELSAVE", 1);
            }

            return PlayerPrefs.GetInt("MAXLEVELSAVE");
        }
        set
        {
            PlayerPrefs.SetInt("MAXLEVELSAVE", value);
        }
    }

    public static int ballCountsFirstSpawnCount
    {
        get
        {
            if (PlayerPrefs.HasKey("ballCountsFirstSpawnCountSave"))
            {
                return PlayerPrefs.GetInt("ballCountsFirstSpawnCountSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("ballCountsFirstSpawnCountSave", value);
        }
    }

    public static string ballCounterPlayerName;

    public static int ballCounterLevelsPassedCount
    {
        get
        {
            if (PlayerPrefs.HasKey("ballCounterLevelsPassedCountSave"))
            {
                return PlayerPrefs.GetInt("ballCounterLevelsPassedCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("ballCounterLevelsPassedCountSave", value);
        }
    }

    [SerializeField]
    private BallSpawnManager ballSpawnManager;
    [SerializeField]
    private BallSetterManager ballSetterManager;
    [SerializeField]
    private BallDestoyer ballDestoyer;
    [SerializeField]
    private ResultUIPage resultUIPage;
    [SerializeField]
    private GameObject tapToScreenPage;
    [SerializeField]
    private GameObject levelNumberPage;

    [SerializeField]
    private TMP_Text levelNumberText;


    public static int redBallCountInScene;
    public static int greenBallCountInScene;
    public static int blueBallCountInScene;

    public static bool hasRigthAnswerByPlayer;



    private void OnEnable()
    {
        BallSetterManager.TapCheckButtonEvent += CheckBallCounts;
        BallDestoyer.AllBallsDestoyedEvent += ShowBallSetPage;
        BallSpawnUIPage.BallSpawnPageAnimationCompleteEvent += SpawnBalls;
        BallSetterManager.ShowRigthResultCopmleteEvent += ShowRsultPage;


        

        if (!PlayerPrefs.HasKey("CanShowapToSceen"))
        {
            PlayerPrefs.SetInt("CanShowapToSceen", 1);
            ShowTapToScrenPage();
        }
        else
        {
            ShowLevelNumberPage();
        }

      
        hasRigthAnswerByPlayer = true;

        levelNumberText.text = "LEVEL " + levelNumber;

        ballSetterManager.Init();
        ballSpawnManager.Init();
        ballDestoyer.Init(redBallCountInScene, greenBallCountInScene, blueBallCountInScene);
    }
    private void OnDisable()
    {
        BallDestoyer.AllBallsDestoyedEvent -= ShowBallSetPage;
        BallSetterManager.TapCheckButtonEvent -= CheckBallCounts;
        BallSpawnUIPage.BallSpawnPageAnimationCompleteEvent -= SpawnBalls;
        BallSetterManager.ShowRigthResultCopmleteEvent -= ShowRsultPage;
    }

    public static void SetBallNumbersCount(List<int> numbersList)
    {
        redBallCountInScene = numbersList[0];
        greenBallCountInScene = numbersList[1];
        blueBallCountInScene = numbersList[2];
    }

    public void SpawnBalls()
    {
        ballSpawnManager.StartSpawn();
    }


    public void ShowTapToScrenPage()
    {
        tapToScreenPage.SetActive(true);

    }
    public void ShowLevelNumberPage()
    {
        levelNumberPage.SetActive(true);
    }
    public void ShowSpawnBallPage()
    {

    }
    public void ShowBallSetPage()
    {
        ballSetterManager.gameObject.SetActive(true);
    }
    public void ShowRsultPage()
    {
        resultUIPage.gameObject.SetActive(true);
    }



    public void CheckBallCounts(List<(ColorType, int)> values)
    {
        bool result = false;
        bool redBallCountCorrect, greenBallCountCorrect, blueBallCountCorrect;

        redBallCountCorrect = greenBallCountCorrect = blueBallCountCorrect = false;


        for (int i = 0; i < values.Count; i++)
        {
            if (values[i].Item1 == ColorType.Red)
            {
                redBallCountCorrect = (values[i].Item2 == redBallCountInScene ? true : false);
            }
            else if (values[i].Item1 == ColorType.Green)
            {
                greenBallCountCorrect = (values[i].Item2 == greenBallCountInScene ? true : false);
            }
            else if(values[i].Item1 == ColorType.Blue)
            {
                blueBallCountCorrect = (values[i].Item2 == blueBallCountInScene ? true : false);
            }
        }

        result = redBallCountCorrect & greenBallCountCorrect & blueBallCountCorrect;

        Debug.Log(result);
    }

    public void GoNextLevel()
    {
        levelNumber++;
    }
}
