using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffaloRunGameBuyComponent : MonoBehaviour, IPointerClickHandler
{
    public int BuffaloSkinIndex;

    public int BuffaloSkinPrice;

    [SerializeField]
    private TMP_Text _equipState;

    [SerializeField]
    private TMP_Text _priceTxt;

    private void Start()
    {
        Updater();
    }

    private int _buffaloRunGameBuyState
    {
        get
        {
            if (PlayerPrefs.HasKey($"BuffaloRunGameBuyStateData{BuffaloSkinIndex}"))
            {
                return PlayerPrefs.GetInt($"BuffaloRunGameBuyStateData{BuffaloSkinIndex}");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt($"BuffaloRunGameBuyStateData{BuffaloSkinIndex}", value);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnBuy();
    }

    public void Updater()
    {
        if (_buffaloRunGameBuyState != 0)
        {
            _priceTxt.transform.parent.gameObject.SetActive(false);
            _equipState.gameObject.SetActive(true);
            if (BuffaloSkinIndex == BuffaloRunGameController.BuffaloSkinIndex)
            {
                _equipState.text = "EQUIPED";
            }
            else
            {
                _equipState.text = "EQUIP";
            }
        }
        else
        {
            _priceTxt.transform.parent.gameObject.SetActive(true);
            _equipState.gameObject.SetActive(false);
            _priceTxt.text = "x" + BuffaloSkinPrice.ToString();
        }
    }

    private void OnEquip()
    {
        BuffaloRunGameController.BuffaloSkinIndex = BuffaloSkinIndex;
        foreach (var item in FindObjectOfType<BuffaloRunMenuComponent>()._buffaloBuyComponents)
        {
            item.Updater();
        }
        Updater();
    }

    public void OnBuy()
    {
        if (_buffaloRunGameBuyState != 0)
        {
            OnEquip();
        }
        else
        {
            if (BuffaloRunGameController.BuffaloCoins >= BuffaloSkinPrice)
            {
                BuffaloRunGameController.BuffaloCoins -= BuffaloSkinPrice;
                _buffaloRunGameBuyState = 1;
                OnEquip();
            }
            else
            {
                Handheld.Vibrate();
            }
        }
    }
}
