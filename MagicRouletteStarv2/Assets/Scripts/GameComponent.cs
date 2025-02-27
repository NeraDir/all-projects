using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameComponent : MonoBehaviour
{
    public static int MaxLevel
    {
        get => PlayerPrefs.GetInt("MaxCompletedLevel", 1);
        set => PlayerPrefs.SetInt("MaxCompletedLevel", value);
    }

    [SerializeField] private Sprite[] gemsCharacter;

    [SerializeField] private Image _gemsCharacterImage;

    public static Action<CrystallType> onLaunchGame;
    public static Action<CrystallType> onCheckCrystall;

    private float _targetScore;
    private float _time;
    private float _score;
    public static int Level;
    private CrystallType _targetType;
    private bool _isLaunched;
    public static float speed;

    [SerializeField] private Image _progressBar;

    [SerializeField] private Text _timerTxt;
    [SerializeField] private Text _levelTxt;
    [SerializeField] private ResultComponent _result;

    [SerializeField] private SpawnerComponent[] _spawners;

    private void Awake()
    {
        CustomButton.isPressed = false;
        speed = 0;
        _levelTxt.text = "LVL - " + Level.ToString();
        for (int i = 0; i < Level; i++)
        {
            speed += 100f;
            _targetScore += 10;
        }
        if (speed >= 800)
        {
            speed = 800;
        }
        _time = 0;
        for (int i = 0; i < _targetScore; i++)
        {
            if (i % 2 == 0)
            {
                _time += 2;
            }
        }
        onCheckCrystall += OnCheckingCrystall;
        onLaunchGame += OnLaunch;
        _isLaunched = false;
    }

    private void OnDestroy()
    {
        onCheckCrystall -= OnCheckingCrystall;
        onLaunchGame -= OnLaunch;
    }

    private void LateUpdate()
    {
        if (!_isLaunched)
            return;
        _time -= Time.deltaTime;
        _timerTxt.text = _time.ToString("0.0") + "s";
        if (_time <= 0)
        {
            _result.resultData = "LEVEL NOT COMPLETED";
            _result.gameObject.SetActive(true);
            _time = 0;
            _isLaunched = false;
        }
        _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, (_score / _targetScore), 11 * Time.deltaTime);
    }

    private void OnCheckingCrystall(CrystallType type)
    {
        if (_targetType != type)
        {
            _time -= 0.5f;
        }
        else
        {
            _score += 1;
            if (_score >= _targetScore)
            {
                _isLaunched = false;
                _result.resultData = "LEVEL COMPLETED";
                _result.gameObject.SetActive(true);
            }
        }
    }

    private void OnLaunch(CrystallType type)
    {
        _targetType = type;
        _gemsCharacterImage.sprite = gemsCharacter[(int)type];
        _isLaunched = true;
        foreach (var item in _spawners)
        {
            item.Init();
        }
    }
}
