using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenPartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private int _dir = -1;
    public static int x = 0;
    private static int flag = 0;
    public void OnPointerDown(PointerEventData eventData)
    {
        flag++;
        if(_dir > 0)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        flag--;
        if (flag <= 0)
        {
            x = 0;
        }
    }
}
