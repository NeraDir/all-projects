using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelHandler : MonoBehaviour
{
    public TMP_Text coinsDisplayText;
    public Slider progressSlider;

    public Ball ball;
    public FinalPlatrorm finalPlatrorm;

    public TMP_Text levelNumberDisplayText;


    public static int levelCoinsCount;


    public GameObject completeUIPanel;
    public GameObject gameOverUIPanel;

    public int levelNumber;



    private float ballLeftDistance = 0;
    private Vector3 ballStartPosition;

    private float levelDistance = 0;

    private bool canCalculateDistance;


    public int levelMaxCoinsCount;
    public static int maxCoinsCount;
    public static int levelStarsCount;


    //public LevelData _levelData;
    public static LevelData levelData;

    public string nextLevelSceneKey;
    public string levelKey;


    private bool canShowLosePage = true;


    private void Awake()
    {
        //Debug.Log("All Coins: " + Configs.allCoinsCount);
        LoadLevelData();
        //Debug.Log("All Coins: " + Configs.allCoinsCount);
        maxCoinsCount = levelMaxCoinsCount;

        levelCoinsCount = 0;
        ballStartPosition = ball.transform.position;

        levelDistance = Vector3.Distance(ball.transform.position, finalPlatrorm.transform.position);

        progressSlider.value = levelDistance;
        progressSlider.maxValue = levelDistance;

        canCalculateDistance = true;

        levelNumberDisplayText.text = "LEVEL " + levelNumber;
    }

    private void OnEnable()
    {
        FinalPlatrorm.BallOnStartFinalPlatformEvent += BallInStartFinalPlatform;
        FinalPlatrorm.BallOnFinalFinalPlatformEvent += BallInFinalFinalPlaatform;

        BallDeathTrigger.BallTrigerEvent += GameOver;
    }
    private void OnDisable()
    {
        FinalPlatrorm.BallOnStartFinalPlatformEvent -= BallInStartFinalPlatform;
        FinalPlatrorm.BallOnFinalFinalPlatformEvent -= BallInFinalFinalPlaatform;

        BallDeathTrigger.BallTrigerEvent -= GameOver;
    }


    private void Update()
    {
        coinsDisplayText.text = levelCoinsCount.ToString();

        CalculateDistance();

       // Debug.Log("(UPD)All Coins: " + Configs.allCoinsCount);

    }


    private void LoadLevelData()
    {
        levelData = new();
        levelData.levelKey = levelKey;
        levelData.starsCount = PlayerPrefs.GetInt(levelKey);
    }

    private void CalculateDistance()
    {

        if (!canCalculateDistance)
            return;

        ballLeftDistance = Vector3.Distance(ball.transform.position, finalPlatrorm.transform.position);

        if (ballLeftDistance < 1.5)
            canCalculateDistance = false;

        progressSlider.value = progressSlider.maxValue - ballLeftDistance;

    }


    public void BallInStartFinalPlatform()
    {
        //Debug.Log("start");
    }
    public void BallInFinalFinalPlaatform()
    {
        //Debug.Log("ffinall");
        //Configs.allCoinsCount += levelCoinsCount;
        //SaveLevelStars();

        SaveCoins();
        completeUIPanel.SetActive(true);
        canShowLosePage = false;
    }



    private void GameOver()
    {
        if (!canShowLosePage)
            return;

        gameOverUIPanel.SetActive(true);
    }



    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneKey);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MENU SCENE");
    }


    public static void SaveLevelStars()
    {       
        PlayerPrefs.SetInt(levelData.levelKey, levelStarsCount);
    }

    private void SaveCoins()
    {
        //Debug.Log("(Save)All Coins: " + Configs.allCoinsCount);
        //Configs.allCoinsCount = Configs.allCoinsCount + levelCoinsCount;
        Configs.allCoinsCount += levelCoinsCount;
    }
}
