using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiddayGameManager : MonoBehaviour
{
    public static int middayBestLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("middayBestLevelSaveKey"))
                return PlayerPrefs.GetInt("middayBestLevelSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("middayBestLevelSaveKey", value);
        }
    }

    public static int middayPlayerStartFoodCount
    {
        get
        {
            if (PlayerPrefs.HasKey("middayPlayerStartFoodCountSaveKey"))
            {
                return PlayerPrefs.GetInt("middayPlayerStartFoodCountSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("middayPlayerStartFoodCountSaveKey", value);
        }
    }

    public static string middayPlayerName;

    public static int middayBestScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("middayBestScoreSaveKey"))
                return PlayerPrefs.GetInt("middayBestScoreSaveKey");
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("middayBestScoreSaveKey", value);
        }
    }

    public static int middayTigerEatingCoungvalue
    {
        get
        {
            if (PlayerPrefs.HasKey("middayTigerEatingCoungvalueSaveKey"))
            {
                return PlayerPrefs.GetInt("middayTigerEatingCoungvalueSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("middayTigerEatingCoungvalueSaveKey", value);
        }
    }

    public GameObject[] middayFoods;

    public Sprite[] middayFoodsSprites;

    public Transform spawnPos;

    public Transform checkPos;

    public TMP_Text[] middayScoreDisplay;

    public static int middayScore;

    public TMP_Text middayNeedFoodDisplay;

    public Image middayNeedFoodIamgeDispay;

    public static int middayCurrentLevel;

    public static int middayTotalFood;

    public GameObject middayResultScreen;

    public TMP_Text[] middayLevelDisplay;

    public static int middayNeedFoodIndex;

    public Canvas canvas;

    public GameObject middayNextButton;

    public GameObject[] middayPlayerHearts;

    public static int middayPlayerHeartsCount;

    private bool middayLoose;

    private List<Sequence> sequences = new List<Sequence>();

    private void Start()
    {
        if (middayCurrentLevel == 0)
            middayCurrentLevel = 1;
        middayPlayerHeartsCount = 4;
        foreach (var item in middayPlayerHearts)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(item.transform.DOScale(new Vector3(1.13f, 1.13f, 1.13f), 0.5f));
            sequence.Append(item.transform.DOScale(new Vector3(1, 1, 1), 0.5f));
            sequence.SetLoops(-1, LoopType.Yoyo);
            sequences.Add(sequence);
        }
        SetNewFood();
    }

    private void SetNewFood() 
    {
        middayTotalFood = Random.Range(1, 2) * middayCurrentLevel;
        middayNeedFoodIndex = Random.Range(0, middayFoods.Length);
        middayNeedFoodIamgeDispay.sprite = middayFoodsSprites[middayNeedFoodIndex];
        middayNeedFoodDisplay.text = "x" + middayTotalFood.ToString();
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood() 
    {
        GameObject lastFood = null;
        while (true)
        {
            if (lastFood == null) 
            {
                lastFood = Instantiate(middayFoods[Random.Range(0, middayFoods.Length)], spawnPos.position, Quaternion.identity, spawnPos.parent);
                lastFood.GetComponent<MiddayFoodComponent>().canvas = canvas;
                lastFood.transform.SetSiblingIndex(6);
            }
            else
            {
                if (lastFood.transform.position.x > checkPos.transform.position.x)
                {
                    lastFood = Instantiate(middayFoods[Random.Range(0, middayFoods.Length)], spawnPos.position, Quaternion.identity, spawnPos.parent);
                    lastFood.GetComponent<MiddayFoodComponent>().canvas = canvas;
                    lastFood.transform.SetSiblingIndex(6);
                }
            }
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (middayTotalFood <= 0)
        {
            middayLoose = false;
            middayResultScreen.SetActive(true);
        }
        foreach (var item in middayLevelDisplay)
        {
            item.text = "LVL " + middayCurrentLevel.ToString();
        }
        foreach (var item in middayScoreDisplay)
        {
            item.text = middayScore.ToString("0");
        }

        if (middayPlayerHeartsCount <= 0)
        {
            middayLoose = true;
            middayResultScreen.SetActive(true);
        }

        if (middayLoose)
        {
            middayNextButton.SetActive(false);
        }

        for (int i = 0; i < middayPlayerHearts.Length; i++)
        {
            if (i > middayPlayerHeartsCount - 1)
            {
                sequences[i].Kill();
                middayPlayerHearts[i].transform.DOScale(Vector3.zero, 0.5f);
            }
        }

        middayNeedFoodDisplay.text = "x" + middayTotalFood.ToString();
    }

    public void OnNextButtonPressed() 
    {
        middayCurrentLevel++;
        middayScore += Random.Range(5, 10) * middayCurrentLevel;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnRestartButtonPressed()
    {
        middayScore = 0;
        middayCurrentLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 
    }

    public void OnMenuButtonPressed()
    {
        
        middayScore += Random.Range(5, 10) * middayCurrentLevel;
        if (middayCurrentLevel > middayBestLevel)
        {
            middayBestLevel = middayCurrentLevel;
        }
        if (middayScore > middayBestScore)
        {
            middayBestScore = middayScore;
        }
        middayScore = 0;
        middayCurrentLevel = 0;
        SceneManager.LoadScene("MiddayMenuScene");
    }
}
