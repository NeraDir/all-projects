using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonCustom : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _closePage;
    [SerializeField] private GameObject _openPage;

    public static bool isClicked;

    public UnityEvent action;

    private AudioClip _click;

    private void Start()
    {
        _click = Resources.Load("Audio/click") as AudioClip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        isClicked = true;
        SettingsManager.playSound?.Invoke(_click);
        transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f).OnComplete(() => transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>
        {
            StartCoroutine(DoSomthing());
        })));
    }

    private IEnumerator DoSomthing()
    {
        SpawnBlocks.spawn = true;
        yield return new WaitForSeconds(1.3f);
        OnOpen();
        SpawnBlocks.spawn = false;
        action?.Invoke();
        isClicked = false;
    }

    private void OnOpen()
    {
        if(_closePage != null)
            _closePage.SetActive(false);
        if(_openPage != null)
            _openPage.SetActive(true);
        
    }
}
