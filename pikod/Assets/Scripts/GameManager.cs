using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static int PlayerCanvasScaleParameter
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerCanvasScaleParameterSave"))
            {
                return PlayerPrefs.GetInt("PlayerCanvasScaleParameterSave");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerCanvasScaleParameterSave", value);
        }
    }

    public static string loadinggameParameters;

    public static int PlayerGameSettingParameter
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerGameSettingParameterSave"))
            {
                return PlayerPrefs.GetInt("PlayerGameSettingParameterSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PlayerGameSettingParameterSave", value);
        }
    }

    public static int PlayerBestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("PlayerReachedBestScorePlimoSave"))
                return PlayerPrefs.GetInt("PlayerReachedBestScorePlimoSave");
            return 0;
        }

        set
        {
            PlayerPrefs.SetInt("PlayerReachedBestScorePlimoSave", value);
        }
    }

    private int _score = 0;

    [SerializeField]
    private GameObject _ballPrefab;

    [SerializeField]
    private Transform[] _ballsSpawnFills;

    [SerializeField]
    private List<Sprite> _ballSprites;

    [SerializeField]
    private Image _timeFillBar;

    [SerializeField]
    private Button[] _selectButtons;

    [SerializeField]
    private TMP_Text _timeShow;

    [SerializeField]
    private float _defaultTime;

    [SerializeField]
    private GameObject _resultateScreen;

    [SerializeField]
    private TMP_Text[] _scoreShow;

    [SerializeField]
    private int _startBallsCount;

    [SerializeField]
    private Image _needBallShow;

    private float _currentTime;

    private bool _gameHasBenInitializated;

    private BallsContainers _currentNeedBall;

    private int _trueAnswerCount = 0;

    [SerializeField]
    private List<BallsContainers> _totalBallsList = new List<BallsContainers>();

    private void Start()
    {
        _currentTime = _defaultTime;
        _gameHasBenInitializated = true;
        StartCoroutine(GameStatus());
    }

    private IEnumerator GameStatus()
    {
        _score = 0;
        _trueAnswerCount = 0;
        for (int i = 0; i < _startBallsCount; i++)
        {
            BallsContainers newTempContainer = new BallsContainers();
            for (int j = 0; j < _ballsSpawnFills.Length; j++)
            {
                GameObject tempBall = Instantiate(_ballPrefab, _ballsSpawnFills[j]);
                Image tempIamge = tempBall.GetComponent<Image>();
                newTempContainer.needBallSprite = _ballSprites[Random.Range(0, _ballSprites.Count)];
                newTempContainer.ballsInLine.Add(tempIamge);
            }
            foreach (var item in newTempContainer.ballsInLine)
            {
                item.sprite = _ballSprites[Random.Range(0, _ballSprites.Count)];
                if (item.sprite == newTempContainer.needBallSprite)
                {
                    item.sprite = _ballSprites.Find(x => x != newTempContainer.needBallSprite);
                }
            }
            newTempContainer.ballsInLine[Random.Range(0, newTempContainer.ballsInLine.Count)].sprite = newTempContainer.needBallSprite;
            _totalBallsList.Add(newTempContainer);
        }

        _currentNeedBall = _totalBallsList[0];

        _needBallShow.sprite = _currentNeedBall.needBallSprite;

        while (_gameHasBenInitializated)
        {
            if (_score > PlayerBestScore)
            {
                PlayerBestScore = _score;
            }
            yield return null;
        }
        _resultateScreen.SetActive(true);
    }

    private void UpdateTimerFilling()
    {
        if (_timeFillBar != null)
            _timeFillBar.fillAmount = Mathf.MoveTowards(_timeFillBar.fillAmount, (_currentTime / _defaultTime), 10 * Time.deltaTime);
    }

    private void UpdateScoreTxt()
    {
        if (_scoreShow.Length > 0)
            foreach (var item in _scoreShow)
                item.text = _score.ToString();
    }

    public void PlayAgainPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPressed() 
    {
        SceneManager.LoadScene("PlimoMenu");
    }

    private void LateUpdate()
    {
        UpdateTime();
        UpdateTimerFilling();
        UpdateScoreTxt();
    }

    private void UpdateTime() 
    {
        _currentTime -= Time.deltaTime;
        _timeShow.text = _currentTime.ToString("0.0") + "s";
        if (_currentTime <= 0)
        {
            _gameHasBenInitializated = false;
        }
    }

    public void SelectTubePressed(int tubeIndex) 
    {
        UpdateTrueAnswerTubes(_currentNeedBall.needBallSprite, _currentNeedBall.ballsInLine[tubeIndex].sprite);
    }

    private void UpdateTrueAnswerTubes(Sprite currentSprite, Sprite needSprite) 
    {
        if (currentSprite == needSprite)
        {
            
            foreach (var item in _currentNeedBall.ballsInLine)
            {
                Destroy(item.gameObject);
            }
            _totalBallsList.Remove(_currentNeedBall);
            BallsContainers newTempContainer = new BallsContainers();
            for (int j = 0; j < _ballsSpawnFills.Length; j++)
            {
                GameObject tempBall = Instantiate(_ballPrefab, _ballsSpawnFills[j]);
                Image tempIamge = tempBall.GetComponent<Image>();
                newTempContainer.needBallSprite = _ballSprites[Random.Range(0, _ballSprites.Count)];
                newTempContainer.ballsInLine.Add(tempIamge);
            }
            foreach (var item in newTempContainer.ballsInLine)
            {
                item.sprite = _ballSprites[Random.Range(0, _ballSprites.Count)];
                if (item.sprite == newTempContainer.needBallSprite)
                {
                    item.sprite = _ballSprites.Find(x => x != newTempContainer.needBallSprite);
                }
            }
            newTempContainer.ballsInLine[Random.Range(0, newTempContainer.ballsInLine.Count)].sprite = newTempContainer.needBallSprite;
            _totalBallsList.Add(newTempContainer);

            _currentNeedBall = _totalBallsList[0];

            _needBallShow.sprite = _currentNeedBall.needBallSprite;

            _score += Random.Range(100, 200);
            _trueAnswerCount++;
            _currentTime += 1 * Mathf.Pow(0.94f,_trueAnswerCount);
        }
        else
        {
            Handheld.Vibrate();
        }
    }
}

[Serializable]
public class BallsContainers 
{
    public Sprite needBallSprite;

    public List<Image> ballsInLine = new List<Image>();
}
