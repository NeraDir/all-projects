using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Image> _eggLeftImages;
    [SerializeField] private List<Image> _eggRightImages;
    [SerializeField] private List<Sprite> _eggSprites;
    [SerializeField] private GameObject _lineConnectorPrefab; 
    [SerializeField] private Transform _lineContainer;       

    [SerializeField] private Text _levelTxt;
    [SerializeField] private Text _timeTxt;

    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private Text _resultTxt;
    [SerializeField] private Text _mainResultTxt;
    [SerializeField] private GameObject _nextButton;

    private List<Image> _levelEggs = new List<Image>();
    private List<Image> _currentSelectedEggs = new List<Image>();

    private Image _lastEgg;

    public static int level = 10;

    private float _time;
    private bool _connect = false;

    private int _maxEggsCount = 7;
    private int _eggsPerLevel = 0;

    private float _maxSizeOfEgg = 160;
    private float _minSizeOfEgg = 100;

    private bool _isLaunched;

    private void Start()
    {
        _levelTxt.text = "LEVEL " + level.ToString();
        for (int i = 0; i < level; i++)
        {
            _time += 1.5f;
            if (_time >= 10)
            {
                _time = 10;
            }
            _eggsPerLevel += 1;
            if (_eggsPerLevel >= _maxEggsCount)
            {
                _eggsPerLevel = _maxEggsCount;
            }
        }
        SetupGame();
        _isLaunched = true;
    }

    private void LateUpdate()
    {
        if (!_isLaunched)
            return;
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            OnGameEnd(false);
            return;
        }
        _timeTxt.text = _time.ToString("0.0") + "s";

        if (Input.GetMouseButtonDown(0))
        {
            OnBeginConnect();
        }

        if (Input.GetMouseButton(0) && _connect)
        {
            UpdateConnection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnConnectComplete();
        }
    }

    private void SetupGame()
    {
        _eggsPerLevel = Mathf.Min(_eggsPerLevel, _maxEggsCount);

        for (int i = 0; i < _eggsPerLevel; i++)
        {
            _eggLeftImages[i].gameObject.SetActive(true);
            _eggRightImages[i].gameObject.SetActive(true);
        }

        List<int> availableLeftIndices = new List<int>();
        List<int> availableRightIndices = new List<int>();
        for (int i = 0; i < _eggsPerLevel; i++)
        {
            availableLeftIndices.Add(i);
            availableRightIndices.Add(i);
        }

        for (int i = 0; i < _eggsPerLevel; i++)
        {
            int rndEgg = Random.Range(0, _eggSprites.Count);

            int rndLeftIndex = Random.Range(0, availableLeftIndices.Count);
            int rndRightIndex = Random.Range(0, availableRightIndices.Count);

            int leftIndex = availableLeftIndices[rndLeftIndex];
            int rightIndex = availableRightIndices[rndRightIndex];

            _eggLeftImages[leftIndex].sprite = _eggSprites[rndEgg];
            _eggRightImages[rightIndex].sprite = _eggSprites[rndEgg];

            _levelEggs.Add(_eggLeftImages[leftIndex]);
            _levelEggs.Add(_eggRightImages[rightIndex]);

            availableLeftIndices.RemoveAt(rndLeftIndex);
            availableRightIndices.RemoveAt(rndRightIndex);

            _eggSprites.RemoveAt(rndEgg);
        }
    }

    private void OnBeginConnect()
    {
        Image egg = GetEgg();

        if (egg != null)
        {
            _connect = true;
            _lastEgg = egg;
            _currentSelectedEggs.Add(_lastEgg);
        }
    }

    private void UpdateConnection()
    {
        Image egg = GetEgg();

        if (egg != null && egg != _lastEgg)
        {
            _lastEgg = egg;
            _currentSelectedEggs.Add(_lastEgg);
        }
    }

    private void OnConnectComplete()
    {
        if (_currentSelectedEggs.Count == 2)
        {
            if (_currentSelectedEggs[0].sprite == _currentSelectedEggs[1].sprite)
            {
                foreach (Image egg in _currentSelectedEggs)
                {
                    _levelEggs.Remove(egg);
                }
                CreateLineConnector(_currentSelectedEggs);
            }
        }

        _currentSelectedEggs.Clear();
        _connect = false;

        if (_levelEggs.Count <= 0)
        {
            OnGameEnd(true);
        }
    }

    private void CreateLineConnector(List<Image> connectedEggs)
    {
        GameObject newLineConnector = Instantiate(_lineConnectorPrefab, _lineContainer);
        UILineConnector lineConnector = newLineConnector.GetComponent<UILineConnector>();

        RectTransform[] eggTransforms = new RectTransform[connectedEggs.Count];
        for (int i = 0; i < connectedEggs.Count; i++)
        {
            eggTransforms[i] = connectedEggs[i].GetComponent<RectTransform>();
        }
        lineConnector.transforms = eggTransforms;
    }

    private void OnGameEnd(bool value)
    {
        _isLaunched = false;
        _resultScreen.SetActive(true);
        _nextButton.SetActive(value);
        _resultTxt.text = value ? "LEVEL COMPLETED" : "LEVEL NOT COMPLETED";
        _mainResultTxt.text = value ? "VICTORY" : "LOOSE";
    }

    public void OnClickNext()
    {
        level += 1;
        if (level > MenuManager.maxReachedLevel)
        {
            MenuManager.maxReachedLevel = level;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private Image GetEgg()
    {
        Vector2 pointerPos = Input.mousePosition;

        foreach (Image item in _levelEggs)
        {
            RectTransform itemRect = item.rectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(itemRect, pointerPos, Camera.main))
            {
                return item;
            }
        }

        return null;
    }
}
