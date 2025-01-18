using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinControll : MonoBehaviour
{
    public TMP_Text WInResult;

    public void Init(int num1, int num2, bool TihsNon = false)
    {
        if (!TihsNon)
            WInResult.text = num1 + " / " + num2;
        else
            WInResult.text = num1.ToString();
    }
}
