using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    White,
    Blue,
    Green,
    Purple,
    Yellow,
    Orange,
    Red
}

public class MagicCrazTideFruitBlockComponent : MonoBehaviour
{
    public FruitType fruitType;

    public bool isPressed;

    private AudioClip _clip;

    private void Start()
    {
        _clip = Resources.Load("Audio/Pop") as AudioClip;
    }

    private void OnMouseDown()
    {
        if (transform.parent != null)
        {
            if (transform.parent.GetChild(0) != transform)
                return;
        }
        if (isPressed)
            return;
        isPressed = true;
        MagicCrazTideSettingsManager.playSound?.Invoke(_clip);
        MagicCrazTideGameManager.action?.Invoke(this);
    }
}
