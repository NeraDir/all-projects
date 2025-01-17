using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UniWebViewLogger;

public class AviaFlyingElementComponent : MonoBehaviour, IPointerClickHandler
{
    public Image elementImage;

    public Sprite[] elementsSprites;

    private bool _canClick = false;

    private float _waitingTime = 4;

    private void Start()
    {
        elementImage.sprite = elementsSprites[Random.Range(0, elementsSprites.Length)];
        transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => { _canClick = true; StartCoroutine(WaitingIenumerat()); });
    }

    private IEnumerator WaitingIenumerat()
    {
        yield return new WaitForSeconds(_waitingTime);
        _canClick = false;
        transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
            {
                GameAviaManager.score -= Random.Range(5, 10);
                Destroy(gameObject);
            });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_canClick)
            return;
        ClickMethod();
    }

    private void ClickMethod()
    {
        _canClick = false;
        GameAviaManager.clickedCount++;
        StopAllCoroutines();
        transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
        {
            GameAviaManager.score += Random.Range(10, 20);
            if (GameAviaManager.clickedCount >= 10)
            {
                GameAviaManager.level += 1;
                GameAviaManager.clickedCount = 0;
            }
            Destroy(gameObject);
        });
    }
}
