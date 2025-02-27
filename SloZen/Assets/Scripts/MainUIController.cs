using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainUIController : MonoBehaviour
{
    public static bool AnimationButtonClicked;

    [SerializeField] private Window[] _windows;

    private void Awake()
    {
        foreach (var item in _windows)
        {
            item.Init();
        }
    }
}
