using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class checkingmanager : MonoBehaviour
{
    private Image _checkingImage;

    private TMP_Text _checkingTxt;

    public int _ckekingCount;

    private void Start()
    {
        _ckekingCount = 0;
        _checkingImage = GetComponent<Image>();
        _checkingTxt = GetComponentInChildren<TMP_Text>();
        UpdateTxt();
    }

    public bool GetState()
    {
        if (gameManager.spawnedFruits.Find(x => x.fruitSprite == _checkingImage.sprite) != null)
        {
            if (_ckekingCount == gameManager.spawnedFruits.Find(x => x.fruitSprite == _checkingImage.sprite).fruitCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
        return false;
    }

    private void UpdateTxt()
    {
        _checkingTxt.text = "x" + _ckekingCount.ToString();
    }

    public void OnChangeValue(int value)
    {
        _ckekingCount += value;
        if (_ckekingCount < 0)
        {
            _ckekingCount = 0;
        }
        UpdateTxt();
    }
}
