using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MINIGAMEITEMCOMPONENT : MonoBehaviour,IPointerClickHandler
{
    private bool _isClicked;

    private Vector3 _scale;

    private void OnEnable()
    {
        _isClicked = true;
        float scale = Random.Range(0.5f, 1.2f);
        _scale = new Vector3(scale, scale, scale);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-360, 360));
        transform.localScale = Vector3.zero;
        transform.DOScale(_scale, 0.1f).OnComplete(() => _isClicked = false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked)
            return;
        _isClicked = true;
        transform.DOScale(_scale * 1.3f, 0.1f).OnComplete(() => transform.DOScale(_scale / 1.1f, 0.1f).OnComplete(() => transform.DOScale(Vector3.zero, 0.1f).OnComplete(() => gameObject.SetActive(false))));
    }
}
