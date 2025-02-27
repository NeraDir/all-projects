using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _timerTxt;
    [SerializeField] private Text _levelTxt;
    [SerializeField] private ResultScreen _resultScreen;

    [Space(16)]
    [SerializeField] private List<Sprite> _fruitSprites;
    [SerializeField] private FruitComponent _fruitPrefab;
    [SerializeField] private Transform[] _spawnPositions;

    [Space(16)]
    [SerializeField] private GameObject[] _heartIamges;

    [Space(16)]
    [SerializeField] private UILineConnector _lineConnecter;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _pointRect;

    public static Action<int> takeDamage;
    public static Action<bool> isGameEnd;

    private List<Image> _fruitsComponents = new List<Image>();
    private List<Image> _connectedFruits = new List<Image>();

    private float _timer = 2.5f;
    private bool _gameLaunched;
    private int _fruitCount = 1;
    private int _hearts;

    private bool _isConnecting = false; 
    private Image _lastFruit = null;

    private void Start()
    {
        _hearts = 3;
        _levelTxt.text = "LEVEL " + (TlineGameDataSaves.TlineCurrentLevel + 1).ToString();
        for (int i = 0; i < TlineGameDataSaves.TlineCurrentLevel; i++)
        {
            _timer += 2.5f;
            if (i % 2 == 0 && i != 0)
            {
                _fruitCount += 1;
            }
        }
        SetupLevel();
        isGameEnd += OnGameEnd;
        takeDamage += OnTakeDamage;
        _gameLaunched = true;
    }

    private void OnDestroy()
    {
        isGameEnd -= OnGameEnd;
        takeDamage -= OnTakeDamage;
    }

    private void LateUpdate()
    {
        if (!_gameLaunched)
            return;
        _timer -= Time.deltaTime;
        if (_timer <= 0 )
        {
            isGameEnd?.Invoke(false);
            _timer = 0;
            return;
        }
        _timerTxt.text = _timer.ToString("0.0") + "s";

        if (Input.GetMouseButtonDown(0)) 
        {
            StartConnection();
        }

        if (Input.GetMouseButton(0) && _isConnecting) 
        {
            UpdateConnection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishConnection();
        }
    }

    private void OnGameEnd(bool value)
    {
        _gameLaunched = false;
        _resultScreen.gameObject.SetActive(true);
        _resultScreen.SetupData(value);
    }

    private void OnCheckfruits()
    {

    }

    private void StartConnection()
    {
        Image hitFruit = GetFruitUnderPointer();

        if (hitFruit != null)
        {
            _isConnecting = true;
            _lastFruit = hitFruit;
            _connectedFruits.Add(_lastFruit);
            UpdateLineRenderer();
        }
    }

    private void UpdateConnection()
    {
        Image hitFruit = GetFruitUnderPointer();

        if (hitFruit != null && hitFruit != _lastFruit) 
        {
            _lastFruit = hitFruit;
            _connectedFruits.Add(_lastFruit);
            UpdateLineRenderer();
        }
    }

    private void FinishConnection()
    {
        if (_connectedFruits.Count == 2) 
        {
            if (_connectedFruits[0].sprite == _connectedFruits[1].sprite)
            {
                foreach (Image fruit in _connectedFruits)
                {
                    Destroy(fruit.gameObject);
                    _fruitsComponents.Remove(fruit);
                }
            }
            else
            {
                takeDamage?.Invoke(1);
            }
        }
        else if(_connectedFruits.Count > 2)
        {
            _connectedFruits.Clear();
            takeDamage?.Invoke(1);
            UpdateLineRenderer();
        }

        _connectedFruits.Clear();
        UpdateLineRenderer();
        _isConnecting = false;

        if (_fruitsComponents.Count <= 0)
        {
            isGameEnd?.Invoke(true);
        }
    }

    private void UpdateLineRenderer()
    {
        _lineConnecter.transforms = new RectTransform[_connectedFruits.Count];

        for (int i = 0; i < _connectedFruits.Count; i++)
        {
            _lineConnecter.transforms[i] = _connectedFruits[i].GetComponent<RectTransform>();
        }

        if (_connectedFruits.Count == 0)
        {
            _lineConnecter.transforms = new RectTransform[0];
        }
    }

    private Image GetFruitUnderPointer()
    {
        Vector2 pointerPos = Input.mousePosition;

        foreach (Image fruit in _fruitsComponents)
        {
            RectTransform fruitRect = fruit.rectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(fruitRect, pointerPos, Camera.main))
            {
                return fruit;
            }
        }

        return null;
    }

    private void SetupLevel()
    {
        for (int i = 0; i < _fruitCount; i++)
        {
            Sprite newSprite = _fruitSprites[Random.Range(0, _fruitSprites.Count)];
            for (int j = 0; j < 2; j++)
            {
                FruitComponent newFruit = Instantiate(_fruitPrefab, 
                    new Vector3(
                        Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), 
                        Random.Range(_spawnPositions[0].position.y, _spawnPositions[1].position.y), 
                        0), 
                    Quaternion.Euler(
                        0, 
                        0, 
                        Random.Range(-360, 360)),
                    _spawnPositions[0].parent);
                newFruit.transform.SetSiblingIndex(0);
                newFruit.SetupData(newSprite);
                _fruitsComponents.Add(newFruit.GetComponent<Image>());
            }
        }
    }

    private void OnTakeDamage(int value)
    {
        _hearts -= value;
        if (_hearts <= 0)
            isGameEnd?.Invoke(false);
        UpdateHearts();
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < _heartIamges.Length; i++)
        {
            if (i >= _hearts)
            {
                _heartIamges[i].transform.DOScale(Vector3.zero, 0.25f);
            }
        }
    }
}
