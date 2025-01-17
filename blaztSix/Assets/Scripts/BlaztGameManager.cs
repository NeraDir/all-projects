using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BlaztGameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] _scoresShow;

    [SerializeField]
    private TMP_Text[] _levelsShow;

    [SerializeField]
    private Transform _fruitsSpawnPosition;

    [SerializeField]
    private Animator[] _starsAnimators;

    [SerializeField]
    private GameObject _winScreen;

    [SerializeField]
    private GameObject _looseScreen;

    [SerializeField]
    private BlaztTriggerButtonPlace _triggerPlacePref;

    [SerializeField]
    private Transform[] _placesSpawnPositions;

    [SerializeField]
    private Image _fallFruitPref;

    [SerializeField]
    private BlaztLevelDatas _levelDatas;

    public static int score
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztThunderCurrentScoreSaveKey"))
                return PlayerPrefs.GetInt("BlaztThunderCurrentScoreSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztThunderCurrentScoreSaveKey", value);
        }
    }

    public static int BestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("BlaztThunderBestScoreSaveKey"))
                return PlayerPrefs.GetInt("BlaztThunderBestScoreSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BlaztThunderBestScoreSaveKey", value);
        }
    }

    private List<Sprite> _currentFruitsList = new List<Sprite>();

    public static int CurrentLevel {
        get {
            if (PlayerPrefs.HasKey("BlaztThunderCurrentLevelSaveKey"))
                return PlayerPrefs.GetInt("BlaztThunderCurrentLevelSaveKey");
            return 1;
        }
        set {
            PlayerPrefs.SetInt("BlaztThunderCurrentLevelSaveKey", value);
        }
    }

    public static int MaxLevel {
        get {
            if (PlayerPrefs.HasKey("BlaztThunderMaxLevelSaveKey"))
                return PlayerPrefs.GetInt("BlaztThunderMaxLevelSaveKey");
            return 1;
        }
        set {
            PlayerPrefs.SetInt("BlaztThunderMaxLevelSaveKey", value);
        }
    }

    public static float MoveSpeed;

    private LevelData _currentLeveldata;

    public static int starsCount;

    private bool _isBeginned;

    private void Start()
    {
        _isBeginned = false;
        MoveSpeed = 0;
        starsCount = 3;
        _currentLeveldata = _levelDatas.levelDatas[CurrentLevel];
        SpawnPlaces();
    }

    private void LateUpdate()
    {
        if (!_isBeginned)
            return;
        for (int i = 0; i < _starsAnimators.Length; i++)
        {
            if (i < starsCount)
            {
                _starsAnimators[i].enabled = false;
            }
            else
            {
                _starsAnimators[i].enabled = true;
            }
        }
        if (starsCount <= 0)
        {
            _looseScreen.SetActive(true);
            _winScreen.SetActive(false);
        }

        foreach (var item in _scoresShow)
        {
            item.text = score.ToString();
        }

        foreach (var item in _levelsShow)
        {
            item.text = CurrentLevel.ToString();
        }

        if (starsCount > 0 && FindObjectOfType<BlaztFallFruitComponent>() == null)
        {
            _winScreen.SetActive(true);
            _looseScreen.SetActive(false);
        }
        if (score > BestScore)
        {
            BestScore = score;
        }

        if (CurrentLevel > MaxLevel)
        {
            MaxLevel = CurrentLevel;
        }
    }

    private void OnApplicationQuit()
    {
        CurrentLevel = 1;
        score = 0;
    }

    public void OnClickMenu()
    {
        CurrentLevel = 1;
        score = 0;
        Scene nextScene = SceneManager.CreateScene("BlaztThunderMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }

    public void OnClickRestart()
    {
        CurrentLevel = 1;
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickNext()
    {
        CurrentLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SpawnPlaces()
    {
        for (int i = 0; i < _currentLeveldata.levelPlacesSprites.Length; i++)
        {
            BlaztTriggerButtonPlace tempPlace = Instantiate(_triggerPlacePref, _placesSpawnPositions[i].position,Quaternion.identity, _placesSpawnPositions[0].parent);
            tempPlace.transform.SetSiblingIndex(1);
            tempPlace.Init(_currentLeveldata.levelFruits[i], _currentLeveldata.levelPlacesSprites[i]);
        }
        MoveSpeed = _currentLeveldata.speedValue;
        SpawnFallFruits();
    }

    private void SpawnFallFruits()
    {
        float _lastPosition = _fruitsSpawnPosition.GetComponent<RectTransform>().position.y;
        for (int i = 0; i < _currentLeveldata.fallFruitsCount; i++)
        {
            Image tempFruit = Instantiate(_fallFruitPref, _fruitsSpawnPosition.parent.transform);
            tempFruit.transform.SetSiblingIndex(0);
            tempFruit.transform.position = new Vector3(0, _lastPosition, 0);
            BlaztFallFruitComponent sweetBallTemper = tempFruit.GetComponent<BlaztFallFruitComponent>();
            tempFruit.sprite = _currentLeveldata.levelFruits[Random.Range(0, _currentLeveldata.levelFruits.Length)];
            _lastPosition += 10;
        }
        _isBeginned = true;
    }
}
