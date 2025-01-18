using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PimoGameController : MonoBehaviour
{
    [SerializeField]
    private Transform[] _heartsImages;

    [SerializeField]
    private TMP_Text[] _scoreShow;

    [SerializeField]
    private TMP_Text[] _ballsShow;

    [SerializeField]
    private GameObject _resulScreen;

    public static UnityEvent<int> doSomthingWithHearts = new UnityEvent<int>();
    public static UnityEvent gameInitialization = new UnityEvent();
    public static UnityEvent onBallReachCup = new UnityEvent();

    public static int _ballsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("PimoMayhemBallsCountSaveKey"))
                return PlayerPrefs.GetInt("PimoMayhemBallsCountSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PimoMayhemBallsCountSaveKey", value);
        }
    }
    public static int _scoreCount
    {
        get
        {
            if (PlayerPrefs.HasKey("PimoMayhemScoreCountSaveKey"))
                return PlayerPrefs.GetInt("PimoMayhemScoreCountSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PimoMayhemScoreCountSaveKey", value);
        }
    }

    public static int BallsMaxCount
    {
        get
        {
            if (PlayerPrefs.HasKey("PimoMayhemBallsMaxCountSaveKey"))
                return PlayerPrefs.GetInt("PimoMayhemBallsMaxCountSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PimoMayhemBallsMaxCountSaveKey", value);
        }
    }

    public static int MaxScore
    {
        get
        {
            if (PlayerPrefs.HasKey("PimoMayhemMaxScoreSaveKey"))
                return PlayerPrefs.GetInt("PimoMayhemMaxScoreSaveKey");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PimoMayhemMaxScoreSaveKey", value);
        }
    }

    private int _heartsCount;

    private void Awake()
    {
        _heartsCount = 3;
        PimoTargetMove.moveSpeed = 3;
        onBallReachCup.AddListener(OnBallReachedCup);
        doSomthingWithHearts.AddListener(OnSomthing);
    }

    private void OnDestroy()
    {
        doSomthingWithHearts.RemoveListener(OnSomthing);
        onBallReachCup.RemoveListener(OnBallReachedCup);
    }

    private void OnApplicationQuit()
    {
        _scoreCount = 0;
        _ballsCount = 0;
    }

    public void OnClickRestart()
    {
        _scoreCount = 0;
        _ballsCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        _scoreCount = 0;
        _ballsCount = 0;
        Scene nextScene = SceneManager.CreateScene("PlimoMayhemMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }

    public void OnClickBonus()
    {
        SceneManager.LoadScene("PimoBonusScene");
    }

    private void OnBallReachedCup()
    {
        _ballsCount++;
        _scoreCount+= Random.Range(5,15);
        foreach (var item in _scoreShow)
        {
            item.text = _scoreCount.ToString();
        }
        foreach (var item in _ballsShow)
        {
            item.text = "x" + _ballsCount.ToString();
        }
        if (PimoGameController._scoreCount > PimoGameController.BallsMaxCount)
        {
            PimoGameController.MaxScore = PimoGameController._scoreCount;
        }
        if (PimoGameController._ballsCount > BallsMaxCount)
        {
            BallsMaxCount = PimoGameController._ballsCount;
        }
        if (_ballsCount % 5 == 0 && _ballsCount != 0)
        {
            PimoTargetMove.moveSpeed += 0.5f;
        }
    }

    private void OnSomthing(int value)
    {
        _heartsCount += value;
        if (_heartsCount <= 0)
        {
            _resulScreen.SetActive(true);
        }
        UpdateHeartsVisuals();
    }

    private void UpdateHeartsVisuals()
    {
        for (int i = 0; i < _heartsImages.Length; i++)
        {
            if (i >= _heartsCount)
            {
                _heartsImages[i].DOScale(Vector3.zero, 0.25f);
            }
            else
            {
                _heartsImages[i].DOScale(Vector3.one, 0.25f);
            }
        }
    }

    private void Start()
    {
        gameInitialization?.Invoke();
    }
}
