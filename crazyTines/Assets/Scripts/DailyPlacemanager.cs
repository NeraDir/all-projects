using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyPlacemanager : MonoBehaviour
{
    private TMP_Text _winShowTxt;

    public int winValue;
    
    private void Start()
    {
        winValue = Random.Range(1, 100);
        _winShowTxt = GetComponent<TMP_Text>();
        _winShowTxt.text = winValue.ToString();
    }
}
