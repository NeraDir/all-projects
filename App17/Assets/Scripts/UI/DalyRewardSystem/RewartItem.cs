using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewartItem : MonoBehaviour
{
    public int Cost = 0;
    public int Number = 0;

    public TMP_Text CostTXT;
    public TMP_Text NumberTXT;

    private void Start()
    {
        CostTXT.text = $"{Cost}";
        NumberTXT.text = $"{Number}";
    }
}
