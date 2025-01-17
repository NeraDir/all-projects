using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour, IPointerClickHandler
{
    public CirclGameController SecCNTRT;

    public void OnPointerClick(PointerEventData eventData)
    {
        SecCNTRT.Flip();
    }
}
