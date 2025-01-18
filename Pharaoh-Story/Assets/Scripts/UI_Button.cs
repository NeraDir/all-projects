using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(Button))]
public class UI_Button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator _closeObjectAnimator;

    [SerializeField]
    private GameObject _openObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(AniamtionWait());
    }

    private IEnumerator AniamtionWait() 
    {
        _closeObjectAnimator.SetBool("UI_ANIMATIONSTATEINDEX",true);
        yield return new WaitForSeconds(0.7f);
        _openObject.SetActive(true);
        _closeObjectAnimator.gameObject.SetActive(false);
    }
}
