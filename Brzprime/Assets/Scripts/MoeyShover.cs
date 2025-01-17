using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoeyShover : MonoBehaviour
{
    [SerializeField]
    private Text[] showMoney;

    private void LateUpdate()
    {
        foreach (var item in showMoney)
        {
            item.text = MoneyCounter._currentMoney.ToString();
        }
    }
}
