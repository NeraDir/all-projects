using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NewAnimationButtonComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _closeScreen;
    [SerializeField] private Animator _openScreen;

    public static bool isPressed;

    [SerializeField] private UnityEvent _action = new UnityEvent();

    private Transform _buttonTransform;
    private Vector3 _beginScale;
    private Vector3 _beginPosition;

    private void Awake()
    {
        _buttonTransform = transform;
        _beginScale = _buttonTransform.localScale;
        _beginPosition = _buttonTransform.localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPressed)
            return;
        isPressed = true;
        OnClick();
    }

    private void OnClick()
    {
        _buttonTransform.DOScale(_beginScale / 1.5f, 0.12f).OnComplete(() =>
        {
            _buttonTransform.DOScale(_beginScale, 0.12f).OnComplete(() =>
            {
                StartCoroutine(AnimationLaunch());
            });
        });
    }

    private IEnumerator AnimationLaunch()
    {
        if (_closeScreen != null)
            _closeScreen.SetBool("TigerClawsUIScreenState", true);
        yield return new WaitForSeconds(0.5f);
        if (_closeScreen != null)
            _closeScreen.gameObject.SetActive(false);
        if (_openScreen != null)
            _openScreen.gameObject.SetActive(true);
        isPressed = false;
        _action?.Invoke();
    }
}
