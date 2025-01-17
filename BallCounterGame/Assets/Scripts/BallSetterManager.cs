using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallSetterManager : MonoBehaviour
{
    [SerializeField]
    private List<BallCounterItem> items;

    private List<(ColorType, int)> ItemsInfoList = new();

    public delegate void TapCheckButtonDelegate(List<(ColorType, int)> values);
    public static event TapCheckButtonDelegate TapCheckButtonEvent;

    public delegate void ShowRigthResultCopmleteDelegate();
    public static event ShowRigthResultCopmleteDelegate ShowRigthResultCopmleteEvent;


    private Animator myAnimator;

    [SerializeField]
    private TMP_Text emptyText;
    [SerializeField]
    private string ballsSetText;
    [SerializeField]
    private string ballCheckText;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color rightColor;
    [SerializeField]
    private Color wrongColor;

    private bool canTapCheckkButton = true;

    [SerializeField]
    private GameObject greatIcon;



    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
        StartCoroutine(fillText(ballsSetText));
    }

    public void Init()
    {
        foreach (var item in items)
        {
            item.Init(defaultColor, rightColor, wrongColor);
        }
    }

    public void ResetItems()
    {
        foreach(var item in items)
        {
            item.ResetItem();
        }
    }

    public void CheckItems()
    {
        if (canTapCheckkButton)
        {
            canTapCheckkButton = false;

            if (ItemsInfoList.Count > 0)
                ItemsInfoList.Clear();


            foreach (var item in items)
                ItemsInfoList.Add(item.GetColorAndValue());

            if (TapCheckButtonEvent != null)
                TapCheckButtonEvent(ItemsInfoList);

            ShowRightBallCount();

            StartCoroutine(fillText(ballCheckText));

            if (GamePlayController.hasRigthAnswerByPlayer)
            {
                Invoke(nameof(ShowGreatIcon), 2f);
            }
            Invoke(nameof(CallCanShowRsultEvent), 5f);
        }
    }


    public void ShowItems()
    {
        
        StartCoroutine(showItemsText());
    }

    private IEnumerator showItemsText()
    {
        for (int i = 0; i < items.Count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            items[i].EnableTextAnimator();
        }
    }

    public void CallCanShowRsultEvent()
    {
        myAnimator.SetInteger("parameterID", 2);

        if (ShowRigthResultCopmleteEvent != null)
            ShowRigthResultCopmleteEvent();
    }

    public void ShowRightBallCount()
    {
        foreach (var item in items)
        {
            item.ShowRigthBallCount();
        }
    }

    private void ShowGreatIcon()
    {
        greatIcon.SetActive(true);
    }

    public void DisableItems()
    {

    }

    private IEnumerator fillText(string textToFill)
    {
        emptyText.text = "";
        for (int i = 0; i < textToFill.Length; i++)
        {
            emptyText.text += textToFill[i];
            yield return new WaitForSeconds(0.02f);
        }

    }
}
