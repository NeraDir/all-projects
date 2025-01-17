using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunsOfEgyptEnemieManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _healthTxt;

    [SerializeField]
    private GameObject _turnTxt;

    [SerializeField]
    private SunsOfEgyptCardComponent _cardPref;

    [SerializeField]
    private Transform _spawnPos;

    [SerializeField]
    private Transform _targetPos;

    [SerializeField]
    private SunsOfEgypCardDatas _cardsDatas;

    public static SunsOfEgyptCardComponent _currentCard;

    public static int _currentHealth = 30;

    private void Start()
    {
        _currentHealth = 30;
        for (int i = 0; i < SunsGameManager.CurrentLevel; i++)
        {
            _currentHealth += 5;
        }
        if (_currentHealth >= 100)
            _currentHealth = 100;
        SunsGameManager.onEndFunction.AddListener(AddCard);
    }

    private void OnDestroy()
    {
        SunsGameManager.onEndFunction.AddListener(AddCard);
    }

    private void LateUpdate()
    {
        if (_currentHealth <= 0)
            _currentHealth = 0;
        _healthTxt.text = _currentHealth.ToString();
    }

    public void AddCard()
    {
        if (_currentHealth > 0)
        {
            _turnTxt.transform.DOScale(Vector3.one, 0.25f);
            _currentCard = Instantiate(_cardPref, _spawnPos.position, Quaternion.identity, _spawnPos.parent);
            _currentCard.Init(_cardsDatas.cardsDatas[Random.Range(0, _cardsDatas.cardsDatas.Count)]);
            _currentCard.transform.SetSiblingIndex(0);
            Vector3 startScale = _currentCard.transform.localScale;
            _currentCard.transform.DOScale(startScale * 1.5f, 0.25f).OnComplete(() => _currentCard.transform.DOScale(startScale, 0.25f));
            _currentCard.transform.DOMove(_targetPos.position, 0.5f);
            _turnTxt.transform.DOScale(Vector3.zero, 0.25f);
        }
        else
        {
            SunsGameManager.onEnd?.Invoke(true);
        }
    }
}
