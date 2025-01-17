using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldXFallPlace : MonoBehaviour
{
    public int goldXCount;

    private TMP_Text goldShowX;

    private void Start()
    {
        goldXCount = Random.Range(1, 5);
        goldShowX = GetComponent<TMP_Text>();
        goldShowX.text = "X" + goldXCount.ToString("0");
    }
}
