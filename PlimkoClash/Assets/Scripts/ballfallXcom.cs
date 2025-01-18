using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballfallXcom : MonoBehaviour
{
    public int ballsIncase;

    public int X;

    private Text _xTxt;

    private void Start()
    {
        X = Random.Range(1, 10);
        _xTxt = GetComponentInChildren<Text>();
        _xTxt.text = "x"+X.ToString();
    }
}
