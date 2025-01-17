using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_GamePlayLayer : MonoBehaviour, IPointerClickHandler
{

    public delegate void TapToScreenDelegate();
    public static event TapToScreenDelegate TapToScreenEvent;


    public void OnPointerClick(PointerEventData eventData)
    {
        if(TapToScreenEvent != null)
        {
            TapToScreenEvent();
        }
    }

}
