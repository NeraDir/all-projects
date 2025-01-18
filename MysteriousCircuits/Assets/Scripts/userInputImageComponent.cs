using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class userInputImageComponent : MonoBehaviour, IPointerClickHandler
{
    public static Action<Vector3> sendTapPosition;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 postion;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
               (RectTransform)_canvas.transform,
               eventData.position,
        _canvas.worldCamera,
        out postion);

        sendTapPosition?.Invoke(postion);
    }
}
