using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour
{
    [SerializeField] private Animator _closePage;
    [SerializeField] private Animator _openPage;

    private Button _button;
    
    private Vector3 _originalScale;

    private const float ScaleDevider = 1.5f;
    private const float TransitionTime = .5f;
    private const float ButtonTransitionTime = 0.1f;

    public static bool isClicked;
    
    private void Awake()
    {
        _originalScale = transform.localScale;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (isClicked)
            return;
        isClicked = true;
        transform.DOScale(_originalScale / ScaleDevider, ButtonTransitionTime).OnComplete(() =>
        {
            transform.DOScale(_originalScale * (ScaleDevider / 2), ButtonTransitionTime).OnComplete(() =>
            {
                transform.DOScale(_originalScale,ButtonTransitionTime).OnComplete(() =>
                {
                    StartCoroutine(OnClicked());
                });
            });
        });
    }

    private IEnumerator OnClicked()
    {
        if(_closePage != null)
            _closePage.SetBool("JELLY_UI_STATE", true);
        yield return new WaitForSeconds(TransitionTime);
        if(_closePage != null)
            _closePage.gameObject.SetActive(false);
        if(_openPage != null)
            _openPage.gameObject.SetActive(true);
        isClicked = false;
    }
}
