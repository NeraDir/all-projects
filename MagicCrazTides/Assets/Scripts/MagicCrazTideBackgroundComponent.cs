using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCrazTideBackgroundComponent : MonoBehaviour
{
    public static int MagicCrazTideBackgroundIndex
    {
        get => PlayerPrefs.GetInt("MagicCrazTideBackgroundIndex", 0);
        set => PlayerPrefs.SetInt("MagicCrazTideBackgroundIndex", value);
    }

    [SerializeField] private Sprite[] _backgroundSprites;

    public static Action backgroundChanged;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        if (_image != null) 
            _image.sprite = _backgroundSprites[MagicCrazTideBackgroundIndex];
    }
}
