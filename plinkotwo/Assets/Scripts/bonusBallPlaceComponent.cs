using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bonusBallPlaceComponent : MonoBehaviour
{
    private TMP_Text _showTxt;

    public int starsX;

    private void Start()
    {
        starsX = Random.Range(1, 10);
        _showTxt = GetComponentInChildren<TMP_Text>();
        _showTxt.text = "x" + starsX.ToString();
    }
}
