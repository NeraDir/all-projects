using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AnimationButtonComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Window _openWindow;
    [SerializeField] private Window _closeWindow;

    [SerializeField] private UnityEvent _onCompleteEvent;

    private Transform _transform;

    private Vector3 _scale;
    private Quaternion _rotation;
    private AudioClip _clip;

    private void Awake()
    {
        _transform = transform;
        _scale = _transform.localScale;
        _rotation = _transform.rotation;
        _clip = Resources.Load("Sounds/click") as AudioClip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MainUIController.AnimationButtonClicked)
            return;
        MainUIController.AnimationButtonClicked = true;
        if (_clip!=null)
            SettingsController.onPlayEffect?.Invoke(_clip);
        _transform.DORotateQuaternion(Quaternion.Euler(0, 0, 15), 0.1f).OnComplete(() =>
        {
            _transform.DORotateQuaternion(Quaternion.Euler(0, 0, -15), 0.1f).OnComplete(() =>
            {
                _transform.DORotateQuaternion(_rotation, 0.1f);
            });
        });
        _transform.DOScale(_scale / 1.6f, 0.1f).OnComplete(() =>
        {
            _transform.DOScale(_scale * 1.2f, 0.1f).OnComplete(() =>
            {
                _transform.DOScale(_scale, 0.1f).OnComplete(() =>
                {
                    StartCoroutine(Motion());
                });
            });
        });
    }

    private IEnumerator Motion()
    {
        if(_closeWindow != null)
            _closeWindow.Hide();
        yield return new WaitForSeconds(0.5f);
        if(_openWindow != null)
            _openWindow.Show();
        if(_closeWindow != null)
            _closeWindow.gameObject.SetActive(false);
        MainUIController.AnimationButtonClicked = false;
        _onCompleteEvent?.Invoke();
    }

    public void SetCloseWindow(Window window)
    {
        _closeWindow = window;
    }
}
