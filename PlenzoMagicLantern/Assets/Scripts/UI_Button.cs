using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UI_Button : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private GameObject _openPage;

    [SerializeField]
    private Animator _closePage;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _sound;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Opening());
    }

    private IEnumerator Opening() 
    {
        _audioSource.PlayOneShot(_sound);
        _closePage.SetBool("UI_STATE", true);
        yield return new WaitForSeconds(0.5f);
        _openPage.SetActive(true);
        _closePage.gameObject.SetActive(false);
    }
}
