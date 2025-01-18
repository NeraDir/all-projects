using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FallPlace : MonoBehaviour
{
    private TMP_Text xshow;

    public int xvalue;

    private void Start()
    {
        xvalue = Random.Range(1,3) * GameManager.level;
        xshow = GetComponentInChildren<TMP_Text>();
        xshow.text = "X" + xvalue.ToString("0");
    }
}
