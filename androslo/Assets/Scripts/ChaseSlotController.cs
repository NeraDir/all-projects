using DG.Tweening;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ChaseSlotController : MonoBehaviour
{
    [SerializeField]
    private ChaseGameComponent _chaseGameComponent;

    [SerializeField]
    private Button _spinButton;

    [SerializeField] private Button _plusBetButton;
    [SerializeField] private Button _minusBetButton;

    private ChaseSlotComponent[] _slotComponents;
    private Animator _animator;
    private packComponent _packComponent;

    public static Action spinIsFinish;
    public static Action spinIsBeggining;
    public static Action spinHide;

    private bool _isFinished;

    private void Start()
    {
        _slotComponents = _chaseGameComponent.GetCurrentPack().GetComponentsInChildren<ChaseSlotComponent>();
        _animator = _chaseGameComponent.GetCurrentPack().GetComponent<Animator>();
        _packComponent = _chaseGameComponent.GetCurrentPack().GetComponent<packComponent>();
        _spinButton.onClick.AddListener(OnSpinButtonPressed);
        spinIsFinish += OnFinishSpin;
        spinIsBeggining += OnSpiningBeggining;
        spinHide += OnSpinHiding;
        _isFinished = false;
        _plusBetButton.onClick.AddListener(() => OnBetChangeButtonPressed(1));
        _minusBetButton.onClick.AddListener(() => OnBetChangeButtonPressed(-1));
    }

    private void OnDestroy()
    {
        spinIsFinish -= OnFinishSpin;
        spinIsBeggining -= OnSpiningBeggining;
        spinHide -= OnSpinHiding;
    }

    private void OnSpinHiding()
    {
        _animator.SetBool("Spinning", false);
        foreach (var item in _slotComponents)
        {
            item.UpdateVisual();
        }
    }

    private void OnSpinButtonPressed()
    {
        if (!_isFinished)
            return;
        if (ChaseGameComponent.betValue >= ChasePlayerDataComponent.ChasePlayerCoins)
            return;
        _packComponent.DestroyLines();
        _chaseGameComponent.MinusSpin();
        ChasePlayerDataComponent.ChasePlayerCoins -= ChaseGameComponent.betValue;
        _isFinished = false;
        _animator.SetBool("Spinning", true);
    }

    private void OnSpiningBeggining()
    {
        foreach (var item in _slotComponents)
        {
            item.SetData();
        }
    }

    private void OnBetChangeButtonPressed(int value)
    {
        if (ChaseGameComponent.betValue + (10 * value) < 10)
            return;
        if (ChaseGameComponent.betValue + (10 * value) >= ChasePlayerDataComponent.ChasePlayerCoins)
            return;
        if (ChaseGameComponent.betValue <= 0)
            return;
        ChaseGameComponent.betValue += (10 * value);
    }

    private void OnFinishSpin()
    {
        StartCoroutine(SpinFinishing());
    }

    private IEnumerator SpinFinishing()
    {
        foreach (var item in _slotComponents)
        {
            yield return new WaitForSeconds(0.1f);
            item.OpenCell();
        }
        yield return new WaitForSeconds(1.2f);
        _packComponent.CheckLines();
        _isFinished = true;
    }
}

[System.Serializable]
public class WinLineData
{
    [HideInInspector] public UILineConnector connector;
    public List<ChaseTriggerPlaceComponent> chaseTriggerPlaceComponents;
    public int countTocheck;

    private List<ChaseTriggerPlaceComponent> _currentTriggers = new List<ChaseTriggerPlaceComponent>();

    public void GetWinLine(UILineConnector connector)
    {
        _currentTriggers.Clear();
        this.connector = connector;
        foreach (var item in chaseTriggerPlaceComponents)
        {
            bool lineIsEmpty = _currentTriggers.Count <= 0;
            if (lineIsEmpty)
            {
                _currentTriggers.Add(item);
            }
            else
            {
                bool isSameSprites = _currentTriggers[0].item.GetSprite() == item.item.GetSprite();
                if (isSameSprites)
                {
                    _currentTriggers.Add(item);
                }
                else
                {
                    break;
                }
            }
        }

        if (_currentTriggers.Count >= countTocheck)
        {
            List<Vector2> rectTransformsOfTriggers = new List<Vector2>();
            foreach (var item in _currentTriggers)
            {
                rectTransformsOfTriggers.Add(new Vector2(item.transform.localPosition.x, item.transform.localPosition.y - 25));
                int value = ChaseGameComponent.betValue * 2 / 3;
                ChasePlayerDataComponent.ChasePlayerCoins += value;
                GameObject.FindObjectOfType<ChaseGameComponent>().ChangeCurrentScore(value);
            }
            this.connector.GetComponent<UILineRenderer>().Points = rectTransformsOfTriggers.ToArray();
        }
    }

    public void Clear()
    {
        _currentTriggers.Clear();
    }

    public UILineConnector GetConnector() => connector;
}
