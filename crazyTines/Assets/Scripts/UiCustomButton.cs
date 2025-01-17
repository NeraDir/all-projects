using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UiCustomButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator _closePage;

    [SerializeField]
    private Animator _openPage;

    [SerializeField]
    private Animator _dailyPage;

    public static bool _buttonIsClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_buttonIsClicked)
            return;
        _buttonIsClicked = true;
        StartCoroutine(Motion());
    }

    private IEnumerator Motion()
    {

        if (_closePage != null)
            _closePage.SetBool("Ui_State", true);
        yield return new WaitForSeconds(0.5f);
        if (!PlayerPrefs.HasKey("CrazyFirstDailySeesSaveKey"))
        {
            _dailyPage.gameObject.SetActive(true);
            if (_closePage != null)
                _closePage.gameObject.SetActive(false);
            PlayerPrefs.SetInt("CrazyFirstDailySeesSaveKey", 1);
        }
        else
        {
            if (_openPage != null)
                _openPage.gameObject.SetActive(true);
            if (_closePage != null)
                _closePage.gameObject.SetActive(false);
        }

      
        _buttonIsClicked = false;
    }
}
