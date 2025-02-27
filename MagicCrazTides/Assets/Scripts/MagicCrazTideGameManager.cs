using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MagicCrazTideGameManager : MonoBehaviour
{
    public static int MaxReachedLevel
    {
        get => PlayerPrefs.GetInt("MagicCrazTideMaxReachedLevel", 0);
        set => PlayerPrefs.SetInt("MagicCrazTideMaxReachedLevel", value);
    }

    public static int Level;
    public static int DestructedCount;
    public static List<Transform> TempPlaces = new List<Transform>();
    public static UnityEvent onShowEnd = new UnityEvent();
    public static UnityEvent<MagicCrazTideFruitBlockComponent> action = new UnityEvent<MagicCrazTideFruitBlockComponent>();

    public static List<FruitType> order = new List<FruitType>();

    [SerializeField] private List<Transform> tempPlaces;

    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _doubleButton;

    [SerializeField] private Text _levelTxt;

    [SerializeField] private GameObject _result;
    [SerializeField] private Text _resultTxt;
    [SerializeField] private Text _timerTxt;

    [SerializeField] private Slider _levelProgress;

    [SerializeField] private LevelData[] levels;


    private bool _isLaunched;
    private float _timer;

    private float _maxOrdersCount;
    private int index;

    private void Start()
    {
        index = Level > levels.Length ? Random.Range(0, levels.Length) : Level;

        DestructedCount = 0;
        _levelTxt.text = "LEVEL" + (Level + 1).ToString();
        TempPlaces = tempPlaces;
        levels[index].level.SetActive(true);
        order = levels[index].order.ToList();
        _timer = levels[index].timer;
        action.AddListener(OnMoveBlockToPlace);
        _maxOrdersCount = order.Count;
        _isLaunched = true;
        onShowEnd.AddListener(OnShowEnd);
    }

    private void OnDestroy()
    {
        action.RemoveListener(OnMoveBlockToPlace);
        onShowEnd?.RemoveListener(OnShowEnd);
    }

    private void LateUpdate()
    {
        if (!_isLaunched)
            return;
        _timer -= Time.deltaTime;
        _timerTxt.text = _timer.ToString("0.0") + "s";
        if (_timer <= 0)
        {
            MagicCrazButtomComponent.isPressed = false;
            _result.SetActive(true);
            _nextButton.SetActive(false);
            _resultTxt.text = "LEVEL NOT COMPLETED";
            _isLaunched = false;
        }
        _levelProgress.value = Mathf.Lerp(_levelProgress.value, DestructedCount / _maxOrdersCount, 10 * Time.deltaTime);
    }

    private void OnShowEnd()
    {
        if (DestructedCount >= _maxOrdersCount)
        {
            Invoke(nameof(DisplayResults), 1f);
        }
    }

    private void DisplayResults()
    {
        if (!_isLaunched)
            return;
        _isLaunched = false;
        _nextButton.SetActive(true);
        MagicCrazButtomComponent.isPressed = false;
        _result.SetActive(true);
        _resultTxt.text = "LEVEL COMPLETED";
    }

    private void OnMoveBlockToPlace(MagicCrazTideFruitBlockComponent value)
    {
        MagicCrazTideFruitPlaceComponent target = levels[index].targets.Find(x => x.fruitType == value.fruitType);
        value.transform.parent = null;
        value.transform.DOMove(target.transform.position, 0.5f).OnComplete(() => value.transform.DOScale(90,0.25f).OnComplete(() =>
        {
            if (target.fruitType == value.fruitType)
            {
                value.isPressed = true;
                value.transform.parent = target.transform;
                target.Destruction();
            }
        }));
    }
}

[Serializable]
public struct LevelData
{
    public FruitType[] order;
    public GameObject level;
    public float timer;
    public List<MagicCrazTideFruitPlaceComponent> targets;
}