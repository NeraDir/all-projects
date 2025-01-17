using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffaloWallComponent : MonoBehaviour
{
    public int xValue;

    private TMP_Text _buffaluWallXValueTxt;

    public static int mainXValue;

    private void Start()
    {
        mainXValue += 1;
        xValue = mainXValue;
        _buffaluWallXValueTxt = GetComponentInChildren<TMP_Text>();
        _buffaluWallXValueTxt.text = "x" + xValue.ToString();
    }
}
