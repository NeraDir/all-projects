using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LampComponent : MonoBehaviour
{
    private Light _lamp;
    
    private void Start()
    {
        _lamp = GetComponentInChildren<Light>();
        GameManager.nightMode += OnNightChange;
    }

    private void OnDestroy()
    {
        GameManager.nightMode -= OnNightChange;
    }

    private void OnNightChange(bool isNight)
    {
        _lamp.DOColor(isNight ? Color.black : Color.yellow,0.25f);
    }
}
