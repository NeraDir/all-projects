using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PiloGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _piloPartPrefabs;

    [SerializeField]
    private float _minDistance;

    [SerializeField]
    private float _maxDistance;

    [SerializeField]
    private GameObject _resultPanel;

    [SerializeField]
    private TMP_Text[] _scoreShows;

    [SerializeField]
    private GameObject[] _heartsObjects;

    public static int BestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("PiloBestScoreKey"))
            {
                return PlayerPrefs.GetInt("PiloBestScoreKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PiloBestScoreKey", value);
        }
    }

    private int _currentScore;
    private int _heartsCount;
    private int _piloPartCount;

    private List<GameObject> _piloParts = new List<GameObject>();

    public static UnityEvent partReachead = new UnityEvent();
    public static UnityEvent ballIsDead = new UnityEvent();
    public static UnityEvent addScore = new UnityEvent();
    public static UnityEvent addHeart = new UnityEvent();

    public static bool _gameRunned;

    private void Awake()
    {
        _gameRunned = false;
        _heartsCount = 4;
        _piloParts.Clear();
        PiloPartComponent.partsReachedCount = 0;
        addScore.AddListener(AddScore);
        ballIsDead.AddListener(OnDead);
        addHeart.AddListener(AddHeart);
        for (int i = 0; i < 4; i++)
        {
            SpawnRoad();
        }
        _gameRunned = true;
        partReachead.AddListener(SpawnRoad);
    }

    private void OnDestroy()
    {
        partReachead.RemoveListener(SpawnRoad);
        addScore.RemoveListener(AddScore);
        ballIsDead.RemoveListener(OnDead);
        addHeart.RemoveListener(AddHeart);
    }

    private void AddHeart()
    {
        _heartsCount++;
        if (_heartsCount >= 4)
        {
            _heartsCount = 4;
        }
        UpdateHearts();
    }

    private void AddScore()
    {
        _currentScore += 1;
        if (_currentScore > BestScore)
        {
            BestScore = _currentScore;
        }
        foreach (var item in _scoreShows)
        {
            item.text = "x" + _currentScore.ToString("0");
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < _heartsObjects.Length; i++)
        {
            if (i >= _heartsCount)
            {
                _heartsObjects[i].transform.DOScale(Vector3.zero, 0.25f);
            }
            else
            {
                _heartsObjects[i].transform.DOScale(Vector3.one, 0.25f);
            }
        }
    }

    private void OnDead()
    {
        _heartsCount--;
        UpdateHearts();
        if (_heartsCount <= 0)
        {
            _resultPanel.SetActive(true);
        }
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        Scene nextScene = SceneManager.CreateScene("PlioTumbleMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }

    private void SpawnRoad()
    {
        if (_gameRunned)
        {
            _piloPartCount++;
            Destroy(_piloParts[0].gameObject, 5);
            _piloParts.Remove(_piloParts[0]);
        }

        if (_piloParts.Count <= 0)
        {
            GameObject tempPart = Instantiate(_piloPartPrefabs[Random.Range(0, _piloPartPrefabs.Length)], new Vector3(0, 0, 0), Quaternion.identity);
            _piloParts.Add(tempPart);
        }
        else
        {
            GameObject tempPart = Instantiate(_piloPartPrefabs[Random.Range(0, _piloPartPrefabs.Length)], new Vector3(0, 0, _piloParts[_piloParts.Count - 1].transform.position.z + Random.Range(_minDistance, _maxDistance)), Quaternion.identity);
            _piloParts.Add(tempPart);
        }
    }
}
