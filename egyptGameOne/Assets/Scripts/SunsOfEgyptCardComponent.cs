using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SunsOfEgyptCardComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TMP_Text _healthTxt;

    [SerializeField]
    private TMP_Text _damageTxt;

    [SerializeField]
    private Image _cardImage;

    private SunsOfEgyptCardData _data;
    private Vector3 beginScale;
    private bool _isPlayer;
    private Transform _targetPos;

    public static bool _isClicked;

    public void Init(SunsOfEgyptCardData data,bool isPlayer = false, Transform targetPos = null)
    {
        _targetPos = targetPos;
        _isPlayer = isPlayer;
        beginScale = transform.localScale;
        _data = data;
        _healthTxt.text = _data.cardHealth.ToString();
        _damageTxt.text = _data.cardDamage.ToString();
        _cardImage.sprite = _data.cardSprite;
    }

    public SunsOfEgyptCardData GetData()
    {
        return _data;
    }

    public void DoDamage(SunsOfEgyptCardComponent card)
    {
        Vector3 startPos = transform.position;
        SunsOfEgyptCardData tempData = card.GetData();
        if (tempData != null)
        {
            transform.DOScale(beginScale * 1.5f, 0.25f).OnComplete(() => transform.DOScale(beginScale, 0.25f));
            transform.DOMove(card.transform.position, 0.5f).OnComplete(() =>
            {
                if (_data.cardHealth >= tempData.cardDamage && _data.cardDamage <= tempData.cardHealth)
                {
                    card.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(card.gameObject));
                    transform.DOScale(beginScale * 1.5f, 0.25f).OnComplete(() => transform.DOScale(beginScale, 0.25f));
                    transform.DOMove(startPos, 0.5f).OnComplete(() => { _isClicked = false; Destroy(gameObject); SunsGameManager.onEndFunction?.Invoke(); });
                }
                else
                {
                    if (_data.cardDamage > tempData.cardHealth)
                    {
                        int damage = tempData.cardHealth - _data.cardDamage;
                        SunsOfEgyptEnemieManager._currentHealth -= Mathf.Abs(damage);
                        card.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(card.gameObject));
                        transform.DOScale(beginScale * 1.5f, 0.25f).OnComplete(() => transform.DOScale(beginScale, 0.25f));
                        transform.DOMove(startPos, 0.5f).OnComplete(() => { _isClicked = false; Destroy(gameObject); SunsGameManager.onEndFunction?.Invoke(); });
                    }
                    else if (_data.cardHealth < tempData.cardDamage)
                    {
                        int damage = _data.cardHealth - tempData.cardDamage;
                        SunsOfEgyptPlayerManager._health -= Mathf.Abs(damage);
                        card.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(card.gameObject));
                        transform.DOScale(beginScale * 1.5f, 0.25f).OnComplete(() => transform.DOScale(beginScale, 0.25f));
                        transform.DOMove(startPos, 0.5f).OnComplete(() => { _isClicked = false; Destroy(gameObject); SunsGameManager.onEndFunction?.Invoke(); });
                    }
                }
            });
        }
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isPlayer)
            return;
        if (_isClicked)
            return;
        _isClicked = true;
        DoDamage(SunsOfEgyptEnemieManager._currentCard);
    }
}
