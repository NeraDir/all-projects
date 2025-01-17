using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _roadPrefabs;

    [SerializeField]
    private GameObject _startRoadPrefab;

    [SerializeField]
    private GameObject _endRoadPrefab;

    [SerializeField]
    private AudioSource _moneySource;

    [SerializeField]
    private GameObject _gameEndScreen;

    [SerializeField]
    private GameObject _gameEndNextButton;

    [SerializeField]
    private TMP_Text _gameEndTxt;

    [SerializeField]
    private TMP_Text[] _gameLevelTxts;

    [SerializeField]
    private TMP_Text[] _gameScoreTxts;

    [SerializeField]
    private TMP_Text _gameEndAddTxt;

    public static AudioSource moneySource;

    private float _maxDistance;
    private float _minDistance;
    private int _roadMaxIndex;

    private int _roadsCountPerLevel;

    private GameObject _lastRoad;

    public static int TopSkinIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztOasisTopSkinIndexKey"))
            {
                return PlayerPrefs.GetInt("BlaztOasisTopSkinIndexKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztOasisTopSkinIndexKey", value);
        }
    }

    public static int BottomSkinIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztOasisBottomSkinIndexKey"))
            {
                return PlayerPrefs.GetInt("BlaztOasisBottomSkinIndexKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztOasisBottomSkinIndexKey", value);
        }
    }

    public static int CurrentScore
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztOasisCurrentScoreKey"))
            {
                return PlayerPrefs.GetInt("BlaztOasisCurrentScoreKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztOasisCurrentScoreKey", value);
        }
    }

    public static int blaztOasisTrysCount
    {
        get
        {
            if (PlayerPrefs.HasKey("blaztOasisTrysCountsaves"))
            {
                return PlayerPrefs.GetInt("blaztOasisTrysCountsaves");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("blaztOasisTrysCountsaves", value);
        }
    }

    public static string blaztOasisName;

    public static int blaztOasisWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("blaztOasisWinsCountSave"))
            {
                return PlayerPrefs.GetInt("blaztOasisWinsCountSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("blaztOasisWinsCountSave", value);
        }
    }

    public static int MaxScore
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztOasisMaxScoreKey"))
            {
                return PlayerPrefs.GetInt("BlaztOasisMaxScoreKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztOasisMaxScoreKey", value);
        }
    }

    public static int MaxLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztOasisMaxLevelKey"))
            {
                return PlayerPrefs.GetInt("BlaztOasisMaxLevelKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztOasisMaxLevelKey", value);
        }
    }

    public static int CurrentLevel
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztOasisCurrentLevelKey"))
            {
                return PlayerPrefs.GetInt("BlaztOasisCurrentLevelKey");
            }
            return 1;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztOasisCurrentLevelKey", value);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        MainCharacterComponent.mainCharacterIsDead.AddListener(OnCharacterDdead);
        MainCharacterComponent.mainCharacterIsFinished.AddListener(OnCharacterFinished);
        moneySource = _moneySource;
        OnSpawnLevel();
    }

    private void LateUpdate()
    {
        foreach (var item in _gameLevelTxts)
        {
            item.text = CurrentLevel.ToString();
        }
        foreach (var item in _gameScoreTxts)
        {
            item.text = "x" + CurrentScore.ToString("0");
        }
    }

    private void OnCharacterFinished()
    {
        Time.timeScale = 0;
        _gameEndScreen.SetActive(true);
        _gameEndNextButton.SetActive(true);
        _gameEndTxt.text = "VICTORY";
        _gameEndAddTxt.text = $"{CurrentLevel} LEVEL COMPLETE";
    }

    private void OnCharacterDdead()
    {
        Time.timeScale = 0;
        _gameEndScreen.SetActive(true);
        _gameEndNextButton.SetActive(false);
        _gameEndTxt.text = "YOU LOOSE";
        _gameEndAddTxt.text = $"{CurrentLevel} NOT LEVEL COMPLETE";
    }

    private void OnApplicationQuit()
    {
        CurrentLevel = 1;
        CurrentScore = 0;
    }

    public void OnClickMenu()
    {
        if (CurrentLevel > MaxLevel)
        {
            MaxLevel = CurrentLevel;
        }
        if (CurrentScore > MaxScore)
        {
            MaxScore = CurrentScore;
        }
        CurrentLevel = 1;
        CurrentScore = 0;
        SceneManager.LoadScene("menuScene");
    }

    public void OnClickRestart()
    {
        if (CurrentLevel > MaxLevel)
        {
            MaxLevel = CurrentLevel;
        }
        if (CurrentScore > MaxScore)
        {
            MaxScore = CurrentScore;
        }
        CurrentScore = 0;
        CurrentLevel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickNext()
    {
        if (CurrentLevel > MaxLevel)
        {
            MaxLevel = CurrentLevel;
        }
        if (CurrentScore > MaxScore)
        {
            MaxScore = CurrentScore;
        }
        CurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSpawnLevel()
    {
        if (CurrentLevel < 10)
            _roadMaxIndex = 3;
        else if (CurrentLevel > 10 && CurrentLevel < 20)
            _roadMaxIndex = 5;
        else if (CurrentLevel > 20)
            _roadMaxIndex = 8;

        for (int i = 0; i < CurrentLevel; i++)
        {
            _roadsCountPerLevel += 4;
        }
        _lastRoad = Instantiate(_startRoadPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        for (int i = 0; i < _roadsCountPerLevel; i++)
        {
            if (Random.Range(0,2) != 0)
            {
                _lastRoad = Instantiate(_roadPrefabs[Random.Range(0, _roadMaxIndex)], new Vector3(0, 0, _lastRoad.transform.position.z + 117.43f), Quaternion.identity);
            }
            else
            {
                _lastRoad = Instantiate(_roadPrefabs[Random.Range(0, _roadMaxIndex)], new Vector3(0, 0, _lastRoad.transform.position.z + 79.91f), Quaternion.identity);
            }
        }
        _lastRoad = Instantiate(_endRoadPrefab, new Vector3(0, 0, _lastRoad.transform.position.z + 79.8f), Quaternion.identity);
    }
}
