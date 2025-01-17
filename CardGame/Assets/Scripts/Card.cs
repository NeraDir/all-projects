using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int index;

    private CardAnimationController cardAnimationController;
    private float changeSideSpeed;

    public static bool canRotate;


    public delegate void SecondCardFoundDelegate();
    public static event SecondCardFoundDelegate SecondCardFoundEvent;

    private void OnEnable()
    {
        Init();
    }


    public void Init()
    {
        cardAnimationController = GetComponent<CardAnimationController>();
        //changeSideSpeed = myAnimator.runtimeAnimatorController.animationClips[1].length;
        //GetComponent<ScaleChanger>().ChangeScale();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.canChooseCard)
            return;

        CheckCard();
        //myAnimator.SetInteger("state", 1);
        //Invoke(nameof(CheckCard), changeSideSpeed);

    }

    public void CheckCard()
    {
        if (GameManager.firstRotatedCard == null && GameManager.firstRotatedCard != this)
            GameManager.firstRotatedCard = this;
        else if (GameManager.secondRotatedCard == null && GameManager.secondRotatedCard != this)
        {
            GameManager.secondRotatedCard = this;

            if (SecondCardFoundEvent != null)
                SecondCardFoundEvent();
        }

        cardAnimationController.PlayRotateToFrontSideAnimation();
    }


    public int GetCardIndex()
    {
        return index;
    }

    public void RotateCard()
    {
        cardAnimationController.PlayRotateToBackSideAnimation();
    }

    public void DestroyCard()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }
    
}
