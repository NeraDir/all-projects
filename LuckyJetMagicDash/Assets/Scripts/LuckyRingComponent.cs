using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuckyRingComponent : MonoBehaviour
{
    private int luckySumOne = 0;
    private int luckySumTwo = 0;

    public int luckyTotalSum = 0;

    private TMP_Text luckySumsDisplay;

    private void Start()
    {
        luckySumsDisplay = transform.parent.GetComponentInChildren<TMP_Text>();
        luckySumOne = Random.Range(0, 20);
        luckySumTwo = Random.Range(0, 20);
        if (Random.Range(0, 2) != 0)
        {
            luckySumsDisplay.text = luckySumOne + "+" + luckySumTwo;
            luckyTotalSum = luckySumOne + luckySumTwo;
        }
        else
        {
            luckySumsDisplay.text = luckySumOne + "-" + luckySumTwo;
            luckyTotalSum = luckySumOne - luckySumTwo;
        }
    }
}
