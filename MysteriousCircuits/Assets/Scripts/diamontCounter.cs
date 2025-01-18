using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class diamontCounter : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        menuScreenController.updateDiamondCount += UpdateTxt;
        UpdateTxt();
    }

    private void OnDestroy()
    {
        menuScreenController.updateDiamondCount -= UpdateTxt;
    }

    private void UpdateTxt()
    {
        if (_text != null) 
            _text.text = "x" + menuScreenController.userDiamondsCount.ToString();
    }
}
