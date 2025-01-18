using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public delegate void DetectBox();
    public static event DetectBox BoxHasBeenTrigger;

    private Animator myAnimator;
    private bool isOpen;

    private bool oneShotTrigger;

    private void OnEnable()
    {
        //TestSlotPageController.SlotGamesComleted += CloseBox;
        SlotPageManager.CloseSlot += CloseBox;
        oneShotTrigger = false;
        myAnimator = GetComponent<Animator>();
        isOpen = false;
    }

    private void OnDisable()
    {
        //TestSlotPageController.SlotGamesComleted -= CloseBox;
        SlotPageManager.CloseSlot -= CloseBox;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (!oneShotTrigger)
            {
                oneShotTrigger = true;
                myAnimator.SetInteger("stateIdx", 1);
            }
        }

    }

   


    public void OpenBox()
    {
        isOpen = true;

        if (BoxHasBeenTrigger != null)
        {
            BoxHasBeenTrigger();
            //myAnimator.SetInteger("stateIdx", 1);
        }
    }

    public void CloseBox()
    {
        if (isOpen)
        {
            //stateIdx
            myAnimator.SetInteger("stateIdx", 2);
        }
    }
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
