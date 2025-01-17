using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class blaztBonusPlace : MonoBehaviour
{
    public int multi;

    private TMP_Text _displayMulti;

    private void Start()
    {
        _displayMulti = GetComponentInChildren<TMP_Text>();
        multi = Random.Range(0, 98);
        _displayMulti.text = "x" + multi.ToString();
    }
}
