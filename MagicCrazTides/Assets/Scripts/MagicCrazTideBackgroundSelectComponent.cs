using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicCrazTideBackgroundSelectComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _backgroundIndex;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unSelectedColor;
    [SerializeField] private Image _displayImage;

    private Vector3 _scale;
    private AudioClip _clip;

    private void Awake()
    {
        _scale = transform.localScale;
        _clip = Resources.Load("Audio/Click") as AudioClip;
        _displayImage.color = MagicCrazTideBackgroundComponent.MagicCrazTideBackgroundIndex != _backgroundIndex ? _unSelectedColor : _selectedColor;
        MagicCrazTideBackgroundComponent.backgroundChanged += OnBackgroundChanged;
    }

    private void OnDestroy()
    {
        MagicCrazTideBackgroundComponent.backgroundChanged -= OnBackgroundChanged;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MagicCrazButtomComponent.isPressed)
            return;
        MagicCrazButtomComponent.isPressed = true;
        MagicCrazTideSettingsManager.playSound?.Invoke(_clip);
        transform.DOScale(_scale * 1.2f, 0.1f).OnComplete(() => transform.DOScale(_scale / 1.2f, 0.1f).OnComplete(() => transform.DOScale(_scale, 0.1f).OnComplete(() =>
        {
            OnChoosed();
        })));
    }

    private void OnChoosed()
    {
        MagicCrazTideBackgroundComponent.MagicCrazTideBackgroundIndex = _backgroundIndex;
        MagicCrazButtomComponent.isPressed = false;
        MagicCrazTideBackgroundComponent.backgroundChanged?.Invoke();
    }

    private void OnBackgroundChanged()
    {
        _displayImage.color = MagicCrazTideBackgroundComponent.MagicCrazTideBackgroundIndex != _backgroundIndex ? _unSelectedColor : _selectedColor;
    }
}
