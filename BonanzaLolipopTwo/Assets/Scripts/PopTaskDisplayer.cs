using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopTaskDisplayer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _popTaskCountDisplay;

    [SerializeField]
    private Image _popTaskSpriteDisplay;

    public void SetDataOfTask(int count, Sprite sprite)
    {
        _popTaskCountDisplay.text = "X " + count.ToString("0");
        _popTaskSpriteDisplay.sprite = sprite;
    }
}
