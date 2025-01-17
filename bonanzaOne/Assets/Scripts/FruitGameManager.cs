using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum FruitsMoveDirections
{
    Left,
    Right,
}

public class FruitGameManager : MonoBehaviour
{
    public static Sprite TargetFruitSprite;

    public static int CurrentScoreCount;
    public static int TargetScoreCount;

    public static int CurrentLevelValue
    {
        get
        {
            if (PlayerPrefs.HasKey("CherryManiaFruitsCurrentLevelValueSave"))
            {
                return PlayerPrefs.GetInt("CherryManiaFruitsCurrentLevelValueSave");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("CherryManiaFruitsCurrentLevelValueSave", value);
        }
    }

    public static int pantherTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("pantherTryCountssaves"))
            {
                return PlayerPrefs.GetInt("pantherTryCountssaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("pantherTryCountssaves", value);
        }
    }

    public static string panthermathName;

    public static int pantherMathWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("pantherMathWinsCountSave"))
            {
                return PlayerPrefs.GetInt("pantherMathWinsCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("pantherMathWinsCountSave", value);
        }
    }

    public static int BestReachLevelValue
    {
        get
        {
            if (PlayerPrefs.HasKey("CherryManiaFruitsBestReachLevelValueSave"))
            {
                return PlayerPrefs.GetInt("CherryManiaFruitsBestReachLevelValueSave");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("CherryManiaFruitsBestReachLevelValueSave", value);
        }
    }

    [SerializeField]
    private Sprite[] _fruitSprites;

    [SerializeField]
    private TMP_Text _fruitTimeTxt;

    [SerializeField]
    private TMP_Text _fruitCurrentLevelTxt;

    [SerializeField]
    private TMP_Text _currentScoreTxt;

    [SerializeField]
    private TMP_Text _targetScoreTxt;

    [SerializeField]
    private TMP_Text _resultScreenTxt;

    [SerializeField]
    private TMP_Text _resultAdditionalTxt;

    [SerializeField]
    private Image _targetFruitImage;

    [SerializeField]
    private GameObject _resultScreen;

    [SerializeField]
    private GameObject _nextButton;

    [SerializeField]
    private Slider _fruitSlider;

    [SerializeField]
    private FruitLineSpawnerComponent[] fruitLineSpawners;

    private float _fruitTimerValue;

    private void Start()
    {
        _fruitTimerValue = 30;
        TargetFruitSprite = _fruitSprites[Random.Range(0, _fruitSprites.Length)];
        _targetFruitImage.sprite = TargetFruitSprite;
        CurrentScoreCount = 0;
        float moveSpeed = 0;
        for (int i = 0; i < CurrentLevelValue; i++)
        {
            moveSpeed += 2;
            TargetScoreCount += 500;
        }
        foreach (var item in fruitLineSpawners)
        {
            item.Init(moveSpeed);
        }
    }

    private void LateUpdate()
    {
        if (CurrentScoreCount >= TargetScoreCount)
        {
            _resultScreen.SetActive(true);
            _resultScreenTxt.text = "AMAZING!";
            _nextButton.SetActive(true);
            _resultAdditionalTxt.text = "LEVEL COMPLETED";
            return;
        }
        _fruitTimerValue -= Time.deltaTime;
        if (_fruitTimerValue <= 0)
        {
            _resultScreen.SetActive(true);
            _resultScreenTxt.text = "YOU LOOSE!";
            _nextButton.SetActive(false);
            _resultAdditionalTxt.text = "LEVEL NOT COMPLETED";
            return;
        }
        _fruitSlider.value = Mathf.Lerp(_fruitSlider.value, _fruitTimerValue / 30, 10 * Time.deltaTime);
        if (CurrentScoreCount <= 0)
        {
            CurrentScoreCount = 0;
        }
        _currentScoreTxt.text = CurrentScoreCount.ToString();
        _targetScoreTxt.text = TargetScoreCount.ToString();
        _fruitCurrentLevelTxt.text = CurrentLevelValue.ToString();
        _fruitTimeTxt.text = _fruitTimerValue.ToString("0.0") + "s";
        if (CurrentLevelValue > BestReachLevelValue)
        {
            BestReachLevelValue = CurrentLevelValue;
        }   
    }
    private void OnApplicationQuit()
    {
        CurrentLevelValue = 1;
    }

    public void OnClickPlayAgain()
    {
        CurrentLevelValue = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickNextLevelLoad()
    {
        CurrentLevelValue += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenuLoad()
    {
        CurrentLevelValue = 1;
        SceneManager.LoadScene("MenuScene");
    }
}
