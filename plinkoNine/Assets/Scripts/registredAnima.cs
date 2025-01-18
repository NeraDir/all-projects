using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registredAnima : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (!MainPageController._isRegistrationButtonClicked)
        {
            DoBack();
        }
    }

    private void DoBack()
    {
        animator.SetBool("UI_PAGE_STATE", true);
    }

    public void OnEnd()
    {
        gameObject.SetActive(false);
    }
}
