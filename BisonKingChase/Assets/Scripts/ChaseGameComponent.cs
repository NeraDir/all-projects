using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChaseGameComponent : MonoBehaviour
{
    [SerializeField] private GameObject[] _chasingLevelsPacks;
    [SerializeField] private Image[] _backgroundImages;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    [SerializeField] private GameObject _resultPage;
    [SerializeField] private GameObject _winButtons;
    [SerializeField] private GameObject _looseButtons;

    [SerializeField] private Text[] _balanceText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _resultText;
    [SerializeField] private Text _taskText;
    [SerializeField] private Text _betText;
    [SerializeField] private Text _spinsCountText;

    public static int betValue;

    private int _spinsCount;
    private int _needScore;
    private int _score;

    private bool _isLaunch;

    private void Awake()
    {
        _isLaunch = false;
        foreach (var item in _backgroundImages)
        {
            item.sprite = Resources.Load<Sprite>(ChasePlayerDataComponent.ChasePlayerBackgroundSpriteName);
        }
        betValue = 10;
        _score = 0;
        _spinsCount = 3;
        for (int i = 0; i < ChasePlayerDataComponent.ChasePlayerCurrentLevel + 1; i++)
        {
            _needScore += 100;
            _spinsCount += 3;
        }
        _musicSource.volume = ChasePlayerDataComponent.ChaseMuiscVolume;
        _soundSource.volume = ChasePlayerDataComponent.ChaseSoundsVolume;
        GetCurrentPack().SetActive(true);
        _isLaunch = true;
    }

    private void LateUpdate()
    {
        if (!_isLaunch)
            return;
        _betText.text = betValue.ToString();
        foreach (var pair in _balanceText)
        {
            pair.text = ChasePlayerDataComponent.ChasePlayerCoins.ToString();
        }
        _levelText.text = "LEVEL " + (ChasePlayerDataComponent.ChasePlayerCurrentLevel + 1);
        _taskText.text = $"{_score}/{_needScore}";
        _spinsCountText.text = "SPINS LEFT " + _spinsCount.ToString();
    }

    public GameObject GetCurrentPack()
    {
        return _chasingLevelsPacks[GetActiveLevelPack()];
    }

    public void ChangeCurrentScore(int value)
    {
        _score += value;
        if (_score >= _needScore)
        {
            OnShowResult(true);
        }
    }

    public void MinusSpin()
    {
        _spinsCount -= 1;
        if (_spinsCount <= 0)
        {
            OnShowResult(false);
        }
    }

    private void OnShowResult(bool isWin)
    {
        _resultPage.SetActive(true);
        _winButtons.SetActive(isWin);
        _looseButtons.SetActive(!isWin);
        _resultText.text = isWin ? "LEVEL PASSED" : "LEVEL NO PASSED";
    }

    private int GetActiveLevelPack()
    {
        int activeLevelPack = 0;
        
        for (int i = 0; i < ChasePlayerDataComponent.ChasePlayerCurrentLevel; i++)
        {
            if (i != 0 && i % 8 == 0)
            {
                activeLevelPack += 1;
            }
        }
        
        return activeLevelPack;
    }

    public void OnNextButtonPressed()
    {
        ChasePlayerDataComponent.ChasePlayerCurrentLevel += 1;
        if (ChasePlayerDataComponent.ChasePlayerCurrentLevel > ChasePlayerDataComponent.ChasePlayerMaxReachedLevel)
        {
            ChasePlayerDataComponent.ChasePlayerMaxReachedLevel = ChasePlayerDataComponent.ChasePlayerCurrentLevel;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuButtonPressed()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene nextScene = SceneManager.CreateScene("CheaseMenuScene");
        SceneManager.SetActiveScene(nextScene);
        SceneManager.UnloadScene(currentScene);
        GameObject menuObject = Resources.Load<GameObject>("Prefabs/ChaseMenuPrefab");
        Instantiate(menuObject);

    }
}
