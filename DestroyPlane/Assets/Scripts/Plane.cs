using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plane : MonoBehaviour, IPointerClickHandler
{
    public PlaneColor planeColor;

    public delegate void TapPlaneDelegate(PlaneColor color);
    public event TapPlaneDelegate TapPlaneEvent;

    private Animator animator;

    private bool canCallEvent = true;


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }



    public void DestoyPlane()
    {
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetInteger("animationIndex", 1);
        if (canCallEvent)
        {
            canCallEvent = false;

            if (TapPlaneEvent != null)
                TapPlaneEvent(planeColor);

        }
    }
}

public enum PlaneColor
{
    Green,
    Red,
    Orange,
    Blue
}