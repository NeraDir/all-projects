using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BonusPlaceComponent : MonoBehaviour
{
    public int MultiPlay;

    private TMP_Text MultiPlayTxt;

    private void Start()
    {
        MultiPlayTxt = GetComponent<TMP_Text>();
        MultiPlay = Random.Range(1, 30);
        MultiPlayTxt.text = "x" + MultiPlay.ToString();
    }
}
