using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Action<bool> nightMode;
    public static Action<bool> resultShow;
    
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private TigetManager _tigetManager;

    [SerializeField] private ResultScreenComponent _resultScreen;
    
    [SerializeField] private Light _light;

    [SerializeField] private Color _dayColor;
    [SerializeField] private Color _nightColor;
    
    [SerializeField] private TMP_Text _coinsText;

    [SerializeField] private AudioClip _gameClip;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _looseClip;
    
    private bool _isDay;
    public static bool gameLaunched;

    public static int TigerSkinIndex
    {
        get => PlayerPrefs.GetInt("TigerSkinIndex", 0);
        set => PlayerPrefs.SetInt("TigerSkinIndex", value);
    }

    public static int TigerCurrentLevel
    {
        get => PlayerPrefs.GetInt("TigerCurrentLevel", 1);
        set => PlayerPrefs.SetInt("TigerCurrentLevel", value);
    }
    
    public static int TigerMaxReachedLevel
    {
        get => PlayerPrefs.GetInt("TigerMaxReachedLevel", 1);
        set => PlayerPrefs.SetInt("TigerMaxReachedLevel", value);
    }

    public static int TigerCoinsCount
    {
        get => PlayerPrefs.GetInt("TigerCoinsCount", 0);
        set => PlayerPrefs.SetInt("TigerCoinsCount", value);
    }
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameLaunched = false;
        _levelManager.Init();
        _healthManager.Init();
        _tigetManager.Init();
        WallSpawnerComponent.playerReachedWall += OnPlayerReachWall;
        resultShow += OnResultShow;
        SettingsManager.changeMusic?.Invoke(_gameClip);
        StartCoroutine(ChangeLight());
    }

    private void OnDestroy()
    {
        resultShow -= OnResultShow;
        WallSpawnerComponent.playerReachedWall -= OnPlayerReachWall;
    }
    
    private void OnPlayerReachWall()
    {
        nightMode?.Invoke(!_isDay);
    }

    private void OnResultShow(bool isLoose)
    {
        if(_resultScreen.gameObject.activeInHierarchy)
            return;
        SettingsManager.playSound?.Invoke(isLoose ? _looseClip : _victoryClip);
        _resultScreen.gameObject.SetActive(true);
        _resultScreen.Init(isLoose);
    }

    private void LateUpdate()
    {
        _coinsText.text = "x" + TigerCoinsCount.ToString();
    }
    
    private IEnumerator ChangeLight()
    { 
        yield return new WaitForSeconds(1f);
        gameLaunched = true;
        yield return new WaitForSeconds(3);
        nightMode?.Invoke(true);
        while (true)
        {
            if (!_isDay)
            {
              
                _light.color = Color.LerpUnclamped(_light.color, _dayColor, 0.5f * Time.deltaTime);
                if (_light.color == _dayColor)
                {
                    _isDay = true;
                    nightMode?.Invoke(false);
                    yield return new WaitForSeconds(3);
                }
            }
            else
            {
               
                _light.color = Color.LerpUnclamped(_light.color, _nightColor, 0.5f * Time.deltaTime);
                if (_light.color == _nightColor)
                {
                    _isDay = false;
                    nightMode?.Invoke(true);
                    yield return new WaitForSeconds(3);
                }
            }

            yield return null;
        }
    }
}
