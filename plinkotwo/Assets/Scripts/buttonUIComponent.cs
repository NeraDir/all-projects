using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonUIComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator _currentScreen;

    [SerializeField]
    private GameObject _nextScreen;

    private bool _motion;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_motion)
            return;
        _motion = true;
        StartCoroutine(DoMotion());
    }

    private IEnumerator DoMotion()
    {
        _currentScreen.SetBool("uiIndex", true);
        yield return new WaitForSeconds(0.5f);
        _nextScreen.SetActive(true);
        _motion = false;
        _currentScreen.gameObject.SetActive(false);
    }
}
