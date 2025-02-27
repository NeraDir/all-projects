using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TigerClawsGameController : MonoBehaviour
{
    public static float PlatformSpeedMultiplayer = 0;
    public static UnityEvent<bool> onGameEnd = new UnityEvent<bool>();
    public static UnityEvent onMoveTiger = new UnityEvent();

    [SerializeField] private PlatformsMove _platformPrefab;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Transform _checkingPosition;
    [SerializeField] private PlatformsMove _lastPlatform;
    [SerializeField] private Transform _tigerTransform;
    [SerializeField] private float _yDistance;

    [Header("UI")]
    [SerializeField] private TMP_Text _coinsTxt;
    [SerializeField] private TMP_Text _resultTxt;
    [SerializeField] private TMP_Text _levelTxt;
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TMP_Text[] _buttonTxt;

    [SerializeField] private GameObject _goodItem;
    [SerializeField] private GameObject _badItem;
    [SerializeField] private Transform _itemsSpawnPosition;

    [SerializeField] private List<PlatformsMove> _platforms = new List<PlatformsMove>();

    private int _maxX = 10;
    private int _maxPlatformsCount = 0;
    private int _currentPlatform = 0;

    private int[] _maxXes = new int[]
    {
        10,20,30,40,50,60,70,80,100,200,300,400,500,600,700,800,999
    };

    private char[] _mathFunc = new char[]
    {
        '+', '-'
    };

    private void Awake()
    {
        _levelTxt.text = "LEVEL " + (TigerClawsGameData.TigerClawsMCurentLevel + 1).ToString();
        int index = 0;
        _maxPlatformsCount = 3;
        for (int i = 0; i < (TigerClawsGameData.TigerClawsMCurentLevel + 1); i++)
        {
            _maxPlatformsCount += 5;
            _maxPlatformsCount = Mathf.Clamp(_maxPlatformsCount, 5, 30);
            PlatformSpeedMultiplayer += 0.1f;
            PlatformSpeedMultiplayer = Mathf.Clamp(PlatformSpeedMultiplayer, 0, 2);
            index += 1;
            index = Mathf.Clamp(index, 0, _maxXes.Length - 1);
        }
        _maxX = _maxXes[index];
        onGameEnd.AddListener(OnGameEnd);
        onMoveTiger.AddListener(OnMoveTiger);

        for (int i = 0; i < _maxPlatformsCount; i++)
        {
            SetupNewPlatform();
        }

        StartCoroutine(GameLive());
        for (int i = 0; i < _platforms.Count; i++)
        {
            int x = Random.Range(0, _maxX);
            int y = Random.Range(0, _maxX);
            char xy = _mathFunc[Random.Range(0, _mathFunc.Length)];
            int total = xy == '+' ? x + y : xy == '-' ? x - y : xy == '*' ? x * y : xy == '/' ? x / y : 0;

            _platforms[i].transform.SetSiblingIndex(0);
            _platforms[i].Init($"{x.ToString("0")}{xy}{y.ToString("0")}=?", total);
        }
        SetupButtons((int)_platforms[_currentPlatform + 1].totalValue);
    }

    private void OnDestroy()
    {
        onGameEnd.RemoveAllListeners();
        onMoveTiger.RemoveAllListeners();
    }

    private void SetupButtons(int value)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;
            int rndValue = Random.Range(0, _maxX);
            _buttonTxt[index].text = rndValue.ToString();
            _buttons[index].onClick.RemoveAllListeners();
            _buttons[index].onClick.AddListener(() => OnGetAnswer(rndValue.ToString()));
        }
        int rndButton = Random.Range(0,_buttons.Length);
        _buttons[rndButton].onClick.RemoveAllListeners();
        _buttons[rndButton].onClick.AddListener(() => OnGetAnswer(value.ToString()));
        _buttonTxt[rndButton].text = value.ToString();
    }

    private IEnumerator GameLive()
    {
        int currentPlatformsCount = 0;
        while (true)
        {
            if (_lastPlatform.transform.position.y <= _checkingPosition.position.y && currentPlatformsCount < _maxPlatformsCount)
            {
                SetupNewPlatform();
                currentPlatformsCount += 1;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGetAnswer(string value)
    {
        if (!int.TryParse(value, out int totalValue)) return; 

        PlatformsMove targetPlatform = _platforms[_currentPlatform + 1];

        if (totalValue == targetPlatform.totalValue) 
        {
            Instantiate(_goodItem, _itemsSpawnPosition.position, Quaternion.identity, _itemsSpawnPosition.parent);
            _currentPlatform++; 
            onMoveTiger?.Invoke();
            targetPlatform.OnPlaced(totalValue);
        }
        else
        {
            Instantiate(_badItem, _itemsSpawnPosition.position,Quaternion.identity, _itemsSpawnPosition.parent);
            onGameEnd?.Invoke(false);
        }
    }

    private void OnMoveTiger()
    {

        PlatformsMove nextPlatform = _platforms[_currentPlatform];

        _tigerTransform.DOMove(nextPlatform.transform.position, 0.25f).OnComplete(() =>
        {
            _tigerTransform.parent = nextPlatform.transform;
            SetupButtons((int)_platforms[_currentPlatform + 1].totalValue);
            if (_currentPlatform >= _maxPlatformsCount)
            {
                onGameEnd?.Invoke(true);
            }
        });
    }

    private void OnGameEnd(bool value)
    {
        int getCoinsCount = Random.Range(5, 20);

        _resultScreen.SetActive(true);
        _resultTxt.text = value ? "AMAZING" : "LOOSE";
        _coinsTxt.text = "x" + (getCoinsCount * (TigerClawsGameData.TigerClawsMCurentLevel + 1)).ToString();
        TigerClawsGameData.TigerClawsUserCoins += getCoinsCount;
        _nextButton.SetActive(value);
        _gameScreen.SetActive(false);
    }

    private void SetupNewPlatform()
    {
        float x = Random.Range(0, _maxX);
        float y = Random.Range(0, _maxX);
        char xy = _mathFunc[Random.Range(0, _mathFunc.Length)];
        float total = xy == '+' ? x + y : xy == '-' ? x - y : xy == '*' ? x * y : xy == '/' ? x / y : 0;

        PlatformsMove newPlatform = SpawnNewPlatform(_lastPlatform.transform.position.y + _yDistance);
        newPlatform.transform.SetSiblingIndex(0);
        newPlatform.Init($"{x}{xy}{y}=?",total);
        _lastPlatform = newPlatform;
        _platforms.Add(newPlatform);
    }

    private PlatformsMove SpawnNewPlatform(float y)
    {
        return Instantiate(_platformPrefab, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), y, 0), Quaternion.identity, _spawnPositions[0].parent);
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnNext()
    {
        TigerClawsGameData.TigerClawsMCurentLevel += 1;
        if (TigerClawsGameData.TigerClawsMCurentLevel > TigerClawsGameData.TigerClawsMaxReachedLevels)
        {
            TigerClawsGameData.TigerClawsMaxReachedLevels = TigerClawsGameData.TigerClawsMCurentLevel;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
