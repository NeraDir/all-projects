using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class gameController : MonoBehaviour
{
    [SerializeField]
    private objectsSpawner _objectsSpawner;

    [SerializeField]
    private resultPageComponent _resultScreen;

    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private TMP_Text _levelText;

    public static Action<bool> getResult;
    public static Action<int> changeScore;

    public static int LevelIndex;

    private int _score;

    private int _needScore;

    private void Awake()
    {
        menuScreenController.updateDiamondCount?.Invoke();
        _score = 0;
        _objectsSpawner.Init();
        getResult += OnGetresult;
        changeScore += OnScoreChanged;
        _levelText.text = "LEVEL " + (LevelIndex + 1).ToString();
        _needScore = (LevelIndex + 1) * 50;
        _scoreText.text = _score.ToString() + "/" + _needScore.ToString();
    }

    private void OnDestroy()
    {
        getResult -= OnGetresult;
        changeScore -= OnScoreChanged;
    }

    public bool IsReached()
    {
        return _score >= _needScore;
    }

    private void OnScoreChanged(int value)
    {
        _score += value;
        _scoreText.text = _score.ToString() + "/" + _needScore.ToString();
        if (_score > menuScreenController.userMaxScore)
        {
            menuScreenController.userMaxScore = _score;
        }
    }

    private void OnGetresult(bool result)
    {
        _resultScreen.gameObject.SetActive(true);
        _resultScreen.Init(result);
    }
}
