using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class fruitShieldManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Color _clickedColor;

    [SerializeField]
    private Color _unClickedColor;

    private Image _shieldImage;

    private float _shieldActiveTime;

    private bool _shieldActive;

    private void Start()
    {
        _shieldImage = GetComponent<Image>();
        _shieldImage.color = _unClickedColor;
        _shieldActiveTime = 2f;
    }

    public bool GetShieldState()
    {
        return _shieldActive;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_shieldActive)
            return;
        _shieldActive = true;
        StartCoroutine(ShieldActvate());
    }

    private IEnumerator ShieldActvate()
    {
        _shieldImage.DOColor(_clickedColor, 0.15f);
        yield return new WaitForSeconds(_shieldActiveTime);
        _shieldImage.DOColor(_unClickedColor, 0.15f).OnComplete(() => _shieldActive = false);
    }
}
