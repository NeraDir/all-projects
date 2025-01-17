using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirclesComponent : MonoBehaviour
{
    public int xValue;

    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        xValue = Random.Range(1, 20);
        text.text = "x" + xValue.ToString();
    }

    public int GetXValue() 
    {
        return xValue;
    }
}
