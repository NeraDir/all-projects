using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnubisCoins : MonoBehaviour
{
    private Text _coinsLabel;

    public static Action UpdateCoins;

    private void Awake()
    {
        _coinsLabel = GetComponent<Text>();
        UpdateCoins += UpdateLabelContent;
        UpdateLabelContent();
    }

    private void OnDestroy()
    {
        UpdateCoins -= UpdateLabelContent;
    }

    private void UpdateLabelContent()
    {
        if(_coinsLabel != null)
            _coinsLabel.text =  AnubisUserData.Coins.ToString();
    }
}
