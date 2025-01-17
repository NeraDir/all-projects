using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CandysGameManager : MonoBehaviour
{
    public static int candysPlayerEnterToGameAnalyticsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("candysPlayerEnterToGameAnalyticsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("candysPlayerEnterToGameAnalyticsCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("candysPlayerEnterToGameAnalyticsCountSaveKey", value);
        }
    }

    public static string candysPlayerGeneratedName;

    public static int candysPlayerGenerateCandyCount
    {
        get
        {
            if (PlayerPrefs.HasKey("candysPlayerGenerateCandyCountSaveKey"))
            {
                return PlayerPrefs.GetInt("candysPlayerGenerateCandyCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("candysPlayerGenerateCandyCountSaveKey", value);
        }
    }

    public static float candysSpawningTime 
    {
        get 
        {
            if (PlayerPrefs.HasKey("candysSpawningTimeSaveKey"))
            {
                return PlayerPrefs.GetFloat("candysSpawningTimeSaveKey");
            }
            return 3;
        }
        set 
        {
            PlayerPrefs.SetFloat("candysSpawningTimeSaveKey", value);
        }
    }

    public static bool candysGameEnded;

    public static int candysHealth;

    public static int candysCurrentScore 
    {
        get 
        {
            if (PlayerPrefs.HasKey("candysCurrentScoreSaveKey"))
            {
                return PlayerPrefs.GetInt("candysCurrentScoreSaveKey");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("candysCurrentScoreSaveKey", value);
        }
    }

    public static int candysBestScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("candysBestScoreSaveKey"))
            {
                return PlayerPrefs.GetInt("candysBestScoreSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("candysBestScoreSaveKey", value);
        }
    }

    public static int candysCurrentLevel 
    {
        get
        {
            if (PlayerPrefs.HasKey("candysCurrentLevelSaveKey"))
            {
                return PlayerPrefs.GetInt("candysCurrentLevelSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("candysCurrentLevelSaveKey", value);
        }
    }

    public static int candysMaximumAchievedLevel 
    {
        get
        {
            if (PlayerPrefs.HasKey("candysMaximumAchievedLevelSaveKey"))
            {
                return PlayerPrefs.GetInt("candysMaximumAchievedLevelSaveKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("candysMaximumAchievedLevelSaveKey", value);
        }
    }

    public static int candysNeedScore;

    public static float candysTimer;

    public static AudioSource candysAudioPlayer;

    [SerializeField]
    private AudioSource candysTempPlayer;

    [SerializeField]
    private GameObject[] candysPrefabs;

    [SerializeField]
    private Transform[] candysSpawningPositions;

    [SerializeField]
    private Button[] menuButton;

    [SerializeField]
    private Button[] restartButton;

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private TMP_Text[] scandysCurrentScoreDisplayer;

    [SerializeField]
    private TMP_Text[] candysCurrentLevelDisplayer;

    [SerializeField]
    private TMP_Text candysNeedScoreDisplayer;

    [SerializeField]
    private TMP_Text candysLeftTimeDispalyer;

    [SerializeField]
    private GameObject candysLoosePanel;

    [SerializeField]
    private GameObject candysWinPanel;

    [SerializeField]
    private GameObject[] candysHealthImages;

    private void Start()
    {
        candysNeedScore = 75 * candysCurrentLevel;
        candysAudioPlayer = candysTempPlayer;
        candysTimer = 45;
        candysHealth = 3;
        candysGameEnded = false;
        foreach (var item in menuButton)
        {
            item.onClick.AddListener(Menu);
        }
        foreach (var item in restartButton)
        {
            item.onClick.AddListener(Restart);
        }
        nextButton.onClick.AddListener(Next);
        StartCoroutine(SpawningCandys());
    }

    private IEnumerator SpawningCandys() 
    {
        while (!candysGameEnded)
        {
            foreach (var item in candysSpawningPositions)
            {
                Instantiate(candysPrefabs[Random.Range(0, candysPrefabs.Length)], item);
            }
            yield return new WaitForSeconds(candysSpawningTime / candysCurrentLevel);
        }
    }

    private void LateUpdate()
    {
        if (candysGameEnded)
            return;
        candysNeedScoreDisplayer.text = "NEED SCORE: " +candysNeedScore.ToString("0");
        foreach (var item in candysCurrentLevelDisplayer)
        {
            item.text = "LVL " + candysCurrentLevel.ToString("0");
        }

        foreach (var item in scandysCurrentScoreDisplayer)
        {
            item.text = "SCORE: " + candysCurrentScore.ToString("0");
        }

        if (candysCurrentScore >= candysNeedScore)
        {
            candysWinPanel.SetActive(true);
            candysGameEnded = true;
        }

        candysTimer -= Time.deltaTime;
        if (candysTimer <= 0)
        {
            candysGameEnded = true;
            candysLoosePanel.SetActive(true);
            candysTimer = 0;
        }

        if (candysHealth <= 0)
        {
            candysGameEnded = true;
            candysLoosePanel.SetActive(true);
        }

        for (int i = 0; i < candysHealthImages.Length; i++)
        {
            if (i < candysHealth)
            {
                candysHealthImages[i].SetActive(true);
            }
            else
            {
                candysHealthImages[i].SetActive(false);
            }
        }

        if (candysMaximumAchievedLevel < candysCurrentLevel)
        {
            candysMaximumAchievedLevel = candysCurrentLevel;
        }

        if (candysCurrentScore > candysBestScore)
        {
            candysBestScore = candysCurrentScore;
        }

        candysLeftTimeDispalyer.text = "LEFT TIME: " + candysTimer.ToString("0.0") + "s";
    }

    private void Next() 
    {
        candysCurrentLevel += 1;
        candysCurrentScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Menu() 
    {
        candysCurrentScore = 0;
        candysCurrentLevel = 1;
        SceneManager.LoadScene("MenumingScene");
    }

    private void Restart() 
    {
        candysCurrentScore = 0;
        candysCurrentLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnApplicationQuit()
    {
        candysCurrentScore = 0;
        candysCurrentLevel = 1;
    }
}
