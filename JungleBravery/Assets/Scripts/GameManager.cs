using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static int playerenterValue
    {
        get
        {
            if (PlayerPrefs.HasKey("playerenterValueSavingKey"))
            {
                return PlayerPrefs.GetInt("playerenterValueSavingKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("playerenterValueSavingKey", value);
        }
    }

    public static string maingamedataString;

    public static int palyerWinsCountValue
    {
        get
        {
            if (PlayerPrefs.HasKey("palyerWinsCountValueSavingKey"))
            {
                return PlayerPrefs.GetInt("palyerWinsCountValueSavingKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("palyerWinsCountValueSavingKey", value);
        }
    }

    public static int tigerCurrentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("tigerCurrentLevelSavingKey"))
            {
                return PlayerPrefs.GetInt("tigerCurrentLevelSavingKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("tigerCurrentLevelSavingKey", value);
        }
    }

    public static int tigerBestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("tigerBestScoreSavingKey"))
            {
                return PlayerPrefs.GetInt("tigerBestScoreSavingKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("tigerBestScoreSavingKey", value);
        }
    }

    [SerializeField]
    private GameObject _foodPieces;

    [SerializeField]
    private Transform _foodSpawnPosition;

    [SerializeField]
    private Image _tigerNeedFoodDisplay;

    [SerializeField]
    private Sprite[] _tigerNeedFoodsSprites;

    [SerializeField]
    private TMP_Text _tigerNeedCountDispaly;

    [SerializeField]
    private Image[] _tigerHeartsImages;

    [SerializeField]
    private TMP_Text[] _scoreDisplay;

    [SerializeField]
    private TMP_Text _winOrLooseTxt;

    [SerializeField]
    private GameObject _nextButton;

    [SerializeField]
    private TMP_Text[] _levelDisplay;

    public static Transform target;

    public static int score;

    public static int needFoodCount;

    public static bool gameLaunched;

    public static int tigerHeartsCount;

    public static int foodIndex;

    private bool loose;

    private GameObject _lastPiece;

    [SerializeField]
    private GameObject _resultPage;

    [SerializeField]
    private Transform _needPosBorder;

    private void Start()
    {

        loose = false;
        gameLaunched = false;
        LinePieceComponente.speed = 2 * (((float)tigerCurrentLevel) / 2);
        tigerHeartsCount = _tigerHeartsImages.Length;
        target = _tigerNeedCountDispaly.transform;
        StartCoroutine(LaunchGame());
    }

    private void LateUpdate()
    {
        if (!gameLaunched)
            return;
        if (tigerHeartsCount <= 0)
        {
            _winOrLooseTxt.text = "LOOSE";
            gameLaunched = false;
            loose = true;
        }
        else if (needFoodCount <= 0)
        {
            _winOrLooseTxt.text = "WIN";
            gameLaunched = false;
            loose = false;
        }

        if (score > tigerBestScore)
        {
            tigerBestScore = score;
        }

        foreach (var item in _scoreDisplay)
        {
            item.text = score.ToString("0");
        }

        foreach (var item in _levelDisplay)
        {
            item.text = tigerCurrentLevel.ToString("0");
        }

        _tigerNeedCountDispaly.text = "NEED X" + needFoodCount.ToString("0");
        _tigerNeedFoodDisplay.sprite = _tigerNeedFoodsSprites[foodIndex];
        for (int i = 0; i < _tigerHeartsImages.Length; i++)
        {
            if (i > tigerHeartsCount - 1)
            {
                _tigerHeartsImages[i].transform.DOScale(Vector3.zero, 0.5f);
            }
        }
    }

    public void OnNextButtonPressed()
    {
        tigerCurrentLevel += 1;
        SceneManager.LoadScene("Game");
    }

    public void OnTakeBonusGameButtonPressed()
    {
        tigerCurrentLevel = 1;
        SceneManager.LoadScene("BonusGame");
    }

    public void OnRestartButtonPressed()
    {
        score = 0;
        tigerCurrentLevel = 1;
        SceneManager.LoadScene("Game");
    }

    public void OnMenuButtonPressed()
    {
        score = 0;
        tigerCurrentLevel = 1;
        SceneManager.LoadScene("Menu");
    }

    private void OnApplicationQuit()
    {
        score = 0;
        tigerCurrentLevel = 1;
    }

    private IEnumerator LaunchGame()
    {
        gameLaunched = true;
        foodIndex = Random.Range(0, _tigerNeedFoodsSprites.Length);
        needFoodCount = Random.Range(2, 8);
        while (gameLaunched)
        {
            if (_lastPiece != null)
            {
                if (_lastPiece.transform.position.x <= _needPosBorder.position.x)
                {
                    _lastPiece = Instantiate(_foodPieces, _foodSpawnPosition.position, _foodPieces.transform.rotation);
                }
            }
            else
                _lastPiece = Instantiate(_foodPieces, _foodSpawnPosition.position, _foodPieces.transform.rotation);
            yield return null;
        }
        _resultPage.SetActive(true);
    }
}
