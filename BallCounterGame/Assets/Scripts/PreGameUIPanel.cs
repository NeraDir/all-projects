using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PreGameUIPanel : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private GameObject levelNumberUIPage;
    [SerializeField]
    private TMP_Text emptyDisplayText;
    [SerializeField]
    private string textToFill;

    [SerializeField]
    private RotateComponent ballIconsRotateComp;

    private Animator myAnimator;

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(fillText());
    }
    private void OnDisable()
    {
        levelNumberUIPage.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayDissableAnimation();
    }

    private IEnumerator fillText()
    {
        emptyDisplayText.text = "";


        for (int i = 0; i < textToFill.Length; i++)
        {
            emptyDisplayText.text += textToFill[i];
            yield return new WaitForSeconds(0.05f);
        }


    }

    public void PlayEnableAnimation()
    {
        myAnimator.SetInteger("parameterID", 0);
    }
    public void PlayLoopAnimation()
    {
        myAnimator.SetInteger("parameterID", 1);
    }
    public void PlayDissableAnimation()
    {
        ballIconsRotateComp.ChangeSpeed(10f);
        myAnimator.SetInteger("parameterID", 2);
    }

    private void CloseMyPage()
    {
        ballIconsRotateComp.gameObject.transform.parent = levelNumberUIPage.gameObject.transform;
        gameObject.SetActive(false);
    }

}
