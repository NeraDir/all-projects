using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class buttenBotton : MonoBehaviour, IPointerClickHandler
{
    private Animator animator;
    [SerializeField]
    private GameObject anima;

    private bool isActive;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isActive)
            return;
        isActive = true;
        animator.SetBool("animationIndex", true);
        Invoke("MakeAnimation", 0.5f);
    }

    private void MakeAnimation() 
    {
        animator.gameObject.SetActive(false);
        anima.SetActive(true);
        isActive = false;
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }


}
