using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CrazyGameControllerComponent : MonoBehaviour
{
    public static List<Sprite> needCombinations = new List<Sprite>();

    public static List<Sprite> currentCombintaions = new List<Sprite>();

    public static List<Transform> currentCombintaionTransforms = new List<Transform>();
    [SerializeField]
    private CrazyTableCellComponent _cellPrefab;

    [SerializeField]
    private Transform _showCombinationPos;

    [SerializeField]
    private Image _bgImage;

    [SerializeField]
    private Sprite[] _bgSprites;

    [SerializeField]
    private GameObject _winPanel;

    [SerializeField]
    private GameObject _loosePanel;

    [SerializeField]
    private TMP_Text _levelRxt;

    [SerializeField]
    private TMP_Text _timerTxt;

    [SerializeField]
    private TMP_Text[] _getPointsTxt;

    [SerializeField]
    private Slider _timerSlider;

    private int _winGPointsCount;

    public static Action<bool> OnCombintaionGet;
    public static Action OnShowCombintation;

    private bool _isLaunched;

    private float _timer = 30;

    public List<Sprite> tempCombintaion = new List<Sprite>();

    private void Awake()
    {
        _levelRxt.text = "LVL " + (GameSavesData.SelectedLevelIndex + 1).ToString();
        OnShowCombintation += ShowCombintation;
        for (int i = 0; i < GameSavesData.SelectedLevelIndex; i++)
        {
            _timer -= 2.5f;
        }
        if (_timer <= 5)
        {
            _timer = 5;
        }
        OnCombintaionGet += OnWin;
        _timerSlider.maxValue = _timer;
        _timerSlider.value = _timer;
        _bgImage.sprite = _bgSprites[GameSavesData.SelectedBgIndex];
    }

    private void ShowCombintation()
    {
        for (int i = 0; i < needCombinations.Count; i++)
        {
            CrazyTableCellComponent tempCell = Instantiate(_cellPrefab, _showCombinationPos);
            tempCell.Init(needCombinations[i], false);
        }
    }

    private void LateUpdate()
    {
        if (_isLaunched)
            return;
        tempCombintaion = needCombinations;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            OnWin(false);
        }
        UpdateTimer();
    }

    private void OnDestroy()
    {
        OnCombintaionGet -= OnWin;
        OnShowCombintation -= ShowCombintation;
    }

    private void UpdateTimer()
    {
        _timerTxt.text = _timer.ToString("0.0") + "s";
        _timerSlider.value = _timer;
    }

    private void OnWin(bool value)
    {
        if (value)
        {
            _winPanel.SetActive(true);
            _winGPointsCount = Random.Range(5, 50);
        }
        else
        {
            _loosePanel.SetActive(true);
            _winGPointsCount = Random.Range(-15, -5);
        }
        foreach (var item in _getPointsTxt)
        {
            item.text = _winGPointsCount.ToString() + "G";
        }
        _isLaunched = true;
    }

    public void OnClickNext()
    {
        GameSavesData.PlayerGCoinsCount += _winGPointsCount;
        needCombinations.Clear();
        if (GameSavesData.SelectedLevelIndex >= GameSavesData.MaxReachLevel)
        {
            GameSavesData.MaxReachLevel = GameSavesData.SelectedLevelIndex;
        }
        GameSavesData.SelectedLevelIndex += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        needCombinations.Clear();
        Scene nextScene = SceneManager.CreateScene("CrazyTinesMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }

    public void OnClickRestart()
    {
        needCombinations.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
