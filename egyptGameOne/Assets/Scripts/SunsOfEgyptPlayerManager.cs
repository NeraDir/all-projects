using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SunsOfEgyptPlayerManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _healthTxt;

    [SerializeField]
    private GameObject _turnTxt;

    [SerializeField]
    private SunsOfEgyptCardComponent _cardPref;

    [SerializeField]
    private Transform _targetPos;

    [SerializeField]
    private SunsOfEgypCardDatas _cardsDatas;

    [SerializeField]
    private Transform _spawnPos;

    public static int _health = 30;

    private void Start()
    {
        _health = 30;
        for (int i = 0; i < SunsGameManager.CurrentLevel; i++)
        {
            _health -= 2;
           
        }
        if (_health <= 10)
            _health = 10;
        SunsGameManager.onEndFunction.AddListener(OnAddCard);
    }

    private void OnDestroy()
    {
        SunsGameManager.onEndFunction.RemoveListener(OnAddCard);
    }

    private void LateUpdate()
    {
        if (_health <= 0)
            _health = 0;
        _healthTxt.text = _health.ToString();
    }

    public void Init()
    {
        for (int i = 0; i < 3; i++)
        {
            OnAddCard();
        }
    }

    private void OnAddCard()
    {
        if (_health > 0)
        {
            SunsOfEgyptCardComponent _currentCard = Instantiate(_cardPref, _spawnPos);
            _currentCard.Init(_cardsDatas.cardsDatas[Random.Range(0, _cardsDatas.cardsDatas.Count)], true);
            _currentCard.transform.SetSiblingIndex(0);
            Vector3 startScale = _currentCard.transform.localScale;
            _currentCard.transform.DOScale(startScale * 1.5f, 0.25f).OnComplete(() => _currentCard.transform.DOScale(startScale, 0.25f));
        }
        else
        {
            SunsGameManager.onEnd?.Invoke(false);
        }
    }
}
