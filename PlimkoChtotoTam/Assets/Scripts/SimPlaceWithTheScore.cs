using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimPlaceWithTheScore : MonoBehaviour
{
    private TMP_Text showX;

    public int value;

    private void Start()
    {
        showX = GetComponentInChildren<TMP_Text>();
        value = Random.Range(1, 10);
        showX.text = "X" + value.ToString("0");
    }
}
