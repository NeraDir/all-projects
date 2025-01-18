using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ballBuyContainer : MonoBehaviour,IPointerClickHandler
{
    private Text _buyContainerTxt;

    public int priceStar;

    public int ballIndex;

    [SerializeField]
    private ballBuyContainer[] _ballBuysContainers;

    private void Start()
    {
        _buyContainerTxt = GetComponentInChildren<Text>();
        UpdateState();
    }

    public void UpdateState()
    {
        if (!PlayerPrefs.HasKey($"ballBuyContainer{ballIndex}DataSave"))
        {
            if (_buyContainerTxt != null)
                _buyContainerTxt.text = "x" + priceStar.ToString("0");
        }
        else
        {
            if (gameManager.ballIndex == ballIndex)
            {
                if(_buyContainerTxt != null)
                    _buyContainerTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                if (_buyContainerTxt != null)
                    _buyContainerTxt.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void Buy()
    {
        if (!PlayerPrefs.HasKey($"ballBuyContainer{ballIndex}DataSave"))
        {
            if (gameManager.maxStarsCount >= priceStar)
            {
                gameManager.maxStarsCount -= priceStar;
                PlayerPrefs.SetInt($"ballBuyContainer{ballIndex}DataSave", 1);
                Equip();
            }
            else
            {
                Handheld.Vibrate();
            }
        }
        else
        {
            Equip();
        }
    }

    private void Equip()
    {
        gameManager.ballIndex = ballIndex;
        foreach (var item in _ballBuysContainers)
        {
            item.UpdateState();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Buy();
    }
}
